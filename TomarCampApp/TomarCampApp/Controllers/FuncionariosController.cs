using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TomarCampApp.Models;

namespace TomarCampApp.Controllers
{
    
    public class FuncionariosController : Controller
    {
        //Criar Var que representa a BD
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Funcionarios
        public ActionResult Index()
        {
            //procura a totalidade dos agentes na BD
            //Instrução feita em LINQ
            //SELECT * FROM Agentes ORDER BY nome
            var listaFuncionarios = db.Funcionarios.OrderBy(a => a.Nome).ToList();
            return View(listaFuncionarios);
        }

        /// <summary>
        /// Mostra os dados de um Agente
        /// </summary>
        /// <param name="id">identifica o Funcionario</param>
        /// <returns>devolve a View com os dados</returns>
        // GET: Funcionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //este erro ocorre porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");

            }
            // SELECT * FROM Agentes WHERE Id=id
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            //O funcionario foi encontrado?
            if (funcionarios == null)
            {
                //O Funcionario não foi encontrado, porque o utilizador está a fazer asneira
                return RedirectToAction("Index");
            }
            return View(funcionarios);
        }
        [Authorize(Roles = "Admin")]
        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.


        /// <summary>
        /// criação de um novo funcionário
        /// </summary>
        /// <param name="funcionarios">recolhe os dados do Nome do Funcionário</param>
        /// <param name="fotografia">representa a fotografia que identifica o Funcionário</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Nome")] Funcionarios funcionarios, HttpPostedFileBase fotografia)
        {

            /// precisamos de processar a fotografia
            /// 1º será que foi fornecido um ficheiro
            /// 2º será do tipo correto 
            /// 3º se for do tipo correto, guarda-se
            /// senão, atribui-se um ' avatar generico' ao utilizador

            //var auxiliar
            string caminho = "";
            bool haFicheiro = false;

            //há ficheiro?
            if (fotografia == null)
            {
                //não há ficheiro, atribui-se-lhe o avatar
                funcionarios.Foto = "nouser.png";
            }
            else
            {
                if (fotografia.ContentType == "image/jpeg" || fotografia.ContentType == "image/png")
                {
                    string extensao = Path.GetExtension(fotografia.FileName).ToLower();
                    Guid g;
                    g = Guid.NewGuid();
                    string nome = g.ToString() + extensao;

                    caminho = Path.Combine(Server.MapPath("~/imagens"), nome);
                    //atribuir ao agente o nome do ficheiro
                    funcionarios.Foto = nome;
                    //assinalar q ha foto
                    haFicheiro = true;
                }
            }


            if (ModelState.IsValid) //valida os dados fornecidos estão de acordo com as regras definidas no modelo 
            {
                try
                {
                    db.Funcionarios.Add(funcionarios);
                    db.SaveChanges();
                    //vou guardar o ficheiro no disco rigido
                    if (haFicheiro) fotografia.SaveAs(caminho);
                    //redireciona o utilizador para a página do INDEX
                    return RedirectToAction("Index");
                }catch (Exception)
                {
                    ModelState.AddModelError("", "Ocurreu um erro com a escrita dos dados do novo funcionário");
                }
            }

            return View(funcionarios);
        }

        [Authorize(Roles = "Admin, Func")]
        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //este erro ocorre porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            if (funcionarios == null)
            {
                //o Funcionario nao foi encontrado porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Func")]
        public ActionResult Edit([Bind(Include = "ID,Nome,Foto")] Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionarios);
        }

        [Authorize(Roles = "Admin")]
        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //este erro ocorre porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            if (funcionarios == null)
            {
                //o Funcionario nao foi encontrado porque o utilizador anda a fazer asneiras
                return RedirectToAction("Index");
            }
            // O Funcionario foi encontrado 
            // vou salvaguardar os dados para posterior validação
            // - guardar o ID do Funcionario num Cookie cifrado
            // - guardar o ID numa variavel de sessão
            
            Session["Funcionario"] = funcionarios.ID;
            //mostra na View os dados do Agente
            return View(funcionarios);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null){

                //o utilizar está a tentar dar-me a volta ao código
                return RedirectToAction("Index");
            }
            //o ID não é null
            //será o ID o que eu espero?
            // vamos validar se o ID está correto
            if (id != (int)Session["Funcionario"])
            {
                // Caso o utilizador esteja a fazer asneiras
                return RedirectToAction("Index");
            }

            Funcionarios funcionarios = db.Funcionarios.Find(id);
            if(funcionarios == null)
            {
                //nao foi encontrado o agente
                return RedirectToAction("Index");
            }

            try
            {
                db.Funcionarios.Remove(funcionarios);
                db.SaveChanges();

            }
            catch (Exception)
            {
                //informar que houve um erro
                ModelState.AddModelError("", "Não é possivel remover o Funcionario " + funcionarios.Nome + ". Provalvemente possui concretizações associadas a ele");
                return View(funcionarios);
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
