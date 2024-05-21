using BussinessLayer.Interface;
using ModelLayer.UserRegistration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class UserServicebl : UserInterfaceBL
    {
        private readonly UserInterface _user;
        public UserServicebl(UserInterface user)
        {
            _user = user;
        }

        public Task<bool> RegisterUser(UserRegistrationModel userRegistrationModel)
        {

            return _user.RegisterUser(userRegistrationModel);
        }
        public Task<string> UserLogin(UserLoginModel userLogin)
        {
            return _user.UserLogin(userLogin);
        }
    }
}
