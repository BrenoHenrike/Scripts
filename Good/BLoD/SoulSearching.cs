/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SoulSearchingc
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public BattleUnder BattleUnder = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoulSearching();

        Core.SetOptions(false);
    }

    public void SoulSearching(int quant = 65000)
    {
        if (Core.CheckInventory("Spirit Orb", quant))
            return;

        Core.AddDrop("Undead Essence", "Cavern Celestite", "Spirit Orb");
        if (!Bot.Quests.IsUnlocked(939))
            BattleUnder.BattleUnderC();
        Core.FarmingLogger("Spirit Orb", quant);

        Core.RegisterQuests(939, 2082);
        while (!Bot.ShouldExit && !Core.CheckInventory("Spirit Orb", quant))
        {
            Core.HuntMonster("battleundera", "Bone Terror", "Bone Terror Soul", log: false);
            Core.HuntMonster("battleunderb", "Undead Champion", "Undead Champion Soul", log: false);
            Core.HuntMonster("battleunderc", "Crystalized Jellyfish", "Jellyfish Soul", log: false);

            Bot.Wait.ForPickup("Undead Essence");
            Bot.Wait.ForPickup("Spirit Orb");
            if (Core.CheckInventory("Spirit Orb", quant))
                break;
            else Core.FarmingLogger("Spirit Orb", quant);
        }
        Core.CancelRegisteredQuests();
    }
}
