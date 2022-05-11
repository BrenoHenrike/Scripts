//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Evil/NecroticSwordOfDoom.cs
//cs_include Scripts/Dailies/NSoDDaily.cs
using RBot;

public class ArchFiend
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreNulgath Nulgath = new CoreNulgath();
    public NecroticSwordOfDoom NSOD = new NecroticSwordOfDoom();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.SetOptions();

        GetArchfiend();

        Core.SetOptions(false);
    }

    public void GetArchfiend(bool rankUp = true)
    {
        if (Core.CheckInventory("ArchFiend"))
            return;

        if (!Core.CheckInventory("Abyssal Contract"))
        {
            Core.AddDrop(Nulgath.bagDrops);
            Core.AddDrop("Abyssal Contract");
            Farm.Experience(50);
            Core.EnsureAccept(8476);
            Nulgath.FarmUni13(3);
            Core.HuntMonster("guru", "Guru Chest", "Pink Star Diamond of Nulgath", 1, false);
            Core.HuntMonster("mercutio", "Mercutio", "Immortal Joe's Black Star", 1, false);
            if (!Core.CheckInventory("Abyssal Star"))
            {
                Nulgath.FarmDarkCrystalShard(200);
                Nulgath.SwindleBulk(300);
                Nulgath.FarmGemofNulgath(200);
                Core.BuyItem("evilwarnul", 456, "Abyssal Star");
            }
            if (!Core.CheckInventory("Gold Star of Avarice"))
            {
                Farm.Gold(19800000);
                Core.BuyItem("tercessuinotlim", 1951, "Receipt of Swindle", 16);
                Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
                Core.BuyItem("tercessuinotlim", 1951, "Gold Star of Avarice");
            }
            if (!Core.CheckInventory("Blood Star of the Archfiend"))
            {
                Nulgath.FarmBloodGem(20);
                Nulgath.FarmTotemofNulgath(8);
                if (!Core.CheckInventory("Sepulchure's DoomKnight Armor"))
                    NSOD.RetrieveVoidAuras(2);
                else NSOD.VoidAuras(2);
                Nulgath.ApprovalAndFavor(0, 999);
                Core.BuyItem("shadowblast", 1206, "Blood Star of the Archfiend");
            }
            Core.HuntMonster("fiendshard", "Dirtlicker", "Dirtlicker Demoted", 1, false);
            Core.EnsureComplete(8476);
            Bot.Wait.ForPickup("Abyssal Contract");
        }
        Core.BuyItem("tercessuinotlim", 695, "ArchFiend");
        if (rankUp)
            Adv.rankUpClass("ArchFiend");
    }
}