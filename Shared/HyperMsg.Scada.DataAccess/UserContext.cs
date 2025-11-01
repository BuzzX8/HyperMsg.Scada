using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HyperMsg.Scada.DataAccess;

public class UserContext(DbContextOptions<Microsoft.AspNet.Identity.EntityFramework.IdentityUser> options) : IdentityDbContext<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>(options)
{
}
