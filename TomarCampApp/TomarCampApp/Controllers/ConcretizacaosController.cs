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
        [Authorize(Roles = "Func")]
        public ActionResult Create()
        {
            ViewBag.AtividadeFK = new SelectList(db.Atividades, "ID", "Nome");
            ViewBag.ListaObjetosDeFunc = db.Funcionarios.OrderBy(f => f.Nome).ToList();
            return View();
        }

        // POST: Concretizacaos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Func")]
        public ActionResult Create([Bind(Include = "ID,dataInicioConcretizacao,dataFimConcretizacao,local,AtividadeFK")] Concretizacao concretizacao, string[] opcoesEscolhidasDeFunc)
        {


            if (opcoesEscolhidasDeFunc == null)
            {
                ModelState.AddModelError("", "Necessita escolher pelo menos um Funcionário para associar à concretização.");
                // gerar a lista de objetos de PA que podem ser associados a Criança
                ViewBag.ListaObjetosDeFunc = db.Funcionarios.OrderBy(f => f.Nome).ToList();

                // devolver controlo à View
                return View(concretizacao);
            }

            List<Funcionarios> listaDeObjetosDeFuncEscolhidos = new List<Funcionarios>();
            foreach (string item in opcoesEscolhidasDeFunc)
            {

                Funcionarios f = db.Funcionarios.Find(Convert.ToInt32(item));
                // adicioná-lo à lista
                listaDeObjetosDeFuncEscolhidos.Add(f);
            }

            // adicionar a lista ao objeto de crianças
            concretizacao.ListaDeObjetosDeFuncionario = listaDeObjetosDeFuncEscolhidos;

            if (ModelState.IsValid)
            {
                db.Concretizacao.Add(concretizacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtividadeFK = new SelectList(db.Atividades, "ID", "Nome", concretizacao.AtividadeFK);
            ViewBag.ListaObjetosDeFunc = db.Funcionarios.OrderBy(f => f.Nome).ToList();
            return View(concretizacao);
        }

        // GET: Concretizacaos/Edit/5
        [Authorize(Roles = "Func")]
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
            ViewBag.ListaObjetosDeFunc = db.Funcionarios.OrderBy(f => f.Nome).ToList();
            return View(concretizacao);
        }

        // POST: Concretizacaos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Func")]
        public ActionResult Edit([Bind(Include = "ID,dataInicioConcretizacao,dataFimConcretizacao,local,AtividadeFK")] Concretizacao concretizacao, string[] opcoesEscolhidasDeFunc)
        {


            var conc = db.Concretizacao.Include(f => f.ListaDeObjetosDeFuncionario).Where(f => f.ID == concretizacao.ID).SingleOrDefault();



            if (ModelState.IsValid)
            {
                conc.dataInicioConcretizacao = concretizacao.dataInicioConcretizacao;
                conc.dataFimConcretizacao = concretizacao.dataFimConcretizacao;
                conc.local = concretizacao.local;
                conc.AtividadeFK = concretizacao.AtividadeFK;

            }
            else
            {
                ViewBag.AtividadeFK = new SelectList(db.Atividades, "ID", "Nome", concretizacao.AtividadeFK);
                ViewBag.ListaObjetosDeFunc = db.Funcionarios.OrderBy(f => f.Nome).ToList();
                return View(concretizacao);
            }
            // tentar fazer o UPDATE
            if (TryUpdateModel(conc, "", new string[] { nameof(conc.dataInicioConcretizacao), nameof(conc.dataFimConcretizacao), nameof(conc.local), nameof(conc.AtividadeFK), nameof(conc.ListaDeObjetosDeFuncionario) }))
            {
                var elementosDeFunc = db.Funcionarios.ToList();

                if (opcoesEscolhidasDeFunc != null)
                {
                    // se existirem opções escolhidas, vamos associá-las
                    foreach (var ff in elementosDeFunc)
                    {
                        if (opcoesEscolhidasDeFunc.Contains(ff.ID.ToString()))
                        {
                            // se uma opção escolhida ainda não está associada, cria-se a associação
                            if (!conc.ListaDeObjetosDeFuncionario.Contains(ff))
                            {
                                conc.ListaDeObjetosDeFuncionario.Add(ff);
                            }
                        }
                        else
                        {
                            // caso exista associação para uma opção que não foi escolhida, 
                            // remove-se essa associação
                            conc.ListaDeObjetosDeFuncionario.Remove(ff);
                        }
                    }
                }
                else
                {
                    // não existem opções escolhidas!
                    // vamos eliminar todas as associações
                    foreach (var ff in elementosDeFunc)
                    {
                        if (conc.ListaDeObjetosDeFuncionario.Contains(ff))
                        {
                            conc.ListaDeObjetosDeFuncionario.Remove(ff);
                        }
                    }
                }
            }

            // guardar as alterações
            db.SaveChanges();

            // devolver controlo à View
            return RedirectToAction("Index");

        }

        // GET: Concretizacaos/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
