using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TomarCampApp.Models
{
    public class Concretizacao
    {
        public Concretizacao()
        {
            ListaDeObjetosDeFuncionario = new HashSet<Funcionarios>();
            ListaDeObjetosDePlanoDeAtividades = new HashSet<PlanoDeAtividades>();

        }

        public int ID { get; set; }

        public DateTime dataInicioConcretizacao { get; set; }

        public DateTime dataFimConcretizacao { get; set; }

        public string local { get; set; }


        [ForeignKey("Atividade")]
        public int AtividadeFK { get; set; }
        public virtual Atividades Atividade { get; set; }

        public virtual ICollection<Funcionarios> ListaDeObjetosDeFuncionario { get; set; }

        public virtual ICollection<PlanoDeAtividades> ListaDeObjetosDePlanoDeAtividades { get; set; }
    }
}