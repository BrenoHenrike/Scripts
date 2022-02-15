//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/7DeadlyDragons/MysteriousEgg.cs

using RBot;

public class GetSDD
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public MysteriousEgg Egg = new MysteriousEgg();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        ShadowDragonDefender();

        Core.SetOptions(false);
    }

    public void ShadowDragonDefender()
    {
        if (Core.CheckInventory("Shadow Dragon Defender"))
            return;
        Core.AddDrop("Shadow Dragon Defender", "Manticore Cub Pet");

        if (!Core.CheckInventory("Manticore Cub Pet"))
        {
            Egg.GetMysteriousEgg();
            if (!Bot.Quests.IsUnlocked(6902))
            {
                Core.EnsureAccept(6901);
                Core.KillMonster("volcano", "r5", "Left", "*", "Lava Golem Chunk", 10);
                Core.EnsureComplete(6901);
            }
            if (!Bot.Quests.IsUnlocked(6903))
            {
                Core.EnsureAccept(6902);
                Core.KillMonster("embersea", "r8", "Bottom", "*", "Ever-Hot Lava", 5);
                Core.EnsureComplete(6902);
            }
            if (!Bot.Quests.IsUnlocked(6904))
            {
                Core.EnsureAccept(6903);
                Core.KillMonster("ashfallcamp", "r7", "Left", "*", "Dragon Meat", 10);
                Core.EnsureComplete(6903);
            }
            if (!Bot.Quests.IsUnlocked(6905))
            {
                Core.EnsureAccept(6904);
                Core.KillMonster("gilead", "r3", "Left", "Water Elemental", "Elemental Water", 5);
                Core.EnsureComplete(6904);
            }
            if (!Bot.Quests.IsUnlocked(6906))
            {
                Core.EnsureAccept(6905);
                Core.KillMonster("crossroads", "Enter", "Spawn", "Koalion", "Shaggy Mane");
                Core.EnsureComplete(6905);
            }
            if (!Bot.Quests.IsUnlocked(6907))
            {
                Core.EnsureAccept(6906);
                Core.KillMonster("mountain", "r5", "Left", "*", "Big Scorpion Tail");
                Core.EnsureComplete(6906);
            }
            if (!Bot.Quests.IsUnlocked(6908))
                Core.MapItemQuest(6907, "void", 6453, GetReward: false);
            if (!Bot.Quests.IsUnlocked(6909))
            {
                Core.EnsureAccept(6908);
                Core.KillMonster("void", "r11", "Left", "*", "Void Energy", 8);
                Core.EnsureComplete(6908);
            }
            if (!Bot.Quests.IsUnlocked(6910))
                Core.MapItemQuest(6909, "void", 6454, GetReward: false);
            if (!Bot.Quests.IsUnlocked(6911))
            {
                Core.EnsureAccept(6910);
                Core.KillMonster("void", "r9", "Left", "*", "Intact Void Husk");
                Core.EnsureComplete(6910);
            }
            if (!Bot.Quests.IsUnlocked(6912))
            {
                Core.EnsureAccept(6911);
                Core.HuntMonster("void", "Void Elemental", "Condensed Void", 6);
                Core.EnsureComplete(6911);
            }
            if (!Bot.Quests.IsUnlocked(6913))
            {
                Core.EnsureAccept(6912);
                Core.HuntMonster("void", "Void Dragon", "Void Soul");
                Core.EnsureComplete(6912);
            }
            if (!Bot.Quests.IsUnlocked(6914))
                Core.MapItemQuest(6913, "mysteriousegg", 6455, GetReward: false);
            Core.EnsureAccept(6914);
            Core.EnsureComplete(6914);
        }
        Core.BuyItem("mysteriousegg", 1728, "Shadow Dragon Defender");
    }

}