using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MvcSn.Models
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
