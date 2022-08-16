//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/Doomwood/Necrodungeon.cs
//cs_include Scripts/Story/Doomwood/Necrotower.cs

using RBot;

public class DoomWoodSaga
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public AQWZombies AQWZombies = new();
    public DoomwoodPart3 DoomwoodPart3 = new();
    public NecroDungeon NecroDungeon = new();
    public NecroTowerStory NecroTower = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        AQWZombies.Storyline();
        NecroDungeon.NecrodungeonStoryLine();
        NecroTower.DoAll();
        DoomwoodPart3.StoryLine();

        Core.SetOptions(false);
    }
}
