using events_service.Services;
using events_service_data.Repositories;
using EventsServiceData;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

NpgsqlConnectionStringBuilder connection = new();
connection.ConnectionString = builder.Configuration["POSTGRES_CONNECTION"];
connection.Password = builder.Configuration["POSTGRES_PASSWORD"];

builder.Services.AddDbContext<EventsServiceContext>(options =>
{
    options.UseNpgsql(connection.ConnectionString);
});

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventEntryRepository, EventEntryRepository>();
builder.Services.AddScoped<IFavouriteEventRepository, FavouriteEventRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddSingleton<TokenDecoder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
