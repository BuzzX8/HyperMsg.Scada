using Microsoft.AspNet.Identity.EntityFramework;

namespace HyperMsg.Scada.DataAccess;

public class UserContext<TUser>() : IdentityDbContext<TUser> where TUser : IdentityUser
{
    
}
