//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaPrologue 
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteSaga();

        Core.SetOptions(false);
    }

    public void CompleteSaga()
    {
        Core.BuyItem("battleon", 944, "Ascended Avatar's Blade");
        if (Core.CheckInventory("Ascended Avatar's Blade", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Ascended Avatar's Bladed");
            Core.Logger("Chapter: \"Prologue\" already complete. Skipping");
            return;
        }

        //Map: PortalUndead
        Core.KillQuest(183, "portalundead", "Skeletal Fire Mage", FollowupIDOverwrite: 176);                        // Enter the gates
        //Map: SwordhavenUndead
        Core.KillQuest(176, "swordhavenundead", "Skeletal Soldier");                                                // Undead Assault
        Core.KillQuest(177, "swordhavenundead", "Skeletal Ice Mage");                                               // Skull Crusher Mountain
        Core.KillQuest(178, "swordhavenundead", "Undead Giant");                                                    // The Undead Giant
        //Map: CastleUndead
        Core.MapItemQuest(179, "castleundead", 38, 5);                                                              // Talk to the Knights
        Core.KillQuest(180, "castleundead", "*", FollowupIDOverwrite: 196);                                         // Defend the Throne
        if (!Core.QuestProgression(1, FollowupIDOverwrite: 196))                                                    // The Arrival of Drakath cutscene
        {
            Bot.Player.Join("castleundead", "King2", "Center");
            Bot.SendPacket($"%xt%zm%updateQuest%188220%41%{(Core.HeroAlignment > 1 ? 1 : Core.HeroAlignment)}%");
            Bot.Sleep(2000);
            Bot.Player.Join("shadowfall");
            Bot.Sleep(2000);
        }
        //Map: ChaosCrypt
        Core.KillQuest(196, "chaoscrypt", "Chaorrupted Armor", FollowupIDOverwrite: 6216);                          // Recover Sepulchure's Cursed Armor!
        //Map: Prison
        Core.MapItemQuest(6216, "prison", 39, 5, FollowupIDOverwrite: 6218);                                        // Unlife Insurance
        Core.BuyQuest(6216, "prison", 1559, "Unlife Insurance Bond", FollowupIDOverwrite: 6218);
        //Map: Multiple
        Core.MapItemQuest(6217, "chaoscrypt", 5662);                                                                // Enter the Crypt
        Core.KillQuest(6218, "chaoscrypt", "Chaorrupted Knight");                                                   // Rescue the Knights
        Core.KillQuest(6219, "forestchaos", new[] {"Chaorrupted Wolf", "Chaorrupted Bear"}, hasFollowup: false);    // Forest of Chaos

        Core.Relogin();
        Core.BuyItem("battleon", 944, "Ascended Avatar's Blade");
        Bot.Sleep(700);
        Core.ToBank("Ascended Avatar's Bladed");
    }
}