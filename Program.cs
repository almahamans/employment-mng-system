using System.Text.RegularExpressions;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

List<Employee> employees = new List<Employee>();

app.MapGet("/employees", (HttpRequest request) =>
{
    int page = int.TryParse(request.Query["page"], out int parsedPage) ? parsedPage : 1;
    int limit = int.TryParse(request.Query["limit"], out int parsedLimit) ? parsedLimit : 5;
    int skip = (page - 1) * limit;
    var paginatedEmployees = employees.Skip(skip).Take(limit);
    var resorce = new
    {
        Accepte = true,
        Message = "all employees",
        Employees = paginatedEmployees
    };
    return Results.Ok(resorce);
});

app.MapGet("/employees/{id}", (Guid id) =>
{
    var foundEmployee = employees.FirstOrDefault(emp => emp.Id == id);
    if (foundEmployee == null)
    {
        return Results.NotFound();
    }
    else
    {
        var resorce = new
        {
            Accepte = true,
            Message = "all employees",
            Employees = foundEmployee
        };
        return Results.Ok(resorce);
    }

});
app.MapPost("/emplyees", (Employee newEmployee) =>
{
    if (string.IsNullOrEmpty(newEmployee.FirstName))
    {
        return Results.BadRequest("invalid inputs");
    }

    if (string.IsNullOrEmpty(newEmployee.LastName))
    {
        return Results.BadRequest("invalid inputs");
    }
    if (string.IsNullOrEmpty(newEmployee.Position))
    {
        return Results.BadRequest("invalid inputs");
    }
    var foundedEmail = employees.FirstOrDefault(employee => employee.Email == newEmployee.Email).Email;

    if (foundedEmail != null)
    {
        return Results.Conflict("Email Already exits");
    }
// branch testing
    string emailPattern = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)+$";

    if (!Regex.IsMatch(newEmployee.Email, emailPattern))
    {
        return Results.BadRequest("not correct format");

    }
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
    foundEmployee.LastName = UpdatedEmployee.LastName ?? foundEmployee.LastName;
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
// not doing git add . in this case  when switching to develop  branch
// using git stash to save current work and checkout to develop branch