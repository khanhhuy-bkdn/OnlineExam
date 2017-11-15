using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Areas.Admin.Controllers
{
    public class LanguageController : BaseController
    {
        // GET: Admin/Language
        public ActionResult Index()
        {
            var imp = new SubjectDao();
            var model = imp.ListAllLan();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new SubjectDao().ViewDetailLan(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.Language lan)
        {
            var mk = lan.NameLanguage;
            if (mk != null)
            {
                var dao = new SubjectDao();
                var id = dao.UpdateLan(lan);
                if (id)
                {
                    SetAlert("Update thành công!", "success");
                    return RedirectToAction("Index", "Language");
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
            return View("Edit");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Model.EF.Language lan)
        {
            var tdn = lan.NameLanguage;
            if (tdn != null)
            {

                var dao = new SubjectDao();
                var user = dao.GetByLan(tdn);
                if (user == null)
                {
                    int id = dao.InsertLan(lan);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành công!", "success");
                        return RedirectToAction("Index", "Language");
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
            return View("Create");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                new SubjectDao().DeleteLan(id);
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
    }
}