using Microsoft.AspNetCore.Mvc;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
UserService userService = new UserService();

app.MapGet("/api/users", ()=> userService.GetAllUsers());
app.MapGet("/api/usersbyname", ([FromQuery] string? name)=> userService.GetUsersByFilter(name));
app.MapGet("/api/users/{id}", ([FromRoute] int id) => userService.GetUserById(id));
app.MapDelete("/api/users/{id}", ([FromRoute] int id)=> userService.DeleteUser(id));
app.MapPost("/api/users", ([FromBody] User user)=> userService.AddUser(user));
app.MapPut("/api/users", ([FromBody] User user)=> userService.UpdateUser(user));

app.Run();

