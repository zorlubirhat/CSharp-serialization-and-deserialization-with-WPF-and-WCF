using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItemCatalog
{
    [DataContract(Name = "catalog")]
    public class ItemCatalog
    {

        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "books", Order = 1)]
        public List<Items> items { get; set; }
    }

    [DataContract(Name = "book")]
    public class Items
    {
        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "id", Order = 1)]
        public string id { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "author", Order = 2)]
        public string author { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "title", Order = 3)]
        public string title { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "genre", Order = 4)]
        public string genre { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "price", Order = 5)]
        public double price { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = true, Name = "publish_date", Order = 6)]
        public string publish_date { get; set; }

        public Items(string bookid, string bookauthor, string booktitle, string bookgenre, double bookprice, string publishdate)
        {
            this.id = bookid;
            this.author = bookauthor;
            this.title = booktitle;
            this.genre = bookgenre;
            this.price = bookprice;
            this.publish_date = publishdate;
        }
    }
} 