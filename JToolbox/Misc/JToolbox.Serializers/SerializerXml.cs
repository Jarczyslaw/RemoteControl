﻿using System.IO;
using System.Xml.Serialization;

namespace JToolbox.Serializers
{
    public class SerializerXml : ISerializer
    {
        public void ToFile<T>(T obj, string filePath)
        {
            var serialized = ToString(obj);
            File.WriteAllText(filePath, serialized);
        }

        public T FromFile<T>(string filePath)
        {
            var serialized = File.ReadAllText(filePath);
            return FromString<T>(serialized);
        }

        public string ToString<T>(T val)
        {
            var s = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                s.Serialize(writer, val);
                return writer.ToString();
            }
        }

        public T FromString<T>(string input)
        {
            var ser = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }
    }
}