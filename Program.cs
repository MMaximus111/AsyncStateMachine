using System.Runtime.CompilerServices;

namespace AsyncStateMachine;

internal class Program
{
    private static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    private static Task MainAsync(string[] args)
    {
        MainAsync_StateMachine stateMachine = new MainAsync_StateMachine
        {
            AsyncTaskMethodBuilder = AsyncTaskMethodBuilder.Create(),
            args = args,
            state = -1
        };

        stateMachine.AsyncTaskMethodBuilder.Start(ref stateMachine);
        return stateMachine.AsyncTaskMethodBuilder.Task;
    }

    internal static Task WorkAsync()
    {
        WorkAsync_StateMachine stateMachine = new WorkAsync_StateMachine
        {
            AsyncTaskMethodBuilder = AsyncTaskMethodBuilder.Create(),
            State = -1
        };

        stateMachine.AsyncTaskMethodBuilder.Start(ref stateMachine);
        return stateMachine.AsyncTaskMethodBuilder.Task;
    }
}