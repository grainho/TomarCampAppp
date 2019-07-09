using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TomarCampApp.Models
{
    public class Funcionarios
    {

        public Funcionarios()
        {
            ListaDeObjetosDeConcretizacao = new HashSet<Concretizacao>();


        }

        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{1,}[ ][A-Z][a-z]{1,}",
        ErrorMessage = "Apenas primeiro e ultimo nomes")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public string Foto { get; set; }



        public virtual ICollection<Concretizacao> ListaDeObjetosDeConcretizacao { get; set; }

        
    }
}