using System.Runtime.CompilerServices;

namespace AsyncStateMachine;

    internal sealed class GetDelayAsync_StateMachine : IAsyncStateMachine
    {
        public int State;
        public AsyncTaskMethodBuilder<int> AsyncTaskMethodBuilder;
        private TaskAwaiter taskAwaiter;

        void IAsyncStateMachine.MoveNext()
        {
            int num = State;
            int result;
            try
            {
                TaskAwaiter awaiter;
                if (num != 0)
                {
                    Console.WriteLine($"GetDelay started in thread {Environment.CurrentManagedThreadId}");

                    awaiter = Task.Delay(1000).GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        num = (State = 0);
                        taskAwaiter = awaiter;
                        GetDelayAsync_StateMachine stateMachine = this;
                        AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                        return;
                    }
                }
                else
                {
                    awaiter = taskAwaiter;
                    taskAwaiter = default;
                    num = (State = -1);
                }
                awaiter.GetResult();
                result = 1000;

                Console.WriteLine($"GetDelay finished in thread {Environment.CurrentManagedThreadId}");
            }
            catch (Exception exception)
            {
                State = -2;
                AsyncTaskMethodBuilder.SetException(exception);
                return;
            }
            State = -2;
            AsyncTaskMethodBuilder.SetResult(result);
        }

        void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
        {
            AsyncTaskMethodBuilder.SetStateMachine(stateMachine);
        }
    }