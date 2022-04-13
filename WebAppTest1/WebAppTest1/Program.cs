List<Person> users = new List<Person>
{
    new() { Id = Guid.NewGuid().ToString(), Email = "tom@gmail.com", Password = "12345678" },
    new() { Id = Guid.NewGuid().ToString(), Email = "bob@gmail.com", Password = "66666666" }
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", () => users);

app.MapGet("/api/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound(new { message = "Error" });

    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{

    Person? user = users.FirstOrDefault(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "Error" });

    users.Remove(user);
    return Results.Json(user);
});

app.MapPost("/api/users", (Person user) => {

    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});

app.MapPut("/api/users", (Person userData) => {

    var user = users.FirstOrDefault(u => u.Id == userData.Id);

    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    user.Password = userData.Password;
    user.Email = userData.Email;
    return Results.Json(user);
});

app.Run();

public class Person
{
    public string Id { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}