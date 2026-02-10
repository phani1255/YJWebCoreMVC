
// venkat 02/06/2026 copied from mvc to work with core
using System.Collections.Concurrent;

namespace YJWebCoreMVC.Services
{
    public class BackgroundJobProgress
    {
        private class ProgressInfo
        {
            public int Current { get; set; }
            public int Total { get; set; }

            public string Error { get; set; }
        }

        private static readonly ConcurrentDictionary<string, ProgressInfo>
            _progress = new ConcurrentDictionary<string, ProgressInfo>();

        public static void Init(string jobId)
        {
            _progress[jobId] = new ProgressInfo
            {
                Current = 0,
                //Total = 0
                Total = -1
            };
        }

        public static void SetTotal(string jobId, int total)
        {
            _progress[jobId] = new ProgressInfo
            {
                Current = 0,
                Total = total
            };
        }

        public static void Increment(string jobId)
        {
            if (_progress.ContainsKey(jobId))
            {
                _progress[jobId].Current++;
            }
        }

        public static object Get(string jobId)
        {
            if (!_progress.ContainsKey(jobId))
                return null;

            var p = _progress[jobId];
            return new
            {
                current = p.Current,
                total = p.Total,
                error = p.Error
            };
        }

        public static void Remove(string jobId)
        {
            ProgressInfo removed;
            _progress.TryRemove(jobId, out removed);
        }

        public static void SetError(string jobId, string errorMessage)
        {
            if (_progress.ContainsKey(jobId))
            {
                _progress[jobId].Error = errorMessage;
            }
        }
    }
}
