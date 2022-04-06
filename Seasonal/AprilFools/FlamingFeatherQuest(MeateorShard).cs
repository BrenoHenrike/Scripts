//cs_include Scripts/CoreBots.cs
using RBot;

public class FlamingFeatherQuest
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    string[] Drops = {
    "Meateor Shard",
    "ChocoBlade of DOOM",
    "Slice of Awe",
    "Pocky Manslayer",
    "Skull With Stick",
    "Burger of Destiny",
    "Boxadin",
    "Boxadin Helm",
    "Boxadin Winged Helm",
    "Cardboard Cape",
    "Cardboard of Awe",
    "CAPuccino Hat",
    "CAPuccino Hat + Locks",
    "Chef's Toque",
    "Chef's Toque + Bangs",
    "Macintrobble Apple Pet"
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("Meateor Shard");

        Core.SetOptions();

        CompleteQuest();

        Core.SetOptions(false);
    }

    public void CompleteQuest()
    {
        Core.AddDrop(Drops);
        Core.AddDrop("Altar of Caladbacon");
        Bot.Options.AttackWithoutTarget = true;

        while (!Core.CheckInventory(Drops) & !Bot.Inventory.ContainsHouseItem("Altar of Caladbacon"))
        {
            Core.EnsureAccept(8605);
            Core.KillMonster("battleontown", "r9", "Right", "ChickenCow", "Flaming Feather", 25);
            // Core.HuntMonster("battleontown", "ChickenCow", "Flaming Feather", 25);
            Core.EnsureComplete(8605);
        }

        Bot.Options.AttackWithoutTarget = false;
    }
}