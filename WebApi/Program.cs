using Application;
using Infrastructure;
using WebApi;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddIdentitySettings();
        builder.Services.AddIdentityServices();
        builder.Services.AddValidators();
        builder.Services.AddApplicationServices();
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.RegisterSwagger();
        builder.Services.AddJwtAuthentication(builder.Services.GetApplicationSettings(builder.Configuration));
        var app = builder.Build();
        app.SeedDatabase();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}