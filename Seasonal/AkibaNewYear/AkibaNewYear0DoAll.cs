/*
name: Akiba New Year DoAll
description: Completes all of the akiba new year quests
tags: seasonal, yokai, akibacny, akiba new year, story, bingwen, fausto, lengjing, senlin-mas, zhu, yokaihunt, ladyluna
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/AkibaNewYear/Bingwen.cs
//cs_include Scripts/Seasonal/AkibaNewYear/LadyLua.cs
//cs_include Scripts/Seasonal/AkibaNewYear/Zhu.cs
//cs_include Scripts/Seasonal/AkibaNewYear/YokaiHunt.cs
//cs_include Scripts/Seasonal/AkibaNewYear/SenlinMas.cs
//cs_include Scripts/Seasonal/AkibaNewYear/Parades.cs
using Skua.Core.Interfaces;

public class AkibaNewYear0DoAll
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    private Bingwen Bingwen = new();
    private LadyLua LadyLua = new();
    private Parades Parades = new();
    private YokaiHunt YokaiHunt = new();
    private Zhu Zhu = new();
    private SenlinMas SenlinMas = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Bingwen.Storyline();
        LadyLua.Storyline();
        Parades.Storyline();
        YokaiHunt.DoAll();
        Zhu.Storyline();
        SenlinMas.Storyline();
    }
}
