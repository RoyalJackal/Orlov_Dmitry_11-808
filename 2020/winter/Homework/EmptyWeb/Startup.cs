using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyWeb.Controllers;
using EmptyWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmptyWeb
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IStorage, MessageStorage>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStorage storage)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", new HomeController(storage).GetForm);
				endpoints.MapPost("/Home/AddEntry", new HomeController(storage).AddEntry);
				endpoints.MapGet("/PostsList", new HomeController(storage).GetPosts);
				endpoints.MapGet("/Edit/{name}", new HomeController(storage).GetEditForm);
				endpoints.MapPost("/Edit/{name}", new HomeController(storage).EditMessage);
				endpoints.MapGet("/Delete/{name}", new HomeController(storage).DeleteMessage);
				endpoints.MapGet("/Comment/{name}", new CommentController(storage).GetCommentForm);
				endpoints.MapPost("/Comment/{name}", new CommentController(storage).CreateComment);
				endpoints.MapGet("/EditComment/{parent}/{name}", new CommentController(storage).GetEditForm);
				endpoints.MapPost("/EditComment/{parent}/{name}", new CommentController(storage).EditComment);
				endpoints.MapGet("/DeleteComment/{parent}/{name}", new CommentController(storage).DeleteComment);
			});
			
		}
	}
}
