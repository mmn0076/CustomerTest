using CustomerTest.Application;
using CustomerTest.Infrastructure;
using CustomerTest.Presentation.Api;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddPresentation();
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:5091")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");
app.UseExceptionHandler("/error");

app.MapControllers();
app.Run();

public partial class ApiProgram { }
