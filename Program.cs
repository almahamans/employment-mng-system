var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

List<Employee> employees = new List<Employee>();


app.MapPost("/emplyees", (Employee newEmployee) =>
{

    newEmployee.Id = Guid.NewGuid();
    newEmployee.CreatedAt = DateTime.Now;
    employees.Add(newEmployee);

    var response = new
    {
        success = true,
        massseg = "Successfully added employee",
        Employees = newEmployee
    };
    return Results.Ok(response);
}
);

app.MapPut("/emplyee/{id}", (Guid id, Employee UpdatedEmployee) =>
{

    var foundEmployee = employees.FirstOrDefault(employee => employee.Id == id);
    if (foundEmployee == null)
    {
        return Results.NotFound("Employee not found");
    }

    foundEmployee.FirstName = UpdatedEmployee.FirstName ?? foundEmployee.FirstName;
    foundEmployee.LastName = UpdatedEmployee.LastName?? foundEmployee.LastName;
    foundEmployee.Email = UpdatedEmployee.Email ?? foundEmployee.Email;
    foundEmployee.Position = UpdatedEmployee.Position ?? foundEmployee.Position;
    foundEmployee.Salary = UpdatedEmployee.Salary ?? foundEmployee.Salary;
    
    var response = new
    {
        success = true,
        massseg = "Successfully Updated emplyee",
        Employees = UpdatedEmployee
    };
    return Results.Ok(response);
}

);
app.Run();

class Employee
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Position { get; set; }
    public decimal? Salary { get; set; }
    public DateTime CreatedAt { get; set; }
}
