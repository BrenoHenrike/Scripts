//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Hollowborn/HollowbornReapersScythe.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Enhancement/InventoryEnhancer.cs
//cs_include Scripts/Other/Classes/REP-based/EternalInversionist.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs

using RBot;

public class FarmerJoeOutfit
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreFarms Farm = new CoreFarms();
    public HollowbornScythe Scythe = new HollowbornScythe();
    public EternalInversionist EI = new();
    public project InvEn = new();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Outfit();

        Core.SetOptions(false);
    }

    public void Outfit()
    {
        RagsandHat();
        ServersAreDown();
        if (Bot.Player.Level < 50)
        {
            Adv.EnhanceEquipped(EnhancementType.Lucky);
            Core.Logger($"{Bot.Player.Level} / 50, Leveling {100 - Bot.Player.Level} Levels");
            Farm.Experience(50);
        }
        if (!Core.CheckInventory("Eternal Inversionist"))
        {
            Adv.EnhanceEquipped(EnhancementType.Lucky);
            Core.Logger("Aquiring Eternal Inversionist");
            EI.GetEI();
        }
        if (Bot.Player.Level < 100)
        {
            Adv.EnhanceEquipped(EnhancementType.Lucky);
            Core.Logger($"{Bot.Player.Level} / 100, Leveling {100 - Bot.Player.Level} Levels");
            Farm.Experience();
        }
        if (!Core.CheckInventory("Hollowborn Reaper's Scythe"))
        {
            Core.Logger("Aquiring Hollowborn Reaper's Scythe");
            Scythe.GetHBReapersScythe();
            Adv.EnhanceEquipped(EnhancementType.Lucky);
        }
        // DeathsScythe();
        // BackItem();
        // Pet();

        Core.Equip(new[] { "Peasant Rags", "Scarecrow Hat", "The Server is Down", "Hollowborn Reaper's Scythe" });

        Core.Logger("We are farmers, bum ba dum bum bum bum bum", messageBox: true);

    }

    public void RagsandHat()
    {
        if (Core.CheckInventory("Peasant Rags") | Core.CheckInventory("Scarecrow Hat"))
            return;

        Core.Logger("Farming Rags & Hat");

        Adv.BuyItem("yulgar", 41, "Peasant Rags");
        Bot.Wait.ForPickup("Peasant Rags");
        Adv.BuyItem("yulgar", 16, "Scarecrow Hat");
        Bot.Wait.ForPickup("Scarecrow Hat");
    }

    public void ServersAreDown()
    {
        if (Core.CheckInventory("The Server is Down"))
            return;

        Core.Logger("Farming Servers Are Down Sign");
        Core.HuntMonster("undergroundlabb", "Rabid Server Hamster", "The Server is Down", isTemp: false);
        Bot.Wait.ForPickup("The Server is Down");
    }

    // public void DeathsScythe()
    // {

    //     if (Core.CheckInventory(25117))
    //         return;


    //     while (!Bot.ShouldExit() && !Core.CheckInventory(25117))
    //         Bot.Player.Hunt("Death");
    // }

    // public void Pet()
    // {
    //     if (Core.CheckInventory("item"))
    //         return;

    // }


}