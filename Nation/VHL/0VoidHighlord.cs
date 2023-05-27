/*
name: Void Highlord (Class)
description: This will farm all requirements for the Void Highlord class to obtain.
tags: farm, class, nation, vhl, void, highlord, roentgenium, void-crystal-a, void-crystal-b
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class VoidHighlord
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreVHL VHL = new();
    public static CoreVHL sVHL = new();
    public CoreNation Nation = new();

    public string OptionsStorage = sVHL.OptionsStorage;
    public bool DontPreconfigure = true;
    public List<IOption> Options = sVHL.Options;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        VHL.GetVHL();

        Core.SetOptions(false);
    }
}
