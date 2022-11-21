    //cs_include Scripts/CoreBots.cs
    //cs_include Scripts/CoreFarms.cs
    using Skua.Core.Interfaces;

    public class ExaltedApotheosisPreReqs
    {
        public IScriptInterface Bot => IScriptInterface.Instance;
        public CoreBots Core => CoreBots.Instance;
        public CoreFarms Farm = new();

        public void ScriptMain(IScriptInterface bot)
        {
            Core.SetOptions();

            PreReqs();

            Core.SetOptions(false);
        }

        public void PreReqs()
        {
            Core.AddDrop("Exalted Node", "Thaumaturgus Alpha", "Apostate Alpha", "Exalted Relic Piece", "Exalted Forgemetal", "Exalted Artillery Shard");
            if (Core.CheckInventory("Exalted Node", 300) && Core.CheckInventory("Thaumaturgus Alpha") && Core.CheckInventory("Apostate Alpha"))
                Core.Logger("Got all prerequisites! Kill the ultra bosses manually for insignias next to complete Exalted Apotheosis.");
                return;

            if (!Core.CheckInventory("Apostate Alpha") && !Core.CheckInventory("Thaumaturgus Alpha"))
                Core.EquipClass(ClassType.Solo);
                while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Node", 300))
                {
                    Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", log: true);
                }
                
                while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Relic Piece", 10))
                {
                    Core.KillMonster("timeinn", "r7", "Bottom", "The Warden", log: true);
                }
                
                while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Artillery Shard", 10))
                {
                    Core.KillMonster("timeinn", "r8", "Left", "The Engineer", log: true);
                }
                
                while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Forgemetal", 10))
                {
                    Core.KillMonster("timeinn", "r6", "Left", "Ezrajal", log: true);
                }
                
                Core.BuyItem("timeinn", 2010, "Apostate Alpha");
                Core.BuyItem("timeinn", 2010, "Thaumaturgus Alpha");
                
                while(!Bot.ShouldExit && !Core.CheckInventory("Exalted Node", 300))
                {
                    Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", log: true);
                }
                Core.Logger("Got all prerequisites! Kill the ultra bosses manually for insignias next to complete Exalted Apotheosis.");
            return; 
        }
    }
