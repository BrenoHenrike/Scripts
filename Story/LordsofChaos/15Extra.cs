//cs_include Scripts/CoreBots.cs

using System.IO.Compression;
using System.Diagnostics;
using System.Collections.Generic;

public class SagaExtra
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        StoryLine();

        Core.SetOptions(false);
    }
    public void StoryLine()
    {
        if (Core.QuestProgression(3824))
        return;

        
        //Arrive in DreadHaven
        Core.ChainQuest(3812);


        //Kill SlugWrath in Dreadhaven
        Core.ChainQuest(3813);

        //Kill Bandit Drakath in Dreadhaven
        Core.ChainQuest(3814);

        //Up the Mountain
        Core.KillQuest(3815, "falcontower", "Lady Knight|Sir Knight");

        //Higher Up
        Core.KillQuest(3816, "falcontower", "Lady Knight|Sir Knight");

        //Even Higher
        Core.KillQuest(3817, "falcontower", "Lady Knight|Sir Knight");

        //Falconreach Tower
        Core.KillQuest(3818, "falcontower", "Lady Knight|Sir Knight");

        //Climb the Tower
        Core.KillQuest(3819, "falcontower", "Lady Knight|Sir Knight");

        //To the Dragonlord
        Core.KillQuest(3820, "falcontower", "Lady Knight|Sir Knight");

        //Defeat the Dragonlord
        Core.KillQuest(3821, "falcontower", "DragonLord");

        //Defeat Dragon Drakath
        Core.KillQuest(3822, "falcontower", "Dragon Drakath");

        //Defeat Sepulchure
        Core.KillQuest(3823, "falcontower", "Sepulchure");

        //Defeat Alteon
        Core.KillQuest(3824, "falcontower", "Alteon", hasFollowup: false);
    }
}
