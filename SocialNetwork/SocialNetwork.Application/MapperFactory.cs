using AutoMapper;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
using SocialNetwork.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application
{
    public class MapperFactory
    {
        public IMapper Create()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                Posts(cfg);
                Users(cfg);
            });

            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            return mapper;
        }

        private static void Users(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CreateUserRequest, User>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Subscriptions, opt => opt.Ignore());

            cfg.CreateMap<UpdateUserRequest, User>().ForMember(x => x.Subscriptions, opt => opt.Ignore());
            cfg.CreateMap<User, CreateUserResponse>().ForMember(x => x.Subscriptions, opt => opt.MapFrom(src => src.Subscriptions.Select(x => x.Id)));
            cfg.CreateMap<User, GetUserResponse>();
            cfg.CreateMap<User, UpdateUserResponse>();
        }

        private static void Posts(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CreatePostRequest, Post>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.User, opt => opt.Ignore());

            cfg.CreateMap<Post, GetPostResponse>();
        }
    }
}
