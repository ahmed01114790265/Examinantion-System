using Application.Examinantion_System.MappingProfile;
using Examinantion_System.MappingProfil;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(
    typeof(ApplicationMapping).Assembly,
    typeof(ApiMapping).Assembly);

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
