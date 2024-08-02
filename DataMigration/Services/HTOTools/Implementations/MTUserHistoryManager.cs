using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMigration.Data;
using HTOTools;
using HTOTools.UserHistoryManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DataMigration.Services.HTOTools.Implementations
{
    public class MTUserHistoryManager : UserHistoryManager, IUserHistoryManager
    {

        //private readonly DataMigrationDbContext _dbcontext;
        //private readonly UserManager<ApplicationUser> _userManager;

        //UserManager<ApplicationUser> userManager,
        //(DataMigrationDbContext dbcontext
        //IHttpContextAccessor context

        public MTUserHistoryManager()
        {
            //_dbcontext = dbcontext;
            //_userManager = userManager;
            //_context = context;
        }

        /// <summary>
        /// _historyStore, SaveFromUsertoStore, and User methods and properties are necessary to implement UserHistoryManager.
        /// </summary>
        protected override IStoresUserHistory _historyStore
        {
            get
            {
                //string username = null;
                //try
                //{
                //    username = _context.HttpContext.User?.Identity?.Name;
                //}
                //catch (Exception o)
                //{
                //}
                //return !string.IsNullOrEmpty(username) ? null : null;
                return null;
            }
        }

        public override async Task<bool> SaveFromUsertoStore()
        {
            bool saved = false;
            //try
            //{
            //    var user = User;
            //    user.Userhistorystore = JsonConvert.SerializeObject(_userhistory);
            //    //_dbcontext.ApplicationUser.Update(user);
            //    //await _dbcontext.SaveChangesAsync();
            //    //saved = true;
            //}
            //catch (Exception o)
            //{
            //    var x = o.InnerException;
            //    return false;
            //}
            return saved;

        }


        //private ApplicationUser User
        //{
        //    get
        //    {
        //        return _userManager.FindByEmailAsync(_context.HttpContext.User?.Identity?.Name).Result;
        //    }
        //}


    }
}
