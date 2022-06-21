//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class WeaponMasteryAC
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);

        Core.SetOptions();

        GetWM();

        Core.SetOptions(false);
    }

    public void GetWM()
    {
        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop("Evolved Warlord Axe", "Evolved Warlord Hammer");
        if (Core.CheckInventory("Evolved Warlord Orb"))
        {
            if (Core.CheckInventory("Evolved Warlord Hammer") && Core.CheckInventory("Evolved Warlord Axe"))
            {
                Core.Logger($"Weapon Mastery archieved!");
                return;
            }
            int i = 1;
            while (!Bot.ShouldExit() && !Core.CheckInventory("Evolved Warlord Hammer") || !Core.CheckInventory("Evolved Warlord Axe"))
            {
                Core.EnsureAccept(4784);

                Nulgath.FarmUni13(1);

                Nulgath.SwindleBulk(10);

                Nulgath.FarmDarkCrystalShard(10);

                Nulgath.FarmTotemofNulgath(1);

                Nulgath.FarmGemofNulgath(10);

                if (!Core.CheckInventory("Underfriend Blade of Nulgath"))
                {
                    if (!Core.CheckInventory("Mirror Realm Token", 10))
                    {
                        Core.HuntMonster("BattleOff", "Evil Moglin", "Mirror Realm Token", 10, false);
                    }
                    if (Bot.Player.Gold <= 100000)
                    {
                        Farm.Gold(100000);
                    }
                    Core.BuyItem("mirrorportal", 618, "Underfriend Blade of Nulgath");
                }

                Core.EnsureComplete(4784);
                Core.Logger($"Completed x{i++}");
            }
        }
        else
        {
            Core.Logger($"Evolved Warlord Orb not found!");
            return;
        }
    }
}
//Made by ToxlcChain