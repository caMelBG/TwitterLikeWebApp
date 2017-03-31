namespace Twitter.Data
{
    using System;
    using System.Data.Entity;

    using Models;

    public interface ITwitterData : IDisposable
    {
        DbContext Context { get; }

        IEfRepository<User> Users { get; }

        IEfRepository<Tweet> Tweets { get; }

        IEfRepository<Notification> Notifications { get; }

        IEfRepository<Message> Messages { get; }

        IEfRepository<Image> Images { get; }

        int SaveChanges();
    }
}
