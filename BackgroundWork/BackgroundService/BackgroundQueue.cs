using System.Collections.Concurrent;

namespace BackgroundWork.BackgroundService
{
	public class BackgroundQueue : IBackgroundQueue
	{
        private ConcurrentQueue<Func<CancellationToken, Task>> _tasks;
        private SemaphoreSlim _signal;

		public BackgroundQueue()
		{
            _tasks = new ConcurrentQueue<Func<CancellationToken, Task>>();
            _signal = new SemaphoreSlim(0);
		}

        public void QueueTask(Func<CancellationToken, Task> task)
        {
            _tasks.Enqueue(task);
            _signal.Release();
        }

        public async Task<Func<CancellationToken, Task>> PopQueue(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _tasks.TryDequeue(out var task);
            return task;
        }
    }
}

