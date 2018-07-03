using System;
using ViperEngine;

namespace CurlNoiseExtension
{
    public class Extension : IViperExtension
    {
        public void RegisterOps()
        {
            ViperOp.RegisterStaticMethods(typeof(Random.CurlNoise));
        }
    }
}