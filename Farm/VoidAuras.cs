//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Evil/NecroticSwordOfDoom.cs
using Skua.Core.Interfaces;

public class VoidAurasForIdiots
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public NecroticSwordOfDoom NSoD = new();

    public void ScriptMain(IScriptInterface bot)
    {

        Core.SetOptions();
        Core.Logger($"{Bot.Player.Username} ty for being retarted enough to run this👍, your the {Bot.Random.Next(0, 9999999)} 'th person to Request/run this.");
        NSoD.VoidAuras(7500);
        Core.SetOptions(false);

    }
}
