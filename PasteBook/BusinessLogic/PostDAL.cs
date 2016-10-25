using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PostDAL
    {
        List<Exception> errorList = new List<Exception>();

        public List<PB_POST> RetrievePost(int id)
        {
            List<PB_POST> postList = new List<PB_POST>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return postList = context.PB_POST
                          .Include("PB_USER")
                          .Include("PB_USER1")
                          .Include("PB_LIKE.PB_USER")
                          .Include("PB_COMMENT")
                          .Where(x => x.PROFILE_OWNER_ID == id )
                          .OrderByDescending(x => x.CREATED_DATE)
                          .ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return postList = null;
        }

        public PB_POST RetrieveSpecificPost(int PostID)
        {
            PB_POST post = new PB_POST();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return post = context.PB_POST
                          .Include("PB_USER")
                          .Include("PB_USER1")
                          .Include("PB_LIKE.PB_USER")
                          .Include("PB_COMMENT")
                          .Where(x=>x.ID == PostID).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return post = null;

        }

        public List<PB_POST> RetrievePostHomePage(List<PB_FRIENDS> listOfFriends, int ownerID)
        {
            List<PB_POST> postList = new List<PB_POST>();
            var user = listOfFriends.Select(x => x.USER_ID).ToList();
            var friend = listOfFriends.Select(x => x.FRIEND_ID).ToList();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return postList = context.PB_POST
                          .Include("PB_USER")
                          .Include("PB_USER1")
                          .Include("PB_LIKE.PB_USER")
                          .Include("PB_COMMENT")
                          .Where(x=> user.Contains(x.POSTER_ID) || friend.Contains(x.POSTER_ID) || x.POSTER_ID == ownerID)
                          .OrderByDescending(x => x.CREATED_DATE)
                          .ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return postList;
        }

        public int AddNotification(PB_NOTIFICATION notif)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PB_NOTIFICATION.Add(notif);
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
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

        public int AddComment(PB_COMMENT comment)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PB_COMMENT.Add(comment);
                    PB_NOTIFICATION notid = new PB_NOTIFICATION() {
                        NOTIF_TYPE = "C",
                        POST_ID = comment.POST_ID,
                        SEEN = "N",
                        SENDER_ID = comment.POSTER_ID,
                        RECEIVER_ID = context.PB_POST.Where(x => x.ID == comment.POST_ID).SingleOrDefault().POSTER_ID,
                        CREATED_DATE = DateTime.Now,
                        COMMENT_ID = comment.ID
                    };
                    context.PB_NOTIFICATION.Add(notid);
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public int AddLikePost(PB_LIKE like, int ReceiverID)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.PB_LIKE.Add(like);

                    PB_NOTIFICATION notif = new PB_NOTIFICATION();
                    notif.SENDER_ID = like.LIKED_BY;
                        notif.CREATED_DATE = DateTime.Now;
                         notif.NOTIF_TYPE = "L";
                         notif.SEEN = "N";
                         notif.POST_ID = like.POST_ID;
                         notif.RECEIVER_ID = ReceiverID;
                 
                    context.PB_NOTIFICATION.Add(notif);
                    returnValue = context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public int UnlikePost(int like)
        {
            int returnValue = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    var postToUnlike = context.PB_LIKE.Where(x => x.ID == like).SingleOrDefault();
                    context.PB_LIKE.Remove(postToUnlike);
                    returnValue = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return returnValue;
        }

        public List<PB_NOTIFICATION> RetreiveNotification(int id)
        {
            List<PB_NOTIFICATION> notifList = new List<PB_NOTIFICATION>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    return notifList = context.PB_NOTIFICATION
                          .Include("PB_USER")
                          .Include("PB_USER1")
                          .Include("PB_COMMENT")
                          .Include("PB_POST")
                          .Where(x => x.RECEIVER_ID == id && x.SEEN == "N")
                          .ToList();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return notifList = null;
        }

     

        //public List<PB_COMMENT> RetrieveComment()
        //{
        //    List<PB_COMMENT> commentList = new List<PB_COMMENT>();
        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities())
        //        {
        //            return commentList = context.PB_COMMENT
        //                                        .Include("PB_USER")
        //                                         .ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorList.Add(ex);
        //    }
        //    return commentList = null;
        //}



        //public List<PB_LIKE> RetrieveLike(int id)
        //{
        //    List<PB_LIKE> likeList = new List<PB_LIKE>();
        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities())
        //        {
        //            return likeList = context.PB_LIKE
        //                                     .Include("PB_USER")
        //                                     .Where(x => x.LIKED_BY == id)
        //                                     .ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorList.Add(ex);
        //    }
        //    return likeList = null;
        //}

        //public List<PB_LIKE> RetrieveAllLike()
        //{
        //    List<PB_LIKE> likeList = new List<PB_LIKE>();
        //    try
        //    {
        //        using (var context = new PASTEBOOKEntities())
        //        {
        //            return likeList = context.PB_LIKE
        //                                     .Include("PB_USER")
        //                                     .ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorList.Add(ex);
        //    }
        //    return likeList = null;
        //}
    }
}
