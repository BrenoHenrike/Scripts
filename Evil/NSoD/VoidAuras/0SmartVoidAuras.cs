//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
using RBot;
using RBot.Options;

public class SmartVoidAuras
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNSOD NSoD = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "NSoD_OptionsV1";
    public List<IOption> Options = new()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<bool>("getSDKA", "Get SDKA first [Mem]", "If true, the bot will attempt to get SDKA first, so that it can use the fastest Void Aura farm available\nMember-Only", true),
    };

    public void ScriptMain(ScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("SkipOption") && !Core.CheckInventory(14474, toInv: false) && Core.IsMember)
            Bot.Config.Configure();

        if ((!Bot.Config.Get<bool>("getSDKA") && !Core.IsMember) || (!Core.CheckInventory(14474, toInv: false) && !Core.IsMember))
            Core.BankingBlackList.AddRange(NSoD.Essences);
        Core.BankingBlackList.Add("Void Aura");

        Core.SetOptions();

        NSoD.VoidAuras();

        Core.SetOptions(false);
    }
}
