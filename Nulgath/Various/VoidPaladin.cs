//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Evil/NecroticSwordOfDoom.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
using RBot;

public class VoidPaladin
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();
    public NecroticSwordOfDoom NSOD = new NecroticSwordOfDoom();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.BankingBlackList.AddRange(ADKRewards);
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Core.AddDrop(Nulgath.bagDrops);

        ADarkTemptation();
        DeeperandDeeperintoDarkness();
        Sacrifice();
        CyberSet();

    }

    public readonly string[] ADKRewards =
    {
        "Void Paladin Helm",
        "Void Paladin Horns",
        "Void Paladin Katana Cape",
        "Void Paladin Cape",
        "Void Paladin Katana",
        "Void Paladin Katanas"
    };

    public void ADarkTemptation()
    {
        if (Core.CheckInventory(ADKRewards))
            return;
        Core.AddDrop(ADKRewards);
        Core.AddDrop("Scroll of Underworld", "Archmage Ink", "Mystic Shards");

        int i = 1;
        Core.Logger("Starting [A Dark Temptation] Quest");
        while (!Core.CheckInventory(ADKRewards, toInv: false))
        {
            Core.EnsureAccept(5826);

            Nulgath.FarmDarkCrystalShard(25);
            Nulgath.FarmDiamondofNulgath(13);
            Nulgath.EmblemofNulgath(2);
            Nulgath.SwindleBulk(35);
            Nulgath.FarmTotemofNulgath(1);
            Nulgath.FarmUni13();

            if (!Core.CheckInventory("Scroll of Underworld"))
            {
                if (!Core.CheckInventory("Archmage Ink"))
                {
                    Core.HuntMonster("underworld", "Skull Warrior", "Mystic Shards", 2, false);
                    Core.BuyItem("dragonrune", 549, "Archmage Ink", 1, 5);
                }
                Core.ChainComplete(2346);
                Bot.Player.Pickup("Scroll of Underworld");
            }
            Core.EnsureCompleteChoose(5826);
            Core.Logger($"Completed x{i++}");
        }
    }

    public void DeeperandDeeperintoDarkness()
    {
        if (Core.CheckInventory("Void Paladin"))
            return;

        Core.AddDrop("Void Paladin");
        Core.Logger("Starting [Deeper and Deeper into Darkness] Quest");

        Core.EnsureAccept(5827);

        Nulgath.FarmDiamondofNulgath(25);
        Nulgath.SwindleBulk(40);
        Nulgath.FarmVoucher(false);
        Nulgath.FarmGemofNulgath(25);
        Nulgath.FarmDarkCrystalShard(40);
        Nulgath.FarmUni13(2);

        if (!Core.CheckInventory("Nulgath Shaped Chocolate"))
        {
            Farm.Gold(2000000);
            Core.BuyItem("citadel", 44, "Nulgath Shaped Chocolate");
        }
        NSOD.VoidAuras(2);

        Core.EnsureComplete(5827);
        Bot.Player.Pickup("Void Paladin");
    }

    public void Sacrifice()
    {
        if (Core.CheckInventory("Void Light of Destiny"))
            return;

        Core.AddDrop("Void Light of Destiny");
        Core.Logger("Starting [Sacrifice] Quest");

        Nulgath.FarmDarkCrystalShard(40);
        Nulgath.FarmDiamondofNulgath(40);
        Nulgath.SwindleBulk(40);
        Nulgath.FarmGemofNulgath(20);
        Nulgath.EmblemofNulgath(3);
        Nulgath.FarmTotemofNulgath(1);
        NSOD.VoidAuras(6);

        if (Core.CheckInventory("Blinding Light of Destiny"))
            Core.ChainComplete(5828);
        else if (Core.CheckInventory("Ascended Light of Destiny"))
            Core.ChainComplete(5829);
        Bot.Player.Pickup("Void Light of Destiny");
    }

    public readonly string[] CyberVoidSet =
    {
        "Cyber Void Paladin",
        "Cyber Void Paladin Helm",
        "Cyber Void Cape",
        "Cyber Void Light of Destiny"
    };

    public void CyberSet()
    {
        if (Core.CheckInventory(CyberVoidSet, toInv: false))
            return;

        Core.AddDrop(CyberVoidSet);
        Core.CheckInventory(new[] { "Void Light of Destiny", "Void Paladin", "Void Paladin Helm", "Void Paladin Katana", "Void Paladin Katana Cape" });
        Core.EnsureAccept(6625);
        Core.HuntMonster("dreadspace", "Dread Space Warrior", "Powerpack", 5);
        Core.EnsureComplete(6625);
        Bot.Player.Pickup(CyberVoidSet);
    }
}