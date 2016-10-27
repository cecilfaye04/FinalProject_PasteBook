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
    public class PostBLL
    {
        PostDAL postManager = new PostDAL();
        GenericDAL<PB_POST> postDALManager = new GenericDAL<PB_POST>();
        GenericDAL<PB_COMMENT> commentManager = new GenericDAL<PB_COMMENT>();
        GenericDAL<PB_LIKE> likeManager = new GenericDAL<PB_LIKE>();
        GenericDAL<PB_NOTIFICATION> notificationManager = new GenericDAL<PB_NOTIFICATION>();

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
            friendList = friendList.Where(x => x.REQUEST == "Y").ToList();

            return postManager.RetrievePostHomePage(friendList, id);

        }

        //public int AddNotification(PB_NOTIFICATION notif)
        //{
        //   return postManager.AddNotification(notif);
        //}

        public int AddPost(PB_POST post)
        {
            int result = 0;
            return result = postDALManager.GenericAdd(post);
        }

        public int AddComment(PB_COMMENT comment)
        {
            int result = 0;
            GenericDAL<PB_COMMENT> commentManager = new GenericDAL<PB_COMMENT>();
            int posterID = postDALManager.GetSpecific(x => x.ID == comment.POST_ID).POSTER_ID;
            result = commentManager.GenericAdd(comment);
            if (result == 1 && posterID != comment.POSTER_ID)
            {
                PB_NOTIFICATION notif = new PB_NOTIFICATION()
                {
                    NOTIF_TYPE = "C",
                    POST_ID = comment.POST_ID,
                    SEEN = "N",
                    SENDER_ID = comment.POSTER_ID,
                    RECEIVER_ID = posterID,
                    CREATED_DATE = DateTime.Now,
                    COMMENT_ID = comment.ID
                };
                notificationManager.GenericAdd(notif);
            }
            return result;
        }

        public int LikePost(int PostID, int userID, int ReceiverID)
        {
            int result = 0;
            PB_LIKE like = new PB_LIKE();
            like.LIKED_BY = userID;
            like.POST_ID = PostID;
            var poster = postDALManager.GetSpecific(x => x.ID == like.POST_ID);

            result = likeManager.GenericAdd(like);
            if (result == 1 && like.LIKED_BY != poster.POSTER_ID)
            {
                PB_NOTIFICATION notif = new PB_NOTIFICATION();
                notif.SENDER_ID = like.LIKED_BY;
                notif.CREATED_DATE = DateTime.Now;
                notif.NOTIF_TYPE = "L";
                notif.SEEN = "N";
                notif.POST_ID = like.POST_ID;
                notif.RECEIVER_ID = poster.POSTER_ID;
                notificationManager.GenericAdd(notif);
            }
            return result;
        }

        public int UnlikePost(int like)
        {
            int result = 0;
            PB_LIKE likeToUnlike = likeManager.GetSpecific(x => x.ID == like);
            result = likeManager.GenericDelete(likeToUnlike);
            //result = postManager.UnlikePost(like);

            PB_NOTIFICATION notif = notificationManager.GetSpecific(x => x.POST_ID == likeToUnlike.POST_ID && x.SENDER_ID == likeToUnlike.LIKED_BY);
            return notificationManager.GenericDelete(notif);
        }

        public List<PB_NOTIFICATION> RetrieveNotification(int ID)
        {
            return postManager.RetreiveNotification(ID);
        }

        public PB_POST RetrieveSpecificPost(int postID)
        {
            return postManager.RetrieveSpecificPost(postID);
        }

        public int SeenNotification(int userID,string type)
        {
            int result = 0;
            List<PB_NOTIFICATION> listOfNotification = postManager.RetreiveNotification(userID);
            if (type == "F")
            {
               listOfNotification = listOfNotification.Where(x => x.NOTIF_TYPE == type && x.SEEN == "N").ToList();
            }
            else
            {
               listOfNotification = listOfNotification.Where(x => x.SEEN =="N" && x.NOTIF_TYPE == "C" || x.NOTIF_TYPE == "L").ToList();
            }

            foreach (var item in listOfNotification)
            {
                item.SEEN = "Y";
               result=  notificationManager.GenericEdit(item);
            }

            return result;
        }
        public int CountNotification(int userID, string type) {
            int count = 0;
            var notificationList = postManager.RetreiveNotification(userID);
            if (type == "F")
            {
                count = notificationList.Where(x => x.NOTIF_TYPE == type && x.SEEN == "N").Count();
            }
            else
            {
                count = notificationList.Where(x => x.SEEN == "N" &&( x.NOTIF_TYPE == "C" || x.NOTIF_TYPE == "L")).Count();
            }
            return count;
        }
    }
}
