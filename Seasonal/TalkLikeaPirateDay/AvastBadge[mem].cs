//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;

public class AvastBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Core.IsMember)
            return;
        if (Core.isCompletedBefore(2394)) //&& Core.HasAchievement()) // cant seem to find the achievmentID
        {
            Core.Logger("Story Complete.");
            return;
        }

        Story.PreLoad(this);

        // 2389|Aye for an Aye
        if (!Story.QuestProgression(2389))
        {
            Core.EnsureAccept(2389);
            if (!Core.CheckInventory(27)) //Barbed Horror
                Core.BuyItem("digitalyulgar", 16, "Barbed Horror");
            Core.KillMonster("watchtower", "Frame6", "Left", "Chaos Sp-Eye", "Chaos Sp-eye Eyeball", 5);
            Core.KillMonster("willowcreek", "Storage", "Left", "Speyeder", "Speyeder Eyeball", 5);
            Core.EnsureComplete(2389);
        }
        // 2390|Skull and Crossed Bones
        if (!Story.QuestProgression(2390))
        {
            Core.EnsureAccept(2390);
            Farm.BattleUnderB(quant: 10);
            Adv.BuyItem("Necropolis", 408, "Skullface Mace");
            Core.EnsureComplete(2390);
        }

        // 2391|Off the Hook! 
        if (!Story.QuestProgression(2391))
        {
            Core.EnsureAccept(2391);
            if (!Core.CheckInventory("Fishin' Hooks"))
            {
                Core.AddDrop("Fishin' Chips", "Fishing Dynamite");
                if (!Core.isCompletedBefore(1682))
                    Story.KillQuest(1682, "Greenguardwest", "Slime");

                Farm.FishingREP(6);
                while (!Bot.ShouldExit && !Core.CheckInventory(6646, 60))
                {
                    Core.RegisterQuests(1614);
                    if (!Core.CheckInventory(10850, 30))
                    {
                        Core.FarmingLogger("Fishin' Chips", 60);
                        Core.FarmingLogger("Fish Caught", 30);
                        GetBaitandDynamite(0, 30);
                        while (!Bot.ShouldExit && Core.CheckInventory("Fishing Dynamite") && !Core.CheckInventory(10850, 30))
                        {
                            if (Bot.Inventory.IsMaxStack("Fishing Bait"))
                                Bot.Drops.Remove("Fishing Bait");
                            Bot.Send.Packet($"%xt%zm%FishCast%1%Dynamite%30%");
                            Bot.Sleep(3500);
                            Core.SendPackets("%xt%zm%getFish%1%false%");
                        }
                        Core.HuntMonster("Greenguardwest", "Slime", "Slime Sauce");
                    }
                    Core.CancelRegisteredQuests();
                }
                Core.BuyItem("Greenguardwest", 363, "Fishin' Hooks");
            }
            Core.HuntMonster("akiba", "Kage Nopperabo|Ninja Nopperabo|Samurai Nopperabo", "Hook Sword", isTemp: false);
            // Story.KillQuest(2391, "akiba", "Kage Nopperabo|Ninja Nopperabo|Samurai Nopperabo", "Hook Sword", isTemp: false);
            Core.EnsureComplete(2391);
        }

        // 2392|Booty and the Beast
        if (!Story.QuestProgression(2392))
        {
            Core.EnsureAccept(2392);
            Core.HuntMonster("neverbeast", "Beast Maker", "Beast Maker Booty");
            Core.HuntMonster("voltaire", "Beast of Pirate's Bay", "Beast of Pirate's Bay Booty");
            Core.HuntMonster("bludrut4", "Shadow Serpent", "Shadow Serpent Booty");
            Core.HuntMonster("voltaire", "Braken Head", "Braken Booty");
            Core.EnsureComplete(2392);
        }

        // 2393|A Pirate's Lunch For Me
        if (!Story.QuestProgression(2393))
        {
            Core.EnsureAccept(2393);
            Core.HuntMonster("natatorium", "Anglerfish", "Natatorium Kelp Bread", 2);
            Core.HuntMonster("cloister", "Acornent", "Acornet Butter", 6);
            Core.HuntMonster("battleunderc", "Purple Crystalized Jellyfish", "Crystalized Jelly-fish", 6);
            Core.EnsureComplete(2393);
        }

        // 2394|Thee Seven C's!
        if (!Story.QuestProgression(2394))
        {
            Core.EnsureAccept(2394);
            Core.HuntMonster("highcommand", "Chaorrupted Invader", "Chaorrupted Invader Loot");
            Core.HuntMonster("darkness", "Cloaked Fiend", "Cloaked Fiend Loot");
            Core.HuntMonster("anders", "Copper Sky Pirate", "Copper Sky Pirate Loot");
            Core.HuntMonster("tunnel", "Cadaverous Creeper", "Cadaverous Creeper Loot");
            Core.HuntMonster("pirates", "Capt. Beard", "Capt. 000000Beard Loot");
            Core.HuntMonster("wind", "Cellot", "Cellot Loot");
            Core.HuntMonster("academy", "Chaobold", "Chaobold Loot");
            Core.EnsureComplete(2394);
        }

        void GetBaitandDynamite(int FishingBaitQuant, int FishingDynamiteQuant)
        {
            if (Core.CheckInventory("Fishing Bait", FishingBaitQuant) && Core.CheckInventory("Fishing Dynamite", FishingDynamiteQuant))
                return;

            Core.Logger("Pre-Fishing XP(This is Required)");
            Core.EnsureAccept(1682);
            Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);
            Core.EnsureComplete(1682);

            Core.RegisterQuests(1682);
            Core.FarmingLogger("Fishing Bait", FishingBaitQuant);
            while (!Bot.ShouldExit && FishingBaitQuant != 0 && !Core.CheckInventory("Fishing Bait", FishingBaitQuant))
                Core.KillMonster("greenguardwest", "West3", "Right", "Frogzard", "Fishing Bait", FishingBaitQuant, isTemp: false, log: false);

            Core.FarmingLogger("Fishing Dynamite", FishingDynamiteQuant);
            while (!Bot.ShouldExit && FishingDynamiteQuant != 0 && !Core.CheckInventory("Fishing Dynamite", FishingDynamiteQuant))
                Core.KillMonster("greenguardwest", "West4", "Right", "Slime", "Faith's Fi'shtick", 1, log: false);

            Core.CancelRegisteredQuests();
            Core.Logger("Returing to Fishing Map");
            Core.Join("fishing");
        }
    }
}
