/*
name: Cavern Celestite
description: This script farms the desired quantity of Cavern Celestite.
tags: cavern, celestite, BLOD, blinding, light, destiny, good, soul searching, nsod
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CavernCelestite
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new();
    public CoreStory Story = new();

    public string OptionsStorage = "CavernCelestite";
    public bool DontPreconfigure = true;
    public List<IOption> Options;

    public CavernCelestite()
    {
        Options = new List<IOption>()
        {
            CoreBots.Instance.SkipOptions,
            new Option<int>("Quantity", "Cavern Celestite Quantity", "Choose the quantity of Cavern Celestite to farm. (Farms max stack by default)", GetMaxStack())
        };
    }

    private int GetMaxStack()
    {
        // Uses ternary operator for Tato to understand
        return Bot.Quests.EnsureLoad(939)?.Rewards.FirstOrDefault(reward => reward.Name.Equals("Cavern Celestite"))?.MaxStack ?? 1;
    }

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.SoulSearching("Cavern Celestite", Bot.Config!.Get<int>("Quantity"), false);

        Core.SetOptions(false);
    }
}
