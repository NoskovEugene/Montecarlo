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
                Logger.Info($"F(1) = {Calculator.Calc(ExpElements,1)}");
                Logger.Info($"F(2) = {Calculator.Calc(ExpElements,2)}");
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
            for (int i = 0; i < nInput; i++)
            {
                Random rnd = new Random();
                double X = rnd.NextDouble();
                double FResult = Calculator.Calc(ExpElements,X);
            }
        }

    }
}