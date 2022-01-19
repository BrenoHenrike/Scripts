//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
using RBot;

public class EnhancedNulgathNationHouse
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Daily = new CoreDailys();
    public CoreNulgath Nulgath = new CoreNulgath();
    public CoreBLOD BLOD = new CoreBLOD();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop("Nulgath Nation House", "Enchanted Nulgath Nation House", "Cemaros' Amethyst", "Aluminum", "Pink Star Diamond of Nulgath", "Musgravite of Nulgath", "NUE Necronomicon");

        if (!Core.CheckInventory("Nulgath Nation House"))
        {
            Nulgath.FarmUni10(400);
            Nulgath.FarmUni13();
            Nulgath.FarmVoucher(false);
            Nulgath.FarmDiamondofNulgath(300);
            Nulgath.FarmDarkCrystalShard(250);
            Nulgath.FarmTotemofNulgath(30);
            Nulgath.FarmGemofNulgath(150);
            Nulgath.SwindleBulk(200);
            Nulgath.FarmBloodGem(100);
            Nulgath.ApprovalAndFavor(1000, 0);
    
            Farm.ChaosREP(2);
            Core.BuyItem("mountdoomskull", 776, "Cemaros' Amethyst");
    
            BLOD.UnlockMineCrafting();
            Daily.MineCrafting(new[] { "Aluminum" });
    
            Farm.DoomwoodREP();
            Farm.Gold(999);
            Core.BuyItem("lightguard", 277, "NUE Necronomicon");
    
            Core.EnsureAccept(4779);
            if(!Core.EnsureComplete(4779))
                Core.Logger("Could not complete the quest, stopping bot", messageBox: true, stopBot: true);
            bot.Player.Pickup("Nulgath Nation House");
        }

        Core.HuntMonster("guru", "Guru Chest", "Pink Star Diamond of Nulgath", 1, false);
        Core.HuntMonster("timelibrary", "Ancient Chest", "Musgravite of Nulgath", 2, false);
        Core.BuyItem("archportal", 1211, "Enchanted Nulgath Nation House");

        Core.SetOptions(false);
    }
}