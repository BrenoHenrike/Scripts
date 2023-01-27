/*
name:  Eternal Drakath Set
description:  Gets the Eternal Drakath Set
tags: eternal drakath set, drakath, set
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
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

    public void getSet()
    {
        if (Core.CheckInventory(Rewards))
            return;

        Core.AddDrop(Rewards);

        Armor.DrakathArmor();

        if (!Core.CheckInventory("Drakath's Sword"))
            Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("ultradrakath", "Champion of Chaos", "Drakath's Sword", isTemp: false);

        Core.EnsureAccept(8457);

        BLOD.FindingFragmentsBlade(2000, 0);

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

            Core.RegisterQuests(8455);
            while (!Bot.ShouldExit && !Core.CheckInventory("Reality Shard", 200))
            {
                Core.HuntMonster("eternalchaos", "Eternal Drakath", "Eternal Drakath Defeated", 1);
                Bot.Wait.ForPickup("Reality Shard");
            }
            Core.CancelRegisteredQuests();
        }
        Core.EnsureComplete(8457);
    }
}
