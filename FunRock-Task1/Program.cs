using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunRock_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome :D please enter the equation for evaluation ...");
            string equation = Console.ReadLine();

            EquationCalculator equationCalculator = new EquationCalculator();
            equationCalculator.CalculateEquation(equation);
      
            Console.ReadKey();
        } // main
    }
}
