//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/SevenCircles(War).cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs

using RBot;

public class DageChallengeMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreFarms Farm = new CoreFarms();
    public SevenCircles Circles = new SevenCircles();

    string[] Items1 = {
     "Underworld Medal",
    "Underworld Laurel",
    "Underworld Accolade"
    };

    string[] Items2 = {
    "Avarice of the Legion's Scythe",
    "Virgil of the Legion's Staff",
    "Avarice of the Legion",
    "Luxuria of the Legion",
    "Virgil of the Legion",
    "Avarice of the Legion's Helm",
    "Virgil of the Legion's Helm",
    "Avarice of the Legion's Scarf",
    "Eye of Luxuria Runes",
    "Virgil of the Legion's Cape",
    "Underworld Blade of DOOM",
    "Wrath of the Legion",
    "Wrath of the Legion's Cloak"
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Adv.BestGear(GearBoost.Undead, true);
        SoulForge();
        ObsidianRock();
        Circles.CirclesWar();
        DageQuests();
        Merge(true);//edit true > false  if u want a specific merge item from "items2" above, and then put teh itme in quotation marks ex: Merge(false, "exampleitem"); (proper caps is reqired.)

        Core.SetOptions(false);
    }

    public void SoulForge()
    {
        if (Core.CheckInventory("SoulForge Hammer"))
            return;

        Core.AddDrop("SoulForge Hammer");

        Core.AddDrop("Zardman's StoneHammer", "Iron Hammer", "Elemental Rock Hammer", "SoulForge Hammer");
        Core.EnsureAccept(2741);
        Core.HuntMonster("Forest", "Zardman Grunt", "Zardman's StoneHammer", isTemp: false);
        Core.HuntMonster("shadowfall", "Skeletal Warrior", "Iron Hammer", isTemp: false);
        Core.HuntMonster("bludrut", "Rock Elemental", "Elemental Rock Hammer", isTemp: false);
        Core.EnsureComplete(2741);
    }

    public void ObsidianRock(int quant = 10)
    {
        if (Core.CheckInventory("Obsidian Rock", quant))
            return;

        Core.AddDrop("Obsidian Rock");

        Core.EquipClass(ClassType.Farm);
        while (!Core.CheckInventory("Obsidian Rock", 10))
        {
            Core.EnsureAccept(2742);
            Core.HuntMonster("hydra", "Fire Imp", "Obsidian Deposit", 10);
            Core.EnsureComplete(2742);
            Bot.Wait.ForPickup("Obsidian Rock");
        }
    }

    public void DageQuests()
    {
        if (Core.isCompletedBefore(8546))
            return;

        Core.AddDrop("Underworld Medal", "Underworld Laurel", "Underworld Accolade");

        if (!Story.QuestProgression(8544))
        {
            Core.EquipClass(ClassType.Solo);
            //Training with Dage
            Core.EnsureAccept(8544);
            Core.HuntMonster("Dage", "Dage the Evil", "Dage Dueled");
            Core.EnsureComplete(8544);
            Bot.Wait.ForPickup("Underworld Laurel");
        }

        if (!Story.QuestProgression(8545))
        {
            Core.AddDrop("Underworld Medal", "Souls of Heresy", "Dage's Favor");
            Core.EquipClass(ClassType.Farm);

            //Darkness for Darkness'Sake
            Core.EnsureAccept(8545);
            if (!Core.CheckInventory("Dage's Favor", 200))
                Core.HuntMonster("underworld", "Legion Fenrir", "Dage's Favor", 200, isTemp: false);
            ObsidianRock();
            SoH();
            Core.EnsureComplete(8545);
            Bot.Wait.ForPickup("Underworld Medal");
        }

        if (!Story.QuestProgression(8546))
        {
            //Power of the Undead Legion
            Core.EnsureAccept(8546);
            Core.HuntMonster("legionarena", "legion fiend rider", "Fiend Rider's Approval");
            Core.HuntMonster("frozenlair", "lich lord", "Lich Lord's Approval");
            Core.HuntMonster("dagefortress", "Grrrberus", "Grrrberus's Grr Grrr");
            Core.EnsureComplete(8546);
            Bot.Wait.ForPickup("Underworld Accolade");
        }
    }


    public void MergeMaterials(String? item = null, int quant = 1000)
    {
        int i = quant - Bot.Inventory.GetQuantity(item);
        int z = 1;

        if (Core.CheckInventory(item, quant))
            return;

        if (item != null)
            Core.AddDrop(item);

        if (item == "Underworld Laurel")
        {
            while (!Core.CheckInventory("Underworld Laurel", quant))
            {
                Core.Logger($"Farming {item}, {quant - Bot.Inventory.GetQuantity(item)}/{quant}");
                Core.EnsureAccept(8544);
                Core.HuntMonster("Dage", "Dage the Evil", "Dage Dueled");
                Core.EnsureComplete(8544);
                Bot.Wait.ForPickup("Underworld Laurel");
                Core.Logger($"Quest Completed x{z++} times");
            }
        }

        if (item == "Underworld Medal")
        {

            while (!Core.CheckInventory("Underworld Medal", quant))
            {
                Core.Logger($"Farming {item}, {quant - Bot.Inventory.GetQuantity(item)}/{quant}");

                Core.EquipClass(ClassType.Farm);

                Core.EnsureAccept(8545);
                if (!Core.CheckInventory("Dage's Favor", 200))
                    Core.HuntMonster("underworld", "Legion Fenrir", "Dage's Favor", 200, isTemp: false);
                ObsidianRock();
                SoH(30);
                Core.EnsureComplete(8545);
                Bot.Wait.ForPickup("Underworld Medal");
                Core.Logger($"Quest Completed x{z++} times");
            }
        }

        if (item == "Underworld Accolade")
        {
            while (!Core.CheckInventory("Underworld Accolade", quant))
            {
                Core.Logger($"Farming {item}, {quant - Bot.Inventory.GetQuantity(item)}/{quant}");
                Core.EnsureAccept(8546);
                Core.HuntMonster("legionarena", "legion fiend rider", "Fiend Rider's Approval");
                Core.HuntMonster("frozenlair", "lich lord", "Lich Lord's Approval");
                Core.HuntMonster("dagefortress", "Grrrberus", "Grrrberus's Grr Grrr");
                Core.EnsureComplete(8546);
                Bot.Wait.ForPickup("Underworld Accolade");
                Core.Logger($"Quest Completed x{z++} times");
            }
        }
    }

    public void SoH(int quant = 30)
    {
        int z = 1;

        if (Core.CheckInventory("Souls of Heresy", quant))
            return;

        Core.AddDrop("Souls of Heresy");
        Core.SendPackets($"%xt%zm%getQuests%{Bot.Map.Name}%7983%%7980%%7981%");
        Core.EquipClass(ClassType.Farm);

        while (!Core.CheckInventory("Souls of Heresy", quant))
        {
            Core.Logger($"Farming Souls of Heresy, {quant - Bot.Inventory.GetQuantity("Souls of Heresy")}/{quant}");
            Core.EnsureAccept(7980, 7981, 7983);
            while (Core.CheckInventory("Mega War Medal", 3) || Core.CheckInventory("War Medal", 5) && !Core.CheckInventory("Souls of Heresy", quant))
            {
                while (Core.CheckInventory("War Medal", 5))
                {
                    Bot.Sleep(1500);
                    Core.EnsureComplete(7980);
                }
                while (Core.CheckInventory("Mega War Medal", 3))
                {
                    Bot.Sleep(1500);
                    Core.EnsureComplete(7981);
                }
            }
            Core.KillMonster("sevencircleswar", "r5", "Left", "Heresy Guard", "Heresy Guards Defeated", 12, log: false);
            Core.EnsureComplete(7983);
            Core.Logger($"Quest Completed x{z++} times");
        }
    }

    public void Merge(bool Getall = true, string item = "any")
    {
        if (Getall && item == "any" && Core.CheckInventory(Items2))
            return;

        if (!Getall && item != "any" && Core.CheckInventory(item))
            return;

        Core.AddDrop(Items2);

        if (!Getall && item != "any" && !Core.CheckInventory(item))
        {
            MergeMaterials("Underworld Laurel", 7);
            MergeMaterials("Underworld Medal", 7);
            MergeMaterials("Underworld Accolade", 7);

            if (item == "Avarice of the Legion's Helm")
            {
                MergeMaterials("Underworld Laurel", 2);
                MergeMaterials("Underworld Medal", 2);
                MergeMaterials("Underworld Accolade", 2);

                while (!Core.CheckInventory("Avarice of the Legion's Skull") || !Core.CheckInventory("Avarice of the Legion's Hood"))
                {
                    Core.EquipClass(ClassType.Solo);

                    Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Skull");
                    Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Hood");
                }
                Core.BuyItem("dage", 2118, "Avarice of the Legion's Helm");
                Core.ToBank("Avarice of the Legion's Helm");
            }

            Core.BuyItem("dage", 2118, item);
            Core.Bot.Wait.ForPickup(item);
            Core.ToBank(item);
        }

        if (Getall && item == "any" && !Core.CheckInventory(Items2))
        {
            while (!Core.CheckInventory(Items2))
            {
                if (item == "Avarice of the Legion's Helm")
                {
                    MergeMaterials("Underworld Laurel", 2);
                    MergeMaterials("Underworld Medal", 2);
                    MergeMaterials("Underworld Accolade", 2);

                    while (!Core.CheckInventory("Avarice of the Legion's Skull") || !Core.CheckInventory("Avarice of the Legion's Hood"))
                    {
                        Core.AddDrop("Avarice of the Legion's Skull", "Avarice of the Legion's Hood");
                        Core.EquipClass(ClassType.Solo);

                        Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Skull");
                        Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Hood");
                    }
                    Core.BuyItem("dage", 2118, "Avarice of the Legion's Helm");
                    Core.ToBank("Avarice of the Legion's Helm");
                }

                MergeMaterials("Underworld Laurel", 48);
                MergeMaterials("Underworld Medal", 48);
                MergeMaterials("Underworld Accolade", 48);
                Core.AddDrop(Items2);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Skull");
                Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Hood");

                // Merge buying:
                if (!Core.CheckInventory("Avarice of the Legion's Scythe"))
                    Core.BuyItem("dage", 2118, "Avarice of the Legion's Scythe");
                if (!Core.CheckInventory("Virgil of the Legion's Staff"))
                    Core.BuyItem("dage", 2118, "Virgil of the Legion's Staff");
                if (!Core.CheckInventory("Avarice of the Legion"))
                    Core.BuyItem("dage", 2118, "Avarice of the Legion");
                if (!Core.CheckInventory("Luxuria of the Legion"))
                    Core.BuyItem("dage", 2118, "Luxuria of the Legion");
                if (!Core.CheckInventory("Virgil of the Legion"))
                    Core.BuyItem("dage", 2118, "Virgil of the Legion");
                if (!Core.CheckInventory("Avarice of the Legion's Helm"))
                    Core.BuyItem("dage", 2118, "Avarice of the Legion's Helm");
                if (!Core.CheckInventory("Virgil of the Legion's Helm"))
                    Core.BuyItem("dage", 2118, "Virgil of the Legion's Helm");
                if (!Core.CheckInventory("Avarice of the Legion's Scarf"))
                    Core.BuyItem("dage", 2118, "Avarice of the Legion's Scarf");
                if (!Core.CheckInventory("Eye of Luxuria Runes"))
                    Core.BuyItem("dage", 2118, "Eye of Luxuria Runes");
                if (!Core.CheckInventory("Virgil of the Legion's Cape"))
                    Core.BuyItem("dage", 2118, "Virgil of the Legion's Cape");
            }
            Core.ToBank(Items2);
        }
    }
}