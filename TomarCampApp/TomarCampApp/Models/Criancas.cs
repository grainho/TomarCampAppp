using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TomarCampApp.Models
{
    public class Criancas
    {

        public Criancas()
        {
            ListaDeObjetosDePlanoDeAtividades = new HashSet<PlanoDeAtividades>();


        }

        public int ID { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public string Doencas { get; set; }

        public string NumCC { get; set; }

        public string NIF { get; set; }


        [ForeignKey("Pai")]
        public int PaiFK { get; set; }
        public virtual Pais Pai { get; set; }


        public virtual ICollection<PlanoDeAtividades> ListaDeObjetosDePlanoDeAtividades { get; set; }
    }
}