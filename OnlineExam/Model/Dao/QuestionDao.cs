using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class QuestionDao
    {
        OnlineExamDbContext db = null;
        public QuestionDao()
        {
            db = new OnlineExamDbContext();
        }
        public int Insert(Question entity)
        {
            db.Questions.Add(entity);
            db.SaveChanges();
            return entity.QuestionId;
        }

        public bool Update(Question entity)
        {
            try
            {
                var user = db.Questions.Find(entity.QuestionId);
                user.Content = entity.Content;
                user.AnswerA = entity.AnswerA;
                user.AnswerB = entity.AnswerB;
                user.AnswerC = entity.AnswerC;
                user.AnswerD = entity.AnswerD;
                user.CorrectAnswer = entity.CorrectAnswer;
                user.GroupId = entity.GroupId;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Question> ListAll()
        {
            var list = db.Database.SqlQuery<Question>("Select * from [Question]").ToList();
            return list;
        }

        public Question ViewDetail(int id)
        {
            return db.Questions.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Questions.Find(id);
                db.Questions.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Question GetByName(string user)
        {
            return db.Questions.SingleOrDefault(x => x.Content == user);
        }
    }
}
