//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UltraSpeaker
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.Boot();
        Bot.Events.ExtensionPacketReceived += UltraSpeakerListener;

        Kill();

        Bot.Events.ExtensionPacketReceived -= UltraSpeakerListener;
        Bot.Stop();
    }

    void Taunt() => Core.UsePotion();

    void MoveOut() => Bot.Send.Packet($"%xt%zm%mv%{Bot.Map.RoomID}%881%363%10%");
    void MoveIn() => Bot.Send.Packet($"%xt%zm%mv%{Bot.Map.RoomID}%506%351%10%");

    void Kill()
    {
        Core.GetScrollOfEnrage();
        Bot.Quests.UpdateQuest(9125);
        Core.Join("ultraspeaker");
        Core.WaitForArmy(3);
        Core.ChooseBestCell("The First Speaker");
        Core.EnableSkills();

        while (Core.MonsterAlive("The First Speaker") && !Bot.ShouldExit)
        {
            if (equalDetected && CanEnterRedZone())
                MoveIn();
            else
                MoveOut();
        }
    }

    async void UltraSpeakerListener(dynamic packet)
    {
        if (packet?["params"]?.type?.ToString() != "json") return;
        dynamic data = packet["params"].dataObj;
        if (data?.cmd?.ToString() != "ct") return;

        var anims = data.anims as System.Collections.IEnumerable;
        if (anims == null) return;

        foreach (var a in anims)
        {
            string animStr = (a as dynamic)?.animStr?.ToString();
            if (!string.IsNullOrEmpty(animStr))
            {
                if (animStr.Equals("ChargeA", StringComparison.OrdinalIgnoreCase))
                {
                    listenDetected = true;
                    await Task.Delay(2000);
                    listenDetected = false;
                    return;
                }

                if (animStr.Equals("ChargeB", StringComparison.OrdinalIgnoreCase))
                {
                    truthDetected = true;
                    await Task.Delay(2000);
                    truthDetected = false;
                    return;
                }

                if (animStr.Equals("ChargeC", StringComparison.OrdinalIgnoreCase))
                {
                    equalDetected = true;
                    await Task.Delay(3000);
                    equalDetected = false;
                    return;
                }
            }
        }
    }
}
