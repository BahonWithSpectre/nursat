using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nursat.Data;
using nursat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nursat.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        private AppDb db;
        private UserManager<User> um;
        private SignInManager<User> sm;
        private RoleManager<IdentityRole> rm;

        public AdminController(AppDb _db, UserManager<User> _um, SignInManager<User> _sm, RoleManager<IdentityRole> _rm)
        {
            db = _db;
            um = _um;
            sm = _sm;
            rm = _rm;


            
        }



        //[AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (!db.Users.Any())
            {
                await rm.CreateAsync(new IdentityRole("admin"));

                User user = new User { UserName = "nursat", Email = "nursat" };

                var result = await um.CreateAsync(user, "nursat2021");
                
                await db.SaveChangesAsync();
            }

            var data = await db.Children.Where(k=>k.DeleteStats == false).ToListAsync();

            return View(data);
        }



        public IActionResult CreateChild()
        {
            return View();
        }



        [HttpPost]
        public IActionResult CreateChild(Child model)
        {
            Child ch = new Child { ChildName = model.ChildName, IIN = model.IIN, Pan = model.Pan, Parent = model.Parent, PhoneNumber = model.PhoneNumber, StartDate = model.StartDate };

            db.Children.Add(ch);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int Id)
        {
            return View(db.Children.Find(Id));
        }

        public IActionResult EditChild(int Id)
        {
            return View(db.Children.Find(Id));
        }


        [HttpPost]
        public IActionResult EditChild(Child model)
        {
            var ch = db.Children.Find(model.Id);

            ch.ChildName = model.ChildName;
            ch.EndDate = model.EndDate;
            ch.IIN = model.IIN;
            ch.Pan = model.Pan;
            ch.Parent = model.Parent;
            ch.PhoneNumber = model.PhoneNumber;
            ch.StartDate = model.StartDate;

            db.Children.Update(ch);
            db.SaveChanges();

            return RedirectToAction("Detail", new { Id = model.Id });
        }


        public IActionResult Delete(int Id)
        {
            var child = db.Children.Find(Id);

            child.DeleteStats = true;

            db.Children.Update(child);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        




        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                await sm.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await sm.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

    }
}
