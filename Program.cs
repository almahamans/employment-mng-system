var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
// hello
Console.WriteLine(" hi");