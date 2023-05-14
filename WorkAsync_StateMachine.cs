using System.Runtime.CompilerServices;

namespace AsyncStateMachine;

internal sealed class WorkAsync_StateMachine : IAsyncStateMachine
{
    public int State;

    public AsyncTaskMethodBuilder AsyncTaskMethodBuilder;

    private int delay;

    private int tempDelay;

    private TaskAwaiter<int> intTaskAwaiter;

    private TaskAwaiter taskAwaiter;

    void IAsyncStateMachine.MoveNext()
    {
        int num = State;
        try
        {
            TaskAwaiter awaiter;
            TaskAwaiter<int> awaiter2;

            if (num != 0)
            {
                if (num == 1)
                {
                    awaiter = taskAwaiter;
                    taskAwaiter = default;
                    num = (State = -1);
                    goto IL_011a;
                }

                Console.WriteLine($"Work started in thread {Environment.CurrentManagedThreadId}");
                awaiter2 = Program.GetDelayAsync().GetAwaiter();
                if (!awaiter2.IsCompleted)
                {
                    num = (State = 0);
                    intTaskAwaiter = awaiter2;
                    WorkAsync_StateMachine stateMachine = this;
                    AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted(ref awaiter2, ref stateMachine);
                    return;
                }
            }
            else
            {
                awaiter2 = intTaskAwaiter;
                intTaskAwaiter = default;
                num = (State = -1);
            }

            tempDelay = awaiter2.GetResult();
            delay = tempDelay;

            awaiter = Task.Delay(delay).GetAwaiter();

            if (!awaiter.IsCompleted)
            {
                num = (State = 1);
                taskAwaiter = awaiter;
                WorkAsync_StateMachine stateMachine = this;
                AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                return;
            }

            IL_011a:
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