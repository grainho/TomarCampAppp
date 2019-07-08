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
    public class ConcretizacaosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Concretizacaos
        public ActionResult Index()
        {
            var concretizacao = db.Concretizacao.Include(c => c.Atividade);
            return View(concretizacao.ToList());
        }

        // GET: Concretizacaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concretizacao concretizacao = db.Concretizacao.Find(id);
            if (concretizacao == null)
            {
                return HttpNotFound();
            }
            return View(concretizacao);
        }

        // GET: Concretizacaos/Create
        public ActionResult Create()
        {
            ViewBag.AtividadeFK = new SelectList(db.Atividades, "ID", "Nome");
            return View();
        }

        // POST: Concretizacaos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,dataInicioConcretizacao,dataFimConcretizacao,local,AtividadeFK")] Concretizacao concretizacao)
        {
            if (ModelState.IsValid)
            {
                db.Concretizacao.Add(concretizacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtividadeFK = new SelectList(db.Atividades, "ID", "Nome", concretizacao.AtividadeFK);
            return View(concretizacao);
        }

        // GET: Concretizacaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concretizacao concretizacao = db.Concretizacao.Find(id);
            if (concretizacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtividadeFK = new SelectList(db.Atividades, "ID", "Nome", concretizacao.AtividadeFK);
            return View(concretizacao);
        }

        // POST: Concretizacaos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,dataInicioConcretizacao,dataFimConcretizacao,local,AtividadeFK")] Concretizacao concretizacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concretizacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AtividadeFK = new SelectList(db.Atividades, "ID", "Nome", concretizacao.AtividadeFK);
            return View(concretizacao);
        }

        // GET: Concretizacaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concretizacao concretizacao = db.Concretizacao.Find(id);
            if (concretizacao == null)
            {
                return HttpNotFound();
            }
            return View(concretizacao);
        }

        // POST: Concretizacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concretizacao concretizacao = db.Concretizacao.Find(id);
            db.Concretizacao.Remove(concretizacao);
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
