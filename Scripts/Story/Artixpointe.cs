//cs_include Scripts/CoreBots.cs
using RBot;

public class Artixpointe
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        OmniArtifact();

        Core.SetOptions(false);
    }

    public void OmniArtifact()
    {
        if (Core.CheckInventory("Omni Artifact"))
            return;
        Core.EquipClass(ClassType.Solo);
        QuestProgress(3800, "artixpointe", "Corrupted Sushi Chef", "Unholy Wasabi");
        QuestMapItem(3801, "artixpointe", 2911, "Cysero's Doom Sock");
        QuestProgress(3802, "battleontown", "Chickencow", "Chickencow Claw");
        QuestProgress(3803, "graveyard", "Big Jack Sprat", "Zorbak's Staff Skull");
        QuestProgress(3804, "vendorbooths", "Dragon Khan", "Dragon Khan's Corrupt Scepter");
        QuestProgress(3805, "undergroundlabb", "Rabid Server Hamster", "Sir Ver's Broken Power Button");
        if (!Bot.Quests.IsUnlocked(3807))
            Core.SmartKillMonster(3806, "gravefang", "Gravefang", 1, true);
        QuestMapItem(3807, "artixpointe", 2912, "Death's Cursed Hourglass", false);
        Core.BuyItem("artixpointe", 1002, "Omni Artifact");
    }

    public void QuestProgress(int QuestID, string MapName, string MonsterName, string Item = null, bool hasFollowup = true)
    {
        if (Item != null)
        {
            if (Core.CheckInventory(Item))
                return;
            Core.AddDrop(Item);
        }
        else if (hasFollowup)
            if (Bot.Quests.IsUnlocked(QuestID+1))
                return;
        Core.SmartKillMonster(QuestID, MapName, MonsterName, 1, true);
    }

    public void QuestMapItem(int QuestID, string MapName, int MapItemID, string Item = null, bool hasFollowup = true)
    {
        if (Item != null)
        {
            if (Core.CheckInventory(Item))
                return;
            Core.AddDrop(Item);
        }
        else if (hasFollowup)
            if (Bot.Quests.IsUnlocked(QuestID+1))
                return;
        Core.EnsureAccept(QuestID);
        Core.GetMapItem(MapItemID, map: MapName);
        Core.EnsureComplete(QuestID);
    }
}
