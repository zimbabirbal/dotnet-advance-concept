namespace Advance_dotnet_concept.C_Sharp_Dotnet.ThreadSyncronization
{
    /// <summary>
    /// ReaderWriterLockSlim is similar to ReaderWriterLock, but it has simplified rules for recursion and for upgrading and downgrading lock state
    /// The performance of ReaderWriterLockSlim is significantly better than ReaderWriterLock. ReaderWriterLockSlim is recommended for all new development
    /// </summary>
    internal class ReadWriterLockSlimExample
    {

        //The following example shows a simple synchronized cache that holds strings with integer keys.
        //An instance of ReaderWriterLockSlim is used to synchronize access to the Dictionary<TKey,TValue> that
        //serves as the inner cache.
        private ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private Dictionary<int, string> _innerCache = new Dictionary<int, string>();

        public int Count { get { return _innerCache.Count; } }

        public string Read(int key)
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _innerCache[key];
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        public void Add(int key, string value)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _innerCache.Add(key, value);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public bool AddWithTimeout(int key, string value, int Timeout)
        {
            if (_cacheLock.TryEnterWriteLock(Timeout))
            {
                try
                {
                    _innerCache.Add(key, value);
                }
                finally { _cacheLock.ExitWriteLock(); }

                return true;
            }
            else
            {
                return false;
            }
        }

        public AddOrUpdateStatus AddOrUpdate(int key, string value)
        {
            _cacheLock.EnterUpgradeableReadLock();
            try
            {
                string result = null;

                if (_innerCache.TryGetValue(key, out result))
                {
                    if (result == null)
                        return AddOrUpdateStatus.Unchanged;
                    else
                    {
                        _cacheLock.EnterWriteLock();
                        try
                        {
                            _innerCache[key] = value;
                        }
                        finally
                        {
                            _cacheLock.ExitWriteLock();
                        }

                        return AddOrUpdateStatus.Updated;
                    }
                }
                else
                {
                    _cacheLock.EnterWriteLock();
                    try
                    {
                        _innerCache.Add(key, value);
                    }
                    finally
                    {
                        _cacheLock.ExitWriteLock();
                    }

                    return AddOrUpdateStatus.Added;
                }
            }
            finally
            {
                _cacheLock.ExitUpgradeableReadLock();
            }
        }

        public void Delete(int key)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _innerCache.Remove(key);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        ~ReadWriterLockSlimExample()
        {
            if (_cacheLock != null)
                _cacheLock.Dispose();
        }
    }

    public enum AddOrUpdateStatus
    {
        Added,
        Updated,
        Unchanged
    }

}
