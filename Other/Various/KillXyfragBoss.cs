/*
name: KillXyrfragBoss
description: If Legion Revenant is equipped, taunt Xyfrag after "BLEEEEEEEEEEEECCH" is seen on the screen.
tags: null
*/
//cs_include Scripts/CoreBots.cs

using Skua.Core.Interfaces;


public class KillXyfragBoss
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Core.OneTimeMessage("Requirements", "If \"Legion Revenant\" is equipped and \"Scroll of Enrage\" is in your inventory, this bot will taunt Xyfrag after \"BLEEEEEEEEEEEECCH\" is seen on the screen.", forcedMessageBox: true);
		KillXyfrag();
        
        Core.SetOptions(false);
    }

    public void KillXyfrag(string? item = null, int quant = 1, bool isTemp = false, bool log = true)
    {

        if (item != null && (isTemp ? Bot.TempInv.Contains(item, quant) : Core.CheckInventory(item, quant)))
            return;
    
		if(Bot.Player.CurrentClass.Name == "Legion Revenant"){
            Core.Equip("Scroll of Enrage");
            Bot.Events.ExtensionPacketReceived += XyfragListener;
        }
		
		else {
			
			Core.Logger("This bot requires you to have \"Legion Revenant\" equipped.", messageBox: true, stopBot: true);
			return;
		}

        Core.Join("voidxyfrag", publicRoom: true);
      

        if (item == null)
        {
            if (log)
                Core.Logger("Killing Xyfrag");
            while (!Bot.ShouldExit && Core.IsMonsterAlive("Xyfrag"))
                Bot.Combat.Attack("Xyfrag");
        }
        else
        {
            if (!isTemp)
                Core.AddDrop(item);
            if (log)
                Core.Logger($"Killing Xyfrag for {item} ({Core.dynamicQuant(item, isTemp)}/{quant}) [Temp = {isTemp}]");
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                Bot.Combat.Attack("Xyfrag");
        }

        if(Bot.Player.CurrentClass.Name == "Legion Revenant"){
            Bot.Events.ExtensionPacketReceived -= XyfragListener;
        }

        async void XyfragListener(dynamic packet)
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
                                    if (a is not null && (string)a["msg"] is "BLEEEEEEEEEEEECCH"){
                                        Bot.Sleep(1500);
                                        Bot.Send.Packet($"%xt%zm%gar%1%2%i1>m:1%12917%wvz%");
                                    }
                                }
                            }
                            break;
                }
            }
        }
    }
}
