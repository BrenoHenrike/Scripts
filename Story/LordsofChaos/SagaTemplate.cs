//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaTemplate
{
    public CoreBots Core => CoreBots.Instance;

    public int questStart = 0;

    public string OptionsStorage = "Template";
    public bool DontPreconfigure = true;

    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
    };

    public static readonly int[] qIDs =
    {
        0
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        questStart = bot.Config.Get<int>("startQuest");

        for (int i = questStart; i < qIDs.Length; i++)
        {
            bot.Config.Set("startQuest", i);
            Core.Logger($"Starting {i}");
            Core.EnsureAccept(qIDs[i]);
            switch (i)
            {
                case 0: //
                    
                    break;
            }
            Core.EnsureComplete(qIDs[i]);
            Core.Logger($"Finished {i}");
            Core.Rest();
        }

        Core.SetOptions(false);
    }
}
