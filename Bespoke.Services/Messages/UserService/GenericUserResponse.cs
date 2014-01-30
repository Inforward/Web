using Bespoke.Models;

namespace Bespoke.Services.Messages.UserService
{
    public class GenericUserResponse : BaseResponse
    {
        public User User { get; set; }
    }
}
