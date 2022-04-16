﻿using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
using SocialNetwork.Domain.Responses;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        void Create(CreatePostRequest createPostRequest);
        IEnumerable<GetPostResponse> GetByUserName(string user);
        IEnumerable<GetPostResponse> GetAll();
    }
}