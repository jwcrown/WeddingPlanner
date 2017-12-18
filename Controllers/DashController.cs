using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class DashController : Controller
    {
        private WeddingContext _context;
 
        public DashController(WeddingContext context)
        {
            _context = context;
        }
        private User ActiveUser 
        {
            get{ return _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("Id")).SingleOrDefault();}
        }
        [Route("Dashboard")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            if (ActiveUser == null){
                return RedirectToAction("Index", "User");
            }
            Dashboard dashData = new Dashboard
            {
                Weddings = _context.Weddings.Include(w => w.Guests).ToList(),
                User = ActiveUser,
            };
            return View("Dash", dashData);
        }

        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            if (ActiveUser != null){
                TempData["Success"] = "Successfully Logged Out";
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "User");
        }

        [Route("GoHome")]
        [HttpGet]
        public IActionResult GoHome()
        {
            return RedirectToAction("Dashboard", "Dash");
        }

        [Route("CreateForm")]
        [HttpGet]
        public IActionResult CreateForm()
        {
            return View("CreateForm");
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(WeddingView model)
        {
            if (ModelState.IsValid){
                Wedding NewWedding = new Wedding();
                NewWedding.Wedder1 = model.Wedder1;
                NewWedding.Wedder2 = model.Wedder2;
                NewWedding.WeddingAddress = model.WeddingAddress;
                NewWedding.WeddingDate = model.WeddingDate;
                NewWedding.UserId = ActiveUser.UserId;
                _context.Weddings.Add(NewWedding);
                _context.SaveChanges();
                int weddingid = NewWedding.WeddingId;
                return RedirectToAction("Show", new { id = weddingid});
            }
            ViewBag.errors = ModelState.Values;
            return RedirectToAction("CreateForm");
        }
        [Route("rsvp/{id}")]
        [HttpGet]
        public IActionResult Rsvp(int id)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "User");
            RSVP rsvp = new RSVP
            {
                UserId = ActiveUser.UserId,
                WeddingId = id
            };
            _context.rsvps.Add(rsvp);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [Route("unrsvp/{id}")]
        [HttpGet]
        public IActionResult UnRsvp(int id)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "User");
            RSVP toDelete = _context.rsvps.Where(r => r.WeddingId == id)
                                          .Where(r => r.UserId == ActiveUser.UserId)
                                          .SingleOrDefault();
            _context.rsvps.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        [Route("show/{id}")]
        [HttpGet]
        public IActionResult Show(int id)
        {
            if(ActiveUser == null){
               return RedirectToAction("Index", "User");
            }
            Wedding query = _context.Weddings.Include(w => w.Guests).ThenInclude(g => g.Guest).Where(w => w.WeddingId == id).SingleOrDefault();
            return View(query);
        }


    }
}