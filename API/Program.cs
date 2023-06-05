using Business.Implementations;
using Business.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

//Cors
//Add Cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200/")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                      });
});

// Add services to the container.
builder.Services.AddScoped<IFlightManager, FlightManager>();
builder.Services.AddScoped<IFlightApiService, FlightApiService>();

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

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
