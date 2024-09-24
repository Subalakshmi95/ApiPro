using ApiPro.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPro.Data
{
    public class AppDbcontext:DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext>opt):base(opt)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
