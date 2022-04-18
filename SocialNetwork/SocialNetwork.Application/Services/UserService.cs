﻿using AutoMapper;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public CreateUserResponse CreateIfNotExists(CreateUserRequest createUserRequest)
        {
            var existing = _userRepository.GetWhere(x => x.Name == createUserRequest.Name).SingleOrDefault();

            if (existing != null)
            {
                return _mapper.Map<CreateUserResponse>(existing);
            }

            var user = CreateUser(createUserRequest);

            _userRepository.Create(user);
            _userRepository.Save();

            return _mapper.Map<CreateUserResponse>(user);
        }

        public UpdateUserResponse Update(UpdateUserRequest updateUserRequest)
        {
            var user = _userRepository.GetById(updateUserRequest.Id);
            UpdateValues(updateUserRequest, user);
            _userRepository.Update(user);
            _userRepository.Save();

            return _mapper.Map<UpdateUserResponse>(user);
        }

        private static User CreateUser(CreateUserRequest createUserRequest)
        {
            return new User
            {
                Name = createUserRequest.Name
            };
        }

        private void UpdateValues(UpdateUserRequest updateUserRequest, User user)
        {
            user.Name = !string.IsNullOrWhiteSpace(updateUserRequest!.Name) ? updateUserRequest.Name : user.Name;
        }
    }
}
