using CommandLine;
using Tavernkeep.Shared.Options;
using Tavernkeep.Infrastructure.Extensions;
using Tavernkeep.Application.Actions.Users.Commands.CreateUser;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var options = Parser.Default.ParseArguments<LaunchOptions>(args).Value;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDatabaseContext(options);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
builder.Services.AddRouting(o => o.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Import API documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.Services.ApplyDatabaseMigrations();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
