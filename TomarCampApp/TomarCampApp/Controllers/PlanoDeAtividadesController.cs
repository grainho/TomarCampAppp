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
    public class PlanoDeAtividadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanoDeAtividades
        public ActionResult Index()
        {
            return View(db.PlanoDeAtividades.ToList());
        }

        // GET: PlanoDeAtividades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoDeAtividades planoDeAtividades = db.PlanoDeAtividades.Find(id);
            if (planoDeAtividades == null)
            {
                return HttpNotFound();
            }
            return View(planoDeAtividades);
        }

        // GET: PlanoDeAtividades/Create
        public ActionResult Create()
        {
            ViewBag.ListaObjetosDeConc = db.Concretizacao.OrderBy(f => f.dataInicioConcretizacao).ToList();
            return View();
        }

        // POST: PlanoDeAtividades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Turno,dataInicioPA,dataFimPA")] PlanoDeAtividades planoDeAtividades, string [] opcoesEscolhidasDeConc)
        {

            if (opcoesEscolhidasDeConc == null)
            {
                ModelState.AddModelError("", "Necessita escolher pelo menos uma Concretização para associar ao plano de atividades.");

                ViewBag.ListaObjetosDeConc = db.Concretizacao.OrderBy(f => f.dataInicioConcretizacao).ToList();

                // devolver controlo à View
                return View(planoDeAtividades);
            }

            List<Concretizacao> listaDeObjetosDeConcEscolhidos = new List<Concretizacao>();
            foreach (string item in opcoesEscolhidasDeConc)
            {

                Concretizacao c = db.Concretizacao.Find(Convert.ToInt32(item));
                // adicioná-lo à lista
                listaDeObjetosDeConcEscolhidos.Add(c);
            }

            // adicionar a lista ao objeto de crianças
            planoDeAtividades.ListaDeObjetosDeConcretizacao = listaDeObjetosDeConcEscolhidos;

            if (ModelState.IsValid)
            {
                db.PlanoDeAtividades.Add(planoDeAtividades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planoDeAtividades);
        }

        // GET: PlanoDeAtividades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoDeAtividades planoDeAtividades = db.PlanoDeAtividades.Find(id);
            if (planoDeAtividades == null)
            {
                return HttpNotFound();
            }
            return View(planoDeAtividades);
        }

        // POST: PlanoDeAtividades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Turno,dataInicioPA,dataFimPA")] PlanoDeAtividades planoDeAtividades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planoDeAtividades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planoDeAtividades);
        }

        // GET: PlanoDeAtividades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoDeAtividades planoDeAtividades = db.PlanoDeAtividades.Find(id);
            if (planoDeAtividades == null)
            {
                return HttpNotFound();
            }
            return View(planoDeAtividades);
        }

        // POST: PlanoDeAtividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanoDeAtividades planoDeAtividades = db.PlanoDeAtividades.Find(id);
            db.PlanoDeAtividades.Remove(planoDeAtividades);
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
