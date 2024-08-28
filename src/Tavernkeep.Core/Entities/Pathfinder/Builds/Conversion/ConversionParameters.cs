using System.Collections;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion
{
	public class ConversionParameters : IEnumerable<KeyValuePair<string, object>>
	{
		private readonly List<KeyValuePair<string, object>> _parameters = [];
		public ConversionParameters() { }

		public void Add(string key, object value) => _parameters.Add(new KeyValuePair<string, object>(key, value));

		public object? this[string key]
		{
			get
			{
				foreach (var entry in _parameters)
				{
					if (string.Compare(entry.Key, key, StringComparison.Ordinal) == 0)
					{
						return entry.Value;
					}
				}

				return null;
			}
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _parameters.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
