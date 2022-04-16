namespace BackgroundWork.BackgroundService
{
	public class QueueService : Microsoft.Extensions.Hosting.BackgroundService
	{
        private IBackgroundQueue _queue;

		public QueueService(IBackgroundQueue queue)
		{
            _queue = queue;
		}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var task = await _queue.PopQueue(stoppingToken);

                if (stoppingToken.IsCancellationRequested)
                {
                    return;
                }

                // On fail after a minute - cancel.
                using (var source = new CancellationTokenSource())
                {
                    source.CancelAfter(TimeSpan.FromMinutes(1));
                    var timeOutToken = source.Token;
                    await task(timeOutToken);
                }
            }
        }
    }
}

