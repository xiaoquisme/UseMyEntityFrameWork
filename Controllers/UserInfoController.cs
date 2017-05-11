using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEntityFrameWork.DateBaseFactory.BaseClass;
using MyEntityFrameWork.DateBaseFactory.Implement;
using TestMyEntityFramework.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMyEntityFramework.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly SqlServerDatabase _context;
        public UserInfoController( SqlServerDatabase _serverDatabase)
        {
            _context = _serverDatabase;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
           
            return View(_context.GetAllInfo<Users>());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name,pwd")] Users userInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInfo);     
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo = _context.GetAllInfo<Users>().FirstOrDefault(u=>u.ID==id);
            if (userInfo == null)
            {
                return NotFound();
            }
            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Name,pwd")] Users userInfo)
        {
            if (id != userInfo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInfo);
                   
                }
                catch 
                {
                    if (!UserInfoExists(userInfo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception("数据更新失败");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo =  _context.GetAllInfo<Users>()
                .SingleOrDefault(m => m.ID == id);
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userInfo =  _context.GetAllInfo<Users>().SingleOrDefault(m => m.ID == id);

            _context.Remove(userInfo);
            return RedirectToAction("Index");
        }

        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo =  _context.GetAllInfo<Users>()
                .SingleOrDefault(m => m.ID == id);
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }
        private bool UserInfoExists(int id)
        {
            return _context.GetAllInfo<Users>().Any(e => e.ID == id);
        }
    }
}   
