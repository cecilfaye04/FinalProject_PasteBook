using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class ProfileManager
    {
        PasteBookManager BLManager = new PasteBookManager();
        BLToMVCMapper mapper = new BLToMVCMapper();

        public UserModel GetUserInfo(string username)
        {
            UserModel result = new UserModel();
            result = mapper.GetUserMapper(BLManager.GetUserInfo(username));
            return result;
        }

        public string GetCountryName(int id)
        {
            string result = string.Empty;
            result = BLManager.GetCountryName(id);
            return result;
        }

        public int AddPost(PostModel post)
        {
            int result = 0;
            return result = BLManager.AddPost(mapper.AddPostMapper(post));
        }

        public List<PostModel> RetrievePost(int id)
        {
            List<PostModel> listOfPost = new List<PostModel>();
            listOfPost = mapper.PostListMapper(BLManager.RetrievePost(id));
            return listOfPost;
        }

        public int LikePost(LikeModel like)
        {
            int result = 0;
            return result = BLManager.AddLikePost(mapper.LikeMapper(like));
        }

        public List<LikeModel> RetrieveLike()
        {
            List<LikeModel> listOfLike = new List<LikeModel>();
            listOfLike = mapper.LikeListMapper(BLManager.RetrieveLike());
            return listOfLike;
        }

    }
}