using RBot;

public class CoreDarkon
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();

    public void FarmReceipt(int Quantity = 222)
    {
        if (Core.CheckInventory("Darkon's Receipt", Quantity))
            return;

        SecondErrand(Quantity, true);
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

    public void SecondErrand(int Quantity = 222, bool escapeWhile = false)
    {
        if (Core.CheckInventory("Darkon's Receipt", Quantity))
            return;

        bool EnoughPeople = false;
        Core.AddDrop("Darkon's Receipt");
        Core.EquipClass(ClassType.Solo);

        Story.UpdateQuest(2954);
        Core.Join("doomvault", "r5", "Left");

        while (!Core.CheckInventory("Darkon's Receipt", Quantity))
        {
            if (Bot.Map.Name == "doomvault")
            {
                if (Bot.Map.CellPlayers.Count >= 3)
                    EnoughPeople = true;
                else EnoughPeople = false;
            }

            if (!EnoughPeople && !Core.IsMember && escapeWhile)
                return;

            Core.EnsureAccept(7325);

            if (!EnoughPeople && Core.IsMember)
                Core.HuntMonster("ultravoid", "Ultra Kathool", "Ingredients?", 22, false, publicRoom: true);
            else Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Ingredients?", 22, false);

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

            Core.HuntMonster("tercessuinotlim", "Nulgath", "Nulgath's mask", 1, false, publicRoom: true);

            Core.EnsureComplete(7326);
            Bot.Wait.ForPickup("Darkon's Receipt");
        }
    }

    public void AstravianMedal(int Quantity = 300)
    {
        if (Core.CheckInventory("Astravian Medal", Quantity))
            return;

        Core.AddDrop("Astravian Medal");

        while ((!Core.CheckInventory("Astravian Medal", Quantity)))
        {
            Core.EnsureAccept(8257);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("astraviacastle", "Creature 27|Creature 28", "Defaced Portrait", 10);
            Core.HuntMonster("astraviacastle", "Creature 20", "Smashed Sculpture", 4);
            
            Core.JumpWait();
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("astraviacastle", "The Sun", "Burned Banana");

            Core.EnsureComplete(8257);
            Bot.Wait.ForPickup("Astravian Medal");
        }
    }
}