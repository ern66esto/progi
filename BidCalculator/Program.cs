using BidCalculator.Gateway;
using BidCalculator.Gateway.Models;
using BidCalculator.Service;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
            policy => {
                policy.WithOrigins("http://localhost:8080/")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            }
        );
});

// Add services to the container.
builder.Services.AddScoped<IVehicleAuctionService, VehicleAuctionService>();
builder.Services.AddScoped<IVehicleAuctionGateway, VehicleAuctionGateway>();

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
