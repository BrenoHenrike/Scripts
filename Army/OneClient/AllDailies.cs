/*
name:  All do AllDailies
description:  Alldailies
tags: alldailies, army, thefamily
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Dailies/0AllDailies.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyAllDailies
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private FarmAllDailies FAD = new();
    private CoreArmyLite Army = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyAllDailies";
    public List<IOption> Options = new()
    {
        new Option<bool>("randomServers", "Random Servers", "Should the bot use a random server each for each account.", true),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CheckACs(Bot.Config.Get<bool>("randomServers"));

        Core.SetOptions(false);
    }

    public void CheckACs(bool randomServers)
    {
        while (Army.doForAll(randomServers))
            FAD.DoAllDailies();
    }
}
