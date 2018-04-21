using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeiraAPI
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();
            services.AddSingleton<string>(Configuration.GetConnectionString("LiteDB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(b =>
                b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaultGet", "api/{controller}/", new
                {
                    action = "Listar"
                }, new RouteValueDictionary(new { httpMethod = new HttpMethodRouteConstraint("GET") }));

                routes.MapRoute("defaultGetWithId", "api/{controller}/{id:int}",
                    new { action = "GetById" },
                    new RouteValueDictionary(new { httpMethod = new HttpMethodRouteConstraint("GET") }));

                routes.MapRoute("defaultPost", "api/{controller}", new
                {
                    action = "Criar"
                }, new RouteValueDictionary(new { httpMethod = new HttpMethodRouteConstraint("POST") }));

                routes.MapRoute("defaultPut", "api/{controller}", new
                {
                    action = "Editar"
                }, new RouteValueDictionary(new { httpMethod = new HttpMethodRouteConstraint("PUT") }));

                routes.MapRoute("defaultDelete", "api/{controller}/{id}", new
                {
                    action = "Deletar"
                }, new RouteValueDictionary(new { httpMethod = new HttpMethodRouteConstraint("DELETE") }));
                
                routes.MapRoute(name: "qualquerOutroMetodo", template:"api/{controller}/{action}");
            });
        }
    }
}
