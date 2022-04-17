using AutoMapper;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Mappings
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                Posts(cfg);
                Users(cfg);
                DirectMessages(cfg);
            });

            var mapper = configuration.CreateMapper();

            return mapper;
        }

        private static void Users(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, CreateUserResponse>().ForMember(x => x.Subscriptions, opt => opt.MapFrom(src => src.Subscriptions.Select(x => x.Id)));
            cfg.CreateMap<User, GetUserResponse>();
            cfg.CreateMap<User, UpdateUserResponse>();
        }

        private static void Posts(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Post, GetPostResponse>();
        }

        private static void DirectMessages(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DirectMessage, CreateDirectMessageResponse>();
        }
    }
}
