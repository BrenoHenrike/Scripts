using SkuaScriptsGenerator.Generators;

namespace SkuaScriptsGenerator.Factories
{
    public interface ISkuaScriptFactory
    {
        ISkuaScriptWriter Generate(string type);
    }
}
