using ResidentApi.BusinessLogic.Repository;
using ResidentApi.BusinessLogic.UtilityService;

var builder = WebApplication.CreateBuilder(args);

// This will load the MongoDb settings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

// Add services to the container.
// Creates unique instance for each HTTP request
builder.Services.AddHttpClient<IExternalService, ExternalService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["UtilityServiceUrl"]!);
});
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
