//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class CrimsonHanzoOrbQuest
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNulgath Nulgath = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CrimsonHanzoOrb();

        Core.SetOptions(false);
    }
    
    
    public void CrimsonHanzoOrb()
    {
        if (Core.CheckInventory(Nulgath.bagDrops) && Core.CheckInventory("Blood Star Blade"))
            return;

        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop("Blood Star Blade");


        Core.RegisterQuests(4019);
        while (!Core.CheckInventory(Nulgath.bagDrops) && !Core.CheckInventory("Blood Star Blade"))
        {
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Jacked Eye", 5);
            Core.HuntMonster("marsh", "Dreadspider", "Dreadspider Silk");
            Core.HuntMonster("nulgath", "Dreadspider", "Dreadspider Silk");
            if (Core.IsMember)
                Core.HuntMonster("nulgath", "Dark Makai", "Makai Fang", 5);
            else Core.HuntMonster("tercessuinotlim", "Dark Makai", "Makai Fang", 5);
            Core.HuntMonster("bludrut", "Rattlebones", "Rattle Bones", 3);
        }
        Core.CancelRegisteredQuests();
    }
}
