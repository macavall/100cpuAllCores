using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int coreCount = Environment.ProcessorCount;

        Console.WriteLine($"Press Enter to stop. Utilizing {coreCount} CPU cores...");

        CancellationTokenSource cts = new CancellationTokenSource();

        // Start a CPU-intensive task on each core
        Task[] tasks = new Task[coreCount];

        for (int i = 0; i < coreCount; i++)
        {
            tasks[i] = Task.Factory.StartNew(() => Work(cts.Token), TaskCreationOptions.LongRunning);
        }

        // Wait for user to press Enter
        Console.ReadLine();

        // Stop all CPU-intensive tasks
        cts.Cancel();

        Task.WhenAll(tasks).Wait();
    }

    static void Work(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // Perform some CPU-intensive work here
        }
    }
}
