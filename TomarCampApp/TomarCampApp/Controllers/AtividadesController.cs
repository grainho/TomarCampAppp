using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TomarCampApp.Models;

namespace TomarCampApp.Controllers
{
    public class AtividadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Atividades
        public ActionResult Index()
        {
            return View(db.Atividades.ToList());
        }

        // GET: Atividades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividades atividades = db.Atividades.Find(id);
            if (atividades == null)
            {
                return HttpNotFound();
            }
            return View(atividades);
        }

        // GET: Atividades/Create
        [Authorize(Roles = "Func")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Atividades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Func")]
        public ActionResult Create([Bind(Include = "ID,Nome,dataCriacao,materiais,descricao")] Atividades atividades)
        {
            if (ModelState.IsValid)
            {
                db.Atividades.Add(atividades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(atividades);
        }

        // GET: Atividades/Edit/5
        [Authorize(Roles = "Func")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividades atividades = db.Atividades.Find(id);
            if (atividades == null)
            {
                return HttpNotFound();
            }
            return View(atividades);
        }

        // POST: Atividades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Func")]
        public ActionResult Edit([Bind(Include = "ID,Nome,dataCriacao,materiais,descricao")] Atividades atividades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atividades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atividades);
        }

        // GET: Atividades/Delete/5
        [Authorize(Roles = "Func")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividades atividades = db.Atividades.Find(id);
            if (atividades == null)
            {
                return HttpNotFound();
            }
            return View(atividades);
        }

        // POST: Atividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Func")]
        public ActionResult DeleteConfirmed(int id)
        {
            Atividades atividades = db.Atividades.Find(id);
            db.Atividades.Remove(atividades);
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
