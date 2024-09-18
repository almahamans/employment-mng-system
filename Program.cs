var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

List<Employee> employees = new List<Employee>();

app.MapGet("/employees", (HttpRequest request)=>{
    int page = int.TryParse(request.Query["page"], out int parsedPage) ? parsedPage : 1;
    int limit = int.TryParse(request.Query["limit"], out int parsedLimit) ? parsedLimit : 5;
    int skip = (page - 1 ) * limit;
    var paginatedEmployees = employees.Skip(skip).Take(limit);
    var resorce = new{
        Accepte = true,
        Message = "all employees",
        Employees = paginatedEmployees
    };
    return Results.Ok(resorce); 
});

app.MapGet("/employees/{id}", (Guid id)=>{
    var foundEmployee = employees.FirstOrDefault(emp => emp.Id == id);
    if(foundEmployee == null){
        return Results.NotFound();
    }else{
       var resorce = new{
        Accepte = true,
        Message = "all employees",
        Employees = foundEmployee
    };
        return Results.Ok(resorce); 
    }
    
});

app.Run();

class Employee{
    public Guid Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Email {get; set;}
    public string Position {get; set;}
    public decimal Salary {get; set;}
    public DateTime CreatedAt {get; set;}
}
