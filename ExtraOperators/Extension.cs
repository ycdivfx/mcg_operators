using NoiseExtension;
using ViperEngine;

namespace MCG.ExtraOperators
{
    public class Extension : IViperExtension
    {
        public void RegisterOps()
        {
            ViperOpExtensions.RegisterWithDescription(typeof(RandomOps));
            ViperOpExtensions.RegisterWithDescription(typeof(MeshOps));
        }
    }
}