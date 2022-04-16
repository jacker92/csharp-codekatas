using AutoMapper;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
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
                cfg.CreateMap<CreatePostRequest, Post>().ForMember(x => x.Id, opt => opt.Ignore());
            });

            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            return mapper;
        }
    }
}
