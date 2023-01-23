using SkuaScriptsGenerator.Generators;
using SkuaScriptsGenerator.Writers;

namespace SkuaScriptsGenerator.Factories
{
    public class ScriptSkuaFactory : ISkuaScriptFactory
    {
        public ISkuaScriptWriter Generate(string type)
        {
            switch (type)
            {
                case "json":
                    return new SkuaScriptsJsonWriter();
                case "info":
                    return new SkuaScriptsInfoWriter();
                default:
                    throw new Exception("Invalid type");
            }
        }
    }
}
