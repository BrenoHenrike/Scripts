using RBot;
using RBot.Items;

public class CoreLR
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreLegion Legion = new CoreLegion();
    public InfiniteLegionDC ILDC = new InfiniteLegionDC();

    public void GetLR(bool rankUpClass)
    {
        if (Core.CheckInventory("Legion Revenant"))
            return;

        Legion.JoinLegion();

        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.legionMedals);
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF1);
        Core.AddDrop(Legion.LF2);
        Core.AddDrop(Legion.LF3);

        RevenantSpellscroll();
        ConquestWreath();
        ExaltedCrown();

        Core.ChainComplete(6900);
        Bot.Wait.ForPickup("Legion Revenant");
        if (rankUpClass)
            Adv.rankUpClass("Legion Revenant");
    }

    //Legion Fealty 1
    public void RevenantSpellscroll(int quant = 20)
    {
        if (Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

        Legion.JoinLegion();

        bool hasDarkCaster = false;
        if (Core.CheckInventory(new[] { "Love Caster", "Legion Revenant" }, any: true))
            hasDarkCaster = true;
        else
        {
            List<InventoryItem> InventoryData = Bot.Inventory.Items;
            foreach (InventoryItem Item in InventoryData)
            {
                if (Item.Name.Contains("Dark Caster") && Item.Category == ItemCategory.Class)
                {
                    hasDarkCaster = true;
                    break;
                }
            }

            if (!hasDarkCaster)
            {
                List<InventoryItem> BankData = Bot.Bank.BankItems;
                foreach (InventoryItem Item in BankData)
                {
                    if (Item.Name.Contains("Dark Caster") && Item.Category == ItemCategory.Class)
                    {
                        hasDarkCaster = true;
                        Core.Unbank(Item.Name);
                        break;
                    }
                }
            }
        }
        if (!hasDarkCaster)
        {
            ILDC.GetILDC(false);
        }

        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF1);

        int i = 1;
        Core.Logger($"Farming {quant} Revenant's Spellscroll");
        Story.UpdateQuest(2060);
        while (!Core.CheckInventory("Revenant's Spellscroll", quant))
        {
            Core.EnsureAccept(6897);

            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("judgement", "r10a", "Left", "Ultra Aeacus", "Aeacus Empowered", 50, false, publicRoom: true);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("revenant", "r2", "Left", "*", "Tethered Soul", 300, false);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Darkened Essence", 500, false);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Dracolich Contract", 1000, false, publicRoom: true);

            Core.EnsureComplete(6897);
            Bot.Player.Pickup("Revenant's Spellscroll");
            Core.Logger($"Completed x{i++}");
        }
    }

    //Legion Fealty 2
    public void ConquestWreath(int quant = 6)
    {
        if (Core.CheckInventory("Conquest Wreath", quant))
            return;

        Legion.JoinLegion();

        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF2);

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Conquest Wreath");
        Story.UpdateQuest(4614);
        while (!Core.CheckInventory("Conquest Wreath", quant))
        {
            Core.EnsureAccept(6898);

            Core.KillMonster("mummies", "Enter", "Spawn", "*", "Ancient Cohort Conquered", 500, false);
            Core.KillMonster("doomvault", "r1", "Right", "*", "Grim Cohort Conquered", 500, false);
            Core.KillMonster("wrath", "r5", "Left", "*", "Pirate Cohort Conquered", 500, false);
            Core.KillMonster("doomwar", "r6", "Left", "*", "Battleon Cohort Conquered", 500, false);
            Core.KillMonster("overworld", "Enter", "Spawn", "*", "Mirror Cohort Conquered", 500, false);
            Core.KillMonster("deathpits", "r1", "Left", "*", "Darkblood Cohort Conquered", 500, false);
            Core.KillMonster("maxius", "r2", "Left", "*", "Vampire Cohort Conquered", 500, false);
            Core.KillMonster("curseshore", "Enter", "Spawn", "*", "Spirit Cohort Conquered", 500, false);
            Core.KillMonster("dragonbone", "Enter", "Spawn", "*", "Dragon Cohort Conquered", 500, false);
            Core.KillMonster("doomwood", "r6", "Right", "*", "Doomwood Cohort Conquered", 500, false);

            Core.EnsureComplete(6898);
            Bot.Player.Pickup("Conquest Wreath");
            Core.Logger($"Completed x{i++}");
        }
    }

    //Legion Fealty 3
    public void ExaltedCrown(int quant = 10)
    {
        if (Core.CheckInventory("Exalted Crown", quant))
            return;

        Legion.JoinLegion();

        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF3);

        int i = 1;
        Core.Logger($"Farming {quant} Exalted Crown");
        while (!Core.CheckInventory("Exalted Crown", quant))
        {
            Core.EnsureAccept(6899);

            Farm.Gold(500000);
            Core.BuyItem("underworld", 216, "Hooded Legion Cowl");

            Legion.FarmLegionToken(4000);

            Legion.ApprovalAndFavor(0, 300);

            Legion.EmblemofDage(1);

            Legion.DiamondTokenofDage(30);

            Legion.DarkToken(100);

            Core.EnsureComplete(6899);
            Bot.Player.Pickup("Exalted Crown");
            Core.Logger($"Completed x{i++}");
        }
    }
}