/*
name: Army Shogun Paragon Tokens
description: Aggromon farm for the Fotia monsters handing in the Clear a Path and Time for Some Spring Cleaning quests for Legion Tokens. 

***Note*** Stop the 6th account and jump to cell r5, Spawn and start it again. That way the 2 10k HP monsters get killed by 2 accounts at a time.
tags: Legion, Army, Tokens, Token, Shogun, Aggromon
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class Generated_ArmyShogunParagonTokens
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        ArmyShogunParagonTokens();
        Core.SetOptions(false);
    }

    public void ArmyShogunParagonTokens()
        => Army.RunGeneratedAggroMon(map, monNames, questIDs, classtype, drops);
    private List<int> questIDs = new() { 5755, 5753 };
    private List<string> monNames = new() { "Fotia Elemental", "Fotia Spirit", "Femme Cult Worshiper" };
    private List<string> drops = new() { "Legion Token" };
    private string map = "fotia";
    private ClassType classtype = ClassType.Farm;
}
