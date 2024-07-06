using System.Reflection.PortableExecutable;
/*
name: Eternal Drakath Set
description: Gets the Eternal Drakath Set
tags: eternal drakath set, drakath, set
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;

public class EternalDrakath
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public DrakathArmorBot Armor = new();
    public CoreBLOD BLOD = new();
    public StarSinc Star = new();

    private string[] Rewards = new[] { "Drakath the Eternal", "Drakath the Eternal's Visor", "Eternal Chaos Tassels", "Eternal Chaos Tassels", "Dual Everlasting Blades of Chaos" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        getSet();

        Core.SetOptions(false);
    }

    public void getSet(bool singleitem = false, string? item = null)
    {
        if (item != null && Core.CheckInventory(item) || Core.CheckInventory(Rewards))
            return;

        if (item != null)
            Core.AddDrop(new[] { item });
        else
            Core.AddDrop(Rewards);
        Core.AddDrop(25286);

        Armor.DrakathArmor();
        if (!Core.CheckInventory("Drakath Armor")) //"Drakath the Eternal"
        {
            Core.Logger("Cannot continue with \"Drakath Armor\" not enough \"Dage's Scroll Fragment\", cannot complete \"Drakath the Eternal\".");
            return;
        }
        Core.EquipClass(ClassType.Solo);
        if (!Core.CheckInventory(25286))
            Core.Logger("Farming Drakath's Sword");
        while (!Bot.ShouldExit && !Core.CheckInventory(25286))
            Core.HuntMonster("ultradrakath", "Champion of Chaos", log: false);

        Core.EnsureAccept(8457);

        BLOD.SpiritOrb(2000);

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("chaoslab", "r3", "Center", "Chaorrupted Moglin", "Crystallized Chaos", 800, false);

        if (!Core.CheckInventory("Star Fragment", 33))
        {
            Star.StarSincQuests();
            Core.EquipClass(ClassType.Farm);
            Core.AddDrop("Star Fragment");

            Core.RegisterQuests(4413);
            while (!Bot.ShouldExit && !Core.CheckInventory("Star Fragment", 33))
            {
                Core.HuntMonster("starsinc", "Living Star", "Living Star Defeated", 30, isTemp: false);
                Bot.Wait.ForPickup("Star Fragment");
            }
            Core.CancelRegisteredQuests();
        }

        if (!Core.CheckInventory(61825, 5))
        {
            Bot.Quests.UpdateQuest(3799);
            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("shadowattack", "Boss", "Left", "Death", "Death's Oversight", 5, false);
        }

        if (!Core.CheckInventory("Reality Shard", 300))
        {
            Core.EquipClass(ClassType.Solo);
            Core.AddDrop("Reality Shard");

            Core.Logger("Farming Reality Shards");
            Core.RegisterQuests(8455);
            while (!Bot.ShouldExit && !Core.CheckInventory("Reality Shard", 200))
            {
                Core.HuntMonster("eternalchaos", "Eternal Drakath", "Eternal Drakath Defeated", 1, log: false);
                Bot.Wait.ForPickup("Reality Shard");
            }
            Core.CancelRegisteredQuests();
        }
        Core.EnsureComplete(8457);
        if (singleitem && item != null && Core.CheckInventory(item))
        {
            Bot.Wait.ForDrop(item);
            Bot.Wait.ForPickup(item);
            return;
        }
    }
}
