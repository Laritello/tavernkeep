using System.IO;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace Tavernkeep.Statblock.Parser.Utilities
{
	public partial class FlowDocumentHelper
	{
		// Save FlowDocument to a file
		public static void Save(FlowDocument document)
		{
			// Serialize the FlowDocument to a XAML string
			string xamlString = XamlWriter.Save(document);

			var creaturesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "creatures");
			Directory.CreateDirectory(creaturesDirectory);

			var filePath = Path.Combine(creaturesDirectory, $"{GetName(document)}.creature");
			// Write the XAML string to a file
			File.WriteAllText(filePath, xamlString);
		}

		// Load FlowDocument from a file
		public static FlowDocument Load(string creatureName)
		{
			var creaturesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "creatures");
			Directory.CreateDirectory(creaturesDirectory);

			var filePath = Path.Combine(creaturesDirectory, $"{creatureName.ToLower().Replace(' ', '-')}.creature");

			if (!File.Exists(filePath))
			{
				return new FlowDocument();
			}

			// Read the XAML string from the file
			string xamlString = File.ReadAllText(filePath);

			// Deserialize the XAML string back into a FlowDocument
			using var stringReader = new StringReader(xamlString);
			using var xmlReader = XmlReader.Create(stringReader);
			return (FlowDocument)XamlReader.Load(xmlReader);
		}

		private static string GetName(FlowDocument document)
		{
			throw new NotImplementedException();
		}
	}
}
