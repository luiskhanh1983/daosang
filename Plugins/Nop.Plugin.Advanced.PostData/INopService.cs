//Contributor: Nicolas Muniere

using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace Nop.Plugin.Advanced.PostData
{

    [ServiceContract]
    public interface INopService
    {
        [OperationContract]
        void CreateContentFileExample(string contentfile);
        [OperationContract]
        void CreateCategory(string name, string description);
        [OperationContract]
        void InsertCustomerLead(string source, string firstName, string lastName, string email, string ip);

    }
}
