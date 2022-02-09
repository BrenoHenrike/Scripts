//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

using RBot;

public class MysteriousStranger
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SmartPick();

        Core.SetOptions(false);
    }

    public void SmartPick()
    {
        if (Core.CheckInventory("Golden Blade of Fate"))
        {
            Core.Logger("Golden Blade of Fate Already Owned");
            return;
        }
        else if (Bot.Quests.IsUnlocked(5679))
            GoldenBladeofFate();
        else Storyline();
    }

    public void Storyline()
    {
        Core.AddDrop("Golden Blade of Fate", "Purification Dust");
        Core.Logger("Doing Storyline");

        // The Lost Teacher
        // Blade Fragments x6
        // Dropped by Horc Tutor Trainer 
        Core.KillQuest(QuestID: 5669, MapName: "tutor", MonsterName: "Horc Tutor Trainer", AutoCompleteQuest: true);

        // Big Gold Coins
        // Big Gold Coin x1
        // Dropped by Piggy Drake
        Core.KillQuest(QuestID: 5670, MapName: "prison", MonsterName: "Piggy Drake", AutoCompleteQuest: true);

        // Light as a Feather
        // A Feather x1
        // Dropped by Phedra
        Core.KillQuest(QuestID: 5671, MapName: "lavarun", MonsterName: "Phedra", AutoCompleteQuest: true);

        // Shard Shard Shard
        // Armor Fragment x1
        // Dropped by Chaorrupted Armor
        Core.KillQuest(QuestID: 5672, MapName: "chaoscrypt", MonsterName: "Chaorrupted Armor", AutoCompleteQuest: true);

        // White Scales, Light Scales
        // White Scales x100
        Core.KillQuest(QuestID: 5673, MapName: "j6", MonsterName: "Sketchy Frogzard", AutoCompleteQuest: true);

        // The Stench of Defeat
        // Strong Scent x3
        // Click the blue arrows around the MapName in Orc Path
        Core.MapItemQuest(QuestID: 5674, MapName: "orcpath", MapItemID: 5143, Amount: 3, AutoCompleteQuest: true);

        // If you can't stand the heat...
        // Necrotic Shard x1
        // Dropped by Red Dragon (Monster)
        Core.KillQuest(QuestID: 5675, MapName: "lair", MonsterName: "Red Dragon", AutoCompleteQuest: true);

        // The Depths of Despair
        // Ancient Skull x1
        // Click the blue arrow in Well
        Core.MapItemQuest(QuestID: 5676, MapName: "Well", MapItemID: 5144, AutoCompleteQuest: true);

        // All Things Green and Small...
        // Purification Dust x100
        Core.KillQuest(QuestID: 5677, MapName: "cellar", MonsterName: "GreenRat", AutoCompleteQuest: true);

        // Doom... Or Redemption?
        // Dark Spirit x1
        // Dropped by Dark Sepulchure
        Core.KillQuest(QuestID: 5678, MapName: "sepulchure", MonsterName: "Dark Sepulchure", AutoCompleteQuest: true);

        // The Mysterious Reward
        // Stranger Found x1
        // Talk to the Mysterious Stranger (NPC) in Yulgar's Inn
        Core.MapItemQuest(QuestID: 5679, MapName: "yulgar", MapItemID: 5145, AutoCompleteQuest: true);

    }

    public void GoldenBladeofFate()
    {
        Core.AddDrop("Golden Blade of Fate");

        Core.Logger(" Obtaining Golden Blade of Fate");

        Core.EnsureAccept(5679);
        Core.GetMapItem(5145, 1, "yulgar");
        Core.EnsureComplete(5679);
    }
}