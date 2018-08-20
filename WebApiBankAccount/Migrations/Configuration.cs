namespace WebApiBankAccount.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<WebApiBankAccount.Models.WebApiBankAccountContext>
    {
        #region Constructor

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebApiBankAccount.Models.WebApiBankAccountContext";
        }

        #endregion

        #region Protected Override Methods

        protected override void Seed(WebApiBankAccount.Models.WebApiBankAccountContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        #endregion
    }
}
