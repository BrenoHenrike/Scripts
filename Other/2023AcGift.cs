/*
name: Forge Your 2023 ACs Gift
description: Heeeeeey. Did you see the Eclipse last week? No? GOOD. You should never look directly at an eclipse. I sure didn't. I was working on a gift for you. Don't look so surprised. I give you things all the time. Weapons. Puns. Obscure trivia. But TODAY I'm going to give you a birthday present. I hid it inside my Orb. He's a little glassy today, so be careful when you shatter him.
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class FreeAcGift
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        FreeAcs2023();

        Core.SetOptions(false);
    }

    public void FreeAcs2023()
    {
        if (!Core.isCompletedBefore(9444))
        {
            Core.EnsureAccept(9444);
            Core.HuntMonster("eventhub", "Agitated Orb", "Free ACs... and Yogurt", 1);
            Core.EnsureComplete(9444);
        }
    }
}
