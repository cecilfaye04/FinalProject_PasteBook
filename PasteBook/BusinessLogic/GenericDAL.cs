using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class GenericDAL<T> where T : class
    {
        List<Exception> errorList = new List<Exception>();

        public int GenericAdd(T value)
        {
            int result = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(value).State = System.Data.Entity.EntityState.Added;
                    result = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return result;
        }

        public List<T> EntityList()
        {
            List<T> entityList = new List<T>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    entityList = context.Set<T>().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entityList;
        }

        public int GenericEdit(T value)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(value).State = System.Data.Entity.EntityState.Modified;
                    status = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return status;
        }


        public int GenericDelete(T value)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(value).State = System.Data.Entity.EntityState.Deleted;
                    status = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return 0;
        }

       public T GetSpecific(Expression<Func<T, bool>> predicate)
        {
            T item = null;
            using (var context = new PASTEBOOKEntities())
            {
                item = context.Set<T>().FirstOrDefault(predicate);
            }
            return item;
        }

        //public List<T> GetallList(Expression<Func<T, bool>> predicate)
        //{
        //    T item = null;
        //    List<T> entityList = new List<T>();

        //    using (var context = new PASTEBOOKEntities())
        //    {
        //        entityList = context.Set<T>().ToList();
        //        item = context.Set<T>().FirstOrDefault(predicate);
        //    }
        //    return item;
        //}



    }
}
