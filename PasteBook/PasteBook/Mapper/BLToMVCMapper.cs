using BusinessLogic;
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

        public PB_POST AddPostMapper(PostModel post)
        {
            PB_POST returnPost = new PB_POST()
            {
                CONTENT = post.Content,
                CREATED_DATE = post.Created_Date,
                ID = post.ID,
                POSTER_ID = post.Poster_ID,
                PROFILE_OWNER_ID = post.Profile_Owner_ID
            };
            return returnPost;
        }

        public List<PostModel> PostListMapper(List<PB_POST> post)
        {
            PasteBookManager pbManager = new PasteBookManager();

            List<PostModel> listOfPost = new List<PostModel>();
            foreach (var item in post)
            {
                listOfPost.Add(new PostModel()
                {
                    Owner_Name = pbManager.GetUserByID(item.PROFILE_OWNER_ID).FIRST_NAME + " " + pbManager.GetUserByID(item.PROFILE_OWNER_ID).LAST_NAME,
                    Poster_Name = pbManager.GetUserByID(item.POSTER_ID).FIRST_NAME + " " + pbManager.GetUserByID(item.POSTER_ID).LAST_NAME,
                    Content = item.CONTENT,
                    Created_Date = item.CREATED_DATE,
                    ID = item.ID,
                    Poster_ID = item.POSTER_ID,
                    Profile_Owner_ID = item.PROFILE_OWNER_ID
                });
            }
            return listOfPost;
        }

        public PB_LIKE LikeMapper(LikeModel like)
        {
            PB_LIKE returnLike = new PB_LIKE()
            {
              ID = like.ID,
              LIKED_BY = like.Liked_By,
              POST_ID = like.Post_ID
            };
            return returnLike;
        }

        public List<LikeModel> LikeListMapper(List<PB_LIKE> like)
        {
            List<LikeModel> listOfLike = new List<LikeModel>();
            foreach (var item in like)
            {
                listOfLike.Add(new LikeModel()
                {
                    ID = item.ID,
                    Liked_By = item.LIKED_BY,
                    Post_ID = item.POST_ID
                });
            }
            return listOfLike;
        }


    }
}