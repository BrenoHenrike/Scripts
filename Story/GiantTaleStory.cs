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
    GiantQuests();
    SmuurvilQuests();
    TableAndAndreQuests();
  }

  public void GiantQuests() {
    if (Core.isCompletedBefore(720)) return;

    //Property Appraiser
    Core.EquipClass(ClassType.Farm);
    Story.MapItemQuest(717, "giant", 119, 1);

    //Pain in the Grass
    Story.KillQuest(718, "giant", "Red Ant");

    //Dust Busting
    Story.KillQuest(719, "giant", "Dust Bunny");

    //Cat-astrophe
    Core.EquipClass(ClassType.Solo);
    Story.KillQuest(720, "giant", "Giant Cat");
  }

  public void SmuurvilQuests() {
    if (Core.isCompletedBefore(725)) return;

    //Skynner's List
    Core.EquipClass(ClassType.Farm);
    Story.KillQuest(721, "smuurvil", "Smuurvil");

    //A Mushy Situation
    Story.MapItemQuest(722, "smuurvil", 122, 12);

    //W-Tea F
    Story.KillQuest(723, "smuurvil", "Smuurvil");

    //A Skunkweed By Any Other Name...
    Story.KillQuest(724, "smuurvil", "Smuurvilette");

    //There Is No Spoon
    Core.EquipClass(ClassType.Solo);
    Story.KillQuest(725, "smuurvil", "Papa Smuurvil");
  }

  public void TableAndAndreQuests() {
    if (Core.isCompletedBefore(749)) return;

    //Spare Parts
    Story.MapItemQuest(737, "table", new[] {
      123,
      124,
      125,
      126,
      127,
      128
    });

    //Race
    Story.ChainQuest(743);

    //Andre's Foot Defeated
    Story.KillQuest(747, "andre", "Giant Foot");

    //Flea be Gone
    Story.KillQuest(748, "andre", "Giant Flea");

    //Free the Key
    Story.KillQuest(749, "andre", "Giant Necklace");
  }
}
