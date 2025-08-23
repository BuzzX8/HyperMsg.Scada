using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HyperMsg.Scada.DataAccess;

public class DeviceContextFactory : IDesignTimeDbContextFactory<DeviceContext>
{
    public DeviceContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DeviceContext>();

        optionsBuilder.UseSqlite("Data Source=hypermsg_scada_devicecontext.db");

        return new(optionsBuilder.Options);
    }
}
