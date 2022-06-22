//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story\SkyPirate.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class SkyPirateBadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public SkyPirateQuests SkyPirate = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (!Core.IsMember)
            return;

        SkyPirate.Storyline();

        Core.AddDrop("SkyPirate Annhilator Recognition");
        Core.EquipClass(ClassType.Farm);
        
        Core.EnsureAccept(1291);
        Core.KillMonster("strategy", "r22", "Left", "*", "SkyPirate Annihilator Token", 100);
        Core.EnsureComplete(1291);
        
        Core.JumpWait();
    }
}
