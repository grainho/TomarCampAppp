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
    public class CriancasController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Criancas
        public ActionResult Index()
        {
            
            
            ViewBag.mail = User.Identity.Name;
            var criancas = db.Criancas.Include(c => c.Pai);
            return View(criancas.ToList());
        }

        // GET: Criancas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criancas criancas = db.Criancas.Find(id);
            if (criancas == null)
            {
                return HttpNotFound();
            }
            return View(criancas);
        }

        // GET: Criancas/Create/5
        public ActionResult Create(int ? id)
        {
            Session["pai"] = id;
            ViewBag.ListaObjetosDePai = db.Pais.OrderBy(p => p.Nome).ToList();
            ViewBag.PaiID = id;
            ViewBag.ListaObjetosDePA = db.PlanoDeAtividades.OrderBy(pa => pa.Turno).ToList();
            return View();
        }

        // POST: Criancas/Create/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Idade,Doencas,NumCC,NIF")] Criancas criancas, string[] opcoesEscolhidasDePA, string[] opcoesEscolhidasDePais)
        {

            /// avalia se o array com a lista das escolhas de objetos de PlanosDeAtividades associados ao objeto do tipo Criancas
            /// é nula, ou não.
            /// Só poderá avanção se NÃO for nula
            if (opcoesEscolhidasDePA == null)
            {
                ModelState.AddModelError("", "Necessita escolher pelo menos um PlanoDeAtividades para associar à criança.");
                // gerar a lista de objetos de PA que podem ser associados a Criança
                ViewBag.ListaObjetosDePA = db.PlanoDeAtividades.OrderBy(pa => pa.Turno).ToList();
                ViewBag.ListaObjetosDePai = db.Pais.OrderBy(p => p.Nome).ToList();
                ViewBag.PaiID = Session["pai"];
                // devolver controlo à View
                return View(criancas);
            }

            
            List<PlanoDeAtividades> listaDeObjetosDePAEscolhidos = new List<PlanoDeAtividades>();
            foreach (string item in opcoesEscolhidasDePA)
            {
                
                PlanoDeAtividades pa = db.PlanoDeAtividades.Find(Convert.ToInt32(item));
                // adicioná-lo à lista
                listaDeObjetosDePAEscolhidos.Add(pa);
            }

            // adicionar a lista ao objeto de crianças
            criancas.ListaDeObjetosDePlanoDeAtividades = listaDeObjetosDePAEscolhidos;


            if (opcoesEscolhidasDePais == null)
            {
                ModelState.AddModelError("", "Necessita escolher o pai para associar à criança.");
                
                ViewBag.ListaObjetosDePai = db.Pais.OrderBy(p => p.Nome).ToList();
                ViewBag.ListaObjetosDePA = db.PlanoDeAtividades.OrderBy(pa => pa.Turno).ToList();
                ViewBag.PaiID = Session["pai"];
                // devolver controlo à View
                return View(criancas);
            }

            // criar um pai
            Pais pai =db.Pais.Find(Convert.ToInt32(opcoesEscolhidasDePais.First())) ;
            

            // adicionar o ID do pai ao PaiFK
            criancas.PaiFK = pai.ID;


            if (ModelState.IsValid)
            {
                db.Criancas.Add(criancas);
                db.SaveChanges();
                return RedirectToAction("Details", "Pais", new { id = Session["pai"] });
            }

            
            return View(criancas);
        }

        // GET: Criancas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criancas criancas = db.Criancas.Find(id);
            if (criancas == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaiFK = new SelectList(db.Pais, "ID", "Nome", criancas.PaiFK);
            return View(criancas);
        }

        // POST: Criancas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Idade,Doencas,NumCC,NIF,PaiFK")] Criancas criancas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(criancas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaiFK = new SelectList(db.Pais, "ID", "Nome", criancas.PaiFK);
            return View(criancas);
        }

        // GET: Criancas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criancas criancas = db.Criancas.Find(id);
            if (criancas == null)
            {
                return HttpNotFound();
            }
            return View(criancas);
        }

        // POST: Criancas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Criancas criancas = db.Criancas.Find(id);
            db.Criancas.Remove(criancas);
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
