using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class BLToMVCMapper
    {
        public List<Ref_CountryModel> CountryListMapper(List<REF_COUNTRY> country)
        {
            List<Ref_CountryModel> countryList = new List<Ref_CountryModel>();
            foreach (var item in country)
            {
                countryList.Add(new Ref_CountryModel()
                {
                    ID = item.ID,
                    Country = item.COUNTRY
                });
            }
            return countryList;
        }

        public PB_USER UserMapper(UserModel user)
        {
            PB_USER returnUser = new PB_USER()
            {
                ID = user.ID,
                ABOUT_ME = user.About_Me,
                BIRTHDAY = user.Birthday,
                COUNTRY_ID = user.Country_ID,
                DATE_CREATED = user.Date_Created,
                EMAIL_ADDRESS = user.Email_Address,
                FIRST_NAME = user.First_Name,
                GENDER = user.Gender,
                LAST_NAME = user.Last_Name,
                MOBILE_NO = user.Mobile_No,
                PASSWORD = user.Password,
                PROFILE_PIC = user.Profile_Pic,
                SALT = user.Salt,
                USER_NAME = user.User_Name
            };
            return returnUser;
        }

        public UserModel GetUserMapper(PB_USER user)
        {
            UserModel returnUser = new UserModel()
            {
                ID = user.ID,
                About_Me = user.ABOUT_ME,
                Birthday = user.BIRTHDAY,
                Country_ID = user.COUNTRY_ID,
                Date_Created = user.DATE_CREATED,
                Email_Address = user.EMAIL_ADDRESS,
                First_Name = user.FIRST_NAME,
                Gender = user.GENDER,
                Last_Name = user.LAST_NAME,
                Mobile_No = user.MOBILE_NO,
                Password = user.PASSWORD,
                Profile_Pic = user.PROFILE_PIC,
                Salt = user.SALT,
                User_Name = user.USER_NAME
            };
            return returnUser;
        }




    }
}