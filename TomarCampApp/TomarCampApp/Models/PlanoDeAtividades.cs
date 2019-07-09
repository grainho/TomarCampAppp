using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [RegularExpression(@"[A-Z]{1,}$",
         ErrorMessage = "Turno Inválido")]
        public string Turno { get; set; }

        [Required]
        [Display(Name = "Data de Inicio de Plano de Actividades")]
        public DateTime dataInicioPA { get; set; }

        [Required]
        [Display(Name = "Data de fim de Plano de Actividades")]
        public DateTime dataFimPA { get; set; }

        public virtual ICollection<Concretizacao> ListaDeObjetosDeConcretizacao { get; set; }
        public virtual ICollection<Criancas> ListaDeObjetosDeCriancas { get; set; }
    }
}