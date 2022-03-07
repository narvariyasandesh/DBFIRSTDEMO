using DBFIRSTDEMO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBFIRSTDEMO.Controllers
{
    public class CustomerController : Controller
    {
        DBFIRSTCUSTOMEREntities context = new DBFIRSTCUSTOMEREntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View(context.Customers.ToList());
        }

        public ActionResult Create(Customer cust)
        {
            ViewBag.member = context.Memberships.ToList();
            if (cust.id==0)
            {
                return View();
            }
            return View(cust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(Customer customer)
        {
            ViewBag.member = context.Memberships.ToList();
            if (customer != null)
            {
                Customer cust = new Customer();
                cust.id = customer.id;
                cust.firstname = customer.firstname;
                cust.lastname = customer.lastname;
                cust.email = customer.email;
                cust.mobile = customer.mobile;
                cust.MembershipId = customer.MembershipId;
                //cust.Membership = customer.Membership;
                if (cust.id == 0)
                {
                    context.Customers.Add(cust);
                    context.SaveChanges();
                    //try
                    //{
                    //    context.SaveChanges();
                    //}
                    //catch(DbEntityValidationException e)
                    //{
                    //    Console.WriteLine(e);
                    //}
                    return RedirectToAction("Index");
                }
                else
                {
                    context.Entry(cust).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var cust = context.Customers.Where(x => x.id == id).First();
            if (cust != null)
            {
                context.Customers.Remove(cust);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}