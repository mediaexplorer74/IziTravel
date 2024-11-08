// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.XmlSerializerHelper
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Izi.Travel.Utility
{
  public static class XmlSerializerHelper
  {
    public static string Serialize<T>(T obj) where T : class
    {
      StringBuilder sb = new StringBuilder();
      using (StringWriter stringWriter = new StringWriter(sb))
      {
        new XmlSerializer(typeof (T)).Serialize((TextWriter) stringWriter, (object) obj);
        
        stringWriter.Flush();
        stringWriter.Dispose();//.Close();
      }
      return sb.ToString();
    }

    public static T Deserialize<T>(string str) where T : class
    {
      T obj;
      using (StringReader stringReader = new StringReader(str))
      {
        obj = (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) stringReader);
        stringReader.Dispose();//.Close();
      }
      return obj;
    }

    public static T Deserialize<T>(Stream stream) where T : class
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
      T obj;
      using (stream)
      {
        obj = (T) xmlSerializer.Deserialize(stream);
        
        stream.Flush(); 
        stream.Dispose();//.Close();
      }
      return obj;
    }

    public static void SerializeToIsolatedStorage<T>(T obj, string fileName)
    {
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        using (IsolatedStorageFileStream output = storeForApplication.OpenFile(fileName, FileMode.Create))
        {
          using (XmlWriter xmlWriter = XmlWriter.Create((Stream) output))
            new XmlSerializer(typeof (T)).Serialize(xmlWriter, (object) obj);
            
            output.Flush();  
            output.Dispose();//.Close();
        }
      }
    }

    public static T DeserializeFromIsolatedStorage<T>(string fileName)
    {
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (!storeForApplication.FileExists(fileName))
          return default (T);
        using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(fileName, FileMode.Open))
          return (T) new XmlSerializer(typeof (T)).Deserialize((Stream) storageFileStream);
      }
    }
  }
}
