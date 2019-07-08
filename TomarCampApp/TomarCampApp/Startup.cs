using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using TomarCampApp.Models;

namespace TomarCampApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            iniciaAplicacao();
        }

        private void iniciaAplicacao()
        {

            // identifica a base de dados de serviço à aplicação
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Agente'
            if (!roleManager.RoleExists("Admin"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // criar a Role 'GestorMultas'
            if (!roleManager.RoleExists("Func"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Func";
                roleManager.Create(role);
            }

            // criar a Role 'RecursosHumanos'
            if (!roleManager.RoleExists("Pai"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Pai";
                roleManager.Create(role);
            }

            // criar um utilizador 'Funcionario'
            var user = new ApplicationUser();
            user.UserName = "isabelrodrigues@mail.pt";
            user.Email = "isabelrodrigues@mail.pt";
            
            string userPWD = "F@irytail4ever";
            var chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Func-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Func");
            }

        }
    }
}
