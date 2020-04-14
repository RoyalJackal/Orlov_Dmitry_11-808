using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MvcSn.Models;

namespace MvcSn.Validation
{
	public class CommentAuthorizationHandler : AuthorizationHandler<TimeAccessRequirement, Comment>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
														TimeAccessRequirement requirement,
														Comment resource)
		{
			if ((DateTime.Now - resource.Date).Minutes <= requirement.Time)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
