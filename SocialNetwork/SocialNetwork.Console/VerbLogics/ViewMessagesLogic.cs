using SocialNetwork.Application.Repositories;
using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Infrastructure;
using System.Linq;

namespace SocialNetwork.Console.VerbLogics
{
    public class ViewMessagesLogic : IVerbLogic<ViewMessagesOptions>
    {
        private readonly IOutput _output;
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IUserService _userService;

        public ViewMessagesLogic(IOutput output, IDirectMessageRepository directMessageRepository, IUserService userService)
        {
            _output = output;
            _directMessageRepository = directMessageRepository;
            _userService = userService;
        }

        public int Run(ViewMessagesOptions options, string userName)
        {
            var from = _userService.CreateIfNotExists(new CreateUserRequest { Name = userName });

            var messages = _directMessageRepository.GetAll()
                .Where(x => x.From.Id == from.Id || x.To.Id == from.Id);

            if (!messages.Any())
            {
                _output.WriteLine("No direct messages found.");
            }

            foreach (var message in messages)
            {
                _output.WriteLine($"{message.From.Name}: {message.Content}");
            }
            return 0;
        }
    }
}
