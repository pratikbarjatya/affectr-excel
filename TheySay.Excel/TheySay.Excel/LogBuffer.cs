using System.Collections.Generic;

namespace TheySay.Excel
{
    public class LogBuffer
    {
        private readonly Queue<LogEntry> _data;
        private int _maxSize;

        public LogBuffer(int noEntries)
        {
            _data = new Queue<LogEntry>(noEntries);
            _maxSize = noEntries;
        }

        public void Resize(int noEntries)
        {
            if (noEntries < _maxSize)
            {
                for (int i = 0; i < _maxSize - noEntries; i++ )
                {
                    _data.Dequeue();
                }
            }
            _maxSize = noEntries;
        }
        
        public void AddEntry(LogEntry entry)
        {
            if (_data.Count == _maxSize)
            {
                _data.Dequeue();
            }
            _data.Enqueue(entry);
        }

        public IEnumerable<LogEntry> Entries()
        {
            foreach (var logEntry in _data)
            {
                yield return logEntry;
            }
        }

        public void Clear()
        {
            _data.Clear();
        }
    }
}
