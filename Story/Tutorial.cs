//cs_include Scripts/CoreBots.cs
using RBot;

public class Tutorial
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Badges();

        Core.SetOptions(false);
    }

    public void Badges()
    {
        if (Core.HasAchievement(31))
            return;

        string[] Achievements =  {
            "Combat",
            "Interact",
            "Quest",
            "Skill",
            "Shop",
            "Enhance",
            "Rest",
            "World",
            "Emotes",
            "Travel"
        };

        Core.Join("oaklore");
        for (int i = 0; Achievements.Count() > i; i++)
        {
            Core.Logger("Achievement - " + Achievements[i]);
            Core.SetAchievement(22 + i);
            Bot.Sleep(Core.ActionDelay);
        }
    }
}