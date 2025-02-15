using AutoMapper;
using Swarm.Communication.Requests;
using Swarm.Communication.Responses;

namespace Swarm.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestGroupJson, Domain.Entities.Group>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Distinct()));
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseUserProfileJson>();

        CreateMap<Domain.Entities.Group, ResponseRegisteredGroupJson>()
            .ForMember(dest => dest.Name, config => config.MapFrom(source => source.Name));
    }
}
