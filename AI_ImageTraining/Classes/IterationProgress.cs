using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ImageTraining.Classes
{
    public class IterationProgress
    {
        public int CurrentIteration { get; set; }
        public int TotalIterations { get; set; }
        public string CurrentImagePath { get; set; }
    }
}
