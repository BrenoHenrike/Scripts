/*
name: AureateAegisSet
description: Does the `The Empty Mountains Cold` quest for the rewards
tags: the empty mountains cold, gole den, aureate aegis set
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class AureateAegisSet
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public Core13LoC LOC => new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetSet();

        Core.SetOptions(false);
    }

    public void GetSet()
    {
        if (Core.CheckInventory(Core.QuestRewards(9241)))
            return;

        Core.AddDrop(Core.QuestRewards(9241));
        LOC.Vath();

        while (!Bot.ShouldExit && !Core.CheckInventory(Core.QuestRewards(9241)))
        {
            Core.EnsureAccept(9241);
            Core.HuntMonster("darkheart", "Toxic Grove Spider", "Cavern Gold", 1000, isTemp: false);
            Core.HuntMonster($"templedelve", "Doomed Troll", "Troll's Gold", 1000, isTemp: false);
            Core.HuntMonster($"greed", "Sneevil Looter", "Looter's Gold", 1000, isTemp: false);
            Core.HuntMonster($"greed", "Goregold", "Goregold's Gold", 250, isTemp: false);
            Adv.BuyItem("alchemyacademy", 2115, "Gold Voucher 500k", 60);

            Core.EnsureComplete(9241);
        }
    }
}
