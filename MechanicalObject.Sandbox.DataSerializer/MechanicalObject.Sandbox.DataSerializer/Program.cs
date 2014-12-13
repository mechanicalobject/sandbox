using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalObject.Sandbox.DataSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            //var serializer = new DataContractSerializer(typeof(Person));
            //var jesus = GetPerson("Jesus", new DateTime(1, 1, 1, 0, 0, 0));

            var serializer = new DataContractJsonSerializer(typeof(Person));
            var jesus = GetPersonWithUtcBirthday("Jesus", new DateTime(1, 1, 1, 0, 0, 0));
            
            Person deserializedJesus;
            using (var memoryStream = new MemoryStream())
            {
                Serialize(serializer, memoryStream, jesus);
                deserializedJesus = Deserialize(serializer, memoryStream);
            }
            Console.WriteLine(deserializedJesus);
        }

        static void Serialize(XmlObjectSerializer serializer, MemoryStream memoryStream, Person personToSerialize)
        {
            serializer.WriteObject(memoryStream, personToSerialize);
            memoryStream.Position = 0;
        }

        static Person Deserialize(XmlObjectSerializer serializer, MemoryStream memoryStream)
        {
            var deserializedPerson = serializer.ReadObject(memoryStream) as Person;
            return deserializedPerson;
        }

        static Person GetPersonWithUtcBirthday(string name, DateTime birthDay)
        {
            return GetPerson(name, DateTime.SpecifyKind(birthDay, DateTimeKind.Utc));
        }

        static Person GetPerson(string name, DateTime birthday)
        {
            var person = new Person
            {
                Name = name,
                Birthday = birthday
            };
            return person;
        }
    }
}
