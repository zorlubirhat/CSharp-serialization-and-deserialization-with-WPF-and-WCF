using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name +
    // + "IItemService" in both code and config file together.
    [ServiceContract]
    public interface IItemService
    {
        [OperationContract]
        ArrayList getFiles(string filename);

        [OperationContract]
        List<ItemCatalog.Items> getItems(string bodyFile);

        [OperationContract]
        void createNewItem(string xml, ItemCatalog.ItemCatalog catalog, ItemCatalog.Items newbook);

        [OperationContract]
        void editItem(string xml, ItemCatalog.ItemCatalog catalog, ItemCatalog.Items book, int index);
    }
}