using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using VecturaServer.Enteties;
using VecturaServer.Models;
using VecturaServer.Repositories;

namespace VecturaServer.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]

        public async Task<IdentityResult> Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Town = model.Town;
            Task<IdentityResult> result = manager.CreateAsync(user, model.Password);
            return await result;
        }


        [HttpGet]
        [Route("api/GetUserClaims")]

        public AccountModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            AccountModel model = new AccountModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value,
                Town = identityClaims.FindFirst("Town").Value
            };
            return model;
        }


        [HttpGet]
        [Route("api/GetAllUsers")]
        public IEnumerable<AccountModel> GetUsers()
        {
            var context = new ApplicationDbContext();
            var appUsers = context.Users.ToArray();

            var result = new List<AccountModel>();
            foreach (var user in appUsers)
            {
                result.Add(new AccountModel
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Town = user.Town
                });

            }
            return result;
        }

        [HttpGet]
        [Route("api/GetAllUsersNames")]
        public async Task<IEnumerable<string>> GetAllUsersNames()
        {

            return await Task.Run(() =>
            {
                var context = new ApplicationDbContext();
                var res = context.Users.ToList<ApplicationUser>();

                var allNames = new List<string>();
                foreach (var user in res)
                {
                    allNames.Add(user.UserName);
                }
              

                return allNames;
            });

        }

        [HttpGet]
        [Route("api/GetUserInfo")]
        public async Task<AccountModel> GetUserInfo(string userName)
        {

            return await Task.Run(() =>
            {
                var context = new ApplicationDbContext();
                var result = context.Users.ToList<ApplicationUser>().Find(u => u.UserName == userName);
                var account = new AccountModel()
                {
                    UserName = result.UserName,
                    Email = result.Email,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Town = result.Town
                };


                return account;
            });
        }




        [HttpGet]
        [Route("api/GetUserVisits")]
        public async Task<IEnumerable<VisitModel>>GetUserVisits(string userName)
        {

            return await Task.Run(() =>
            {
                var repo = new VisitRepository(new Enteties.VisitsContext("TripConnection"));
                var visits = repo.Find(t => t.User == userName);

                return visits;
            });

        }


    }
}
