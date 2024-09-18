var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

List<Employee> employees = new List<Employee>();

app.Run();

class Employee{
    public Guid Id {get; set;}
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? Email {get; set;}
    public string? Position {get; set;}
    public decimal Salary {get; set;}
    public DateTime CreatedAt {get; set;}
}
