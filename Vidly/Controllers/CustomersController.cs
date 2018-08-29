using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        // GET: Customers

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult New()
        {
            Customer customer = new Customer();
            customer.Birthdate = DateTime.Now;
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = membershipTypes,
            };

            return View("CustomerForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                return RedirectToAction("Index", "Customers");
            } else
            {
                Customer customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                _context.SaveChanges();

                return RedirectToAction("Index", "Customers");
            }


        }

        public ActionResult Edit(int id)
        {
            // searchCustomer = _context.Customers.Find(id);
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // then we need a view model and a view and exception checking

            if(customer == null)
            {
                return HttpNotFound();
            }

            CustomerFormViewModel viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }


        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).ToList().SingleOrDefault(c => c.Id == id);
            CustomerDetailViewModel viewModel = new CustomerDetailViewModel
            {
                Customer = customer,
                MembershipType = customer.MembershipType
        };

            return View(viewModel);
        }
    }
}