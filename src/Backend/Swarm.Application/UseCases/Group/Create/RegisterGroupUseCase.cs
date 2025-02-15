using AutoMapper;
using Swarm.Communication.Requests;
using Swarm.Communication.Responses;
using Swarm.Domain.Repositories;
using Swarm.Domain.Repositories.Group;
using Swarm.Domain.Repositories.User;
using Swarm.Domain.Security.Tokens;
using Swarm.Exceptions;
using Swarm.Exceptions.ExceptionBase;

namespace Swarm.Application.UseCases.Group.Create;

public class RegisterGroupUseCase : IRegisterGroupUseCase
{
    private readonly IGroupWriteOnlyRepository _writeOnlyRepository;
    private readonly IGroupReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterGroupUseCase(IGroupWriteOnlyRepository writeOnlyRepository, IGroupReadOnlyRepository readOnlyRepository, IMapper mapper, IAccessTokenGenerator accessTokenGenerator, IUnitOfWork unitOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _accessTokenGenerator = accessTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredGroupJson> Execute(RequestGroupJson request)
    {
        await Validate(request);

        var group = _mapper.Map<Domain.Entities.Group>(request);


        await _writeOnlyRepository.Add(group);
        await _unitOfWork.Commit();

        return new ResponseRegisteredGroupJson
        {
            Name = request.Name,
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
