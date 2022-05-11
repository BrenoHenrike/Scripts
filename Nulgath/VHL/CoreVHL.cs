//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/AssistingCragAndBamboozle[Mem].cs
using RBot;

public class CoreVHL
{
    // [Can Change]
    // True = When possible, it will use "Assisting Crag and Bamboozle" to get an additional Elders' Blood per day. Needs Crag and Bamboozle and is Legend-Only.
    // False = It will automatically check if you got the things, but if you want to turn this off either way, heres the option.
    // Recommended: true
    private bool UseSparrowMethod = true;

    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreDailies Daily = new();
    public CoreNulgath Nulgath = new CoreNulgath();
    public AssistingCragAndBamboozle ACAB = new AssistingCragAndBamboozle();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.RunCore();
    }

    private int EldersBloodAmount = ScriptInterface.Instance.Inventory.GetQuantity("Elders' Blood");

    public void GetVHL(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Void Highlord"))
            return;

        VHLChallenge(15);
        VHLCrystals();

        Core.BuyItem("tercessuinotlim", 1355, "Void Highlord");

        if (rankUpClass)
            Adv.rankUpClass("Void Highlord");
    }

    public void VHLChallenge(int quant)
    {
        if (Core.CheckInventory("Roentgenium of Nulgath", quant))
            return;

        if (Core.CBO_Active)
            UseSparrowMethod = Core.CBOBool("VHL_Sparrow");

        Core.Logger("Getting Void HighLord Challenge prerequisites");
        Farm.Experience(80);
        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop("Void Highlord Armor", "Helm of the Highlord", "Highlord's Void Wrap", "Roentgenium of Nulgath");

        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Hadean Onyx of Nulgath", 1, false);

		int CurrentRoent = Bot.Inventory.GetQuantity("Roentgenium of Nulgath");
		
        Core.Logger($"Obtaining Roentgenium of Nulgath x{quant - CurrentRoent}");
		
        while (!Core.CheckInventory("Roentgenium of Nulgath", quant))
        {
            Core.EnsureAccept(5660);

            Nulgath.FarmVoucher(false);
            Farm.BlackKnightOrb();
            if (!Core.CheckInventory("Nulgath Shaped Chocolate"))
            {
                Farm.Gold(2000000);
                Core.BuyItem("citadel", 44, 38316);
            }
            Core.BuyItem("yulgar", 16, "Aelita's Emerald");
            Nulgath.FarmUni13(1);
            Nulgath.FarmGemofNulgath(20);
            Nulgath.EmblemofNulgath(20);
            Nulgath.EssenceofNulgath(50);
            Nulgath.SwindleBulk(100);
            Nulgath.ApprovalAndFavor(300, 300);
			
			if(EldersBloodAmount == 0)
			{
				if (EldersBloodAmount < 5)
					Daily.EldersBlood();
				if(UseSparrowMethod)
					if (EldersBloodAmount < 5)
						SparrowMethod();
				if (!Core.CheckInventory("Elders' Blood"))
					Core.Logger($"Not enough \"Elders' Blood\", please do the daily {15 - EldersBloodAmount} more times (not today)", messageBox: true, stopBot: true);
			}
			
            Core.EnsureComplete(5660);
            Bot.Wait.ForPickup("Roentgenium of Nulgath");
        }


        Core.ToBank("Void Highlord Armor", "Helm of the Highlord", "Highlord's Void Wrap");
    }

    public void VHLCrystals()
    {
        if (Core.CheckInventory("Void Crystal A") && Core.CheckInventory("Void Crystal B"))
            return;

        if (Core.CBO_Active)
            UseSparrowMethod = Core.CBOBool("VHL_Sparrow");

        Core.Logger("Obtaining Void Crystal A & Void Crystal B");
        Core.AddDrop(Nulgath.bagDrops);

        Nulgath.FarmUni13(1);
        Nulgath.FarmUni10(200);
        Nulgath.FarmGemofNulgath(150);
        Nulgath.FarmDarkCrystalShard(200);
        Nulgath.FarmDiamondofNulgath(200);
        Nulgath.FarmBloodGem(30);
        Nulgath.FarmTotemofNulgath(15);
        Nulgath.SwindleBulk(200);
		
		if(EldersBloodAmount < 2)
		{
			Daily.EldersBlood();
			
			if(UseSparrowMethod)
				if (EldersBloodAmount < 2)
					SparrowMethod();
				
			if (!Core.CheckInventory("Elders' Blood", 2))
				Core.Logger($"Not enough \"Elders' Blood\", please do the daily {2 - EldersBloodAmount} more times (not today)", messageBox: true, stopBot: true);
		}

        Core.BuyItem("tercessuinotlim", 1355, "Void Crystal A");
        Core.BuyItem("tercessuinotlim", 1355, "Void Crystal B");
    }

    private void SparrowMethod()
    {
        Core.BankingBlackList.AddRange(new[] {"Nulgath Larvae",
                     "Sword of Nulgath", "Gem of Nulgath", "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath",
                     "Totem of Nulgath", "Blood Gem of the Archfiend", "Unidentified 19", "Elders' Blood", "Voucher of Nulgath", "Voucher of Nulgath (non-mem)"});
        Core.SetOptions();

        ACAB.AssistingCandB();

        Core.SetOptions(false);
    }
}