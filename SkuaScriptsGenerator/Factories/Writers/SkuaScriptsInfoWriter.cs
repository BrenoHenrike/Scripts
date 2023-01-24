using SkuaScriptsGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkuaScriptsGenerator.Writers
{
    public class SkuaScriptsInfoWriter : ISkuaScriptWriter
    {
        public void Write()
        {
            foreach(var script in Directory.EnumerateFiles(".\\", "*.cs", SearchOption.AllDirectories))
            {
                // if the directory is SkuaScriptsGenerator, skip
                if (script.Contains("SkuaScriptsGenerator"))
                    continue;

                Console.WriteLine(script);

                // skip if the first line is a comment
                if (File.ReadLines(script).First().StartsWith("/*"))
                    continue;

                // if the first line is not a comment, add the template
                 if(!File.ReadLines(script).First().StartsWith("/*"))
                    File.WriteAllText(script, "/*\nname: null\ndescription: null\ntags: null\n*/\n" + File.ReadAllText(script));
            }
        }
    }
}