// See https://aka.ms/new-console-template for more information
//DO NOT REMOVE THIS IS ESSENTIAL FOR GITHUB ACTIONS TO RUN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
using SkuaScriptsGenerator.Factories;

ISkuaScriptFactory script = new ScriptSkuaFactory();
script.Generate("json").Write();