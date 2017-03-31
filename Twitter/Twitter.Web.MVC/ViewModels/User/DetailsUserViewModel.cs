namespace Twitter.Web.MVC.ViewModels
{
    using System.Collections.Generic;

    using AutoMapper;

    using Models;
    using Infrastructures.Mapping;

    public class DetailsUserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int? AvatarId { get; set; }

        public virtual Image Avatar { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }
        
        public virtual ICollection<Tweet> FavouriteTweets { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }
        
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, DetailsUserViewModel>()
                .ForMember(u => u.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}
