using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineExamDbContext db = null;
        public UserDao()
        {
            db = new OnlineExamDbContext();
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
                user.FullName = entity.FullName;
                user.Password = entity.Password;
                user.Role = entity.Role;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        public List<User> ListAll()
        {
            var listuser = db.Database.SqlQuery<User>("Select * from [User]").ToList();
            return listuser;
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        } 
    }
}
