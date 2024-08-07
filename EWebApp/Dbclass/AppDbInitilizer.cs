using EWebApp.Data.Static;
using EWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EWebApp.Dbclass
{
    public class AppDbInitilizer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                if(!context.Products.Any())
                {
                    context.Products.AddRangeAsync(new List<Products>()
                    {
                        new Products()
                        {
                            Name = "Refrigerator",
                            Description = "This is a Refrigerator",
                            Price = 50.99,
                            ProduceCategory = Data.ProductCategory.Appliances,
                            Photo = "" 
                        },

                        new Products()
                        {
                            Name = "Pot",
                            Description = "This is a Pot",
                            Price = 88.99,
                            ProduceCategory = Data.ProductCategory.Kitchen,
                            Photo = ""
                        },
                        new Products()
                        {
                            Name = "Wrist Watch",
                            Description = "This is a Wrist Watch",
                            Price = 100.99,
                            ProduceCategory = Data.ProductCategory.Clothing,
                            Photo = ""
                        },
                        new Products()
                        {
                            Name = "Chair",
                            Description = "This is a Chair",
                            Price = 120.99,
                            ProduceCategory = Data.ProductCategory.Furniture,
                            Photo = ""
                        },
                        new Products()
                        {
                            Name = "Washing Machine",
                            Description = "This is a Washing Machine",
                            Price = 155.99,
                            ProduceCategory = Data.ProductCategory.Appliances,
                            Photo = ""
                        }

                        
                    });

                    context.SaveChangesAsync();
                    
                }

                
                   
                
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope() )
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //Roles
                if(!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminUserEmail = "admin@ewebapp.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                

                string appUserEmail = "user@ewebapp.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = " Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }



            }
        }

    }

}
    

