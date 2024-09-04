/*
name: ShopAcheivmentandIOGrabber
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ShopAcheivmentandIOGrabber
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public string OptionsStorage = "ShopAcheivmentGrabber";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<string>("ShopMap", "Input Shop Map", "Input Map that the ShopID requires(Leave as none if it doesnt require a map.)", "None"),
        new Option<int>("ShopID", "InPut ShopID", "Input ShopID that you wish to Grab the AcheivmentID & IO of", 0000),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GrabME(Bot.Config!.Get<string>("ShopMap"), Bot.Config!.Get<int>("ShopID"));

        Core.SetOptions(false);
    }

    public void GrabME(string? map, int ShopID)
    {
        Core.Join(map == "None" ? Bot.Map.Name : map);
        while (!Bot.ShouldExit && !Bot.Shops.IsLoaded)
        {
            Bot.Shops.Load(ShopID);
            Bot.Wait.ForTrue(() => Bot.Shops.IsLoaded, 20);
        }

        int achievementID = Bot.Flash.GetGameObject<int>("world.shopinfo.iIndex");
        string? io = Bot.Flash.GetGameObject<string>("world.shopinfo.sField");
        if (achievementID > 0 && io != null)
            Core.Logger($"Acheivment ID:{achievementID}, IO: {io}");
        else
            Core.Logger($"[{ShopID}] invalid, or [{map}] is wrong!");
    }
}
