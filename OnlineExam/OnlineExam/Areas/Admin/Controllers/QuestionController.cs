using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using Model.Dao;
using Model.EF;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
namespace OnlineExam.Areas.Admin.Controllers
{
    public class QuestionController : BaseController
    {
        // GET: Admin/Question
        public ActionResult Index()
        {
            var imp = new QuestionDao();
            var model = imp.ListAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new QuestionDao().ViewDetail(id);
            SetViewBag(user.GroupId);
            return View(user);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Model.EF.Question ques)
        {
            var mk = ques.Content;
            var ansA = ques.AnswerA;
            var ansB = ques.AnswerB;
            var ansC = ques.AnswerC;
            var ansD = ques.AnswerD;
            var CorAns = ques.CorrectAnswer;
            if (mk != null && ansA != null && ansB != null && ansC != null && ansD != null && CorAns != null)
            {
                var dao = new QuestionDao();
                var id = dao.Update(ques);
                if (id)
                {
                    SetAlert("Update thành công!", "success");
                    return RedirectToAction("Index", "Question");
                }
                else
                {
                    ModelState.AddModelError("", "Update không thành công!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường!");
            }
            SetViewBag();
            return View("Edit");
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Model.EF.Question ques)
        {
            var tdn = ques.Content;
            var ansA = ques.AnswerA;
            var ansB = ques.AnswerB;
            var ansC = ques.AnswerC;
            var ansD = ques.AnswerD;
            var CorAns = ques.CorrectAnswer;
            if (tdn != null && ansA != null && ansB != null && ansC != null && ansD != null && CorAns != null)
            {

                var dao = new QuestionDao();
                int id = dao.Insert(ques);
                if (id > 0)
                {
                    SetAlert("Thêm mới thành công!", "success");
                    return RedirectToAction("Index", "Question");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường!");
            }
            SetViewBag();
            return View("Create");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                new QuestionDao().Delete(id);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = e.Message,
                });
            }

        }

        public void SetViewBag(int? selectedId = null)
        {
            var dao = new SubjectDao();
            ViewBag.GroupId = new SelectList(dao.ListAllGr(), "GroupId", "NameGroup", selectedId);
        }

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file, Question ques)
        {
            if (ques.GroupId != null)
            {
                if (file == null || file.ContentLength == 0)
                {
                    ModelState.AddModelError("", "Vui lòng chọn file excel!");
                    return View("Import");
                }
                else
                {
                    if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                    {
                        try
                        {
                            string filepath = Server.MapPath("/ExcelUpload/") + file.FileName;
                            if (System.IO.File.Exists(filepath))
                                System.IO.File.Delete(filepath);
                            file.SaveAs(filepath);

                            Excel.Application application = new Excel.Application();
                            Excel.Workbook workbook = application.Workbooks.Open(filepath);
                            Excel.Worksheet worksheet = workbook.ActiveSheet;
                            Excel.Range range = worksheet.UsedRange;
                            
                            var dao = new QuestionDao();
                            for(int row = 2; row <= range.Rows.Count; row++)
                            {
                                var item = new Question();
                                item.Content = ((Excel.Range)range.Cells[row, 2]).Text; 
                                item.AnswerA = ((Excel.Range)range.Cells[row, 3]).Text;
                                item.AnswerB = ((Excel.Range)range.Cells[row, 4]).Text;
                                item.AnswerC = ((Excel.Range)range.Cells[row, 5]).Text;
                                item.AnswerD = ((Excel.Range)range.Cells[row, 6]).Text;
                                item.CorrectAnswer = int.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                                item.GroupId = ques.GroupId;
                                item.Status = ques.Status;
                                dao.Insert(item);
                            }
                            
                            SetAlert("Import thành công!", "success");
                            return RedirectToAction("Index", "Question");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Lỗi xảy ra trong quá trình import!");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "File không đúng định dạng!");
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường!");
            }

            return View("Import");

        }

        public JsonResult GetLanById()
        {
            OnlineExamDbContext db = new OnlineExamDbContext();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Languages, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLessionById(int id)
        {
            OnlineExamDbContext db = new OnlineExamDbContext();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Lessions.Where(m => m.LanguageId == id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGroupById(int id)
        {
            OnlineExamDbContext db = new OnlineExamDbContext();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Groups.Where(m => m.LessionId == id), JsonRequestBehavior.AllowGet);
        }
    }
}