//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class EmpoweringItems
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        EmpoweringStuff();

        Core.SetOptions(false);
    }
    public void EmpoweringStuff()
    {
        if (Core.CheckInventory("Death Scythe of Nulgath"))
            return;

        Core.AddDrop("Death Scythe of Nulgath");

        Core.EnsureAccept(558);
        Nulgath.FarmUni13();
        Nulgath.FarmDiamondofNulgath(10);
        Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Sigil");
        Core.EnsureComplete(558);
        Bot.Wait.ForPickup("Death Scythe of Nulgath");
    }
}
