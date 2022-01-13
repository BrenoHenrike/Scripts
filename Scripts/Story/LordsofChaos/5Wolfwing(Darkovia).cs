//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaDarkovia
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
        Core.BuyItem("battleon", 944, "Sapphire Mace");
        if (Core.CheckInventory("Sapphire Mace", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Sapphire Mace");
            Core.Logger("Chapter: \"Chaos Lord Wolfwing\" already complete. Skipping");
            return;
        }
            
        //Map: DarkoviaGrave
        Core.MapItemQuest(494, "darkoviagrave", 97);                                                    // Grave Mission
        Core.KillQuest(495, "darkoviagrave", "Skeletal Fire Mage");                                     // Lending a Helping Hand
        Core.KillQuest(496, "darkoviagrave", "Rattlebones");                                            // Bone Appetit
        Core.KillQuest(497, "darkoviagrave", "Albino Bat");                                             // Batting Cage
        Core.KillQuest(498, "darkoviagrave", "Blightfang", FollowupIDOverwrite: 514);                   // His Bark is worse than his Blight
        //Map: GreenguardEast/West
        if (!Core.CheckInventory("Red's Big Wolf Slaying Axe") && !Bot.Quests.IsUnlocked(516))          // Can I axe you something?
        {
            Core.KillQuest(515, "greenguardeast", new[] {"Wolf", "Spider"});
            Core.KillQuest(515, "greenguardwest", new[] {"Frogzard", "Slime", "Big Bad Boar"});
        }
        //Map: DarkoviaForest
        if (!Core.QuestProgression(514, GetReward: false, FollowupIDOverwrite: 516))                    // Lil' Red
            Core.EnsureComplete(514);
        Core.KillQuest(516, "darkoviaforest", "Dire Wolf");                                             // A Dire Situation
        Core.KillQuest(517, "darkoviaforest", new[] {"Blood Maggot", "Blood Maggot", "Blood Maggot"});  // Blood, Sweat, and Tears
        Core.KillQuest(518, "darkoviaforest", "Lich of the Stone");                                     // What a Lich!
        //Map: Safiria
        Core.KillQuest(519, "safiria", "Blood Maggot");                                                 // Feeding Grounds
        Core.KillQuest(520, "safiria", "Albino Bat");                                                   //Going Batty
        Core.KillQuest(521, "safiria", "Chaos Lycan");                                                  //Lycan Knights
        Core.KillQuest(522, "safiria", "Twisted Paw", FollowupIDOverwrite: 534);                        //Twisted Paw
        //Map: Lycan
        Core.KillQuest(534, "lycan", "Dire Wolf");                                                      // A Gift Of Meat
        Core.KillQuest(535, "lycan", new[] {"Lycan", "Lycan Knight"});                                  // No Respect
        Core.KillQuest(536, "lycan", "Chaos Vampire Knight");                                           // Vampire Knights
        Core.KillQuest(537, "lycan", "Sanguine", FollowupIDOverwrite: 564);                             // Sanguine
        //Map: LycanWar
        if (!Core.QuestProgression(1, FollowupIDOverwrite: 564))                                        // Lycan War
        {
            Bot.Player.Join("lycanwar");
            Core.Jump("Boss", "Right");
            Bot.Sleep(5000);
            Core.Jump("Boss", "Left");
            Bot.Player.Kill("Edvard");
            Bot.Sleep(7000);
        }
        //Map: ChaosCave
        Core.MapItemQuest(564, "chaoscave", 107);                                                       // Search and Report
        Core.KillQuest(565, "chaoscave", "Werepyre");                                                   // The Key is the Key
        Core.KillQuest(566, "chaoscave", "Werepyre");                                                   // Secret Words
        Core.KillQuest(567, "chaoscave", "Dracowerepyre", hasFollowup: false);                          // Dracowerepyre
        //Map: Wolfwing
        Core.HuntMonster("wolfwing", "Wolfwing");                                                       // Wolfwing
        Bot.Sleep(3000);
        if (Core.HeroAlignment == 0)
            Core.EnsureComplete(597);
        else
            Core.EnsureComplete(598);

        Core.Relogin();
        Core.BuyItem("battleon", 944, "Sapphire Mace");
        Bot.Sleep(700);
        Core.ToBank("Sapphire Mace");
    }
}
