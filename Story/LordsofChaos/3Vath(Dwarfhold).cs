//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaDwarfhold
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        CompleteSaga();

        Core.SetOptions(false);
    }

    public void CompleteSaga()
    {
        Core.BuyItem("battleon", 947, "Volcanic Fire Sword");
        if (Core.CheckInventory("Volcanic Fire Sword", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Volcanic Fire Sword");
            Core.Logger("Chapter: \"Chaos Lord Vath\" already complete. Skipping");
            return;
        }

        //Map: Pines
        Core.MapItemQuest(319, "tavern", 56, 7);                                                                        // Adorable Sisters
        Core.KillQuest(320, "pines", "Pine Grizzly");                                                                   // Warm and Furry
        Core.KillQuest(321, "pines", "Red Shell Turtle");                                                               // Shell Rock
        Core.KillQuest(322, "pines", "Twistedtooth", FollowupIDOverwrite: 324);                                         // Bear Facts
        Core.KillQuest(324, "pines", "Red Shell Turtle");                                                               // The Spittoon Saloon
        Core.KillQuest(325, "pines", "Pine Grizzly");                                                                   // Bear it all!
        Core.KillQuest(326, "pines", "Leatherwing");                                                                    // Leather Feathers
        if (!Core.CheckInventory("Snowbeard's Gold"))                                                                   // Follow your Nose!
            Core.KillQuest(327, "pines", "Pine Troll", FollowupIDOverwrite: 344);
        if (!Core.QuestProgression(323, FollowupIDOverwrite: 344))                                                      // Give Snowbeard His Gold
            Core.EnsureComplete(323);
        //Map: Dwarfhold
        Core.MapItemQuest(344, "dwarfhold", 60, FollowupIDOverwrite: 331);                                              // Bad Memory
        Core.KillQuest(331, "mountainpath", "Ore Balboa");                                                              // Squeeze Water from Stone
        Core.KillQuest(332, "mountainpath", "Vultragon");                                                               // Carrion Carrying On
        Core.KillQuest(333, "dwarfhold", "Chaos Drow");                                                                 // Bagged Lunch
        Core.KillQuest(334, "dwarfhold", "Glow Worm");                                                                  // Radiant Lamps
        Core.KillQuest(335, "dwarfhold", "Albino Bat");                                                                 // Having a Blast
        Core.KillQuest(336, "dwarfhold", "Chaotic Draconian");                                                          // Secret Weapons
        Core.MapItemQuest(337, "dwarfhold", 59, 7);                                                                     // Rock Star
        Core.KillQuest(338, "dwarfhold", "Chaos Drow");                                                                 // All that Glitters
        Core.KillQuest(339, "dwarfhold", "Chaotic Draconian");                                                          // Gemeralds
        Core.KillQuest(340, "dwarfhold", "Albino Bat");                                                                 // Talc to Me
        if (!Core.QuestProgression(343, FollowupIDOverwrite: 341))                                                        // Upper City Gates
        {
            Core.Join("dwarfhold", "rdoor", "Right");
            Core.EnsureComplete(343);
            Bot.Sleep(2500);
        }
        Core.KillQuest(341, "dwarfhold", "Amadeus", FollowupIDOverwrite: 346);                                          // Rock me Amadeus
        //Map: UpperCity
        Core.MapItemQuest(346, "uppercity", 61);                                                                        // Disapoofed
        Core.KillQuest(347, "uppercity", "Drow Assassin");                                                              // Hoodwinked
        Core.KillQuest(348, "uppercity", "Chaotic Draconian");                                                          // Claws for the Cause
        Core.KillQuest(349, "uppercity", "Chaos Egg");                                                                  // Scrambled Eggs
        Core.KillQuest(350, "uppercity", "Terradactyl");                                                                // The King's Wings
        Core.KillQuest(351, "uppercity", "Rhino Beetle");                                                               // Bugging Out
        Core.KillQuest(352, "uppercity", "Cave Lizard");                                                                // Lizard Gizzard
        if (!Core.QuestProgression(1, FollowupIDOverwrite: 353))                                                        // Confront Vath
        {
            Core.Join("vath");
            Bot.Player.Jump("CutCap", "Left");
            Bot.Sleep(2500);
        }
        Core.KillQuest(353, "dwarfprison", new[] {"Balboa", "Albino Bat", "Chaos Drow"});                               // Mock the Lock
        if (!Core.QuestProgression(354, FollowupIDOverwrite: 355))                                                        // Like Butter
        {
            Core.Join("dwarfprison", "Enter", "Right");
            Core.EnsureComplete(354);
        }
        Core.KillQuest(355, "dwarfprison", "Warden Elfis", FollowupIDOverwrite: 357);                                   // Jailhouse Rock
        if (!Core.CheckInventory("Tee-En-Tee"))
            Core.KillQuest(356, "dwarfprison", new[] {"Balboa", "Albino Bat", "Chaos Drow"}, FollowupIDOverwrite: 362); // Explosives 101
        if (!Core.QuestProgression(357, FollowupIDOverwrite: 362))                                                      // Big Bada-Boom
        {
            Core.Join("dwarfprison");
            Core.EnsureComplete(357);
        } 
        //Map: Roc
        Core.MapItemQuest(362, "roc", 62);                                                                              // Defeat Rock Roc
        //Map: Stalagbite
        Core.MapItemQuest(363, "stalagbite", 63, hasFollowup: false);                                                   // Facing Vath

        Core.Relogin();
        Core.BuyItem("battleon", 947, "Volcanic Fire Sword");
        Bot.Sleep(700);
        Core.ToBank("Volcanic Fire Sword");
    }
}
