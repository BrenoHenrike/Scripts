using Newtonsoft.Json;

namespace SkuaScriptsGenerator.Generators
{
    public class SkuaScriptsJsonWriter : ISkuaScriptWriter
    {
        public void Write()
        {
            var rawScriptsURL = "https://raw.githubusercontent.com/BrenoHenrike/Scripts/Skua/";
            var scripts = new List<ScriptInfo>();
            foreach (var script in Directory.EnumerateFiles("./", "*.cs", SearchOption.AllDirectories))
            {
                var firstLine = File.ReadLines(script).First();
                if (firstLine.StartsWith("/*"))
                {
                    var scriptInfo = new ScriptInfo();
                    var lines = File.ReadLines(script).Skip(1).TakeWhile(x => !x.StartsWith("*/")).ToArray();
                    foreach (var line in lines)
                    {
                        var parts = line.Split(':');
                        var key = parts[0].Trim();
                        
                        var value = parts[1].Trim();
                        switch (key)
                        {
                            case "name":
                                scriptInfo.Name = value;
                                break;
                            case "description":
                                scriptInfo.Description = value;
                                break;
                            case "tags":
                                scriptInfo.Tags = value.Split(',').Select(x => x.Trim()).Select(x => x.All(c => char.IsUpper(c)) ? x : x.ToLower()).ToArray();
                                break;
                        }
                    }
                    scriptInfo.Path = script.Replace("./", "");
                    scriptInfo.FileName = script.Split('/').Last();
                    scriptInfo.DownloadUrl = rawScriptsURL+scriptInfo.Path;
                    scriptInfo.Size = (int)new FileInfo(script).Length;

                    scripts.Add(scriptInfo);
                }
            }

            var json = JsonConvert.SerializeObject(scripts, Formatting.Indented);
            // File.WriteAllText("scripts.json", json);
            Console.WriteLine(json);
        }
        
        class ScriptInfo 
        {
            [JsonProperty("name")]
            public string? Name { get; set; }
            [JsonProperty("description")]
            public string? Description { get; set; }
            [JsonProperty("tags")]
            public string[]? Tags { get; set; }
            [JsonProperty("path")]
            public string? Path { get; set; }
            [JsonProperty("size")]
            public int? Size { get; set; }
            [JsonProperty("fileName")]
            public string? FileName { get; set; }
            [JsonProperty("downloadUrl")]
            public string? DownloadUrl { get; set; }
        }
    }
}