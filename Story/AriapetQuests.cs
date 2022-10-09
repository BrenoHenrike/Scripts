//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class AriapetQuests {
  public IScriptInterface Bot =>IScriptInterface.Instance;
  public CoreBots Core =>CoreBots.Instance;
  public CoreStory Story = new();

  public void ScriptMain(IScriptInterface bot) {
    Core.SetOptions();
    Storyline();
    Core.SetOptions(false);
  }

  public void Storyline() {
    if (Core.isCompletedBefore(46)) return;

    Story.PreLoad(this);

    //Pet Food Delivery
    Story.KillQuest(10, "farm", "Scarecrow");

    //Starving Pets
    Story.KillQuest(41, "sewer", "Greenrat");

    //Picky Eaters
    Story.KillQuest(42, "river", "River Fishman");

    //Missing Crate
    Story.KillQuest(43, "pirates", "Shark Bait");

    //Wilderness
    Story.KillQuest(44, "guru", "Trobble");

    //Trobble Bath
    Story.KillQuest(45, "swordhaven", "Slime");

    //Home Sick
    Story.KillQuest(46, "marsh2", "Soulseeker");

  }
}
