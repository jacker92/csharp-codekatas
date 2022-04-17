using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;

namespace SocialNetwork.Console.VerbLogics
{
    public class SendMessageLogic : IVerbLogic<SendMessageOptions>
    {
        private readonly IOutput _output;
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IUserRepository _userRepository;

        public SendMessageLogic(IDirectMessageRepository directMessageRepository, IUserRepository userRepository, IOutput output)
        {
            _output = output;
            _directMessageRepository = directMessageRepository;
            _userRepository = userRepository;
        }

        public int Run(SendMessageOptions options, string userName)
        {
            var from = _userRepository.CreateIfNotExists(new CreateUserRequest { Name = userName });
            var to = _userRepository.CreateIfNotExists(new CreateUserRequest { Name = options.UserToSend });

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
