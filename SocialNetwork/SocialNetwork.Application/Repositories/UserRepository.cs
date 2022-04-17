using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
using SocialNetwork.Domain.Responses;
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

        public CreateUserResponse CreateIfNotExists(CreateUserRequest createUserRequest)
        {
            var existing = _applicationDbContext.Users
                .Include(x => x.Subscriptions)
                .AsNoTracking()
                .SingleOrDefault(x => x.Name == createUserRequest.Name)!;

            if (existing != null)
            {
                return _mapper.Map<CreateUserResponse>(existing);
            }

            var user = _mapper.Map<User>(createUserRequest);

            var result = _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            return _mapper.Map<CreateUserResponse>(result.Entity);
        }

        public UpdateUserResponse Update(UpdateUserRequest updateUserRequest)
        {
            var user = _applicationDbContext.Users.Single(x => x.Id == updateUserRequest.Id);
            UpdateValues(updateUserRequest, user);
            var result = _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();

            return _mapper.Map<UpdateUserResponse>(result.Entity);
        }

        private void UpdateValues(UpdateUserRequest updateUserRequest, User user)
        {
            user.Name = updateUserRequest.Name;
            var subscribers = _applicationDbContext.Users.Where(x => updateUserRequest.Subscriptions != null && updateUserRequest.Subscriptions.Any(y => y == x.Id));
            user.Subscriptions = subscribers.ToList();
        }

        public GetUserResponse GetByName(string name)
        {
            var user = _applicationDbContext.Users
                .Include(x => x.Subscriptions)
                .AsNoTracking()
                .SingleOrDefault(x => x.Name == name)!;

            return _mapper.Map<GetUserResponse>(user);
        }
    }
}