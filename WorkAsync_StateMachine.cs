using System.Runtime.CompilerServices;

namespace AsyncStateMachine;

internal sealed class WorkAsync_StateMachine : IAsyncStateMachine
{
    public int State;
    public AsyncTaskMethodBuilder AsyncTaskMethodBuilder;

    private TaskAwaiter taskAwaiter;

    void IAsyncStateMachine.MoveNext()
    {
        int num = State;
        try
        {
            TaskAwaiter awaiter;

            if (num != 0)
            {
                Console.WriteLine($"Work started in thread {Environment.CurrentManagedThreadId}");
                awaiter = Task.Delay(1000).GetAwaiter();

                if (!awaiter.IsCompleted)
                {
                    num = (State = 0);
                    taskAwaiter = awaiter;
                    WorkAsync_StateMachine stateMachine = this;
                    AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                    return;
                }
            }
            else
            {
                awaiter = taskAwaiter;
                taskAwaiter = default;
                num = State = -1;
            }
            awaiter.GetResult();
            Console.WriteLine($"Work finished in thread {Environment.CurrentManagedThreadId}");
        }
        catch (Exception exception)
        {
            State = -2;
            AsyncTaskMethodBuilder.SetException(exception);
            return;
        }

        State = -2;
        AsyncTaskMethodBuilder.SetResult();
    }

    void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
    {
        AsyncTaskMethodBuilder.SetStateMachine(stateMachine);
    }
}