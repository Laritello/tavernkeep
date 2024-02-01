using CommandLine;
using Tavernkeep.Core.Extensions;
using Tavernkeep.Shared.Options;

var builder = WebApplication.CreateBuilder(args);
var options = Parser.Default.ParseArguments<LaunchOptions>(args).Value;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDatabaseContext(options);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
