using DBFIRSTDEMO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBFIRSTDEMO.Controllers
{
    public class MembershipController : Controller
    {
        DBFIRSTCUSTOMEREntities context = new DBFIRSTCUSTOMEREntities();
        // GET: Membership
        public ActionResult Index()
        {
            ViewBag.member = context.Memberships.ToList();
            return View();
        }


        public ActionResult AddMembership(Membership membership)
        {
            
            Membership member = new Membership();
            member.membertype = membership.membertype;
            context.Memberships.Add(member);
            context.SaveChanges();
            return RedirectToAction("Index");
          
        }


        public ActionResult Delete(int id)
        {
            var member = context.Memberships.Where(e => e.id == id).First();
            if (member != null)
            {
                context.Memberships.Remove(member);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}