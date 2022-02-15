using RBot;

public class CoreDarkon
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void FarmReceipt(int Quantity = 222)
    {
        if (Core.CheckInventory("Darkon's Receipt", Quantity))
            return;

        SecondErrand(Quantity);
        FirstErrand(Quantity);
    }

    public void FirstErrand(int Quantity = 222)
    {
        if (Core.CheckInventory("Darkon's Receipt", Quantity))
            return;

        Core.AddDrop("Darkon's Receipt");
        Core.EquipClass(ClassType.Farm);

        while (!Core.CheckInventory("Darkon's Receipt", Quantity))
        {
            Core.EnsureAccept(7324);

            Core.KillMonster("portalmaze", "r8", "Left", "*", "Banana", 22, false);

            Core.EnsureComplete(7324);
            Bot.Wait.ForPickup("Darkon's Receipt");
        }
    }

    public void SecondErrand(int Quantity = 222)
    {
        if (Core.CheckInventory("Darkon's Receipt", Quantity))
            return;

        bool EnoughPeople = false;
        Core.AddDrop("Darkon's Receipt");
        Core.EquipClass(ClassType.Solo);

        Core.UpdateQuest(2954);
        Core.Join("doomvault", "r5", "Left");

        while (!Core.CheckInventory("Darkon's Receipt", Quantity))
        {
            Core.EnsureAccept(7325);

            if (Bot.Map.Name == "doomvault")
            {
                if (Bot.Map.CellPlayers.Count >= 3)
                    EnoughPeople = true;
                else EnoughPeople = false;
            }

            if (!Core.IsMember || EnoughPeople)
                Core.KillMonster("doomvault", "r5", "Left", "Binky", "Ingredients?", 22, false);
            else Core.KillMonster("ultracarnax", "Enter", "Spawn", "*", "Ingredients?", 22, false);

            Core.EnsureComplete(7325);
            Bot.Wait.ForPickup("Darkon's Receipt");
        }
    }

    public void ThirdErrand(int Quantity = 222)
    {
        if (Core.CheckInventory("Darkon's Receipt", Quantity))
            return;

        Core.AddDrop("Darkon's Receipt");
        Core.EquipClass(ClassType.Solo);

        while (!Core.CheckInventory("Darkon's Receipt", Quantity))
        {
            Core.EnsureAccept(7326);

            Core.HuntMonster("tercessuinotlim", "Nulgath", "NUlgath's mask", 1, false);

            Core.EnsureComplete(7326);
            Bot.Wait.ForPickup("Darkon's Receipt");
        }
    }
}