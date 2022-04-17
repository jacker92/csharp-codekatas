﻿using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Application.Services
{
    public interface IPostService
    {
        void Create(CreatePostRequest createPostRequest);
        void Create(IEnumerable<CreatePostRequest> createPostRequests);
        IEnumerable<GetPostResponse> GetAll();
        IEnumerable<GetPostResponse> GetByUserId(int userId);
    }
}