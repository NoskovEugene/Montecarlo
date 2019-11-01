using Analyzer;
using Analyzer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Functions
{
    class Log10 : ClassFunction
    {
        public override string Name => "LOG10";

        public override double Value { get; set; }
        public override List<IElement> Context { get; set; }

        public override int Priority => 4;

        public override double Invoke(double X, double b = 0)
        {
            this.Value = Math.Log10(base.Analyse(X));
            return this.Value;
        }
    }
}
