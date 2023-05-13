using System.Runtime.CompilerServices;

namespace AsyncStateMachine;

internal sealed class MainAsync_StateMachine : IAsyncStateMachine
{
    public int state;
    public AsyncTaskMethodBuilder AsyncTaskMethodBuilder;
    public string[] args;

    private TaskAwaiter taskAwaiter;

    void IAsyncStateMachine.MoveNext()
    {
        int num = state;
        try
        {
            TaskAwaiter awaiter;
            if (num != 0)
            {
                awaiter = Program.WorkAsync().GetAwaiter();
                if (!awaiter.IsCompleted)
                {
                    num = (state = 0);
                    taskAwaiter = awaiter;
                    MainAsync_StateMachine stateMachine = this;
                    AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                    return;
                }
            }
            else
            {
                awaiter = taskAwaiter;
                taskAwaiter = default;
                num = (state = -1);
            }

            awaiter.GetResult();
            Console.WriteLine($"Main finished in thread {Environment.CurrentManagedThreadId}");
            Console.ReadKey();
        }
        catch (Exception exception)
        {
            state = -2;
            AsyncTaskMethodBuilder.SetException(exception);
            return;
        }

        state = -2;
        AsyncTaskMethodBuilder.SetResult();
    }

    void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
    {
        AsyncTaskMethodBuilder.SetStateMachine(stateMachine);
    }
}