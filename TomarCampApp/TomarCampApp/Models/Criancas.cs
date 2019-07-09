using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        
        [Required]
        [Display(Name = "Idade")]
        public int Idade { get; set; }
       
        [Display(Name = "Doenças da Criança")]
        public string Doencas { get; set; }

        [Required]
        /**[RegularExpression("^[1-9][0-9]{7,}",
         ErrorMessage = "CC Invalido")]*/
        [Display(Name = "Número de Cartão de Cidadão")]
        public string NumCC { get; set; }
        [Required]
        /**[RegularExpression("^[1-9][0-9]{8,}",
         ErrorMessage = "NIF invalido")]*/
        [Display(Name = "NIF")]
        public string NIF { get; set; }


        [ForeignKey("Pai")]
        [Display(Name = "Encarregado de Educação")]
        public int PaiFK { get; set; }
        public virtual Pais Pai { get; set; }


        public virtual ICollection<PlanoDeAtividades> ListaDeObjetosDePlanoDeAtividades { get; set; }
    }
}