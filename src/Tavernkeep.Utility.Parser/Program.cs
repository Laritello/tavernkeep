using System.Text.Json;
using Tavernkeep.Utility.Parser;
using Tavernkeep.Utility.Parser.IO;
using Tavernkeep.Utility.Parser.Json;

var target = args.Length > 0 ? args[0] : "Demo.json";

var parser = new Parser();

var json = RelativeFile.ReadAllText(@"Resources\pathbuilder.json");
var creatures = JsonSerializer.Deserialize<List<PathbuilderRecord>>(json) ?? [];

var result = creatures.Select(parser.Parse).ToList();

File.WriteAllText(target, JsonSerializer.Serialize(result));


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
