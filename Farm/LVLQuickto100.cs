//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs

using RBot;
public class LVLQuick
{
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();



        if (bot.Player.Level == 100)
            return;

        if (!Core.CheckInventory("Healer"))
            Core.BuyItem("classhalla", 176, "Healer");

        Core.Join("IceStormArena", publicRoom: true);

        Core.Equip("Healer");

        Adv.BestGear(GearBoost.exp);

        Core.Jump("r4", "Bottom");

        while (bot.Player.Level < 100)
        {
            Core.SendPackets("%xt%zm%aggroMon%134123%70%71%72%73%74%75%");
            bot.Player.Attack("*");
            bot.Player.UseSkill(4);
            bot.Player.UseSkill(3);
            bot.Player.UseSkill(2);
            bot.Sleep(Core.ActionDelay);
        }

        Core.SetOptions(false);
    }
}