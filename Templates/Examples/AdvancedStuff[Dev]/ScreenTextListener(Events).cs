/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;

public class TextListenerExample
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;


    public void ScriptMain(IScriptInterface Bot)
    {
        //if items are used for ultras/bosses add them here so they dont get banked
        Core.BankingBlackList.AddRange(new[] { "item1", "Item2", "Etc" });
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        //Enable
        Bot.Events.ExtensionPacketReceived += TextListener;

        // Code goes here

        //Disable
        Bot.Events.ExtensionPacketReceived -= TextListener;

    }

    void TextListener(dynamic packet)
    {
        string type = packet["params"].type;
        dynamic data = packet["params"].dataObj;
        if (type is not null and "json")
        {
            string cmd = data.cmd.ToString();
            switch (cmd)
            {
                case "ct":
                    if (data.anims is not null)
                    {
                        foreach (var a in data.anims)
                        {
                            if (a is not null
                            //Ultra speaker
                            && ((string)a["msg"] is "I will make you see the truth."
                            || (string)a["msg"] is "All stand equal beneath the eyes of the Eternal."
                            //Xyfrag
                            || (string)a["msg"] is "BLEEEEEEEEEEEECCH"))
                            {
                                Core.Logger($"Detected Text: {(string)a["msg"]}");
                                Bot.Combat.CancelAutoAttack();
                                
                                //Do thing when Text appears (adding sleeps or waiting causes this to either lag to hell or stall the cliente ntirely)

                                break;
                            }

                        }
                    }
                    break;
            }
        }
    }
}
