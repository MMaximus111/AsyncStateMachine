
# AsyncStateMachine 🛰
### Custom lower C# code to which async/await keywords are converted⚙️🛠

Initial asynchronous code example:
```csharp

using System;
using System.Threading.Tasks;

await WorkAsync();

Console.WriteLine($"Main finished in thread {Environment.CurrentManagedThreadId}");

Console.ReadKey();

static async Task WorkAsync()
{
    Console.WriteLine($"Work started in thread {Environment.CurrentManagedThreadId}");

    await Task.Delay(1000);

    Console.WriteLine($"Work finished {Environment.CurrentManagedThreadId}");
}
```

Some facts 🤹:
1.  In fact, `static void Main()` method remained under the hood, and program execution begins with it.
2.  Each asynchronous method, in fact, under the hood is a state machine (a separate class).
3. The code generator usually names variables with unreadable names.





