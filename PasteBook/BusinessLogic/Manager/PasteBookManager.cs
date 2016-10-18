using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PasteBookManager
    {
        List<Exception> errorList = new List<Exception>();

        public PB_USER GetUserInfo(string username)
        {
            PB_USER result = new PB_USER();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return result = context.PB_USER.FirstOrDefault(x => x.USER_NAME.ToLower() == username.ToLower());
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return result;
        }

        public string GetCountryName(int ID)
        {
            string result = string.Empty;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return result = context.REF_COUNTRY.Where(x => x.ID == ID).FirstOrDefault().COUNTRY;
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return result;
        }

        public int AddPost(PB_POST post)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PB_POST.Add(post);
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public List<PB_POST> RetrievePost()
        {
            List<PB_POST> countryList = new List<PB_POST>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return countryList = context.PB_POST.OrderByDescending(x => x.CREATED_DATE ).ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return countryList = null;
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

        public int AddLikePost(PB_LIKE like)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PB_LIKE.Add(like);
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public List<PB_LIKE> RetrieveLike()
        {
            List<PB_LIKE> likeList = new List<PB_LIKE>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return likeList = context.PB_LIKE.ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return likeList = null;
        }
    }
}
