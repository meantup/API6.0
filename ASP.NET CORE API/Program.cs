using ASP.NET_CORE_API.AdapterClass;
using ASP.NET_CORE_API.Repository.IUnity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAdapterRepository, AdapterClass>();
builder.Services.AddSwaggerGen(option => 
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "DOTNET 6.0 API", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DOTNET 6.0 API");
    });
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("http://192.168.210.165/api/swagger/v1/swagger.json", "DOTNET 6.0 API");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
