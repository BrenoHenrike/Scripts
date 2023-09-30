// See https://aka.ms/new-console-template for more information
using SkuaScriptsGenerator.Factories;

ISkuaScriptFactory script = new ScriptSkuaFactory();
script.Generate("json").Write();