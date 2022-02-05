//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/JoinLegion[UndeadWarrior].cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
using RBot;
using RBot.Items;
using System.Collections.Generic;

public class LegionFealty1
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public JoinLegion JoinLegion = new JoinLegion();
    public InfiniteLegionDC ILDC = new InfiniteLegionDC();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        RevenantSpellscroll();

        Core.SetOptions(false);
    }

    public void RevenantSpellscroll(int quant = 20)
    {
        if (Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

        JoinLegion.JoinLegionQuests();

        bool hasDarkCaster = false;
        if (Core.CheckInventory(new[] {"Love Caster", "Legion Revenant"}, any: true))
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
}
