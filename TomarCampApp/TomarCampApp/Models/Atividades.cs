using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TomarCampApp.Models
{
    public class Atividades
    {

        public Atividades()
        {
            ListaDeObjetosDeConcretizacao = new HashSet<Concretizacao>();

        }


        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{1,}[ ][A-Z][a-z]{1,}",
         ErrorMessage = "Apenas primeiro e ultimo nomes")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime dataCriacao { get; set; }

        [Display(Name = "Materiais Necessários")]
        public string materiais { get; set; }

        [Display(Name = "Descrição da actividades")]
        public string descricao { get; set; }


        public virtual ICollection<Concretizacao> ListaDeObjetosDeConcretizacao { get; set; }

        
    }
}