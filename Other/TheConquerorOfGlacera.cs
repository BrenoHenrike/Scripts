/*
name: The Conqueror of Glacera
description: This script will complete "The Conqueror of Glacera" [9492] quest.
tags: glacera,karok,wings,fallen,runed,glaceran,loyalty,sigil,frigid,scythe,vengeance,conqueror
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Other/Classes/FrostSpiritReaver.cs
//cs_include Scripts/Story/Glacera.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;

public class TheConquerorOfGlacera
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private GlaceraStory IWP = new();
    private FrostSpiritReaver FSR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();
        Core.SetOptions(false);
    }

    public void DoQuest()
    {
        if (Core.CheckInventory(Core.QuestRewards(9492)))
            return;

        // Pet Prereq
        Core.HuntMonster("northstar", "Karok The Fallen", "Karok Companion", isTemp: false);

        Core.EnsureAccept(9492);

        Core.AddDrop("Rime Token", "Glaceran Gem", "Ice Chunk", "Glaceran Attunement");

        // Rime Token
        Core.KillMonster("battlegroundd", "r2", "Left", "*", "Rime Token", 200, false);

        // Glaceran Gem
        if (!Core.CheckInventory("Glaceran Gem", 500))
        {
            IWP.IceWindPass();
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(5597, 5598);
            while (!Bot.ShouldExit && !Core.CheckInventory("Glaceran Gem", 500))
            {
                while (!Bot.ShouldExit && Bot.Map.Name != "icewindwar")
                {
                    Core.Join("icewindwar");
                    Core.Sleep();
                }

                while (!Bot.ShouldExit && Bot.Player.Cell != "r2")
                {
                    Core.Jump("r2");
                    Core.Sleep();
                }

                foreach (Monster mob in Bot.Monsters.CurrentAvailableMonsters.Where(m => m.Cell == "r2"))
                {
                    Bot.Kill.Monster(mob.MapID);
                    if (Core.CheckInventory("Glaceran Gem", 500))
                        break;
                }
            }

            Core.CancelRegisteredQuests();
            Bot.Wait.ForPickup("Glaceran Gem");
        }

        // Ice Chunk
        Core.HuntMonster("northmountain", "Ice Elemental", "Ice Chunk", 600, false);

        // Glaceran Attunement
        FSR.GlaceranAttunement(10);

        Core.Unbank("Rime Token", "Glaceran Gem", "Ice Chunk", "Glaceran Attunement");

        Core.EnsureComplete(9492);
    }
}
