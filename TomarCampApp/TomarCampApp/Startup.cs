using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using System.Collections.Generic;
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

            // criar a Role 'Admin'
            if (!roleManager.RoleExists("Admin"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // criar a Role 'Func'
            if (!roleManager.RoleExists("Func"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Func";
                roleManager.Create(role);
            }

            // criar a Role 'Pai'
            if (!roleManager.RoleExists("Pai"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Pai";
                roleManager.Create(role);
            }

            // criar um utilizador 'Funcionario'
            var user1 = new ApplicationUser();
            user1.UserName = "isabelrodrigues@mail.pt";
            user1.Email = "isabelrodrigues@mail.pt";
            
            string userPWD = "F@irytail4ever";
            var chkUser = userManager.Create(user1, userPWD);

            //Adicionar o Utilizador à respetiva Role-Func-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user1.Id, "Func");
            }




            // criar um utilizador 'Funcionario'
            var user2 = new ApplicationUser();
            user2.UserName = "joanarodrigues@mail.pt";
            user2.Email = "joanarodrigues@mail.pt";

            string userPWD2 = "F@irytail4ever";
            var chkUser2 = userManager.Create(user2, userPWD2);

            //Adicionar o Utilizador à respetiva Role-Func-
            if (chkUser2.Succeeded)
            {
                var result1 = userManager.AddToRole(user2.Id, "Func");
            }

            // criar um utilizador 'Funcionario'
            var user3 = new ApplicationUser();
            user3.UserName = "teresarodrigues@mail.pt";
            user3.Email = "teresarodrigues@mail.pt";

            string userPWD3 = "F@irytail4ever";
            var chkUser3 = userManager.Create(user3, userPWD3);

            //Adicionar o Utilizador à respetiva Role-Func-
            if (chkUser3.Succeeded)
            {
                var result1 = userManager.AddToRole(user3.Id, "Func");
            }

        }
    }
}
