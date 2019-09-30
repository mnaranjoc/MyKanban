using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyKanban.Models;

namespace MyKanban.Controllers
{
    public class KanbanItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: KanbanItems
        public ActionResult Index()
        {
            var kanbanItems = db.KanbanItems.Include(k => k.Board);
            return View(kanbanItems.ToList());
        }

        // GET: KanbanItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KanbanItem kanbanItem = db.KanbanItems.Find(id);
            if (kanbanItem == null)
            {
                return HttpNotFound();
            }
            return View(kanbanItem);
        }

        // GET: KanbanItems/Create
        public ActionResult Create()
        {
            ViewBag.BoardID = new SelectList(db.KanbanBoards, "ID", "Description");
            return View();
        }

        // POST: KanbanItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,DateCreated,ColumnID,Critical,BoardID")] KanbanItem kanbanItem)
        {
            var dateTimeNow = DateTime.Now;
            kanbanItem.DateCreated = dateTimeNow.Date;

            if (ModelState.IsValid)
            {
                db.KanbanItems.Add(kanbanItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardID = new SelectList(db.KanbanBoards, "ID", "Description", kanbanItem.BoardID);
            return View(kanbanItem);
        }

        // GET: KanbanItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KanbanItem kanbanItem = db.KanbanItems.Find(id);
            if (kanbanItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardID = new SelectList(db.KanbanBoards, "ID", "Description", kanbanItem.BoardID);
            return View(kanbanItem);
        }

        // POST: KanbanItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,DateCreated,ColumnID,Critical,BoardID")] KanbanItem kanbanItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kanbanItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardID = new SelectList(db.KanbanBoards, "ID", "Description", kanbanItem.BoardID);
            return View(kanbanItem);
        }

        // GET: KanbanItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KanbanItem kanbanItem = db.KanbanItems.Find(id);
            if (kanbanItem == null)
            {
                return HttpNotFound();
            }
            return View(kanbanItem);
        }

        // POST: KanbanItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KanbanItem kanbanItem = db.KanbanItems.Find(id);
            db.KanbanItems.Remove(kanbanItem);
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
