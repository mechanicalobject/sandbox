using System;

namespace MechanicalObject.Sandbox.DataSerializer
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return string.Format("{0} was born on {1}", Name, Birthday.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}