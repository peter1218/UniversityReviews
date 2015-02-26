namespace UniversityReview.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using UniversityReview.Models;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityReview.Models.UniRatingSystem>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false ;
        }

        protected override void Seed(UniversityReview.Models.UniRatingSystem context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Universities.AddOrUpdate(r => r.Name,
              new University { Name = "Flinders Uni", City = "Adelaide" },
              new University { Name = "Adelaide Uni", City = "Adelaide" },
              new University
              {
                  Name = "Sydney Uni",
                  City = "Sydney",
                 
                  Reviews =
                      new List<UniversityReviews> { 
                       new UniversityReviews { Rating = 9, Body="Great University", ReviewerName="peter" }
                   }
              });

            //for (int i = 0; i < 1000; i++)
            //{
            //    context.Universities.AddOrUpdate(r => r.Name,
            //        new University { Name = i.ToString(), City = "Nowhere" });
            //}

            SeedMembership();

        }

        private void SeedMembership()
        {
            if (!WebSecurity.Initialized)
            {
                WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("mia", false) == null)
            {
                membership.CreateUserAndAccount("mia", "password");
            }
            if (!roles.GetRolesForUser("mia").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "mia" }, new[] { "Admin" });
            } 

        }
    }
}
