//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class GiantTaleStoryWIP {
  public IScriptInterface Bot =>IScriptInterface.Instance;
  public CoreBots Core =>CoreBots.Instance;
  public CoreStory Story = new();

  public void ScriptMain(IScriptInterface bot) {
    Core.SetOptions();
    doAll();
    Core.SetOptions(false);
  }

  public void doAll() {
    Story.PreLoad(this);
    RolithQuests();
    LowellCatQuests();
  }

  public void RolithQuests() {
    if (Core.isCompletedBefore(39)) return;

    //Defend the Rats
    Story.KillQuest(32, "noobshire", "Kittarian Mouse Eater");

    //Rats in the Cradle
    Story.MapItemQuest(33, "noobshire", 11, 6);
    Story.KillQuest(33, "noobshire", "Horc Noob");

    //Short in Spoons
    Story.KillQuest(35, "noobshire", "Kittarian Mouse Eater");

    //Short in Forks
    Story.KillQuest(36, "noobshire", "Kittarian Mouse Flayer");

    //Letter Interception
    Story.KillQuest(37, "noobshire", "Horc Noob");

    //Not A Noob
    Story.KillQuest(38, "noobshire", "Horc Trainer");

    //Missing King
    Story.MapItemQuest(39, "noobshire", 12, 1);
    Story.KillQuest(39, "noobshire", "Horc Noob");
  }

  public void LowellCatQuests() {
    if (Core.isCompletedBefore(2196)) return;

    //PORKON RIPOFF
    Story.KillQuest(2188, "orctown", "General Porkon");

    //GRILL THE LOCALS
    Story.KillQuest(2189, "noobshire", "Horc Noob");

    //DOGEAR THE CAT BURGLAR
    Story.KillQuest(2190, "newbie", "Dogear");

    //BLOODTUSK CATWALK
    Story.MapItemQuest(2191, "bloodtusk", 1270, 1);

    //You scratch me back...
    Story.KillQuest(2192, "bloodtusk", "Horc Boar Scout");

    //...And me scratch yours
    Story.KillQuest(2193, "bloodtusk", "Rhison");

    //Sign here
    Story.MapItemQuest(2194, "bloodtusk", 1271, 1);

    //THE FRAME UP
    Story.KillQuest(2195, "sandport", "Giant Cat");

    //THAT DARN CAT
    Story.KillQuest(2196, "giant", "Horc Noob");
  }
}
