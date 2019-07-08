using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomarCampApp.Models
{
    public class PlanoDeAtividades
    {
        public PlanoDeAtividades()
        {
            ListaDeObjetosDeConcretizacao = new HashSet<Concretizacao>();
            ListaDeObjetosDeCriancas = new HashSet<Criancas>();

        }

        public int ID { get; set; }

        public string Turno { get; set; }

        public DateTime dataInicioPA { get; set; }

        public DateTime dataFimPA { get; set; }

        public virtual ICollection<Concretizacao> ListaDeObjetosDeConcretizacao { get; set; }
        public virtual ICollection<Criancas> ListaDeObjetosDeCriancas { get; set; }
    }
}