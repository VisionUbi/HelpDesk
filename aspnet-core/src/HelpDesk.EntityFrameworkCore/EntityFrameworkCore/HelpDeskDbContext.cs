using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HelpDesk.Authorization.Roles;
using HelpDesk.Authorization.Users;
using HelpDesk.MultiTenancy;

namespace HelpDesk.EntityFrameworkCore
{
    public class HelpDeskDbContext : AbpZeroDbContext<Tenant, Role, User, HelpDeskDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options)
            : base(options)
        {
        }
    }
}
