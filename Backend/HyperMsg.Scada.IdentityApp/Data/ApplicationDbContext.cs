using Microsoft.EntityFrameworkCore;

namespace HyperMsg.Scada.IdentityApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}