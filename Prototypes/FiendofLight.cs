//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/SepulchureSaga.cs
using RBot;

public class FiendofLight
{

    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public SepulchureSaga SepulchureSaga = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetFol();

        Core.SetOptions(false);
    }

    private string[] Rewards =
    {
        "Fiend of Light",
        "Fiend of Light Helm",
        "Fiend of Light Hair",
        "Fiend of Light Winged Hair",
        "Fiend of Light Blinded Hair",
        "Fiend of Light Locks",
        "Fiend of Light Helm + Locks",
        "Fiend of Light Backblades",
        "Fiend of Light Wings",
        "Fiend of Light Wings + Tail",
        "Fiend of Light Tail",
        "Fiend of Light Blade",
        "Doomed Fiend of Light Blade",
        "Fiend of Light Blades"
    };

    public void GetFol()
    {
        ////doing pre-story\\\\
        SepulchureSaga.StoryLine();

        ////Checking if all items are obtained\\\\
        if (Core.CheckInventory(Rewards, toInv: false))
        {
            Core.Logger($"Fiend of Light archieved!");
            return;
        }
        ////Farming rewards\\\\
        Core.AddDrop(Rewards);
        Core.RegisterQuests(6408);
        while (!Bot.ShouldExit() && !Core.CheckInventory(Rewards))
            Core.HuntMonster("darkplane", "*", "Crystallized Memory", 10);
        ////If you want the bot to bank the items after farming them all un "//" the following lines.\\\\
        //Core.Logger($"banking Fiend of Light");
        //Core.ToBank(Rewards); 

    }
}
////Made by ToxlcChain\\\\