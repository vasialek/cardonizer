using CardonizerServer.Api.Factories;
using CardonizerServer.Api.Interfaces;
using CardonizerServer.Api.Managers;
using CardonizerServer.Api.Mappers;
using CardonizerServer.Api.Parsers;
using CardonizerServer.Api.Providers;
using CardonizerServer.Api.Repositories;
using CardonizerServer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICardMapper, CardMapper>();
builder.Services.AddSingleton<IMythCardMapper, MythCardMapper>();
builder.Services.AddSingleton<IMythActionsParser, MythActionsParser>();
builder.Services.AddSingleton<IUniqueIdService, UniqueIdService>();
builder.Services.AddSingleton<IJsonService, JsonService>();
builder.Services.AddSingleton<ICardService, CardService>();
builder.Services.AddSingleton<ICardAdminService, CardAdminService>();
builder.Services.AddSingleton<ICardValidationService, CardValidationService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<ICardRandomizerService, CardRandomizerService>();
builder.Services.AddSingleton<IRandomProvider, RandomProvider>();
builder.Services.AddSingleton<ICardProviderFactory, CardProviderFactory>();
builder.Services.AddSingleton<IGameSessionManager, GameSessionManager>();
builder.Services.AddSingleton<ICardRepository, CardRepository>();
builder.Services.AddSingleton<IGameOptionsRepository, GameOptionsRepository>();
builder.Services.AddSingleton<ICardValidationServiceFactory, CardValidationServiceFactory>();
builder.Services.AddScoped<IMythRepository, MythRepository>();
var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseHttpsRedirection();
// app.UseAuthorization();

app.MapControllers();

app.Run();