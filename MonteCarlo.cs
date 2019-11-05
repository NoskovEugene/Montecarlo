using System.Runtime.CompilerServices;
using System.Dynamic;
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
                Logger.Info($"S:({this.Right} - {this.Left}) * {this.Top} {this.S}");
                Logger.Info($"Maximum function founded. Is {max}");

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }


        public void StartEmulate(double N)
        {
            Random rnd = new Random();
            Logger.Info($"Starting simulate with {N} steps");
            var l = DateTime.Now;
            var nInput = 0;
            for (int i = 0; i < N; i++)
            {
                var generatedPoint = GeneratePoint(rnd);
                var calcPoint = new Point(){X = generatedPoint.X, Y = Calculator.Calc(ExpElements,generatedPoint.X)};
                if(ComparePoint(generatedPoint,calcPoint)){
                    nInput++;
                }
            }
            var span = DateTime.Now - l;
            Logger.Info($"Time spent: {span}");
            Logger.Info($"In points: {nInput}");
            Logger.Info($"All points: {N}");
            Logger.Info($"Percentage of hit points: {(nInput / N)*100}%");
            double Integral = (nInput / N) * this.S;
            Logger.Info($"Integral: {Integral}");

        }

        public bool ComparePoint(Point generatedPoint,Point CalcedPoint)
        {
            if(generatedPoint.Y > 0 & generatedPoint.Y < CalcedPoint.Y){
                return true;
            } else {
                return false;
            }
        }

        private Point GeneratePoint(Random rnd){
            double y = (Top) * rnd.NextDouble();
            double x = ((Right - Left) * rnd.NextDouble()) + Left;
            return new Point(){X = x, Y = y};
        }
    }
}