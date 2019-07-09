using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TomarCampApp.Models
{
    public class Pais
    {

        public Pais()
        {
            ListaDeObjetosDeCriancas = new HashSet<Criancas>();


        }

        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{1,}[ ][A-Z][a-z]{1,}$",
        ErrorMessage = "Apenas primeiro e ultimo nomes")]
        public string Nome { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]{0,1})$",
        ErrorMessage = "Apenas primeiro e ultimo nomes")]
        public int Idade { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]{7,})$",
         ErrorMessage = "Characters are not allowed.")]
        public string NumCC { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]{8,})$",
         ErrorMessage = "Characters are not allowed.")]
        public string NIF { get; set; }
        [Required]
        [RegularExpression(@"^[9][1236][0-9]{7}$",
         ErrorMessage = "Número Inválido")]
        public string Telemovel { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public virtual ICollection<Criancas> ListaDeObjetosDeCriancas { get; set; }

    }
}