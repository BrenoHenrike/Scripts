//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class FirewarGoldandXP
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FireWarGold();

        Core.SetOptions(false);
    }

    public void FireWarGold(int goldQuant = 100000000)
    {
        if (Bot.Player.Gold >= goldQuant)
            return;

        Core.AddDrop("");
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.gold);

        Core.Join("firewar", "r2", "Bottom");
        Bot.Player.SetSpawnPoint();
        while (Bot.Player.Gold < goldQuant && Bot.Player.Gold <= 100000000)
        {
            Bot.Player.Attack("*");

            if (Core.CheckInventory("Fire Dragon Scale", 5))
            {
                Bot.Quests.Complete(6294);
                Bot.Sleep(Core.ActionDelay);
            }
            if (Core.CheckInventory("Fire Dragon Heart", 3))
            {
                Bot.Quests.Complete(6295);
                Bot.Sleep(Core.ActionDelay);
            }
        }
    }
}