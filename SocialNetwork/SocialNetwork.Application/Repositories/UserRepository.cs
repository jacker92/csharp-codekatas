using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public UserRepository(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public User Create(User user)
        {
            var result = _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            return result.Entity;
        }

        public UpdateUserResponse Update(UpdateUserRequest updateUserRequest)
        {
            var user = _applicationDbContext.Users.Single(x => x.Id == updateUserRequest.Id);
            UpdateValues(updateUserRequest, user);
            var result = _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();

            return _mapper.Map<UpdateUserResponse>(result.Entity);
        }

        public GetUserResponse GetByName(string name)
        {
            var user = _applicationDbContext.Users
                .Include(x => x.Subscriptions)
                .AsNoTracking()
                .SingleOrDefault(x => x.Name == name)!;

            return _mapper.Map<GetUserResponse>(user);
        }

        private void UpdateValues(UpdateUserRequest updateUserRequest, User user)
        {
            user.Name = !string.IsNullOrWhiteSpace(updateUserRequest!.Name) ? updateUserRequest.Name : user.Name;
            user.Subscriptions = updateUserRequest!.Subscriptions == null ? user.Subscriptions : GetSubscribers(updateUserRequest.Subscriptions);
        }

        private List<User> GetSubscribers(List<int> subscriptions)
        {
            return _applicationDbContext.Users
                .Where(x => subscriptions != null && subscriptions.Any(y => y == x.Id))
                .ToList();
        }

        public IEnumerable<User> GetAll()
        {
            return _applicationDbContext.Users;
        }

        public User Update(User user)
        {
            var result = _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();

            return result.Entity;
        }

        public User? GetById(int id)
        {
           return _applicationDbContext.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}