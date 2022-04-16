//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class LVLQuick
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        QuickLvl();

        Core.SetOptions(false);
    }

    public void QuickLvl(int Level = 100)
    {
        if (Bot.Player.Level == Level)
            return;

        Core.AddDrop("");

        if (Core.CheckInventory("Healer (rare)"))
            Core.Equip("Healer (rare)");

        else
        {
            Core.BuyItem("classhalla", 176, "Healer");
            Core.Equip("Healer");
        }

        Core.Join("IceStormArena", publicRoom: true);
        Adv.BestGear(GearBoost.exp);
        Core.Jump("r4", "Bottom");

        while (Bot.Player.Level < Level)
        {
            if (Bot.Map.PlayerCount < 6)
            {
                Core.Join("WhiteMap");
                Core.Join("IceStormArena", publicRoom: true);
                Core.Jump("r4", "Bottom");
            }

            Core.SendPackets($"%xt%zm%aggroMon%{Bot.Map.RoomID}%70%71%72%73%74%75%");
            Bot.Player.Attack("*");

            if (Bot.Player.CanUseSkill(4))
                Bot.Player.UseSkill(4);
            if (Bot.Player.CanUseSkill(3))
                Bot.Player.UseSkill(3);
            if (Bot.Player.CanUseSkill(2))
                Bot.Player.UseSkill(2);

            Bot.Sleep(Core.ActionDelay);
        }
    }
}