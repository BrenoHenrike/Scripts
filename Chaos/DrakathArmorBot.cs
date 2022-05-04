//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class DrakathArmorBot
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDailies Daily = new();
    public CoreBLOD BLOD = new();
    public CoreNulgath Nulgath = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBoth();

        Core.SetOptions(false);
    }

    public void GetBoth()
    {
        DrakathArmor();
        DrakathOriginalArmor();
    }

    public void DrakathArmor()
    {
        if (Core.CheckInventory("Drakath Armor"))
            return;
        Core.Logger("Attempting Purchase, will check if shop is unlocked. Next message may be incorrect.");
        Core.BuyItem("battleon", 994, "Drakath Armor");
        Bot.Sleep(Core.ActionDelay);
        if (Core.CheckInventory("Drakath Armor"))
            return;
        DrakathArmorQuest();
        Core.BuyItem("battleon", 994, "Drakath Armor");
    }

    public void DrakathOriginalArmor()
    {
        if (Core.CheckInventory("Original Drakath Armor"))
            return;
        Core.Logger("Attempting Purchase, will check if shop is unlocked. Next message may be incorrect.");
        Core.BuyItem("battleon", 994, "Original Drakath Armor");
        Bot.Sleep(Core.ActionDelay);
        if (Core.CheckInventory("Original Drakath Armor"))
            return;
        DrakathArmorQuest();
        Core.BuyItem("battleon", 994, "Original Drakath Armor");
    }

    public void DrakathArmorQuest()
    {
        Core.AddDrop("Dage's Scroll Fragment", "Treasure Chest", "Face of Chaos", "Get Your Original Drakath's Armor");
        if (!Core.CheckInventory("Dage's Scroll Fragment", 13))
        {
            Bot.Sleep(Core.ActionDelay);
            Daily.DagesScrollFragment();
        }
        if (!Core.CheckInventory("Dage's Scroll Fragment", 13))
            Core.Logger("You dont have Dage's Scroll Fragment x13", stopBot: true);
        Core.EnsureAccept(3882);
        Farm.BladeofAweREP(farmBoA: true);
        BLOD.DoAll();
        Nulgath.FarmUni13(3);
        Farm.Gold(3750000);
        Core.BuyItem("hyperspace", 194, "Le Chocolat");
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("swordhavenundead", "Left", "Right", "*", "Treasure Chest", 100, false);
        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("ultradrakath", "r1", "Left", "Champion of Chaos", "Face of Chaos", 1, false, publicRoom: true);
        Core.EnsureComplete(3882);
    }
}
