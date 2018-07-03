using System;

namespace NoiseExtension
{
    public class DisplayNameAttribute : Attribute
    {
        public string Name { get; set; }

        public DisplayNameAttribute(string name)
        {
            Name = name;
        }
    }
}