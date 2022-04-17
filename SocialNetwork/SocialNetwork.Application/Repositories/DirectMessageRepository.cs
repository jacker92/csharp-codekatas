using AutoMapper;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application.Repositories
{
    public class DirectMessageRepository : IDirectMessageRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DirectMessageRepository(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public CreateDirectMessageResponse Create(CreateDirectMessageRequest request)
        {
            var directMessage = _mapper.Map<DirectMessage>(request);
            var created = _applicationDbContext.DirectMessages.Add(directMessage);
            _applicationDbContext.SaveChanges();

            return _mapper.Map<CreateDirectMessageResponse>(created.Entity);
        }
    }
}