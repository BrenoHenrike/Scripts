//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
using Skua.Core.Interfaces;

public class ArchFiend
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();
    public CoreNSOD NSoD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        GetArchfiend();

        Core.SetOptions(false);
    }

    public void GetArchfiend(bool rankUp = true)
    {
        if (Core.CheckInventory("ArchFiend"))
            return;

        AbyssalContract();

        Core.BuyItem("tercessuinotlim", 695, "ArchFiend");
        if (rankUp)
            Adv.rankUpClass("ArchFiend");
    }

    public void AbyssalContract()
    {
        if (Core.CheckInventory("Abyssal Contract"))
            return;

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Abyssal Contract");

        Farm.Experience(50);
        Core.EnsureAccept(8476);

        Nation.FarmUni13(3);

        if (Bot.Player.Gold >= 2000000)
        {
            Core.BuyItem("tercessuinotlim", 1951, "Receipt of Swindle", 6);
            Core.BuyItem("tercessuinotlim", 1951, "Pink Star Diamond of Nulgath");
        }
        else Core.HuntMonster("guru", "Guru Chest", "Pink Star Diamond of Nulgath", 1, false);

        Core.HuntMonster("mercutio", "Mercutio", "Immortal Joe's Black Star", 1, false);

        if (!Core.CheckInventory("Abyssal Star"))
        {
            Nation.FarmDarkCrystalShard(200);
            Nation.SwindleBulk(300);
            Nation.FarmGemofNulgath(200);
            Core.BuyItem("evilwarnul", 456, "Abyssal Star");
        }

        Adv.BuyItem("tercessuinotlim", 1951, "Gold Star of Avarice");

        if (!Core.CheckInventory("Blood Star of the Archfiend"))
        {
            Nation.FarmBloodGem(20);
            Nation.FarmTotemofNulgath(8);
            if (!Core.CheckInventory("Sepulchure's DoomKnight Armor"))
                NSoD.RetrieveVoidAuras(2);
            else NSoD.VoidAuras(2);
            Nation.ApprovalAndFavor(0, 999);
            Core.BuyItem("shadowblast", 1206, "Blood Star of the Archfiend");
        }

        Core.HuntMonster("fiendshard", "Dirtlicker", "Dirtlicker Demoted", 1, false);

        Core.EnsureComplete(8476);
        Bot.Wait.ForPickup("Abyssal Contract");

    }
}