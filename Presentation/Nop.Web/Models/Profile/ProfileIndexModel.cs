using Nop.Web.Models.Customer;
namespace Nop.Web.Models.Profile
{
    public partial class ProfileIndexModel
    {
        public int CustomerProfileId { get; set; }
        public string ProfileTitle { get; set; }
        public int PostsPage { get; set; }
        public bool PagingPosts { get; set; }
        public bool ForumsEnabled { get; set; }
        public CustomerNavigationModel NavigationModel { get; set; }
        public ProfileIndexModel()
        {
            NavigationModel = new CustomerNavigationModel();
        }
    }
}