//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ThatStory {
  public IScriptInterface Bot => IScriptInterface.Instance;
  public CoreBots Core => CoreBots.Instance;
  public CoreStory Story = new();

  public void ScriptMain(IScriptInterface bot) {
    Core.SetOptions();

    Storyline();

    Core.SetOptions(false);
  }

  public void Storyline() {
    if (Core.isCompletedBefore(8374))
      return;

    Story.PreLoad(this);

    //Dolls' Hide and Seek 8363
    if (!Story.QuestProgression(8363)) {
      Core.EnsureAccept(8363);
      Core.Logger("Doing Quest: [8363] - \"Dolls' Hide and Seek\"", "QuestProgression");
      Core.GetMapItem(9249, 8, "necrocarnival");
      Core.HuntMonster("necrocarnival", "Gummy Tapeworm", "Cartilage Gathered", 9);
      Core.EnsureComplete(8363);
      Core.Logger("Completed Quest: [8363] - \"Dolls' Hide and Seek\"", "TryComplete");
    } else Core.Logger("Already Completed: [8363] - \"Dolls' Hide and Seek\"", "QuestProgression");

    //Arts and Crafts 8364
    if (!Story.QuestProgression(8364)) {
      Core.EnsureAccept(8364);
      Core.Logger("Doing Quest: [8364] - \"Arts and Crafts\"", "QuestProgression");
      Core.GetMapItem(9250, 1, "necrocarnival");
      Core.HuntMonster("necrocarnival", "Skeleclown", "Clowns Disciplined", 10);
      Core.EnsureComplete(8364);
      Core.Logger("Completed Quest: [8364] - \"Arts and Crafts\"", "TryComplete");
    } else Core.Logger("Already Completed: [8364] - \"Arts and Crafts\"", "QuestProgression");

    //Lemonade, Chewy Ice 8365
    if (!Story.QuestProgression(8365)) {
      Core.EnsureAccept(8365);
      Core.Logger("Doing Quest: [8365] - \"Lemonade, Chewy Ice\"", "QuestProgression");
      Core.HuntMonster("necrocarnival", "Mooch Treeant", "Red Sap Collected", 9);
      Core.HuntMonster("necrocarnival", "Gummy Tapeworm", "Tape Worms Fried", 9);
      Core.GetMapItem(9251, 2, "necrocarnival");
      Core.EnsureComplete(8365);
      Core.Logger("Completed Quest: [8365] - \"Lemonade, Chewy Ice\"", "TryComplete");
    } else Core.Logger("Already Completed: [8365] - \"Lemonade, Chewy Ice\"", "QuestProgression");

    //Screams and Tag 8366
    if (!Story.QuestProgression(8366)) {
      Core.EnsureAccept(8366);
      Core.Logger("Doing Quest: [8366] - \"Screams and Tag\"", "QuestProgression");
      Core.HuntMonster("necrocarnival", "Skeleclown", "Clown Fired", 10);
      Core.GetMapItem(9252, 1, "necrocarnival");
      Core.EnsureComplete(8366);
      Core.Logger("Completed Quest: [8366] - \"Screams and Tag\"", "TryComplete");
    } else Core.Logger("Already Completed: [8366] - \"Screams and Tag\"", "QuestProgression");

    //Playful Teething 8367
    if (!Story.QuestProgression(8367)) {
      Core.EnsureAccept(8367);
      Core.Logger("Doing Quest: [8367] - \"Playful Teething\"", "QuestProgression");
      Core.HuntMonster("necrocarnival", "Mooch Treeant", "Pet Food", 9);
      Core.GetMapItem(9253, 4, "necrocarnival");
      Core.EnsureComplete(8367);
      Core.Logger("Completed Quest: [8367] - \"Playful Teething\"", "TryComplete");
    } else Core.Logger("Already Completed: [8367] - \"Playful Teething\"", "QuestProgression");

    //Days Pass 8368
    if (!Story.QuestProgression(8368)) {
      Core.EnsureAccept(8368);
      Core.Logger("Doing Quest: [8368] - \"Days Pass\"", "QuestProgression");
      Core.HuntMonster("necrocarnival", "Cotton Tick", "Cotton Tick Fluff Collected", 10);
      Core.GetMapItem(9254, 1, "necrocarnival");
      Core.EnsureComplete(8368);
      Core.Logger("Completed Quest: [8368] - \"Days Pass\"", "TryComplete");
    } else Core.Logger("Already Completed: [8368] - \"Days Pass\"", "QuestProgression");

    //Pinned Bugs 8369
    if (!Story.QuestProgression(8369)) {
      Core.EnsureAccept(8369);
      Core.Logger("Doing Quest: [8369] - \"Pinned Bugs\"", "QuestProgression");
      Core.GetMapItem(9255, 1, "necrocarnival");
      Core.HuntMonster("necrocarnival", "Gummy Tapeworm", "Gum Sludge", 10);
      Core.EnsureComplete(8369);
      Core.Logger("Completed Quest: [8369] - \"Pinned Bugs\"", "TryComplete");
    } else Core.Logger("Already Completed: [8369] - \"Pinned Bugs\"", "QuestProgression");

    //Sweet Glue 8370
    Story.KillQuest(8370, "necrocarnival", "Mooch Treeant");

    //Witch Soup 8371
    if (!Story.QuestProgression(8371)) {
      Core.EnsureAccept(8371);
      Core.Logger("Doing Quest: [8371] - \"Witch Soup\"", "QuestProgression");
      Core.HuntMonster("necrocarnival", "Gummy Tapeworm", "Gum Eye", 10);
      Core.HuntMonster("necrocarnival", "Mooch Treeant", "Sticky Bark", 10);
      Core.HuntMonster("necrocarnival", "Skeleclown", "Finger Tips", 10);
      Core.GetMapItem(9256, 1, "necrocarnival");
      Core.EnsureComplete(8371);
      Core.Logger("Completed Quest: [8371] - \"Witch Soup\"", "TryComplete");
    } else Core.Logger("Already Completed: [8371] - \"Witch Soup\"", "QuestProgression");

    //Written Promise 8372
    if (!Story.QuestProgression(8372)) {
      Core.EnsureAccept(8372);
      Core.Logger("Doing Quest: [8372] - \"Yeti Witual\"", "QuestProgression");
      Core.HuntMonster("necrocarnival", "Cotton Tick", "Red Tick Juice", 10);
      Core.GetMapItem(9257, 7, "necrocarnival");
      Core.EnsureComplete(8372);
      Core.Logger("Completed Quest: [8372] - \"Yeti Witual\"", "TryComplete");
    } else Core.Logger("Already Completed: [8372] - \"Yeti Witual\"", "QuestProgression");

    //All Fall Down 8373
    if (!Story.QuestProgression(8373)) {
      Core.EnsureAccept(8373);
      Core.Logger("Doing Quest: [8373] - \"Yeti Witual\"", "QuestProgression");
      Core.HuntMonster("necrocarnival", "Skeleclown", "Skeleclowns Reaped", 10);
      Core.GetMapItem(9258, 1, "necrocarnival");
      Core.EnsureComplete(8373);
      Core.Logger("Completed Quest: [8373] - \"Yeti Witual\"", "TryComplete");
    } else Core.Logger("Already Completed: [8373] - \"Yeti Witual\"", "QuestProgression");

    //Lullaby 8374
    Story.KillQuest(8374, "necrocarnival", "Deva");
  }
}
