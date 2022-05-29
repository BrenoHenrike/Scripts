//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class GenesisGarden
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();
        
        Core.SetOptions(false);
    }

    public void DoAll()
    {
        TheEmperor();
        TheHermit();
    }

    public void TheEmperor()
    {
        if (Core.isCompletedBefore(8682))
            return;

        //The Fool 8678
        Story.MapItemQuest(8678, "genesisgarden", 10196, 6);

        //The Magician 8679
        Story.KillQuest(8679, "genesisgarden", "Drago's Soldier");

        //The High Priestess 8680
        Story.MapItemQuest(8680, "genesisgarden", MapItemIDs: new[] { 10197, 10198, 10199 });

        //The Empress, Reversed 8681
        Story.KillQuest(8681, "genesisgarden", "Drago's Soldier");

        //The Emperor 8682
        Story.MapItemQuest(8682, "genesisgarden", MapItemIDs:  new[] { 10200, 10201 });
    }

    public void TheHermit()
    {
        if (Core.isCompletedBefore(8687))
            return;

        //The Hierophant 8683
        Story.MapItemQuest(8683, "genesisgarden", 10202, 5);

        //The Lovers, Reversed 8684
        Story.KillQuest(8684, "genesisgarden", new[] { "Ancient Creature", "Plant Beast" });

        //The Chariot 8685
        Story.MapItemQuest(8685, "genesisgarden", 10203);
        Story.KillQuest(8685, "genesisgarden", "Ancient Turret");

        //Strength 8686
        Story.KillQuest(8686, "genesisgarden", "Undead Humanoid");

        //The Hermit 8687
        Story.KillQuest(8687, "genesisgarden", "Ancient Mecha");
    }
}
