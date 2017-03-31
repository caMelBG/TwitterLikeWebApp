namespace Twitter.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Models;

    public class TwitterData : ITwitterData, IDisposable
    {
        private readonly DbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public TwitterData(DbContext context)
        {
            this.context = context;
        }
        
        public DbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IEfRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IEfRepository<Tweet> Tweets
        {
            get
            {
                return this.GetRepository<Tweet>();
            }
        }

        public IEfRepository<Notification> Notifications
        {
            get
            {
                return this.GetRepository<Notification>();
            }
        }

        public IEfRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public IEfRepository<Image> Images
        {
            get
            {
                return this.GetRepository<Image>();
            }
        }

       public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IEfRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IEfRepository<T>)this.repositories[typeof(T)];
        }
    }
}
