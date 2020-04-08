using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tinyOCREngine
{
    public class Serializer
    {
        public static T DeserializeXml<T>(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = File.Open(filename, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        public static void SerializeXml<T>(string filename, T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = File.Open(filename, FileMode.Create))
            {
                serializer.Serialize(stream, obj);
            }
        }

        public static T DeserializeBinary<T>(string fileName)
        {
            using (var ms = File.OpenRead(fileName))
            {
                var formatter = new BinaryFormatter();
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        public static void SerializeBinary(string fileName, object obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                File.WriteAllBytes(fileName, ms.ToArray());
            }
        }
    }
}
