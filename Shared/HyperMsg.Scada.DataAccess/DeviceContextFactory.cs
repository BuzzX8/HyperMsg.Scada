using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Data.Sqlite; // Add this using directive
using Microsoft.EntityFrameworkCore.Sqlite; // Add this using directive

namespace HyperMsg.Scada.DataAccess;

public class DeviceContextFactory : IDesignTimeDbContextFactory<DeviceContext>
{
    public DeviceContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DeviceContext>();

        // Use the non-generic options builder to call UseSqlite
        ((DbContextOptionsBuilder)optionsBuilder).UseSqlite("Data Source=hypermsg_scada_devicecontext.db");

        return new(optionsBuilder.Options);
    }
}
