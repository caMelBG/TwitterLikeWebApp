namespace Twitter.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Infrastructure;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<TwitterDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TwitterDbContext context)
        {
            try
            {
                if (!context.Users.Any())
                {
                    var userManager = new UserManager<User>(new UserStore<User>(context));
                    var importer = new DataImporter(userManager, context);
                    importer.UploadImages();
                    importer.ImportUsers();
                    importer.MakeRelationBetweenUsers();
                    importer.ImportTweets();
                    importer.ImportFavouriteTweets();
                    base.Seed(context);
                }
            }
            catch (System.Exception e)
            {
                var inner = e.InnerException;
                throw;
            }

        }
    }
}
