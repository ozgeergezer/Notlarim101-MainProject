using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Notlarim101.BusinessLayer;
using Notlarim101.Entity;
using Notlarim101.Entity.ValueObject;

namespace Notlarim101.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Test test = new Test();
            ////test.InsertTest();
            ////test.UpdateTest();
            ////test.DeleteTest();
            //test.CommentTest();

            NoteManager nm = new NoteManager();
            
            return View(nm.GetAllNotes().OrderByDescending(s=>s.ModifiedOn).ToList());
        }

        
        public ActionResult ByCategoryId(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes.OrderByDescending(s => s.ModifiedOn).ToList());
        }
        
        public ActionResult ByCategoryTitle(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryByTitle(id);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes.OrderByDescending(s => s.ModifiedOn).ToList());
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                NotlarimUserManager num = new NotlarimUserManager();
                BusinessLayerResult<NotlarimUser> res = num.LoginUser(model);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(s => ModelState.AddModelError("", s.Value));
                    return View(model);
                }
                //yonlendirme ve session
                Session["login"] = res.Result; //sessiona kullanıcı bilgisi aktarma
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //bool hasError = false;
            
            if (ModelState.IsValid)
            {
                NotlarimUserManager num = new NotlarimUserManager();
                BusinessLayerResult<NotlarimUser> res = num.RegisterUser(model);

                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(s => ModelState.AddModelError("",s.Value)); //hataları döngüye sokuyor
                    return View(model);
                }
                //try
                //{
                //    user = num.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("",ex.Message);
                //}
                //if (user==null)
                //{
                //    return View(model);
                //}
                //return RedirectToAction("RegisterOK");


                //if (model.UserName=="aaa")
                //{
                //    ModelState.AddModelError("", "kullanıcı adı alınmış.");
                //    //hasError = true;
                //}
                //if (model.EMail=="aaa@aaa.com")
                //{
                //    ModelState.AddModelError("", "Bu e mail Kullanılıyor.");
                //    //hasError = true;
                //}
                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count>0)
                //    {
                //        return View(model);
                //    }
                //}
                //return RedirectToAction("RegisterOK");
                //if (hasError==true)
                //{
                //    return View(model);// aynı modeli tekrar döndürürür ki yanlış girdiğini düzeltsin
                //}
                //else
                //{
                //    return RedirectToAction("RegisterOK");
                //}
                return RedirectToAction("RegisterOK");
            }
            return View(model);
        }
        public ActionResult RegisterOK()
        {
            return View();
        }
    }
}