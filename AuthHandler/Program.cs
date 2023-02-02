using AuthHandler.Handler;
using AuthHandler.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(UsersStore));


builder.Services.AddAuthentication("Oddiy")
    .AddScheme<AuthenticationSchemeOptions, AuthHandler.Handler.AuthHandler>("Oddiy", null);
    

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
