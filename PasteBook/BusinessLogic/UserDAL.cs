using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class UserDAL
    {
        List<Exception> errorList = new List<Exception>();

        public PB_USER GetUserInfo(string username)
        {
            PB_USER result = new PB_USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                   return result = context.PB_USER.Include("REF_COUNTRY")
                                                    .Include("PB_FRIENDS1")
                                                    .Include("PB_FRIENDS")
                                                    .Where(x=>x.USER_NAME.ToLower() == username.ToLower())
                                                    .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return result;
        }

        public PB_USER GetUserByID(int ID)
        {
            PB_USER result = new PB_USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return result = context.PB_USER.FirstOrDefault(x => x.ID == ID);
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return result;
        }

        public int EditProfilePicture(string userName, byte[] image)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var personsToUpdate = context.PB_USER.Where(o => o.USER_NAME == userName);
                    foreach (var item in personsToUpdate)
                    {
                        item.PROFILE_PIC = image;
                    }
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public List<PB_USER> SearchUser(string name)
        {
            List<PB_USER> userList = new List<PB_USER>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return userList = context.PB_USER
                          .Where(x => x.FIRST_NAME.ToLower() == name.ToLower() || x.LAST_NAME.ToLower() == name.ToLower())
                          .OrderBy(x => x.LAST_NAME)
                          .ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return userList = null;
        }

        //public string GetCountryName(int ID)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities())
        //        {
        //            return result = context.REF_COUNTRY.Where(x => x.ID == ID).FirstOrDefault().COUNTRY;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorList.Add(ex);
        //    }
        //    return result;
        //}

    }
}
