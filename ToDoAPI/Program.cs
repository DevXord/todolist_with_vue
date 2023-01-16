using System.Web.Http;
using System.Web.Http.Cors;
using static Org.BouncyCastle.Math.EC.ECCurve;

internal class Program
{

    public static void Register(HttpConfiguration config)
    {
        config.EnableCors();
 
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:5173",
                                                      "http://192.168.1.106:5173")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod(); 
                              });
        });



        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthorization();

        app.MapControllers();

    
        app.Run();
    }
}