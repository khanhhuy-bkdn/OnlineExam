using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using OnlineExam.Common;

namespace OnlineExam.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            var imp = new AdminDao();
            var model = imp.listall();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new AdminDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Model.EF.Admin admin)
        {
            var mk = admin.Password;
            if (mk != null)
            {
                var dao = new AdminDao();

                var encryptMD5 = Encryptor.MD5Hash(admin.Password);
                admin.Password = encryptMD5;

                var id = dao.Update(admin);
                if (id)
                {
                    SetAlert("Update thành công!", "success");
                    return RedirectToAction("Index", "Admin");
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
        public ActionResult Create(Model.EF.Admin admin)
        {
            var tdn = admin.Username;
            var mk = admin.Password;
            if (tdn != null && mk != null)
            {

                var dao = new AdminDao();

                var encryptMD5 = Encryptor.MD5Hash(admin.Password);
                var user = dao.GetByID(admin.Username);
                admin.Password = encryptMD5;
                if (user == null)
                {
                    int id = dao.Insert(admin);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành công!","success");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới không thành công!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
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
                new AdminDao().Delete(id);
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