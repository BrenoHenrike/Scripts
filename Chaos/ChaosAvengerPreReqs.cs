//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class ChaosAvengerClass
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public Core13LoC LOC => new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetClass();

        Core.SetOptions(false);
    }

    public void GetClass()
    {
        //Progress Check
        if (Core.CheckInventory("Empowered Chaos Avenger's GreatSword"))
            return;

        //Preload Quests
        if (!Core.isCompletedBefore(3881))
            Story.PreLoad(this);

        //Complete 13LoC Saga
        LOC.Hero();

        Core.EnsureAccept(8301);
        //ParallelChaosAmulet();
        FragmentoftheDragon();
        FragmentofMountDoomskull();
        FragmentsoftheLordsA();
        FragmentsoftheLordsB();
        FragmentoftheQueen();
        CompleteandBuy();
    }

    public void ParallelChaosAmulet()
    {
        if (!Core.CheckInventory("Parallel Chaos Amulet"))
        {
            if (Core.CheckInventory("Champion Drakath Insignia", 20))
            {
                Core.BuyItem("championdrakath", 2055, "Parallel Chaos Amulet");
                return;
            }
            else if (Bot.Quests.IsAvailable(7979))
            {
                Core.EnsureAccept(8300);
                Adv.BestGear(GearBoost.Chaos);
                Core.EquipClass(ClassType.Solo);
                Core.KillMonster("championdrakath", "r2", "Left", "Champion Drakath", "Champion Drakath Defeated", publicRoom: true);
                Core.EnsureComplete(8300);
                ParallelChaosAmulet();
            }
        }
    }

    public void FragmentoftheDragon()
    {
        if (!Core.CheckInventory("Fragment of the Dragon"))
        {
            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("chaoslord", "r2", "Left", "*", "Fragment of the Dragon", isTemp: false, publicRoom: true);
        }

    }

    public void FragmentofMountDoomskull()
    {
        if (!Core.CheckInventory("Fragment of Mount Doomskull", 1300))
        {
            Core.EquipClass(ClassType.Farm);
            Adv.BestGear(GearBoost.Chaos);
            Core.HuntMonster("mountdoomskull", "Chaorrupted Rogue", "Fragment of Mount Doomskull", 1300, isTemp: false);
        }

    }

    public void FragmentsoftheLordsA()
    {
        if (!Core.CheckInventory("Fragments of the Lords A"))
        {
            Core.EquipClass(ClassType.Solo);
            bool Ledgermayne = true;

            //Escherion's Robe
            Core.KillEscherion("Escherion's Robe", publicRoom: true);

            //Vath's Chaotic Dragonlord Armor
            Core.KillVath("Vath's Chaotic Dragonlord Armor");
            
            //Chaos Shogun Armor
            Core.HuntMonster("kitsune", "Kitsune", "Chaos Shogun Armor", isTemp: false, publicRoom: true);

            //Wolfwing Armor
            Core.HuntMonster("wolfwing", "Wolfwing", "Wolfwing Armor", isTemp: false, publicRoom: true);

            //Discordia Armor
            Core.HuntMonster("palooza", "Chaos Lord Discordia", "Discordia Armor", isTemp: false, publicRoom: true);

            //Ledgermayne (Armor)
            Core.HuntMonster("Ledgermayne", "Ledgermayne", "Ledgermayne", isTemp: !Ledgermayne, publicRoom: Ledgermayne);

            Core.BuyItem("championdrakath", 2055, "Fragments of the Lords A");
        }
    }

    public void FragmentsoftheLordsB()
    {
        if (!Core.CheckInventory("Fragments of the Lords B"))
        {
            Core.EquipClass(ClassType.Solo);

            //Tibicenas (Armor)
            Core.HuntMonster("djinn", "Tibicenas", "Tibicenas", isTemp: false, publicRoom: true);

            //Soul of Chaos Armor
            Core.HuntMonster("dreamnexus", "Khasaanda", "Soul of Chaos Armor", isTemp: false, publicRoom: true);

            //Iadoa (Armor)
            Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Iadoa", isTemp: false, publicRoom: true);

            //Chaos Lionfang Armor
            Core.HuntMonster("stormtemple", "Chaos Lord Lionfang", "Chaos Lionfang Armor", isTemp: false, publicRoom: true);

            //Chaos Lord Alteon (Armor)
            Core.HuntMonster("swordhavenfalls", "Chaos Lord Alteon", "Chaos Lord Alteon", isTemp: false, publicRoom: true);

            //Xiang Chaos
            Adv.GearStore();
            Core.KillXiang("Xiang Chaos", publicRoom: true);
            Adv.GearStore(true);

            Core.BuyItem("championdrakath", 2055, "Fragments of the Lords B");
        }
    }

    public void FragmentoftheQueen()
    {
        if (!Core.CheckInventory("Fragment of the Queen", 13))
        {
            Core.EquipClass(ClassType.Solo);
            Bot.Quests.UpdateQuest(8094);
            Core.HuntMonster("transformation", "Queen of Monsters", "Fragment of the Queen", 13, false, publicRoom: true);
        }

    }

    public void CompleteandBuy()
    {
        if (Bot.Quests.CanComplete(8301))
        {
            Core.EnsureComplete(8301);
            Core.Relogin();
            Core.BuyItem("championdrakath", 2056, "Chaos Avenger");
        }
        else if (!Core.CheckInventory("Champion Drakath Insignia", 20))
            Core.Logger("You need 20x Champion Drakath Insignia for the Parallel Chaos Amulet (You can get 5x once a week)");
    }
}