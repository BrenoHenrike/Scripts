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
using RBot;

public class VoidAurasForIdiots
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public NecroticSwordOfDoom NSoD = new();

    public void ScriptMain(ScriptInterface bot)
    {

        Core.SetOptions();
        Core.Logger($"{Bot.Player.Username} ty for being retarted enough to run this👍, your the {Bot.Runtime.Random.Next(0, 9999999)} 'th person to Request/run this.");
        NSoD.RetrieveVoidAuras(7500);
        Core.SetOptions(false);

    }
}
