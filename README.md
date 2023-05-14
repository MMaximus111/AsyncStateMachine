
# AsyncStateMachine 🛰
### Custom lower C# code to which async/await keywords are converted⚙️🛠

Initial asynchronous code example:
```csharp

using System;
using System.Threading.Tasks;

await WorkAsync();

Console.WriteLine($"Main finished in thread {Environment.CurrentManagedThreadId}");

Console.ReadKey();

internal static async Task WorkAsync()
{
    Console.WriteLine($"Work started in thread {Environment.CurrentManagedThreadId}");
    
     int delay = await GetDelayAsync();
    
    await Task.Delay(delay);

    Console.WriteLine($"Work finished in thread {Environment.CurrentManagedThreadId}");
}

internal static Task<int> GetDelayAsync()
{
  Console.WriteLine($"GetDelay started in thread {Environment.CurrentManagedThreadId}");

  const int delay = 1000;
  
  await Task.Delay(delay);
  
  Console.WriteLine($"GetDelay finished in thread {Environment.CurrentManagedThreadId}");
  
  return delay;
}
```

Some facts 🤹:
1.  In fact, `static void Main()` method remained under the hood, and program execution begins with it.
2.  Each asynchronous method, in fact, under the hood is a state machine (a separate class).
3. The code generator usually names variables with unreadable names.





