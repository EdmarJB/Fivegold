namespace FiveGold.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FiveGold.Models.Mapeamento.FiveGoldContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FiveGold.Models.Mapeamento.FiveGoldContext";
        }

        protected override void Seed(FiveGold.Models.Mapeamento.FiveGoldContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
