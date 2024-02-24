using CommandLine;
using Tavernkeep.Shared.Options;
using Tavernkeep.Application.Actions.Users.Commands.CreateUser;
using System.Reflection;
using System.Text.Json.Serialization;
using Tavernkeep.Server.Extensions;
using Microsoft.OpenApi.Models;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Microsoft.AspNetCore.SignalR;
using Tavernkeep.Infrastructure.Notifications.Providers;
using Tavernkeep.Infrastructure.Notifications.Serialization;

var builder = WebApplication.CreateBuilder(args);
var options = Parser.Default.ParseArguments<LaunchOptions>(args).Value;

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services
    .AddSignalR()
    .AddJsonProtocol(o => 
    {
        o.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        o.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Until the fix for the https://github.com/dotnet/aspnetcore/issues/52342 comes out.
        o.PayloadSerializerOptions.TypeInfoResolver = new InheritedPolymorphismResolver();
    });

builder.Services.AddSingleton<IUserIdProvider, NotificationsUserProvider>();

builder.Services
    .AddExceptionHandling()
    .AddApplicationServices()
    .AddDatabaseContext(options)
    .AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateUserCommand>())
    .AddRouting(o => o.LowercaseUrls = true)
    .AddSecurity(builder.Configuration["Jwt:Key"]);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Import API documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    o.IncludeXmlComments(xmlPath);
});

builder.Logging.ClearProviders().AddConsole();

var app = builder.Build();

app.Services.ApplyDatabaseMigrations();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseExceptionHandler("/Error");

app.MapControllers();

app.MapHub<ChatHub>("/api/hubs/chat");
app.MapHub<CharacterHub>("/api/hubs/character");

app.MapFallbackToFile("/index.html");
app.Run();
