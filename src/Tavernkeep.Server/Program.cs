using CommandLine;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Tavernkeep.Application.Mapping.Profiles;
using Tavernkeep.Application.UseCases.Users.Commands.CreateUser;
using Tavernkeep.Infrastructure.Data.Extensions;
using Tavernkeep.Infrastructure.Notifications.Hubs;
using Tavernkeep.Infrastructure.Notifications.Providers;
using Tavernkeep.Infrastructure.Notifications.Serialization;
using Tavernkeep.Server.Extensions;
using Tavernkeep.Shared.Options;

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
	.AddDatabaseContext(options)
	.AddAutoMapper(typeof(UserProfile))
	.AddApplicationServices()
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

app.Services
	.ApplyDatabaseMigrations()
	.SeedData();

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
