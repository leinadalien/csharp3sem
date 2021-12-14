using System;
using System.Threading;
using System.Diagnostics;

namespace _053501_SOK_Lab11.Lib
{
    public class MyMath
    {
        public delegate void MyMathDelegate(string msg);
        public static event MyMathDelegate MyMathEvent;
        public static double IntegralOfSin(double x1, double x2, double step)
        {
            MyMathEvent?.Invoke($"Starting calculating in thread {Thread.CurrentThread.ManagedThreadId}");
            Stopwatch sw = new();
            sw.Start();
            double result = 0;
            for(double x = x1; x <= x2; x += step)
            {
                result += Math.Sin(x) * step;
            }
            sw.Stop();
            MyMathEvent?.Invoke($"Result of thread {Thread.CurrentThread.ManagedThreadId}: {result}. Elapsed time: {sw.ElapsedMilliseconds} ms.");
            return result;
        }
    }
}
