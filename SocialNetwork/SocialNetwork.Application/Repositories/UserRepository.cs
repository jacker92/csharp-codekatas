using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
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

        public User CreateIfNotExists(CreateUserRequest createUserRequest)
        {
            var existing = GetByName(createUserRequest.Name);

            if (existing != null)
            {
                return existing;
            }

            var user = _mapper.Map<User>(createUserRequest);

            var result = _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            return result.Entity;
        }

        public User Update(User user)
        {
            var result = _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();
            return result.Entity;
        }

        public User GetByName(string name)
        {
            return _applicationDbContext.Users
                .Include(x => x.Subscriptions)
                .SingleOrDefault(x => x.Name == name)!;
        }
    }
}