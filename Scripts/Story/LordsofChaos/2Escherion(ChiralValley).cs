//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaChiralValley
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;

        CompleteSaga();

        Core.SetOptions(false);
    }

    public void CompleteSaga()
    {
        Core.BuyItem("battleon", 946, "Phoenix Hunter");
        if (Core.CheckInventory("Phoenix Hunter", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Phoenix Hunter");
            Core.Logger("Chapter: \"Chaos Lord Escherion\" already complete. Skipping");
            return;
        }

        //Map: Mobius
        Core.KillQuest(245, "mobius", "Chaos Sp-Eye");                                  // Winged Spies
        Core.MapItemQuest(246, "mobius", 42, 5);                                        // Chaos Prisoners
        Core.KillQuest(247, "mobius", "Fire Imp", FollowupIDOverwrite: 260);            // IMP-possible Task
        Core.MapItemQuest(260, "mobius", 44, FollowupIDOverwrite: 248);                 // You Can't Miss It
        Core.KillQuest(248, "mobius", "Cyclops Raider");                                // Far Sighted
        Core.KillQuest(249, "mobius", "Slugfit");                                       // Slugfest
        //Map: Faerie
        Core.KillQuest(250, "faerie", "Chainsaw Sneevil");                              // Chain Reaction
        Core.MapItemQuest(251, "faerie", 43, 7);                                        // Epic Drops
        Core.KillQuest(252, "faerie", "Chainsaw Sneevil", FollowupIDOverwrite: 255);    // Jarring Theft
        Core.KillQuest(255, "faerie", "Cyclops Warlord");                               // Tree Hugger
        Core.KillQuest(256, "faerie", "Aracara");                                       // The Second Piece
        //Map: Cornelis
        Core.KillQuest(257, "cornelis", "Gargoyle", FollowupIDOverwrite: 261);          // Ruined Ruins
        Core.MapItemQuest(261, "cornelis", 45, FollowupIDOverwrite: 258);               // Energize!
        Core.KillQuest(258, "cornelis", "Gargoyle", FollowupIDOverwrite: 262);          // Blueish Glow
        Core.MapItemQuest(262, "cornelis", 46, FollowupIDOverwrite: 259);               // Quickdraw
        Core.KillQuest(259, "cornelis", "Stone Golem", FollowupIDOverwrite: 263);       // Arm Yourself
        Core.MapItemQuest(263, "cornelis", 47, FollowupIDOverwrite: 266);               // You've Been Framed
        //Map: Mobius
        Core.MapItemQuest(266, "mobius", 48);                                           // Some Assembly Required
        //Map: Cornelis
        Core.MapItemQuest(267, "mobius", 49, FollowupIDOverwrite: 264);                 // Teleporter Report
        Core.KillQuest(264, "mobius", "Cyclops Raider");                                // Disguise!
        Core.KillQuest(265, "faerie", "Chainsaw Sneevil", FollowupIDOverwrite: 268);    // To-go box
        //Map: Relativity
        Core.KillQuest(268, "relativity", "Cyclops Raider");                            // Find the Key! (Part One)
        Core.KillQuest(269, "relativity", "Fire Imp");                                  // Find the Key! (Part Two)
        Core.KillQuest(270, "relativity", "Head Gargoyle");                             // Find the Key! (Part Three)
        //Map: Hydra
        Core.MapItemQuest(271, "hydra", 50);                                            // The Lake Hydra
        Core.MapItemQuest(271, "hydra", 51);
        Core.MapItemQuest(271, "hydra", 52);
        //Map: Escherion
        if (!Core.QuestProgression(272, hasFollowup: false))  // Escherion
        {
            Core.EnsureAccept(272);
            Core.KillEscherion();
            Core.EnsureComplete(272);
        }

        Core.Relogin();
        Core.BuyItem("battleon", 946, "Phoenix Hunter");
        Bot.Sleep(700);
        Core.ToBank("Phoenix Hunter");
    }
}
