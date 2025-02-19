using AutoMapper;
using Sqids;
using Swarm.Communication.Requests;
using Swarm.Communication.Responses;
using Swarm.Domain.Entities;

namespace Swarm.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    private readonly SqidsEncoder<long> _idEncoder;

    public AutoMapping(SqidsEncoder<long> idEncoder)
    {
        _idEncoder = idEncoder;
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        //CreateMap<RequestGroupJson, Group>()
        //    .ForMember(dest => dest.Products, opt => opt.MapFrom(source => source.Products.Distinct()));

        CreateMap<RequestGroupJson, Group>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        CreateMap<RequestProductJson, Product>()
            .ForMember(dest => dest.GroupId, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {
        CreateMap<User, ResponseUserProfileJson>();

        CreateMap<Group, ResponseRegisteredGroupJson>()
            .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEncoder.Encode(source.Id)));
    }
}
