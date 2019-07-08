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
    [Authorize]
    public class PaisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pais
        public ActionResult Index()
        {
            return View(db.Pais.ToList());
        }

        // GET: Pais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //este erro ocorre porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                //O Funcionario não foi encontrado, porque o utilizador está a fazer asneira
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        

        // GET: Pais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //este erro ocorre porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                //o Funcionario nao foi encontrado porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        // POST: Pais/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Idade,NumCC,NIF,Telemovel,Email")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pais).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pais);
        }

        // GET: Pais/Delete/5
        [Authorize(Roles = "Func")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //este erro ocorre porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                //o Funcionario nao foi encontrado porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            Session["pai"] = pais.ID;
            return View(pais);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Func")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {

                //o utilizar está a tentar dar-me a volta ao código
                return RedirectToAction("Index");
            }
            //o ID não é null
            //será o ID o que eu espero?
            // vamos validar se o ID está correto
            if (id != (int)Session["pai"])
            {
                // Caso o utilizador esteja a fazer asneiras
                return RedirectToAction("Index");
            }

            Pais pais = db.Pais.Find(id);
            if (pais == null)
            {
                //nao foi encontrado o agente
                return RedirectToAction("Index");
            }
            try
            {
                db.Pais.Remove(pais);
                db.SaveChanges();
            }catch (Exception)
            {
                //informar que houve um erro
                ModelState.AddModelError("", "Não é possivel remover o Encarregado de Educação " + pais.Nome + ". Provalvemente possui concretizações associadas a ele");
                return View(pais);
            }
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
