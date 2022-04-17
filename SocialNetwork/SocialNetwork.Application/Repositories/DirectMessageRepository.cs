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
            var from = _applicationDbContext.Users.Single(x => x.Id == request.From);
            var to = _applicationDbContext.Users.Single(x => x.Id == request.To);

            var directMessage = new DirectMessage
            {
                Content = request.Content,
                From = from,
                To = to
            };

            var created = _applicationDbContext.DirectMessages.Add(directMessage);
            _applicationDbContext.SaveChanges();

            return _mapper.Map<CreateDirectMessageResponse>(created.Entity);
        }

        public IEnumerable<GetDirectMessageResponse> GetAll()
        {
            return _mapper.Map<IEnumerable<GetDirectMessageResponse>>(_applicationDbContext.DirectMessages.ToList());
        }
    }
}