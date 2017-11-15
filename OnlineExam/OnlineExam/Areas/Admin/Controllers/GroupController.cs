using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineExam.Areas.Admin.Controllers
{
    public class GroupController : BaseController
    {
        // GET: Admin/Group
        public ActionResult Index()
        {
            var imp = new SubjectDao();
            var model = imp.ListAllGr();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new SubjectDao().ViewDetailGr(id);
            SetViewBag(user.LessionId);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.Group lan)
        {
            var mk = lan.NameGroup;
            if (mk != null)
            {
                var dao = new SubjectDao();
                var id = dao.UpdateGr(lan);
                if (id)
                {
                    SetAlert("Update thành công!", "success");
                    return RedirectToAction("Index", "Group");
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
        public ActionResult Create(Model.EF.Group lan)
        {
            var tdn = lan.NameGroup;
            if (tdn != null)
            {

                var dao = new SubjectDao();
                var user = dao.GetByGr(tdn);
                if (user == null)
                {
                    int id = dao.InsertGr(lan);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành công!", "success");
                        return RedirectToAction("Index", "Group");
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
                new SubjectDao().DeleteGr(id);
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
            ViewBag.LessionId = new SelectList(dao.ListAllLes(), "LessionId", "NameLession", selectedId);
        }
    }
}