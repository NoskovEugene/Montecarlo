using System;
namespace Montecarlomethod{
    public class Point{
        public double X{get;set;}
        public double Y{get;set;}

        public override string ToString(){
            return $"X: {this.X}; Y: {this.Y}";
        }
    }
}