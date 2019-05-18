using AngularAuthentication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularAuthentication
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NGAccountController : ApiController
    {
        public NGAccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public NGAccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [Route("api/NGAccount/Login")]
        public dynamic Login(LoginViewModel loginModel)
        {
            try
            {
                var user = UserManager.Find(loginModel.UserName, loginModel.Password);
                if (user != null)
                {
                    SignIn(user, loginModel.RememberMe);
                    return new { success = true, userName = user.UserName };
                }
                else
                {
                    return new { success = false, message = "Invalid username or password." };
                }
            }
            catch(Exception ex)
            {
                return new { success = false, message = ex.Message ?? ex.InnerException.Message };
            }
        }

        [Route("api/Account/Register")]
        public dynamic Register(RegisterViewModel registerModel)
        {
            var user = new ApplicationUser { UserName = registerModel.UserName };
            var result = UserManager.Create(user, registerModel.Password);
            if (result.Succeeded)
            {
                SignIn(user, isPersistent: false);
                return new { success = true, userName = user.UserName };
            }
            return new { success = false, message = string.Join(",", result.Errors) };
        }

        [Route("api/Account/Manage")]
        public dynamic Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            bool success = false;
            string message = "";
            if (hasPassword)
            {
                IdentityResult result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                success = result.Succeeded;
                if (!success)
                {
                    message = GetManageMessage(ManageMessageId.ChangePasswordSuccess);
                }
                else
                {
                    message = string.Join(",", result.Errors);
                }
            }
            else
            {
                IdentityResult result = UserManager.AddPassword(User.Identity.GetUserId(), model.NewPassword);
                success = result.Succeeded;
                if (!success)
                {
                    message = GetManageMessage(ManageMessageId.SetPasswordSuccess);
                }
                else
                {
                    message = string.Join(",", result.Errors);
                }
            }

            // If we got this far, something failed, redisplay form
            return new
            {
                hasLocalPassword = hasPassword,
                returnUrl = "api/Account/Manage",
                message = message
            };
        }

        [Route("api/Account/Disassociate")]
        public dynamic Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            bool success = false;
            IdentityResult result = UserManager.RemoveLogin(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            success = result.Succeeded;
            if (success)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return new { success = success, message = GetManageMessage(message) };
        }

        [Route("api/NGAccount/LogOff"), HttpGet]
        public dynamic LogOff()
        {
            try
            {
                AuthenticationManager.SignOut();
                return new { success = true };
            }
            catch(Exception ex)
            {
                return new { success = false, message = ex.Message ?? ex.InnerException.Message };
            }
        }

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}

        [Authorize]
        [HttpGet, Route("api/NGAccount/ProtectedData")]
        public IHttpActionResult ProtectedData()
        {
            return Ok(new[] { "Amar", "Akbar", "Anthony" });
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private void SignIn(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private string GetManageMessage(ManageMessageId? message)
        {
            return (
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "");
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        #endregion Helpers
    }
}