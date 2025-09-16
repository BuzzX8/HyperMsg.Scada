using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HyperMsg.Scada.DataAccess;

public class UserContext : IdentityDbContext<IdentityUser>
{
}
