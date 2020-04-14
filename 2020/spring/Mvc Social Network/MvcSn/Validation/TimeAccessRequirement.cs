
using Microsoft.AspNetCore.Authorization;

namespace MvcSn.Validation
{
    public class TimeAccessRequirement : IAuthorizationRequirement
    {
        public int Time = 15;
    }
}
