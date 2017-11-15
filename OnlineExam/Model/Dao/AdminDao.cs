using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AdminDao
    {
        OnlineExamDbContext db = null;
        public AdminDao()
        {
            db = new OnlineExamDbContext();
        }
        public int Insert(Admin entity)
        {
            db.Admins.Add(entity);
            db.SaveChanges();
            return entity.AdminID;
        }

        public bool Update(Admin entity)
        {
            try
            {
                var user = db.Admins.SingleOrDefault(x=>x.Username == entity.Username);
                user.Password = entity.Password;
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

        public List<Admin> listall()
        {
            var list = db.Database.SqlQuery<Admin>("Select * from Admin").ToList();
            return list;
        }

        public Admin ViewDetail(int id)
        {
            return db.Admins.Find(id);
        }

        public Admin GetByID(string user)
        {
            return db.Admins.SingleOrDefault(x => x.Username == user);
        }

        public bool Login (string user, string pass)
        {
            var result = db.Admins.Count(x => x.Username == user && x.Password == pass);
            if(result>0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Admins.Find(id);
                db.Admins.Remove(user);
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }
    }
}
