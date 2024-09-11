using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingleCrud.Data;
using SingleCrud.Models;

namespace SingleCrud.Controlles;
public class EmployeeController : Controller
{
    private readonly DataContext _dataContext;

    public EmployeeController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _dataContext.Employees.ToListAsync();

        if (employees is not null)
        {
            return Json(new { data = employees });
        }
        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _dataContext.Add(employee);
            await _dataContext.SaveChangesAsync();
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int Id)
    {
        var employee = await _dataContext.Employees.FindAsync(Id);

        if (employee is not null)
        {
            return Json(new { data = employee });
        }
        return Json(new { success = false });
    }
    [HttpPost]
    public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
    {
        var emp = await _dataContext.Employees.FindAsync(employee.Id);

        if (emp is null)
        {
            return Json(new { success = false });
        }

        if (ModelState.IsValid)
        {
            emp.Salary = employee.Salary;
            emp.Department = employee.Department;
            emp.Name = employee.Name;

            await _dataContext.SaveChangesAsync();

            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmployee(int Id)
    {
        var employee = await _dataContext.Employees.FindAsync(Id);

        if (employee is null)
        {
            return Json(new { success = false });
        }

        _dataContext.Employees.Remove(employee);
        await _dataContext.SaveChangesAsync();
        return Json(new { success = true });
    }
}
