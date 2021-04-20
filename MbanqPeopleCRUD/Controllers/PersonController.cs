using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MbanqPeopleCRUD.DAL;
using MbanqPeopleCRUD.Models;

namespace MbanqPeopleCRUD.Controllers
{
    public class PersonController : Controller
    {
        private MbanqContext db = new MbanqContext();

        // GET: Person
        public ActionResult Index(string sortOrder, string nameFilter, string surnameFilter, string tinFilter, string phoneFilter)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // SELECT FROM
            var people = from s in db.Person
                           select s;

            // WHERE Filters
            if (!String.IsNullOrEmpty(nameFilter))
            {
                people = people.Where(s => s.Name.Contains(nameFilter));
            }
            if (!String.IsNullOrEmpty(surnameFilter))
            {
                people = people.Where(s => s.Surname.Contains(surnameFilter));
            }
            if (!String.IsNullOrEmpty(tinFilter))
            {
                people = people.Where(s => s.TIN.Contains(tinFilter));
            }
            if (!String.IsNullOrEmpty(phoneFilter))
            {
                people = people.Where(s => s.Phone.Contains(phoneFilter));
            }

            // ORDER BY sorting
            switch (sortOrder)
            {
                case "name_desc":
                    people = people.OrderByDescending(s => s.Name);
                    break;
                case "surname_desc":
                    people = people.OrderByDescending(s => s.Surname);
                    break;
                case "phone_asc":
                    people = people.OrderBy(s => s.Phone);
                    break;
                case "place_desc":
                    people = people.OrderByDescending(s => s.Place);
                    break;
                case "address_desc":
                    people = people.OrderByDescending(s => s.Address);
                    break;
                case "email_desc":
                    people = people.OrderByDescending(s => s.Email);
                    break;
                case "tin_asc":
                    people = people.OrderBy(s => s.TIN);
                    break;
                default:
                    people = people.OrderBy(s => s.ID);
                    break;
            }

            //return View(db.Person.ToList());
            return View(people.ToList());
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TIN,Name,Surname,Place,Address,Phone,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Person.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TIN,Name,Surname,Place,Address,Phone,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
