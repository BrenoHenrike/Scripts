//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class TimeinnSTory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);

    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(8151))
            return;

        Story.PreLoad();

        //Unlocking the Antechamber 8146
        Story.KillQuest(8146, "timeinn", "Fire Elemental");
        Story.KillQuest(8146, "timeinn", "Ice Elemental");

        //Ezrajal 8147
        Story.KillQuest(8147, "timeinn", "Ezrajal");

        //Unlocking the Reliquary 8148
        Story.KillQuest(8147, "timeinn", "Nature Elemental");
        Story.KillQuest(8147, "timeinn", "Wind Elemental");

        //The Warden 8149
        Story.KillQuest(8147, "timeinn", "The Warden");
        
        //Unlocking the Apex 8150
        Story.KillQuest(8150, "timeinn", "Energy Elemental");
        Story.KillQuest(8150, "timeinn", "Water Elemental");
        
        //The Engineer 8151
        Story.KillQuest(8151, "timeinn", "The Engineer");
        
    }
}
