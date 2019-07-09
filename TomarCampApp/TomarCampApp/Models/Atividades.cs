﻿using System;
using System.Collections.Generic;
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
        public string Nome { get; set; }

        public DateTime dataCriacao { get; set; }

        public string materiais { get; set; }

        public string descricao { get; set; }


        public virtual ICollection<Concretizacao> ListaDeObjetosDeConcretizacao { get; set; }

        
    }
}