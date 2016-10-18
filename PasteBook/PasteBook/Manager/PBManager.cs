using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class PBManager
    {
        BLToMVCMapper mapper = new BLToMVCMapper();
        RegistrationManager manager = new RegistrationManager();

        public List<Ref_CountryModel> RetrieveCountry()
        {
            List<Ref_CountryModel> countryList = new List<Ref_CountryModel>();
            countryList = mapper.CountryListMapper(manager.RetrieveCountryList());
            return countryList;
        }

        public int AddUserAccount(ViewModel model)
        {
            int result = 0;
            string salt = null;
            PasswordManager pwManager = new PasswordManager();
            model.UserModel.Password = pwManager.GeneratePasswordHash(model.UserModel.Password, out salt);
            model.UserModel.Salt = salt;
            model.UserModel.Date_Created = DateTime.Now;
            return result = manager.AddUser(mapper.UserMapper(model.UserModel));
        }

        public bool Login(UserModel model)
        {
            bool result = false;
            PasswordManager pwManager = new PasswordManager();
            var user = manager.GetUser(model.Email_Address);
            return result = pwManager.IsPasswordMatch(model.Password, user.SALT, user.PASSWORD);
        }

        public UserModel GetUser(string email)
        {
            UserModel user = new UserModel();
             user = mapper.GetUserMapper(manager.GetUser(email));
            return user;
        }

        public bool CheckIfUserNameExists(string username)
        {
            bool result = false;
            return result = manager.CheckIfUsernameExist(username);
        }

        public bool CheckIfEmailExists(string email)
        {
            bool result = false;
            return result = manager.CheckIfEmailExist(email);
        }

    }
}