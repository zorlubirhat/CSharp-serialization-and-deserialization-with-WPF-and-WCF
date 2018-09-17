using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ItemCatalog;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ItemService" 
    // in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ItemService.svc 
    // or ItemService.svc.cs at the Solution Explorer and start debugging.
    public class ItemService : IItemService
    {
        public ArrayList getFiles(string filename)
        {
            readFile rf = new readFile();
            ArrayList pathArray = new ArrayList();
            List<string> files = new List<string>();
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            if (filename.Equals("bin") || filename.Equals("App_Data") || filename.Equals("obj") 
                || filename.Equals("Properties") || filename.Equals("scripts"))
            {
                pathArray.Add("No permission for this file!");
            }
            else
            {
                string bodyFile = Path.Combine(appPath, filename);
                files.Add(bodyFile);
                rf.recursivelySearchFile(files, pathArray);
            }
            return pathArray;
        }

        public List<Items> getItems(string xml)
        {
            ItemCatalog.ItemCatalog catalog = new ItemCatalog.ItemCatalog();
            dcSerialize dcserializer = new dcSerialize();

            catalog.items = new List<Items>();

            catalog = dcserializer.DeserializeFromFile<ItemCatalog.ItemCatalog>(xml, "utf-8");

            catalog = new ItemCatalog.ItemCatalog { items = catalog.items };

            return catalog.items;
        }

        public void createNewItem(string xml, ItemCatalog.ItemCatalog catalog, ItemCatalog.Items newbook)
        {
            dcSerialize dcserializer = new dcSerialize();

            catalog.items.Add(newbook);

            catalog = new ItemCatalog.ItemCatalog { items = catalog.items };

            dcserializer.SerializeToFile(catalog, "utf-8", xml, true);
        }

        public void editItem(string xml, ItemCatalog.ItemCatalog catalog, ItemCatalog.Items item, int index)
        {

            dcSerialize dcserializer = new dcSerialize();

            catalog.items[index] = item;

            catalog = new ItemCatalog.ItemCatalog { items = catalog.items };

            dcserializer.SerializeToFile(catalog, "utf-8", xml, true);
        }
    }
}