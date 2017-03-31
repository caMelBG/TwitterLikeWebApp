namespace Twitter.Web.MVC.ViewModels
{
    using AutoMapper;

    using Twitter.Web.MVC.Infrastructures.Mapping;

    public class UserViewModel : IMapFrom<Models.User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }
        
        public int? AvatarId { get; set; }

        public virtual Models.Image Avatar { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Models.User, UserViewModel>()
                .ForMember(u => u.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}
