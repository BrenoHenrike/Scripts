//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoidHighlordsChallenge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();
    public CoreDailys Dailys = new CoreDailys();
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
        {
            Core.SetOptions();

            Farm.IcestormArena(80);
            Challenge();

            Core.SetOptions(false);
        }


    public void Challenge()
        {

            Core.AddDrop(Nulgath.bagDrops);
            Core.AddDrop(Nulgath.VHLDrops);
            

            if(!Core.CheckInventory("Hadean Onyx of Nulgath"))
            {
                Core.JoinTercessuinotlim();
                Core.Jump("m4", "Right");
                Bot.Player.KillForItem("Shadow of Nulgath", "Hadean Onyx of Nulgath", 1);
            }

            Nulgath.FarmVoucher(false);

            Core.EnsureAccept(5660);

            Farm.BlackKnightOrb();
            
            if(!Core.CheckInventory("Nulgath Shaped Chocolate"))
            {
                if (Bot.Player.Gold < 2000000)
                    Farm.Gold(2000000);
                Core.BuyItem("citadel", 44, 38316);
            }
            
            Dailys.EldersBlood();

            Core.BuyItem("yulgar", 16, "Aelita's Emerald");

            Nulgath.FarmUni13();

            Nulgath.FarmGemofNulgath(20);

            Nulgath.EmblemofNulgath(20);

            Nulgath.EssenceofNulgath(50);

            Nulgath.SwindleBulk(100);

            Nulgath.ApprovalAndFavor(300, 300);
            
            Bot.Sleep(Core.ActionDelay);
            if (Bot.Quests.CanComplete(5660))
                Core.EnsureComplete(5660);
            else
                Core.Logger("Couldn't complete the quest");
        }
}