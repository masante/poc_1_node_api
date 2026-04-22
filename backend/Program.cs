using MongoDB.Driver;
using PersonApi.Services;

var builder = WebApplication.CreateBuilder(args);

// MongoDB configuration
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://mongo:27017";
var mongoClient = new MongoClient(mongoConnectionString);

builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IOrganisationService, OrganisationService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAccreditationService, AccreditationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"MongoDB Connection String: {mongoConnectionString}");
app.Run();
