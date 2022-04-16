using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Responses
{
    public class GetPostResponse
    {
        public string Content { get; set; }
        public object Created { get; set; }
        public int  UserId { get; set; }
    }
}
