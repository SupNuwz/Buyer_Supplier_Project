using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mainframe.BuyerSupplier.Data.Models;

namespace Mainframe.BuyerSupplier.Data.DataServices
{
    public interface IUserService : IBaseDataService
    {
        int AddUser(Users user);
        IEnumerable<Users> GetUsers();
        Users GetUser(int userID);
    }
    public class UsersService : BaseDataService, IUserService
    {
        private DatabaseContext databaseContext;
        public UsersService(DatabaseContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public int AddUser(Users user)
        {
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();

            return user.ID;
        }
        public IEnumerable<Users> GetUsers()
        {
            var user = (from e in databaseContext.Users
                            where e.IsDeleted == false
                            select e).ToList();
            return user;
        }
       
        public Users GetUser(int userID)
        {
            var user = from e in databaseContext.Users
                       where e.ID == userID
                       select e;

            return user.FirstOrDefault();
        }

    }
}