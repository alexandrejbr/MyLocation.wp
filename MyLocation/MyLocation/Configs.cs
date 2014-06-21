using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace MyLocation
{
    [DataContract]
    public class Configs
    {
        private const string ConfigurationsFile = "Configurations.dat";
        [DataMember]
        public bool CanAccessLocationData { get; set; }

        public static void PersistData(Configs configs)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                XmlSerializer xs = new XmlSerializer(typeof(Configs));

                MemoryStream ms = new MemoryStream();

                xs.Serialize(ms, configs);

                ms.Seek(0, SeekOrigin.Begin);
                using (IsolatedStorageFileStream fileStream = isf.CreateFile(ConfigurationsFile))
                {
                    using (ms)
                    {
                        ms.WriteTo(fileStream);
                    }
                }
            }
        }

        public static Configs LoadPersistedData()
        {
            Configs saved = null;

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(ConfigurationsFile,
                    System.IO.FileMode.OpenOrCreate, isf))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Configs));
                    if (stream.Length > 0)
                    {
                        saved = (Configs)serializer.Deserialize(stream);
                    }
                }
            }
            return saved;
        }
    }
}
