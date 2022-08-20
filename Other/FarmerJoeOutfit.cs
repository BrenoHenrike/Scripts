//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Hollowborn/HollowbornReapersScythe.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Enhancement/InventoryEnhancer.cs
//cs_include Scripts/Other/Classes/REP-based/EternalInversionist.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Other/Classes/DragonShinobi.cs
//cs_include Scripts/Good/GearOfAwe/CapeOfAwe.cs
//cs_include Scripts/Enhancement/InventoryEnhancer.cs

using RBot;
using RBot.Options;

public class FarmerJoeOutfit
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public HollowbornScythe Scythe = new();
    public EternalInversionist EI = new();
    public project InvEn = new();
    public ArchPaladin AP = new();
    public DragonShinobi DS = new();
    public CapeOfAwe COA = new();
    public project project = new();

    public string OptionsStorage = "FarmerJoePet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<PetChoice>("PetChoice", "Choose Your Pet", "Extra stuff to choose, if you have any suggestions -form in disc, and put it under request. or dm Tato(the retarded one on disc)", PetChoice.None),
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Outfit();

        Core.SetOptions(false);
    }

    public void Outfit()
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
            Bot.Config.Configure();

        //Pre-Farm Enh
        Adv.EnhanceEquipped(EnhancementType.Lucky);
        project.EnhanceInventory();

        //Easy Difficulty Stuff
        RagsandHat();
        ServersAreDown();
        Farm.Experience(50);
        Adv.EnhanceEquipped(EnhancementType.Lucky);

        //Medium Difficulty Stuff
        DS.GetDSS();
        EI.GetEI();
        AP.GetAP();
        COA.GetCoA();
        Farm.Experience(80);
        Adv.EnhanceEquipped(EnhancementType.Lucky);

        //Hard Difficulty Stuff
        Scythe.GetHBReapersScythe();
        Adv.EnhanceEquipped(EnhancementType.Lucky);
        Farm.Experience();

        //Extra Stuff
        Pets();

        Core.Equip(new[] { "Peasant Rags", "Scarecrow Hat", "The Server is Down", "Hollowborn Reaper's Scythe" });

        Core.Logger("We are farmers, bum ba dum bum bum bum bum");

    }

    public void Pets()
    {
        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.None)
            return;

        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.HotMama && !Core.CheckInventory("Hot Mama"))
        {
            Core.HuntMonster("battleundere", "Hot Mama", "Hot Mama", isTemp: false);
            Bot.Wait.ForPickup("Hot Mama");
            Core.Equip("Hot Mama");
        }

        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.Akriloth && !Core.CheckInventory("Akriloth Pet"))
        {
            Core.HuntMonster("gravestrike", "Ultra Akriloth", "Akriloth Pet", isTemp: false);
            Bot.Wait.ForPickup("Akriloth Pet");
            Core.Equip("Akriloth Pet");
        }
    }

    public void RagsandHat()
    {
        if (Core.CheckInventory("Peasant Rags") | Core.CheckInventory("Scarecrow Hat"))
            return;

        Core.Logger("Farming Rags & Hat");

        Adv.BuyItem("yulgar", 41, "Peasant Rags");
        Bot.Wait.ForPickup("Peasant Rags");
        Core.Equip("Peasant Rags");
        Adv.BuyItem("yulgar", 16, "Scarecrow Hat");
        Bot.Wait.ForPickup("Scarecrow Hat");
        Core.Equip("Scarecrow Hat");
    }

    public void ServersAreDown()
    {
        if (Core.CheckInventory("The Server is Down"))
            return;

        Core.Logger("Farming Servers Are Down Sign");
        Core.HuntMonster("undergroundlabb", "Rabid Server Hamster", "The Server is Down", isTemp: false);
        Bot.Wait.ForPickup("The Server is Down");
        Core.Equip("The Server is Down");
    }
    
    public enum PetChoice
    {
        None,
        HotMama,
        Akriloth
    };
}