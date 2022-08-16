//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
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
        Core.Logger($"Story: AQW Zombies - Complete");

        NecroDungeon.NecrodungeonStoryLine();
        Core.Logger($"Story: Necro Dungeon - Complete");

        NecroTower.DoAll();
        Core.Logger($"Story: Necro Tower - Complete");

        DoomwoodPart3.StoryLine();
        Core.Logger($"Story: Doomwood Part 3 - Complete");

        Core.SetOptions(false);
    }
}
