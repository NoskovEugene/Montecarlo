using System;
using System.Collections.Generic;
using Analyzer;
using PolishNotation;
namespace Montecarlomethod
{
    public class Montecarlo
    {
        protected _Analyzer Analyzer{get;}
        
        protected Logger Logger {get;}

        protected RevercePolishNotation NotationWorker{get;}

        protected Calculator.Calculator Calculator{get;}        

        public string Expression {get;set;}

        public int Left{get;set;}

        public int Right{get;set;}

        public double S{get;set;}

        public double Top{get;set;}

        public List<IElement> ExpElements{get;set;}

        public Montecarlo(string expression,int left,int right)
        {
            this.Expression = expression;
            this.Left = left;
            this.Right = right;
            this.Analyzer = new _Analyzer();
            this.NotationWorker = new RevercePolishNotation();
            this.Calculator = new Calculator.Calculator();
            this.Logger = new Logger();
            Init();
        }

        private void Init()
        {
            try
            {
                Logger.Info("Starting initialization");
                Logger.Info("Analyze string expression");
                ExpElements = Analyzer.Analyze(Expression);
                Logger.Info("Converting to reverse polish notation");
                ExpElements = NotationWorker.GetNotation(ExpElements);
                Logger.Info("Testing function");
                Logger.Info($"F(1) = {Calculator.Calc(ExpElements,this.Left)}");
                Logger.Info($"F(2) = {Calculator.Calc(ExpElements,this.Left + 1)}");
                double max = -999999;
                for(double i = Left; i < Right;i+=0.1){
                    double f = Calculator.Calc(ExpElements,i);
                    if(f > max){
                        max = f;
                    }
                }
                this.Top = max;
                this.S = (Right - Left) * Top;
                Logger.Info($"S: {this.S}");
                Logger.Info($"Maximum function founded. Is {max}");

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }


        public void StartEmulate(double N)
        {
            Logger.Info($"Starting simulate with {N} steps");
            double nInput = -1;
            for (int i = 0; i < N; i++)
            {
                Random rnd = new Random();
                var generatedPoint = GeneratePoint();
                var calcPoint = new Point(){X = generatedPoint.X, Y = Calculator.Calc(ExpElements,generatedPoint.X)};
                if(ComparePoint(generatedPoint,calcPoint)){
                    if(nInput == -1) nInput = 0;
                    nInput++;
                }
            }
            Logger.Info($"In points: {nInput}");
            Logger.Info($"All points: {N}");
            double Integral = (nInput / N)*this.S;
            Logger.Info($"Integral: {(nInput / N)*this.S}");

        }

        public bool ComparePoint(Point generatedPoint,Point CalcedPoint)
        {
            if(generatedPoint.Y < CalcedPoint.Y){
                return true;
            }
            else {
                return false;
            }
        }

        private Point GeneratePoint(){
            Random rnd = new Random();
            return new Point(){X = rnd.Next(Left,Right), Y = rnd.Next(0,(int)Top)};
        }
    }
}