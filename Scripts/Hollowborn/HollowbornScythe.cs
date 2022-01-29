//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class HollowbornScythe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public string[] reqName =
    {
        "Hollow Soul",
        "Bone Dust",
        "Undead Energy",
        "Death's Oversight",
        "Incarnation of Glitches Scythe",
        "Unmolded Fiend Essence",
        "Hollowborn Reaper's Minion",
        "Hollowborn Reaper's Daggers",
        "Hollowborn Reaper's Kamas",
        "Hollowborn Reaper's Kama"
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AddDrop(reqName);

        Core.Unbank(reqName);

        //Minion
        if(!Core.CheckInventory("Hollowborn Reaper's Minion"))
        {
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("shadowrealm", "Hollowborn Sentinel", "Hollow Soul", 250, false);
            Farm.BattleUnderB("Bone Dust", 2000);
            Core.HuntMonster("shadowattack", "Death", "Death's Oversight", 2, false, publicRoom : true);
            Core.BuyItem("shadowrealm", 1889, "Hollowborn Reaper's Minion");
        }

        //Daggers, Kamas, Kama
        for (int i = 7; i < 10; i++)
        {
            if (!Core.CheckInventory(reqName[i]))
            {
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("shadowrealm", "Hollowborn Sentinel", "Hollow Soul", 250, false);
                Farm.BattleUnderB("Bone Dust", 3000);
                Core.HuntMonster("shadowattack", "Death", "Death's Oversight", 5, false, publicRoom : true);
                Core.Logger("Incarnation of Glitches Scythe (stop to buy back, ignore to farm)");
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("cathedral", "Incarnation of Time", "Incarnation of Glitches Scythe", 1, false, publicRoom : true);
                if (!bot.Inventory.Contains("Unmoulded Fiend Essence"))
                {
                    Farm.Gold(15000000);
                    Core.JoinTercessuinotlim();
                    Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
                    bot.Wait.ForItemBuy();
                }
                Core.BuyItem("shadowrealm", 1889, reqName[i]);
            } 
        }
        Core.Logger("All necessary items acquired");
        Core.SetOptions(false);
    }
}