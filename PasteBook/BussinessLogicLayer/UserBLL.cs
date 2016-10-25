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
    public class UserBLL
    {
        UserDAL userManager = new UserDAL();
        GenericDAL<PB_USER> userDAL = new GenericDAL<PB_USER>();

        public PB_USER GetUserInfo(string username)
        {
            return userManager.GetUserInfo(username);
        }

        public PB_USER GetUserByID(int id)
        {
            return userManager.GetUserByID(id);
        }

        public List<PB_USER> SearchUser(string name)
        {
            List<PB_USER> searchResult = new List<PB_USER>();
            return searchResult = userManager.SearchUser(name);
        }

        public int EditUserProfile(PB_USER user)
        {
            PB_USER baseUser = userDAL.GetSpecific(x => x.ID == user.ID);
            int result = 0;
            user.SALT = baseUser.SALT;
            user.PASSWORD = baseUser.PASSWORD;
            user.EMAIL_ADDRESS = baseUser.EMAIL_ADDRESS;
            user.DATE_CREATED = baseUser.DATE_CREATED;
            user.ABOUT_ME = baseUser.ABOUT_ME;
            user.PROFILE_PIC = baseUser.PROFILE_PIC;

            result = userDAL.GenericEdit(user);
            return result;

        }

        public int EditSecurityAccount(PB_USER user)
        {
            PasswordBLL pwManager = new PasswordBLL();
            PB_USER baseUser = userDAL.GetSpecific(x => x.ID == user.ID);
            string salt = null;
            baseUser.PASSWORD = pwManager.GeneratePasswordHash(user.PASSWORD, out salt);
            baseUser.SALT = salt;
            return userDAL.GenericEdit(baseUser);
        }

        public int EditAboutMe(int userID, string content)
        {
            return userManager.EditAboutMe(userID, content);
        }


        public int EditProfilePicture(string username, byte[] image)
        {
            return userManager.EditProfilePicture(username, image);
        }
    }
}
