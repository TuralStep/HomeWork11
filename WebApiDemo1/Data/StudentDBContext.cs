using Microsoft.EntityFrameworkCore;
using WebApiDemo1.Entities;

namespace WebApiDemo1.Data
{
    public class StudentDBContext : DbContext
    {

        public StudentDBContext(DbContextOptions<StudentDBContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }

    }
}
