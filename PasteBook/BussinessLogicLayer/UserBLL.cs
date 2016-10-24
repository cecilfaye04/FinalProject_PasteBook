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

        //public string GetCountryName(int id)
        //{
        //    string result = string.Empty;
        //    result = userManager.GetCountryName(id);
        //    return result;
        //}

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
