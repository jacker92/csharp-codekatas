using AutoMapper;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Services
{
    public class DirectMessageService : IDirectMessageService
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DirectMessageService(IDirectMessageRepository directMessageRepository, IUserRepository userRepository, IMapper mapper)
        {
            _directMessageRepository = directMessageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public CreateDirectMessageResponse Create(CreateDirectMessageRequest request)
        {
            var from = _userRepository.GetById(request.From);
            var to = _userRepository.GetById(request.To);

            var directMessage = CreateDirectMessage(request, from, to);

            _directMessageRepository.Create(directMessage);
            _directMessageRepository.Save();
            return _mapper.Map<CreateDirectMessageResponse>(directMessage);
        }

        public IEnumerable<GetDirectMessageResponse> GetBySender(int fromId)
        {
            var result = _directMessageRepository.GetWhere(x => x.From.Id == fromId || x.To.Id == fromId);
            return _mapper.Map<IEnumerable<GetDirectMessageResponse>>(result);
        }

        private static DirectMessage CreateDirectMessage(CreateDirectMessageRequest request, User? from, User? to)
        {
            return new DirectMessage
            {
                Content = request.Content,
                From = from,
                To = to
            };
        }
    }
}