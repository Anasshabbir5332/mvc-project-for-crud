using Microsoft.EntityFrameworkCore;
using mvc_project_for_crud.Models;

namespace mvc_project_for_crud.Services
{
    public class ApplicationdDbContext : DbContext
    {
        public ApplicationdDbContext(DbContextOptions options) : base(options)
            {

            }
        public DbSet<Product> products { get; set; }    }
}