using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analyzer.Classes;
namespace Analyzer.Functions
{
    class LogE : ClassFunction
    {
        public override string Name => "LOG";

        public override double Value { get; set; }
        public override List<IElement> Context { get; set; }

        public override int Priority => 4;

        public override double Invoke(double X, double b = 0)
        {
            this.Value = Math.Log(base.Analyse(X));
            return this.Value;
        }
    }
}
