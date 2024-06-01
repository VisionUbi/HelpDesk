using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HelpDesk.Authorization.Roles;
using HelpDesk.Authorization.Users;
using HelpDesk.MultiTenancy;
using HelpDesk.Tickets;
using HelpDesk.Departments;
using HelpDesk.Categorys;

namespace HelpDesk.EntityFrameworkCore
{
    public class HelpDeskDbContext : AbpZeroDbContext<Tenant, Role, User, HelpDeskDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options)
            : base(options)
        {
        }
    }
}
