using BD_TASK.Configurations;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;

namespace BD_TASK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // Add services to the container.
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(
                    c =>
                    {
                        c.SwaggerDoc("v1", new()
                        { Title = "BD_TASK", Version = "v1",Description = "API for IP Geolocation and Country Blocking" ,
                        Contact = new OpenApiContact { Name = "Sherif Ahmed Mohamed", Email = "Q0oqU@example.com" } });
                    }
                )
                .AddHttpClient()
                .AddServices(builder.Configuration);

            builder.Services.AddRateLimiter(options =>
            {

                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: "global",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromMinutes(1),
                        }
                    )
                );

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(
                        "{\"message\": \"You have reached your limit.\"}", token);
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRateLimiter();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
