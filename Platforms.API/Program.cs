using Platforms.API;
using Platforms.API.Endpoints;
using Platforms.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddServiceLogic()
    .AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.MapPlatformEndpoints();
app.Run();