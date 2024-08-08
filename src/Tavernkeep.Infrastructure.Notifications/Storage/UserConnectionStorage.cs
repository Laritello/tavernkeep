namespace Tavernkeep.Infrastructure.Notifications.Storage
{
	public interface IUserConnectionStorage<T> where T : notnull
	{
		public void Add(T key, string connectionId);
		public IEnumerable<string> GetConnections(T key);
		public void Remove(T key, string connectionId);
	}

	public class UserConnectionStorage<T> : IUserConnectionStorage<T> where T : notnull
	{
		private readonly Dictionary<T, HashSet<string>> _connections = [];

		public int Count => _connections.Count;

		public void Add(T key, string connectionId)
		{
			lock (_connections)
			{
				if (!_connections.TryGetValue(key, out var connections))
				{
					connections = [];
					_connections.Add(key, connections);
				}

				lock (connections)
				{
					connections.Add(connectionId);
				}
			}
		}

		public IEnumerable<string> GetConnections(T key)
		{
			if (_connections.TryGetValue(key, out var connections))
			{
				return connections;
			}

			return [];
		}

		public void Remove(T key, string connectionId)
		{
			lock (_connections)
			{
				if (!_connections.TryGetValue(key, out var connections))
				{
					return;
				}

				lock (connections)
				{
					connections.Remove(connectionId);

					if (connections.Count == 0)
					{
						_connections.Remove(key);
					}
				}
			}
		}
	}
}
