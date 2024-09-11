using Microsoft.EntityFrameworkCore;
using SingleCrud.Models;

namespace SingleCrud.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}
