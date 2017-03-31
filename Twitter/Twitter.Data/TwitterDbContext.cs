namespace Twitter.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class TwitterDbContext : IdentityDbContext<User>
    {
        public TwitterDbContext()
           : base("TwitterConnectionString", throwIfV1Schema: false)
        {
        }
        
        public virtual IDbSet<Tweet> Tweets { get; set; }

        public virtual IDbSet<Message> Messages { get; set; }
        
        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public static TwitterDbContext Create()
        {
            return new TwitterDbContext();
        }
    }
}
