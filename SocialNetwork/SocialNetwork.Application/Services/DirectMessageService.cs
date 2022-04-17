﻿using AutoMapper;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;

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

            var directMessage = new DirectMessage
            {
                Content = request.Content,
                From = from,
                To = to
            };

            var created = _directMessageRepository.Create(directMessage);
            return _mapper.Map<CreateDirectMessageResponse>(created);
        }

        public IEnumerable<GetDirectMessageResponse> GetAll()
        {
            return _mapper.Map<IEnumerable<GetDirectMessageResponse>>(_directMessageRepository.GetAll());
        }
    }
}