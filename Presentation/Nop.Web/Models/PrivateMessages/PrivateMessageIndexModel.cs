
using Nop.Web.Models.Customer;
namespace Nop.Web.Models.PrivateMessages
{
    public partial class PrivateMessageIndexModel
    {
        public PrivateMessageIndexModel()
        {
            NavigationModel = new CustomerNavigationModel();
        }
        public int InboxPage { get; set; }
        public int SentItemsPage { get; set; }
        public bool SentItemsTabSelected { get; set; }
        public CustomerNavigationModel NavigationModel { get; set; }
    }
}