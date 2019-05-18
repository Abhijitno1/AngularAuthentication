using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using AngularAuthentication.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularAuthentication.App_Start
{
    public class SimpleAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        protected UserManager<ApplicationUser> UserManager {get; private set;}

        public SimpleAuthorizationServerProvider()
        {
            var dbcontext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbcontext));
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new [] {"*"});

            ApplicationUser user = await UserManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("Invalid-grant", "Username or password is incorrect");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            context.Validated(identity);
        }
    }
}
