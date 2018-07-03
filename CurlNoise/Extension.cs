using ViperEngine;

namespace NoiseExtension
{
    public class Extension : IViperExtension
    {
        public void RegisterOps()
        {
            ViperOpExtensions.RegisterWithDescription(typeof(Random));
        }
    }
}