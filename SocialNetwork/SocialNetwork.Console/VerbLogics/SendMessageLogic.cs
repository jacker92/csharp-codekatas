using SocialNetwork.Application.Repositories;
using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;

namespace SocialNetwork.Console.VerbLogics
{
    public class SendMessageLogic : IVerbLogic<SendMessageOptions>
    {
        private readonly IOutput _output;
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IUserService _userService;

        public SendMessageLogic(IDirectMessageRepository directMessageRepository, IUserService userService, IOutput output)
        {
            _output = output;
            _directMessageRepository = directMessageRepository;
            _userService = userService;
        }

        public int Run(SendMessageOptions options, string userName)
        {
            var from = _userService.CreateIfNotExists(new CreateUserRequest { Name = userName });
            var to = _userService.CreateIfNotExists(new CreateUserRequest { Name = options.UserToSend });

            var request = new CreateDirectMessageRequest
            {
                From = from.Id,
                To = to.Id,
                Content = options.Content
            };

            _directMessageRepository.Create(request);
            _output.WriteLine($"Message sent to {options.UserToSend}!");
            return 0;
        }
    }
}
