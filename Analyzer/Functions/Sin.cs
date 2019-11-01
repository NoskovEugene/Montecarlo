using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Analyzer.Classes;

namespace Analyzer.Functions
{
    [Serializable]
    class Sin : ClassFunction
    {
        public override string Name => "SIN";

        public override List<IElement> Context { get; set; }

        public override double Value { get; set; }

        public override int Priority => 4;

        public override double Invoke(double X = double.NaN, double b = 0)
        {
            this.Value = Math.Sin(base.Analyse(X));
            return this.Value;
        }
    }
}
