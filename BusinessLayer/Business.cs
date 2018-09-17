using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Business
    {
        WebServiceReference.ItemServiceClient client = new WebServiceReference.ItemServiceClient();

        public List<object> returnFiles(string filename)
        {
            List<object> files = client.getFiles(filename);

            return files;
        }

        public List<ItemCatalog.Items> returnItems(string file)
        {
            List<ItemCatalog.Items> items = client.getItems(file);

            return items;
        }

        public ItemCatalog.Items returnSelectedItem(string file, int index)
        {
            ItemCatalog.Items item = returnItems(file)[index];

            return item;
        }

        public ItemCatalog.Items createNewItem(string id, string author, string title, string genre, double price, string publishdate, string filename)
        {
            ItemCatalog.Items newitem = new ItemCatalog.Items(id, author, title, genre, price, publishdate);
            ItemCatalog.ItemCatalog catalog = new ItemCatalog.ItemCatalog();

            catalog = new ItemCatalog.ItemCatalog { items = client.getItems(filename) };
            client.createNewItem(filename, catalog, newitem);

            return newitem;
        }

        public ItemCatalog.ItemCatalog returnCatalog(string file)
        {
            ItemCatalog.ItemCatalog catalog = new ItemCatalog.ItemCatalog();
            catalog = new ItemCatalog.ItemCatalog { items = client.getItems(file) };

            return catalog;
        }

        public void editItem(string file, ItemCatalog.ItemCatalog catalog, ItemCatalog.Items item, int index)
        {
            client.editItem(file, catalog, item, index);
        }
    }
}
