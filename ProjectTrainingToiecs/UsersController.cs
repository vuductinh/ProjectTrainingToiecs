﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTrainingToiecs.Common;
using ProjectTrainingToiecs.Common.Enum;
using ProjectTrainingToiecs.Models;
using ProjectTrainingToiecs.Service.Service;
using ProjectTrainingToiecs.Service.Service.Impl;
using ProjectTrainingToiecs.Service.Service.Model;
namespace ProjectTrainingToiecs
{
    public class UsersController : Controller
    {
        private readonly DbTrainingToiecsContext _context;

        public UsersController(DbTrainingToiecsContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index1()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(userName) || role == "User")
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View();
            }
        }
        public JsonResult GetUsers(FilterModel filter)
        {
            var order = 0;
            var users = _context.Users.Where(x => x.RecordStatusId == (int)ERecordStatus.Actived).ToList();
            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                filter.TextSearch = filter.TextSearch.ToLower();
                users = users.Where(x => x.UserName.ToLower().Contains(filter.TextSearch) 
                || x.FullName.ToLower().Contains(filter.TextSearch)
                || x.Email.ToLower().Contains(filter.TextSearch)).ToList();
            }
            users = users.Take(filter.PageSize).ToList();
            var userIds = users.Select(x => x.Id);
            var process = new Dictionary<int, int>();
            if (userIds.Any())
            {
                process = _context.StatusStudies.Where(x => userIds.Contains(x.UserId)).GroupBy(x => x.UserId)
               .Select(x => new
               { x.Key, Value = x.Count() }).ToDictionary(x => x.Key, x => x.Value);
            }
           
            var total = _context.TestDetails.Where(x=>x.RecordStatusId == (int)ERecordStatus.Actived).Count();
            users.ForEach(x =>
            {
                x.Order = order + 1;
                if (process.Any())
                {
                    if (process.ContainsKey(x.Id))
                    {
                        x.Process = (process[x.Id] * 100) / total;
                    }
                }
                order++;
            });
            return Json(new { data = users });
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserName,Password,Email,Phone,Active")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            var user = new Users();
            if (id > 0)
            {
                user = _context.Users.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.model = user;
            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserName,Password,Email,Phone,Active")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DbTrainingToiecsContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Login()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View("Login");
            }
        }
        public async Task<IActionResult> Register()
        {
            var course = _context.Course.Where(x=>x.RecordStatusId == (int)ERecordStatus.Actived).ToList();
            ViewBag.course = course;
            return View("Register");
        }
        public JsonResult LoginUser(AccountModel user)
        {
            var service =  new AccountService();
            //var data = service.CheckLogin(user , _context);
            var val = string.Empty;
            var role = 0;
            try
            {
                //mã hóa mk
                var password = service.DeCodePassword(user.Password);
                var model = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == password);
                if (model != null)
                {
                    val = !model.Active ? "Tài khoản quả bạn đã bị khóa vui lòng liên hệ quản trị viên" : val;
                    if (model.Active)
                    {
                        HttpContext.Session.SetString("UserName", string.IsNullOrEmpty(model.FullName) ? model.UserName : model.FullName);
                        HttpContext.Session.SetString("Role", model.RoleId == ESRole.Admin ? "Admin" : "User");
                        HttpContext.Session.SetString("TypeCourse", model.TypeCourse == 1 ? "Basic" : "Advanced");
                        HttpContext.Session.SetString("UserId", model.Id.ToString());
                        role = model.RoleId;
                    }
                }
                else
                {
                    val = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            catch (Exception ex)
            {
                val = "Gặp lỗi trong quá trình đăng nhập";
            }
            return Json(new { result = string.IsNullOrEmpty(val), role = role });
        }
        public JsonResult RegsiterUser(AccountModel user)
        {
            var service =  new AccountService();
            var val = string.Empty;
            var newUserId = 0;
            try
            {
                var checkMail = _context.Users.Any(x => (x.Email == user.Email || x.UserName == user.UserName)
                    && x.RecordStatusId == (int)ERecordStatus.Actived && x.Active);
                
                if (!checkMail)
                {
                    val = "Email hoặc tên tài khoản này đã được đăng ký vui lòng điền email khác";
                }
                else
                {
                    //gửi mail xác thực
                    val = service.SendEmailDigitNumber(user);
                }
            }
            catch (Exception ex)
            {
                val = "Gặp lỗi trong quá trình tạo tài khoản";
            }
            return Json(new { result = 1, data = val , userId = newUserId });
        }
        public JsonResult ValidateCode(AccountModel user)
        {
            var val = true;
            try
            {
                if (user != null)
                {
                    val = false;
                }
                else
                {
                    //Thêm người dùng vào hệ thống;
                    //mã hóa mật khẩu
                    var service = new AccountService();
                    user.Password = service.DeCodePassword(user.Password);
                    var newUser = new Users()
                    {
                        UserName = user.UserName,
                        Password = user.Password,
                        Email = user.Email,
                        Phone = user.Phone,
                        FullName = user.FullName,
                        RoleId = ESRole.User,
                        TypeCourse = user.TypeCourse,
                        Created = DateTime.Now
                    };
                    _context.Add(newUser);
                    _context.SaveChanges();
                    val = newUser.Id > 0;
                }
            }
            catch (Exception ex)
            {
                val = false;
            }
            return Json(new { result = val });
        }
        public JsonResult SignOut()
        {
            HttpContext.Session.Remove("UserName");
            return  Json(new { result = 1 });
        }
        public JsonResult GetDataAccount(string key)
        {
            var data = _context.Users.Select(x => x);
            if (!string.IsNullOrEmpty(key))
            {
                data = data.Where(x => x.FullName.Contains(key));
            }
            return Json(new { result = data, status = "OK" });
        }
        public JsonResult EditUser(Users data)
        {
            if (data.Id == 0)
            {
                _context.Add(data);
                _context.SaveChanges();
            }
            else
            {
                _context.Update(data);
                _context.SaveChangesAsync();
            }
            return Json(new { data = data.Id });
        }
        public JsonResult DeleteAccount(int id)
        {
            var val = false;
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if(user != null)
            {
                user.RecordStatusId = (int)ERecordStatus.Deleted;
                _context.Update(user);
                _context.SaveChanges();
                val = true;
            }
            return Json(new { Data = val });
        }
    }
}
