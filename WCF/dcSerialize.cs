using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Xml;

namespace WCF
{
    public class dcSerialize
    {
        public void SerializeToFile<T>(T obj, string encoding, string path, bool Indented) where T : class
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (var xmlw = new XmlTextWriter(fs, Encoding.GetEncoding(encoding)))
                {
                    xmlw.Formatting = Indented == true ? Formatting.Indented : Formatting.None;
                    serializer.WriteObject(xmlw, obj);
                    xmlw.Flush();
                }
            }
        }

        public T DeserializeFromFile<T>(string path, string encoding) where T : class
        {
            T obj = Activator.CreateInstance<T>();
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (XmlTextReader xmlr = new XmlTextReader(fs))
                {
                    obj = (T)serializer.ReadObject(xmlr);
                }
            }
            return obj;
        }
    }
}