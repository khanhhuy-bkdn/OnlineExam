using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Areas.Admin.Controllers
{
    public class LessionController : BaseController
    {
        // GET: Admin/Lession
        public ActionResult Index()
        {
            var imp = new SubjectDao();
            var model = imp.ListAllLes();
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new SubjectDao().ViewDetaiLes(id);
            SetViewBag(user.LanguageId);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.Lession lan)
        {
            var mk = lan.NameLession;
            if (mk != null)
            {
                var dao = new SubjectDao();
                var id = dao.UpdateLes(lan);
                if (id)
                {
                    SetAlert("Update thành công!", "success");
                    return RedirectToAction("Index", "Lession");
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

        [HttpPost]
        public ActionResult Create(Model.EF.Lession lan)
        {
            var tdn = lan.NameLession;
            if (tdn != null)
            {

                var dao = new SubjectDao();
                var user = dao.GetByLes(tdn);
                if (user == null)
                {
                    int id = dao.InsertLes(lan);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành công!", "success");
                        return RedirectToAction("Index", "Lession");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới không thành công!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Đã tồn tại trường này!");
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
                new SubjectDao().DeleteLes(id);
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
            ViewBag.LanguageId = new SelectList(dao.ListAllLan(), "LanguageId", "NameLanguage", selectedId);
        }
    }
}