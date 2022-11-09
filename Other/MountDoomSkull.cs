//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class MountDoomSkull
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        //Credit to Arts
        //To skip clearing Lord of Chaos story, useful when starting account to faster farm for others
        Core.SetOptions();

        EasyMountDoomSkull();

        Core.SetOptions(false);
    }

    public void EasyMountDoomSkull()
    {
        Core.Join("mountdoomskull");
        //13Loc
        Core.Logger("Doing 13 Lord of Chaos");
        Core.SendPackets("%xt%zm%tryQuestComplete%60014%3578%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3579%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3580%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3581%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3582%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3583%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3584%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3585%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3586%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3587%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3588%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3589%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3590%-1%false%wvz%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3591%-1%false%wvz%");
        
        //Drakath Defeated
        Core.Logger("Doing drakath defeated");
        Core.Jump("NPC2", "Left");
        Core.EnsureAccept(3765);
        Core.SendPackets("%xt%zm%getMapItem%937075%2726%");
        Core.EnsureComplete(3675);
        Core.SendPackets("%xt%zm%setAchievement%937075%ia1%22%1%");
        Core.SendPackets("%xt%zm%tryQuestComplete%937075%3766%-1%false%wvz%");

        Core.Join("newfinale");

    }
}
