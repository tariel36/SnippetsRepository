// The MIT License (MIT)
//
// Copyright (c) 2022 tariel36
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using NutaDev.CsLib.IO.Serialization.Abstract;
using System;
using System.IO;

namespace NutaDev.CsLib.IO.Serialization.Specific
{
    /// <summary>
    /// Basic XML serializer that serializes to file and deserializes from file.
    /// </summary>
    public class XmlSerializer
    {
        /// <summary>
        /// Serializes <paramref name="obj"/> to XML string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>XML string or null.</returns>
        public static string ToString(object obj)
        {
            if (obj == null) { return null; }

            using (MemoryStream stream = new MemoryStream())
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    CreateSerializer(obj.GetType()).Serialize(stream, obj);

                    stream.Position = 0;

                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Serializes <paramref name="obj"/> to byte array.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Byte array or null.</returns>
        public static byte[] ToBytes(object obj)
        {
            if (obj == null) { return null; }

            using (MemoryStream stream = new MemoryStream())
            {
                CreateSerializer(obj.GetType()).Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes <paramref name="str"/> to object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="str">XML string.</param>
        /// <returns>Object instance or default value.</returns>
        public static T Deserialize<T>(string str)
        {
            using (StringReader reader = new StringReader(str))
            {
                object result = CreateSerializer<T>().Deserialize(reader);

                return result is T
                        ? (T)result
                        : default(T)
                    ;
            }
        }

        /// <summary>
        /// Deserializes <paramref name="bytes"/> to object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="bytes">XML string.</param>
        /// <returns>Object instance or default value.</returns>
        public static T Deserialize<T>(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                object result = CreateSerializer<T>().Deserialize(stream);

                return result is T
                        ? (T)result
                        : default(T)
                    ;
            }
        }

        /// <summary>
        /// Write object to file.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="path">File path.</param>
        public static void Write<T>(T obj, string path)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                CreateSerializer<T>().Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Read XML from file and deserializes it.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="path">File path.</param>
        /// <returns>Deserialized object or default value.</returns>
        public static T Read<T>(string path)
        {
            T result = default(T);

            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    CreateSerializer<T>().Deserialize(streamReader.BaseStream);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates new <see cref="System.Xml.Serialization.XmlSerializer"/> instance.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns><see cref="System.Xml.Serialization.XmlSerializer"/> instance.</returns>
        private static System.Xml.Serialization.XmlSerializer CreateSerializer<T>()
        {
            return CreateSerializer(typeof(T));
        }

        /// <summary>
        /// Creates new <see cref="System.Xml.Serialization.XmlSerializer"/> instance.
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns><see cref="System.Xml.Serialization.XmlSerializer"/> instance.</returns>
        private static System.Xml.Serialization.XmlSerializer CreateSerializer(Type type)
        {
            return new System.Xml.Serialization.XmlSerializer(type);
        }
    }

    /// <summary>
    /// XML serializer for specific object type that writes to file on serialize
    /// and reads from file on deserialize.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public class XmlSerializer<T>
        : IStringSerializer<T>
        , IBytesSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializer{T}"/> class.
        /// </summary>
        public XmlSerializer()
        {
            Type = typeof(T);
            Serializer = new System.Xml.Serialization.XmlSerializer(Type);
        }

        /// <summary>
        /// Object type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Serializer instance.
        /// </summary>
        public System.Xml.Serialization.XmlSerializer Serializer { get; }

        /// <summary>
        /// Serializes <paramref name="obj"/> to XML string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>XML string or null.</returns>
        public string ToString(object obj)
        {
            if (obj == null) { return null; }

            using (MemoryStream stream = new MemoryStream())
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    Serializer.Serialize(stream, obj);

                    stream.Position = 0;

                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Serializes <paramref name="obj"/> to byte array.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Byte array or null.</returns>
        public byte[] ToBytes(object obj)
        {
            if (obj == null) { return null; }

            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes <paramref name="str"/> to object.
        /// </summary>
        /// <param name="str">XML string.</param>
        /// <returns>Object instance or default value.</returns>
        public T Deserialize(string str)
        {
            using (StringReader reader = new StringReader(str))
            {
                object result = Serializer.Deserialize(reader);

                return result is T
                        ? (T)result
                        : default(T)
                    ;
            }
        }

        /// <summary>
        /// Deserializes <paramref name="bytes"/> to object.
        /// </summary>
        /// <param name="bytes">XML string.</param>
        /// <returns>Object instance or default value.</returns>
        public T Deserialize(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                object result = Serializer.Deserialize(stream);

                return result is T
                        ? (T)result
                        : default(T)
                    ;
            }
        }
    }
}
