using media_player_backend.Configurations;
using media_player_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure MongoDB settings from environment variables
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME");
});

builder.Services.AddSingleton<MediaPlayerContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
