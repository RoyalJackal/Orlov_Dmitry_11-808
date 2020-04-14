using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MvcSn.Models;

namespace MvcSn.Validation
{
	public class PostAuthorizationHandler : AuthorizationHandler<TimeAccessRequirement, Post>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
														TimeAccessRequirement requirement,
														Post resource)
		{
			if ((DateTime.Now - resource.Date).Minutes <= requirement.Time)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
