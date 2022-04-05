//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs

using RBot;

public class ObsidianLightofDestiny
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreBLOD BLOD = new CoreBLOD();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Axe();

        Core.SetOptions(false);
    }

    public void Axe()
    {
        if (Core.CheckInventory("Obsidian Light of Destiny"))
            return;

        //The Edge of Destiny
        if (!Core.CheckInventory("Obsidian Light of Destiny"))
        {
            Core.EnsureAccept(7648);

            if (!Core.CheckInventory("Blinding Edge of Obsidian"))
            {
                Farm.MysteriousDungeonREP();
                Core.BuyItem("darkthronehub", 1308, "Blinding Edge of Obsidian");
                Bot.Wait.ForPickup("Blinding Edge of Obsidian");
            }
            BLOD.FindingFragmentsBow(120); //Bright Aura x120
            BLOD.FindingFragmentsMace(40); //Brilliant Aura x40
            BLOD.FindingFragments(2174); //Blinding Aura 
            while (!Core.CheckInventory("Spirit Orb", 5000)) //Spirit Orb (Misc) x5,000 
                BLOD.FindingFragments(2179);
            while (!Core.CheckInventory("Loyal Spirit Orb", 750))
                BLOD.FindingFragments(2179);
            Core.EnsureComplete(7648);
            Bot.Wait.ForPickup("Obsidian Light of Destiny");
        }
    }
}