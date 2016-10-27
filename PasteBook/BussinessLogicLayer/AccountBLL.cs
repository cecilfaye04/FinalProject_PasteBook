using BusinessLogic;
using DataAccess;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public class AccountBLL
    {
        AccountDAL accountManager = new AccountDAL();
        GenericDAL<PB_USER> userManager = new GenericDAL<PB_USER>(); 

        public List<REF_COUNTRY> RetrieveCountry()
        {
            List<REF_COUNTRY> countryList = new List<REF_COUNTRY>();
            countryList = accountManager.RetrieveCountryList();
            return countryList;
        }

        public PB_USER GetUser(string email)
        {
            return userManager.GetSpecific(x => x.EMAIL_ADDRESS.ToLower() == email);
        }

        public int AddUserAccount(PB_USER user)
        {
            int result = 0;
            string salt = null;
            PasswordBLL pwManager = new PasswordBLL();
            user.PASSWORD = pwManager.GeneratePasswordHash(user.PASSWORD, out salt);
            user.SALT = salt;
            user.DATE_CREATED = DateTime.Now;

            return result = userManager.GenericAdd(user);
        }

        public bool Login(PB_USER user)
        {
            bool result = false;
            PasswordBLL pwManager = new PasswordBLL();
            var userResult = userManager.GetSpecific(x=>x.EMAIL_ADDRESS == user.EMAIL_ADDRESS);
            return result = pwManager.IsPasswordMatch(user.PASSWORD, userResult.SALT, userResult.PASSWORD);
        }

        public bool CheckIfUserNameExists(string username)
        {
            var user = userManager.GetSpecific(x => x.USER_NAME == username);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfEmailExists(string email)
        {
            var user = userManager.GetSpecific(x => x.EMAIL_ADDRESS == email);
            if (user!=null)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfPasswordMatch(string password,int userID)
        {
           PasswordBLL pwManager = new PasswordBLL();
           PB_USER oldPassword =  userManager.GetSpecific(x => x.ID == userID);

            return pwManager.IsPasswordMatch(password, oldPassword.SALT, oldPassword.PASSWORD);
        }

    }
}
