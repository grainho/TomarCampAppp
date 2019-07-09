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
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{1,}[ ][A-Z][a-z]{1,}",
         ErrorMessage = "Apenas primeiro e ultimo nomes")]       
        public string Nome { get; set; }
        [RegularExpression(@"^[1-9][0-9]{0,1})$",
         ErrorMessage = "Characters are not allowed.")]
        public int Idade { get; set; }
        [RegularExpression(@"^[1-9] [0-9]{0,1}$",
         ErrorMessage = "Characters are not allowed.")]
        public string Doencas { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]{7,})$",
         ErrorMessage = "Characters are not allowed.")]
        public string NumCC { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]{8,})$",
         ErrorMessage = "Characters are not allowed.")]
        public string NIF { get; set; }


        [ForeignKey("Pai")]
        public int PaiFK { get; set; }
        public virtual Pais Pai { get; set; }


        public virtual ICollection<PlanoDeAtividades> ListaDeObjetosDePlanoDeAtividades { get; set; }
    }
}