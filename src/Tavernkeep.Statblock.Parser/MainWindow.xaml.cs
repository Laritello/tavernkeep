using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Documents;
using Tavernkeep.Core.Entities.Library.Creatures;
using Tavernkeep.Statblock.Parser.Entities;
using Tavernkeep.Statblock.Parser.Parsing;
using Tavernkeep.Statblock.Parser.Utilities;

namespace Tavernkeep.Statblock.Parser
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

		private readonly StatblockParser<FlowDocument> _flowDocumentParser = new();
		private readonly StatblockParser<PathbuilderRecord> _pathbuilderParser = new();

		public MainWindow()
		{
			InitializeComponent();
			LoadFlowDocuments();
			LoadPathbuilder();
		}

		private void Parse_Click(object sender, RoutedEventArgs e)
		{
			var creature = Parse();
			Display(creature);
		}

		private void LoadFlowDocuments()
		{
			var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "creatures");
			Directory.CreateDirectory(directory);

			var files = Directory.GetFiles(directory);

			var creatures = files.Select(x => ti.ToTitleCase(Path.GetFileNameWithoutExtension(x).Replace('-', ' '))).ToList();

			FlowDocumentsList.ItemsSource = creatures;

			if (FlowDocumentsList.Items.Count > 0)
			{
				FlowDocumentsList.SelectedIndex = 0;
			}
		}

		private void LoadPathbuilder()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pathbuilder.json");
			var json = File.ReadAllText(filePath);
			var creatures = JsonSerializer.Deserialize<List<PathbuilderRecord>>(json) ?? [];

			PathbuilderList.ItemsSource = creatures;
			PathbuilderList.DisplayMemberPath = "Name";

			if (PathbuilderList.Items.Count > 0)
			{
				PathbuilderList.SelectedIndex = 0;
			}
		}

		private Creature Parse()
		{
			var parser = new StatblockParser<FlowDocument>();
			return parser.Parse(UserInputBox.Document);
		}

		private void Display(Creature? creature)
		{
			if (creature is null)
			{
				return;
			}

			var resourceStream = Application.GetResourceStream(new Uri("pack://application:,,,/Tavernkeep.Statblock.Parser;component/Resources/Template.html"));

			if (resourceStream != null)
			{
				using var reader = new StreamReader(resourceStream.Stream);
				string html = reader.ReadToEnd();

				var template = Scriban.Template.Parse(html);

				var render = template.Render(new { Body = creature.GetStatBlock() });

				string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template_render.html");
				File.WriteAllText(path, render);

				PreviewBrowser.Source = new Uri("https://google.com");
				PreviewBrowser.Source = new Uri(path);
			}
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			FlowDocumentHelper.Save(UserInputBox.Document);

			var creature = Parse();
			Display(creature);
		}

		private void LoadFlowDocument_Click(object sender, RoutedEventArgs e)
		{
			ShowFlowDocument();
		}

		private void LoadPathbuilder_Click(object sender, RoutedEventArgs e)
		{
			ShowPathbuilder();
		}

		private void NextPathbuilder_Click(object sender, RoutedEventArgs e)
		{
			if (PathbuilderList.SelectedIndex < PathbuilderList.Items.Count - 1)
			{
				PathbuilderList.SelectedIndex += 1;
				ShowPathbuilder();
			}
		}

		private void PreviousPathbuilder_Click(object sender, RoutedEventArgs e)
		{
			if (PathbuilderList.SelectedIndex > 0)
			{
				PathbuilderList.SelectedIndex -= 1;
				ShowPathbuilder();
			}
		}

		private void NextFlowDocument_Click(object sender, RoutedEventArgs e)
		{
			if (FlowDocumentsList.SelectedIndex < FlowDocumentsList.Items.Count - 1)
			{
				FlowDocumentsList.SelectedIndex += 1;
				ShowFlowDocument();
			}
		}

		private void PreviousFlowDocument_Click(object sender, RoutedEventArgs e)
		{
			if (FlowDocumentsList.SelectedIndex > 0)
			{
				FlowDocumentsList.SelectedIndex -= 1;
				ShowFlowDocument();
			}
		}

		private void ShowFlowDocument()
		{
			if (FlowDocumentsList.SelectedItem is string creatureName)
			{
				UserInputBox.Document = FlowDocumentHelper.Load(creatureName);
				var creature = Parse();
				Display(creature);
			}
		}

		private void ShowPathbuilder()
		{
			if (PathbuilderList.SelectedItem is PathbuilderRecord creatureRecord)
			{
				var parser = new StatblockParser<PathbuilderRecord>();
				var creature = parser.Parse(creatureRecord);

				Display(creature);
			}
		}

		private void Json_Click(object sender, RoutedEventArgs e)
		{
			var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "creatures");
			Directory.CreateDirectory(directory);
			var files = Directory.GetFiles(directory);

			var creatures = files.Select(ParseCreature).ToList();

			var json = JsonSerializer.Serialize(creatures);
			File.WriteAllText("temp_flow.json", json);
		}

		private void JsonPathbuilder_Click(object sender, RoutedEventArgs e)
		{
			var items = PathbuilderList.ItemsSource as List<PathbuilderRecord> ?? [];
			var creatures = items.Select(ParseCreature).ToList();

			var json = JsonSerializer.Serialize(creatures);
			File.WriteAllText("temp_pathbuilder.json", json);
		}

		private Creature ParseCreature(string fileName)
		{
			var document = FlowDocumentHelper.Load(fileName);
			var creature = _flowDocumentParser.Parse(document);
			return creature;
		}

		private Creature ParseCreature(PathbuilderRecord record)
		{
			var creature = _pathbuilderParser.Parse(record);
			return creature;
		}
	}
}