//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class NecroCarnivalStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(8374))
            return;

        Story.PreLoad(this);

        //Dolls' Hide and Seek 8363
        Story.MapItemQuest(8363, "necrocarnival", 9249, 8);
        Story.KillQuest(8363, "necrocarnival", "Gummy Tapeworm");

        //Arts and Crafts 8364
        Story.MapItemQuest(8364, "necrocarnival", 9250, 1);
        Story.KillQuest(8364, "necrocarnival", "Skeleclown");

        //Lemonade, Chewy Ice 8365
        Story.MapItemQuest(8365, "necrocarnival", 9251, 2);
        Story.KillQuest(8365, "necrocarnival", new[] { "Mooch Treeant", "Gummy Tapeworm" });

        //Screams and Tag 8366
        Story.KillQuest(8366, "necrocarnival", "Skeleclown");
        Story.MapItemQuest(8366, "necrocarnival", 9252, 1);

        //Playful Teething 8367
        Story.KillQuest(8367, "necrocarnival", "Mooch Treeant");
        Story.MapItemQuest(8367, "necrocarnival", 9253, 4);

        //Days Pass 8368
        Story.KillQuest(8368, "necrocarnival", "Cotton Tick");
        Story.MapItemQuest(8368, "necrocarnival", 9254, 1);

        //Pinned Bugs 8369
        Story.MapItemQuest(8369, "necrocarnival", 9255, 1);
        Story.KillQuest(8369, "necrocarnival", "Gummy Tapeworm");

        //Sweet Glue 8370
        Story.KillQuest(8370, "necrocarnival", "Mooch Treeant");

        //Witch Soup 8371
        Story.KillQuest(8371, "necrocarnival", new[] { "Mooch Treeant", "Gummy Tapeworm", "Skeleclown" });
        Story.MapItemQuest(8371, "necrocarnival", 9256, 1);

        //Written Promise 8372
        Story.KillQuest(8372, "necrocarnival", "Cotton Tick");
        Story.MapItemQuest(8372, "necrocarnival", 9257, 7);

        //All Fall Down 8373
        Story.KillQuest(8373, "necrocarnival", "Skeleclown");
        Story.MapItemQuest(8373, "necrocarnival", 9258, 1);

        //Lullaby 8374
        Story.KillQuest(8374, "necrocarnival", "Deva");
    }
}
