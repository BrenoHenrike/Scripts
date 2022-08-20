//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class SmartEnhance
{
    public IScriptInterface Bot { get; set; }
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        Adv.SmartEnhance(Bot.Player.CurrentClass.Name);

        Core.SetOptions(false);
    }
}
