using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using PrimeiraAPI.Autenticacao;
using System;

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


            var configuracaoAcesso = new ConfiguracaoAcesso();
            services.AddSingleton(configuracaoAcesso);

            var configuracaoToken = new ConfiguracaoToken();


            new ConfigureFromConfigurationOptions<ConfiguracaoToken>
                (Configuration.GetSection("ConfiguracaoToken")).Configure(configuracaoToken);

            services.AddSingleton(configuracaoToken);

            services.AddAuthentication(opcoes =>
            {
                opcoes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opcoes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                var parametrosValidacao = bearer.TokenValidationParameters;
                parametrosValidacao.IssuerSigningKey = configuracaoAcesso.Key;
                parametrosValidacao.ValidAudience = configuracaoToken.Audience;
                parametrosValidacao.ValidIssuer = configuracaoToken.Issuer;

                parametrosValidacao.ValidateIssuerSigningKey = true;
                parametrosValidacao.ValidateLifetime = true;

                parametrosValidacao.ClockSkew = TimeSpan.Zero;
            });


            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            Database.Configuration.Configure(Configuration.GetConnectionString("LiteDB"));
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

                routes.MapRoute(name: "qualquerOutroMetodo", template: "api/{controller}/{action}");
            })
            .Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("<h1 align=\"center\">--- API está em execução! ---</h1>");
            });
        }
    }
}
