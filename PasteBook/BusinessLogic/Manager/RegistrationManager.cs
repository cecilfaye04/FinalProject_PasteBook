using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RegistrationManager
    {
        List<Exception> errorList = new List<Exception>();

        public List<REF_COUNTRY> RetrieveCountryList()
        {
            List<REF_COUNTRY> countryList = new List<REF_COUNTRY>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return countryList = context.REF_COUNTRY.ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return countryList = null;
        }

        public int AddUser(PB_USER user)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PB_USER.Add(user);
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public bool CheckIfUsernameExist(string username)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result =  context.PB_USER.FirstOrDefault(x => x.USER_NAME.ToLower() == username.ToLower());
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return false;
        }

        public bool CheckIfEmailExist(string email)
        {
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var result = context.PB_USER.FirstOrDefault(x => x.EMAIL_ADDRESS.ToLower() == email.ToLower());
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return false;
        }

        public PB_USER GetUser(string email)
        {
            PB_USER result = new PB_USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return result = context.PB_USER.FirstOrDefault(x => x.EMAIL_ADDRESS.ToLower() == email.ToLower());
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return result;
        }
    }
}
