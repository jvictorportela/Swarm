using AutoMapper;
using Swarm.Communication.Requests;
using Swarm.Communication.Responses;
using Swarm.Domain.Repositories;
using Swarm.Domain.Repositories.Group;
using Swarm.Domain.Security.Tokens;
using Swarm.Domain.Services.LoggedUser;
using Swarm.Exceptions.ExceptionBase;

namespace Swarm.Application.UseCases.Group.Create;

public class RegisterGroupUseCase : IRegisterGroupUseCase
{
    private readonly IGroupWriteOnlyRepository _writeOnlyRepository;
    private readonly IGroupReadOnlyRepository _readOnlyRepository;
    private readonly IGroupUpdateRepository _updateRepository;
    private readonly IMapper _mapper;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public RegisterGroupUseCase(IGroupWriteOnlyRepository writeOnlyRepository, IGroupReadOnlyRepository readOnlyRepository, IGroupUpdateRepository groupUpdateRepository, IMapper mapper, IAccessTokenGenerator accessTokenGenerator, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _updateRepository = groupUpdateRepository;
        _mapper = mapper;
        _accessTokenGenerator = accessTokenGenerator;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    //public async Task<ResponseRegisteredGroupJson> Execute(RequestGroupJson request)
    //{
    //    var loggedUser = await _loggedUser.User();

    //    await Validate(request);
    //    var group = _mapper.Map<Domain.Entities.Group>(request);

    //    group.UserId = loggedUser.Id;
    //    group.Products = _mapper.Map<IList<Domain.Entities.Product>>(request.Products);

    //    await _writeOnlyRepository.Add(group);
    //    await _unitOfWork.Commit();

    //    return _mapper.Map<ResponseRegisteredGroupJson>(group);
    //}

    public async Task<ResponseRegisteredGroupJson> Execute(RequestGroupJson request)
    {
        var loggedUser = await _loggedUser.User();

        await Validate(request);

        var group = _mapper.Map<Domain.Entities.Group>(request);
        group.UserId = loggedUser.Id;

        await _writeOnlyRepository.Add(group);
        await _unitOfWork.Commit();

        var products = _mapper.Map<IList<Domain.Entities.Product>>(request.Products);
        foreach (var product in products)
        {
            product.GroupId = group.Id; 
        }

        group.Products = products;

        _updateRepository.Update(group);
        await _unitOfWork.Commit();


        return new ResponseRegisteredGroupJson
        {
            Name = group.Name
        };
    }

    private async Task Validate(RequestGroupJson request)
    {
        var validator = new GroupValidator();

        var result = validator.Validate(request);

        var nameExist = await _readOnlyRepository.ExistActiveGroupWithName(request.Name);

        if (nameExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Name allready registered!"));

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
