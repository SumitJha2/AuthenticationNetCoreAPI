using Authentication.Helper;
using Authentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.DAL
{
    public class UserService : IUserService
    {
        private List<UserModel> _users;
        public UserService()
        {
            _users = new List<UserModel>()
            {
                new UserModel(){
                UserName = "Tarun",
                Id = 1,
                Password = "Rtf2QAxGBZlOg3ZfYNc6VzQMf9+6FThup2s0kfGQ8k0="//admin123                
                },
                new UserModel()
                {
                UserName = "Sumit",
                Id = 2,
                Password = "yqxNfh40BBghXCB4fogu/12YoJ+QWAnPBTb0UoH//DY="//admin456
                }
            };
        }
        public bool IsValidUser(UserModel model)
        {
            return _users.Where(usr => usr.UserName == model.UserName && usr.Password == Utility.encrypt(model.Password)).Any();
        }
        public UserModel GetUserByUserName(string userName)
        {
            return _users.Where(usr => usr.UserName == userName).FirstOrDefault();
        }

       
    }
}
