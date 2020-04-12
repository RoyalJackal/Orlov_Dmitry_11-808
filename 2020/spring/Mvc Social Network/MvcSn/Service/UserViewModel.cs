using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSn.Service
{
    public class UserViewModel
    {
        public UserViewModel(string name, string surname, List<PostViewModel> posts, List<CommentViewModel> comments)
        {
            Name = name;
            Surname = surname;
            Posts = posts;
            Comments = comments;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public List<PostViewModel> Posts { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
