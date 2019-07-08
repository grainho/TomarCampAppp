namespace TomarCampApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TomarCampApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {


            //adicionar Pais
            var pais = new List<Pais> {
            new Pais {ID=1, Nome="Sofia Santos", Idade=45, NumCC="256733841", NIF="230500801", Telemovel="91663344", Email="a@aa.com"},
            new Pais {ID=2, Nome="Sofia Santos", Idade=45, NumCC="256733841", NIF="230500801", Telemovel="91663344", Email="a@aa.com"},
            new Pais {ID=3, Nome="Sofia Santos", Idade=45, NumCC="256733841", NIF="230500801", Telemovel="91663344", Email="a@aa.com"},
            new Pais {ID=4, Nome="Sofia Santos", Idade=45, NumCC="256733841", NIF="230500801", Telemovel="91663344", Email="a@aa.com"},
            new Pais {ID=5, Nome="Sofia Santos", Idade=45, NumCC="256733841", NIF="230500801", Telemovel="91663344", Email="a@aa.com"}
};
            pais.ForEach(pp => context.Pais.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();

            //adicionar criancas
            var criancas = new List<Criancas> {
            new Criancas {ID=1, Nome="Tania Vieira", Idade=6, Doencas="Asma", NumCC="270820676", NIF="230500801", PaiFK=1},
            new Criancas {ID=2, Nome="Luis Santos", Idade=8, Doencas="Alergia", NumCC="270720676", NIF="230500801", PaiFK=1},
            new Criancas {ID=3, Nome="Carlos Vieira", Idade=10, Doencas="Intolerancia", NumCC="270620676", NIF="230500801", PaiFK=2},
            new Criancas {ID=4, Nome="Jose Pais", Idade=9, Doencas="Alergia", NumCC="270520676", NIF="230500801", PaiFK=3},
            new Criancas {ID=5, Nome="Andreia Ferreira", Idade=11, Doencas="Alergia", NumCC="270420676", NIF="230500801", PaiFK=4},
            new Criancas {ID=6, Nome="Joana Pires", Idade=14, Doencas="Asma", NumCC="270320676", NIF="230500801", PaiFK=5},
            new Criancas {ID=7, Nome="Ana Silva", Idade=13, Doencas="", NumCC="270220676", NIF="230500801",PaiFK=5}
};
            criancas.ForEach(cc => context.Criancas.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            //adicionar atividades
            var atividades = new List<Atividades> {
            new Atividades {ID=1, Nome="Redes Sociais", dataCriacao=new DateTime(2019,6,30), materiais="Lorem ipsum dolor sit amet, consectetuer adipisci", descricao=" Nullam enim leo, etae, tellus. Sed odio est, auctor ac, sollicitudin in, consequat vitae, orci. Fusce id felis." },
            new Atividades {ID=2, Nome="Animagic", dataCriacao=new DateTime(2019,6,30), materiais="Lorem ipsum dolor sit amet, consectetuer adm cursus. Morbi ut mi.", descricao=" Nullam enim leo, egestas ac, sollicitudin in, consequat vitae, orci. Fusce id felis." },
            new Atividades {ID=3, Nome="Hollywood, 3, 2, 1...", dataCriacao=new DateTime(2019,6,30), materiais="Lorem ipsum dolor sit amet,  cursus. Morbi ut mi.", descricao=" Nullam enim leo, egestas id, , sollicitudin in, consequat vitae, orci. Fusce id felis." },
};
            atividades.ForEach(aa => context.Atividades.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();

            //adicionar concretizacao
            var concretizacao = new List<Concretizacao> {
            new Concretizacao {ID=1, dataInicioConcretizacao=new DateTime(2019,8,5), dataFimConcretizacao=new DateTime(2019,8,5), local="Praça da Republica", AtividadeFK=1},
            new Concretizacao {ID=2, dataInicioConcretizacao=new DateTime(2019,8,6), dataFimConcretizacao=new DateTime(2019,8,6), local="Praça da Republica", AtividadeFK=2},
            new Concretizacao {ID=3, dataInicioConcretizacao=new DateTime(2019,8,12), dataFimConcretizacao=new DateTime(2019,8,12), local="Praça da Republica", AtividadeFK=1},
            new Concretizacao {ID=4, dataInicioConcretizacao=new DateTime(2019,8,19), dataFimConcretizacao=new DateTime(2019,8,19), local="Praça da Republica", AtividadeFK=3}
};
            concretizacao.ForEach(coco => context.Concretizacao.AddOrUpdate(co => co.local, coco));
            context.SaveChanges();

            //adicionar Funcionarios
            var funcionarios = new List<Funcionarios> {
            new Funcionarios {ID=1, Nome="Isabel Rodrigues", Foto="IsabelRodrigues.jpg", ListaDeObjetosDeConcretizacao = new List<Concretizacao>{concretizacao[1]}},
            new Funcionarios {ID=2, Nome="Joana Rodrigues", Foto="JoanaRodrigues.jpg", ListaDeObjetosDeConcretizacao = new List<Concretizacao>{concretizacao[2]}},
            new Funcionarios {ID=3, Nome="Teresa Rodrigues", Foto="TeresaRodrigues.jpg", ListaDeObjetosDeConcretizacao = new List<Concretizacao>{concretizacao[3]}}
};
            funcionarios.ForEach(ff => context.Funcionarios.AddOrUpdate(f => f.Nome, ff));
            context.SaveChanges();
            

            //adicionar planoDeAtividades
            var planoDeAtividades = new List<PlanoDeAtividades> {
            new PlanoDeAtividades {ID=1, Turno="A", dataInicioPA=new DateTime(2019,8,5), dataFimPA=new DateTime(2019,8,9), ListaDeObjetosDeConcretizacao = new List<Concretizacao>{concretizacao[0], concretizacao[1]}, ListaDeObjetosDeCriancas = new List<Criancas>{criancas[0], criancas[1], criancas[5], criancas[6]}},
            new PlanoDeAtividades {ID=2, Turno="B", dataInicioPA=new DateTime(2019,8,12), dataFimPA=new DateTime(2019,8,16), ListaDeObjetosDeConcretizacao = new List<Concretizacao>{concretizacao[2]}, ListaDeObjetosDeCriancas = new List<Criancas>{criancas[0], criancas[1], criancas[2], criancas[6]}},
            new PlanoDeAtividades {ID=3, Turno="C", dataInicioPA=new DateTime(2019,8,19), dataFimPA=new DateTime(2019,8,23), ListaDeObjetosDeConcretizacao = new List<Concretizacao>{concretizacao[3]}, ListaDeObjetosDeCriancas = new List<Criancas>{criancas[2], criancas[3], criancas[4], criancas[5]}}
};
            planoDeAtividades.ForEach(plpl => context.PlanoDeAtividades.AddOrUpdate(pl => pl.Turno, plpl));
            context.SaveChanges();

            

        }
    }
}
