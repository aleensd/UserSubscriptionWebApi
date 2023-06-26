using Npgsql;
using Polly;

namespace UserSubscriptionWebApi.Configurations
{
    public class RetryHelper
    {
        public static async Task<T> ExecuteWithRetry<T>(Func<Task<T>> action, int retryCount = 3, TimeSpan? retryDelay = null)
        {
            var policy = Policy.Handle<NpgsqlException>()
                .WaitAndRetryAsync(retryCount, i => retryDelay ?? TimeSpan.FromSeconds(Math.Pow(2, i)));

            return await policy.ExecuteAsync(action);
        }
    }
}
