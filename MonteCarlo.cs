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
        }

        public Message Init()
        {
            var message = "";
            try
            {
                message +="Starting initialization\r\n";
                message +="Analyze string expression\r\n";
                ExpElements = Analyzer.Analyze(Expression);
                message +="Converting to reverse polish notation\r\n";
                ExpElements = NotationWorker.GetNotation(ExpElements);
                message +="Testing function";
                message +=$"F(1) = {Calculator.Calc(ExpElements,this.Left)}\r\n";
                message +=$"F(2) = {Calculator.Calc(ExpElements,this.Left + 1)}\r\n";
                double max = -999999;
                for(double i = Left; i < Right;i+=0.1){
                    double f = Calculator.Calc(ExpElements,i);
                    if(f > max){
                        max = f;
                    }
                }
                this.Top = max;
                this.S = (Right - Left) * Top;
                message +=$"S:({this.Right} - {this.Left}) * {this.Top} {this.S}\r\n";
                message +=$"Maximum function founded. Is {max}";
                return new Message(TypeMessage.Message,message);
            }
            catch (Exception ex)
            {
                return new Message(TypeMessage.Error,ex.Message);
            }
        }

        public Message Emulate(string parameter)
        {
            if(double.TryParse(parameter,out double steps)){
                try
                {
                    return new Message(TypeMessage.Message,StartEmulate(steps));
                }
                catch (Exception ex)
                {
                    return new Message(TypeMessage.Error,ex.Message);
                }
            }
            else{
                return new Message(TypeMessage.Error,"Error while converting types");
            }
            
        }

        public string StartEmulate(double N)
        {
            var message = "";
            Random rnd = new Random();
            message +=$"Starting simulate with {N} steps\r\n";
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
            message +=$"Time spent: {span}\r\n";
            message +=$"In points: {nInput}\r\n";
            message +=$"All points: {N}\r\n";
            message +=$"Percentage of hit points: {(nInput / N)*100}%\r\n";
            double Integral = (nInput / N) * this.S;
            message +=$"Integral: {Integral}\r\n";
            return message;
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