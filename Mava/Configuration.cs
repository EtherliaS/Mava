using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mava
{
    internal class Configuration
    {
        public bool SilentMode { get; set; }
        public bool RandomizeTimings { get; set; }
        public Configuration() { }
        public Configuration(bool s, bool act, bool randomizeTimings = false)
        {
            RandomizeTimings = randomizeTimings;
            SilentMode = s;
        }

    }
}
