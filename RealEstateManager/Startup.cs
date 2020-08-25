using GraphiQl;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RealEstateManager.DataAccess.Repositories;
using RealEstateManager.Database;
using RealEstateManager.Mutations;
using RealEstateManager.Queries;
using RealEstateManager.Schema;
using RealEstateManager.Types;

namespace RealEstateManager
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
            ////// If using Kestrel:
            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});
            services.AddControllers().AddNewtonsoftJson(option =>
                //忽略循环引用
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddDbContext<RealEstateContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:RealEstateDb"]));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<PropertyQuery>();
            services.AddTransient<PropertyMutation>();
            services.AddTransient<PropertyType>();
            services.AddSingleton<PaymentType>();
            services.AddSingleton<PropertyInputType>();
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new RealEstateSchema(new FuncDependencyResolver(type => sp.GetService(type))));
            //services.AddGraphQL(options =>
            //{
            //    options.EnableMetrics = true;
            //    options.ExposeExceptions = true;
            //})
            //.AddWebSockets() // Add required services for web socket support
            //.AddDataLoader();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RealEstateContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseGraphiQl();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //// this is required for websockets support
            //app.UseWebSockets();

            //// use websocket middleware for MoviesSchema at path /graphql
            //app.UseGraphQLWebSockets<RealEstateSchema>("/graphql");

            //// use HTTP middleware for RealEstateSchema at path /graphql
            //app.UseGraphQL<RealEstateSchema>("/graphql");


            //// use graphql-playground middleware at default url /ui/playground
            //app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            db.EnsureSeedData();
        }
    }
}
