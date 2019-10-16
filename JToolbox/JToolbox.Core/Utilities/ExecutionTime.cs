using System;
using System.Diagnostics;

namespace JToolbox.Core.Utilities
{
    public static class ExecutionTime
    {
        public static TimeSpan Run(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                action();
            }
            finally
            {
                stopwatch.Stop();
            }
            return stopwatch.Elapsed;
        }
    }
}