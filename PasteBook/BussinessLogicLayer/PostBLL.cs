using DataAccess;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public class PostBLL
    {
        PostDAL postManager = new PostDAL();

        public List<PB_POST> RetrievePost(int id)
        {
            List<PB_POST> listOfPost = new List<PB_POST>();
            listOfPost = postManager.RetrievePost(id);
            return listOfPost;
        }

        public List<PB_POST> RetrieveHomePagePost(int id)
        {
            FriendBLL friendManager = new FriendBLL();
            List<PB_FRIENDS> friendList = new List<PB_FRIENDS>();
            friendList = friendManager.RetrieveFriendList(id);

            return postManager.RetrievePostHomePage(friendList,id);

        }

        public int AddNotification(PB_NOTIFICATION notif)
        {
           return postManager.AddNotification(notif);
        }

        public int AddPost(PB_POST post)
        {
            int result = 0;
            return result = postManager.AddPost(post);
        }

        public int AddComment(PB_COMMENT comment)
        {
            return postManager.AddComment(comment);
        }

        public int LikePost(int PostID,int userID,int ReceiverID)
        {
            int result = 0;
            PB_LIKE like = new PB_LIKE();
            like.LIKED_BY = userID;
            like.POST_ID = PostID;
    
            result = postManager.AddLikePost(like,ReceiverID);
            return result;
        }

        public int UnlikePost(int like)
        {
            return postManager.UnlikePost(like);
        }

        public List<PB_NOTIFICATION> RetrieveNotification(int ID)
        {
            return postManager.RetreiveNotification(ID);
        }

        public PB_POST RetrieveSpecificPost(int postID)
        {
            return postManager.RetrieveSpecificPost(postID);
        }

        //public List<PB_LIKE> RetrieveLike(int id)
        //{
        //    List<PB_LIKE> listOfLike = new List<PB_LIKE>();
        //    listOfLike = postManager.RetrieveLike(id);
        //    return listOfLike;
        //}

        //public List<PB_LIKE> RetrieveAllLike()
        //{
        //    List<PB_LIKE> listOfLike = new List<PB_LIKE>();
        //    listOfLike = postManager.RetrieveAllLike();
        //    return listOfLike;
        //}

   

        //public List<PB_COMMENT> RetrieveComment()
        //{
        //    List<PB_COMMENT> listOfComment = new List<PB_COMMENT>();
        //    listOfComment = postManager.RetrieveComment();
        //    return listOfComment;
        //}
    }
}
