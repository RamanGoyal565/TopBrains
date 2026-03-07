using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcurencyCheck.Data;

public class EmployeeController : Controller
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Edit Salary
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return Content("Not Found");

        return View(employee);
    }

    // POST: Update Salary
    [HttpPost]
    public async Task<IActionResult> Edit(int employeeId, double salary, byte[] rowVersion)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        if (employee == null)
            return NotFound();

        employee.Salary = salary;

        _context.Entry(employee).Property("RowVersion").OriginalValue = rowVersion;

        try
        {
            await _context.SaveChangesAsync();
            ViewBag.Message = "Salary updated successfully";
        }
        catch (DbUpdateConcurrencyException)
        {
            ViewBag.Message = "Concurrency Exception: Another user updated this record.";
        }

        return View(employee);
    }
}