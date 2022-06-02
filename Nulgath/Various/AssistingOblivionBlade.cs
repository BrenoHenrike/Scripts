//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;
using RBot.Items;

public class AssistingOblivionBlade
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNulgath Nulgath = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        doit();

        Core.SetOptions(false);
    }

    public void doit()
    {
        if (!Core.IsMember)
            return;
        if (!Core.CheckInventory("The Secret 2"))
            return;
            
        if (!Core.CheckInventory("Tendurrr The Assistant"))
            Core.HuntMonster("tercessuinotlim", "Dark Makai", "Tendurrr The Assistant");
            
        Core.RegisterQuests(5818);
        foreach (string item in Nulgath.bagDrops)
        {
            List<InventoryItem> invBank = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList().FindAll(x => Nulgath.bagDrops.ToList().Contains(x.Name));
            InventoryItem? _item = invBank.Find(x => x.Name == item);
            Core.AddDrop(item);
            while (!Core.CheckInventory(item, _item.MaxStack))
            {
                Farm.BludrutBrawlBoss("The Secret 4", 1, canSoloBoss: false);
                Nulgath.EssenceofNulgath(20);
                Nulgath.ApprovalAndFavor(50, 50);
                Core.KillMonster("boxes", "Fort2", "Left", "*", "Cubes", 50, false);
                Core.KillMonster("shadowblast", "r13", "Left", "*", "Fiend Seal", 10, false);
                Farm.BattleUnderB(quant: 200);
            }
        }
        Core.CancelRegisteredQuests();
    }
}