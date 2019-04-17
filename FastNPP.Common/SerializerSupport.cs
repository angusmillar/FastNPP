using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FastNPP.Common
{
  public static class SerializerSupport
  {
    public static void SerializeTo(string FilePath, object Item, Type ItemType)
    {    
      XmlSerializer serializer = new XmlSerializer(ItemType);
      using (TextWriter writer = new StreamWriter(FilePath))
      {
        serializer.Serialize(writer, Item);
      }
    }

    public static T DeserializeFrom<T>(string FilePath) where T : class
    {
      if (File.Exists(FilePath))
      {
        XmlSerializer deserializer = new XmlSerializer(typeof(T));
        TextReader reader = new StreamReader(FilePath);
        object obj = deserializer.Deserialize(reader);
        T Item = (T)obj;
        reader.Close();
        return Item;
      }
      else
      {
        return null;
      }
    }
  }
}
