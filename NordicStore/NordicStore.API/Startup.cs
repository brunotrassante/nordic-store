using NordicStore.Domain.Handlers;
using NordicStore.Domain.Repositories;
using NordicStore.Infra.DataContexts;
using NordicStore.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace NordicStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(_ => new NordicStoreDataContext(Configuration.GetConnectionString("NordicStoreConnection")));
            services.AddTransient<ICustomerCommandRepository, CustomerRepository>();
            services.AddTransient<ICustomerQueryRepository, CustomerRepository>();
            services.AddTransient<CustomerHandler, CustomerHandler>();

            services.AddMvc();

            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Info { Title = "NordicStore", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
