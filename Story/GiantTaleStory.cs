//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class GiantTaleStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        DoAll();
        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Story.PreLoad(this);
        GiantQuests();
        SmuurvilQuests();
        TableAndAndreQuests();
    }

    public void GiantQuests()
    {
        if (Core.isCompletedBefore(720))
            return;

        //Property Appraiser 717
        Core.EquipClass(ClassType.Farm);
        Story.MapItemQuest(717, "giant", 119);

        //Pain in the Grass 718
        Story.KillQuest(718, "giant", "Red Ant");

        //Dust Busting 719
        Story.KillQuest(719, "giant", "Dust Bunny");

        //Cat-astrophe 720
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(720, "giant", "Giant Cat");
    }

    public void SmuurvilQuests()
    {
        if (Core.isCompletedBefore(725))
            return;

        //Skynner's List 721
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(721, "smuurvil", "Smuurvil");

        //A Mushy Situation 722
        Story.MapItemQuest(722, "smuurvil", 122, 12);

        //W-Tea F 723
        Story.KillQuest(723, "smuurvil", "Smuurvil");

        //A Skunkweed By Any Other Name... 724
        Story.KillQuest(724, "smuurvil", "Smuurvilette");

        //There Is No Spoon 725
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(725, "smuurvil", "Papa Smuurvil");
    }

    public void TableAndAndreQuests()
    {
        if (Core.isCompletedBefore(749))
            return;

        //Spare Parts 737
        Story.MapItemQuest(737, "table", new[] { 123, 124, 125, 126, 127, 128 });

        //Race 743
        Story.ChainQuest(743);

        //Andre's Foot Defeated 747
        Story.KillQuest(747, "andre", "Giant Foot");

        //Flea be Gone 748
        Story.KillQuest(748, "andre", "Giant Flea");

        //Free the Key 749
        Story.KillQuest(749, "andre", "Giant Necklace");
    }
}
