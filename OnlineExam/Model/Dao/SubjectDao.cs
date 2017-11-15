using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SubjectDao
    {
        OnlineExamDbContext db = null;
        public SubjectDao()
        {
            db = new OnlineExamDbContext();
        }
        public int InsertLan(Language entity)
        {
            db.Languages.Add(entity);
            db.SaveChanges();
            return entity.LanguageId;
        }

        public bool UpdateLan(Language entity)
        {
            try
            {
                var user = db.Languages.Find(entity.LanguageId);
                user.NameLanguage = entity.NameLanguage;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Language> ListAllLan()
        {
            var list = db.Database.SqlQuery<Language>("Select * from [Language]").ToList();
            return list;
        }

        public Language ViewDetailLan(int id)
        {
            return db.Languages.Find(id);
        }
        
        public bool DeleteLan(int id)
        {
            try
            {
                var user = db.Languages.Find(id);
                db.Languages.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Language GetByLan(string user)
        {
            return db.Languages.SingleOrDefault(x => x.NameLanguage == user);
        }

        // Lession

        public int InsertLes(Lession entity)
        {
            db.Lessions.Add(entity);
            db.SaveChanges();
            return entity.LessionId;
        }

        public bool UpdateLes(Lession entity)
        {
            try
            {
                var user = db.Lessions.SingleOrDefault(x => x.LessionId == entity.LessionId);
                user.NameLession = entity.NameLession;
                user.LanguageId = entity.LanguageId;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Lession> ListAllLes()
        {
            var list = db.Database.SqlQuery<Lession>("Select * from [Lession]").ToList();
            return list;
        }

        public Lession ViewDetaiLes(int id)
        {
            return db.Lessions.Find(id);
        }

        public bool DeleteLes(int id)
        {
            try
            {
                var user = db.Lessions.Find(id);
                db.Lessions.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Lession GetByLes(string user)
        {
            return db.Lessions.SingleOrDefault(x => x.NameLession == user);
        }

        //Group

        public int InsertGr(Group entity)
        {
            db.Groups.Add(entity);
            db.SaveChanges();
            return entity.GroupId;
        }

        public bool UpdateGr(Group entity)
        {
            try
            {
                var user = db.Groups.SingleOrDefault(x => x.GroupId == entity.GroupId);
                user.NameGroup = entity.NameGroup;
                user.LessionId = entity.LessionId;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Group> ListAllGr()
        {
            var list = db.Database.SqlQuery<Group>("Select * from [Group]").ToList();
            return list;
        }

        public Group ViewDetailGr(int id)
        {
            return db.Groups.Find(id);
        }
        //xóa theo id
        public bool DeleteGr(int id)
        {
            try
            {
                var user = db.Groups.Find(id);
                db.Groups.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //tìm theo tên
        public Group GetByGr(string user)
        {
            return db.Groups.SingleOrDefault(x => x.NameGroup == user);
        }
    }
}
