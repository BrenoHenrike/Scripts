//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Safiria
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Core.IsMember)
        {
            Core.Logger("Safiria Storyline Is Member Only. Skipping this Script");
            return;
        }

        if (Core.isCompletedBefore(1947))
            return;

        Story.PreLoad(this);

        //The Stolen Ritual 1939
        Story.KillQuest(1939, "Safiria", "Chaos Lycan");

        //A Pound Of Flesh 1940
        Story.KillQuest(1940, "Safiria", "Blood Maggot");

        //Blood Of The Ancients 1941
        Story.KillQuest(1941, "Safiria", "Chaos Lycan");

        //Phinding Phylacteries 1942
        Story.MapItemQuest(1942, "Safiria", 962, 4);

        //Bats Blood Rune 1943
        Story.KillQuest(1943, "Safiria", "Albino Bat");

        //Maggots Blood Rune 1944
        Story.KillQuest(1944, "Safiria", "Blood Maggot");

        //Lycans Blood Rune 1945
        Story.KillQuest(1945, "Safiria", "Chaos Lycan");

        //Twisted Paw's Blood Rune 1946
        Story.KillQuest(1946, "Safiria", "Twisted Paw");

        //Ancient Vitae 1947
        Story.KillQuest(1947, "Safiria", "Chaos Lycan");
    }
}
