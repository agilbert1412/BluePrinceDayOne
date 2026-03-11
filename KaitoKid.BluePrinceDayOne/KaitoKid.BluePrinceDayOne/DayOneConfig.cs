using MelonLoader;

namespace KaitoKid.BluePrinceDayOne
{
    public class DayOneConfig
    {
        private MelonPreferences_Category _speedupCategory;
        //private MelonPreferences_Category _rngCategory;
        //private MelonPreferences_Category _cheatyDay2Category;
        //private MelonPreferences_Category _cheatyDay1Category;

        public MelonPreferences_Entry<bool> OpenAllSafes { get; set; }
        public MelonPreferences_Entry<bool> SolveParlor { get; set; }
        public MelonPreferences_Entry<bool> SolveDarts { get; set; }
        public MelonPreferences_Entry<bool> SolveBoilerRoom { get; set; }

        //public MelonPreferences_Entry<int> Allowance { get; set; }
        //public MelonPreferences_Entry<bool> AppleOrchardOpen { get; set; }
        //public MelonPreferences_Entry<bool> GemstoneCavernOpen { get; set; }
        //public MelonPreferences_Entry<bool> CloisterOpened { get; set; }

        //public MelonPreferences_Entry<bool> WestGateOpen { get; set; }
        //public MelonPreferences_Entry<bool> GrottoOpen { get; set; }
        //public MelonPreferences_Entry<bool> FoundationElevatorDown { get; set; }
        //public MelonPreferences_Entry<bool> SundialOpen { get; set; }
        //public MelonPreferences_Entry<int> GoldInChapel { get; set; }

        public DayOneConfig()
        {
            _speedupCategory = MelonPreferences.CreateCategory("Speed Up");
            OpenAllSafes = _speedupCategory.CreateEntry(nameof(OpenAllSafes), true);
            SolveParlor = _speedupCategory.CreateEntry(nameof(SolveParlor), true);
            SolveDarts = _speedupCategory.CreateEntry(nameof(SolveDarts), true);
            SolveBoilerRoom = _speedupCategory.CreateEntry(nameof(SolveBoilerRoom), true);

            //_rngCategory = MelonPreferences.CreateCategory("RNG Behaviors");

            //_cheatyDay2Category = MelonPreferences.CreateCategory("Cheaty Modifications usually unavailable on Day1");
            //Allowance = _cheatyDay2Category.CreateEntry(nameof(Allowance), 0);
            //AppleOrchardOpen = _cheatyDay2Category.CreateEntry(nameof(AppleOrchardOpen), false);
            //GemstoneCavernOpen = _cheatyDay2Category.CreateEntry(nameof(GemstoneCavernOpen), false);
            //// CaveDoorOpen = _cheatyDay2Category.CreateEntry(nameof(CaveDoorOpen), false);
            //CloisterOpened = _cheatyDay2Category.CreateEntry(nameof(CloisterOpened), false);
            //// GateOpen = _cheatyDay2Category.CreateEntry(nameof(GateOpen), false);

            //_cheatyDay1Category = MelonPreferences.CreateCategory("Cheaty Modifications that can be done on Day1");
            //GrottoOpen = _cheatyDay1Category.CreateEntry(nameof(GrottoOpen), false);
            //WestGateOpen = _cheatyDay1Category.CreateEntry(nameof(WestGateOpen), false);
            //FoundationElevatorDown = _cheatyDay1Category.CreateEntry(nameof(FoundationElevatorDown), false);
            //SundialOpen = _cheatyDay1Category.CreateEntry(nameof(SundialOpen), false);
            //GoldInChapel = _cheatyDay1Category.CreateEntry(nameof(GoldInChapel), 0);
            // Upgraded Rooms
            // Allowance
            // "Apple Orchard Open"
            // "Cave Door Open"
            // "Cloister Opened"
            // "GateOpen"
            // "Gemstone Cavern Open"
            // "Grotto Open"
            // "Foundation Elevator Down"
            // "Sundial Open"
            // "West Gate Open"

            var path = @"UserData\DayOne.cfg";
            _speedupCategory.SetFilePath(path);
            //_rngCategory.SetFilePath(path);
            //_cheatyDay2Category.SetFilePath(path);
            //_cheatyDay1Category.SetFilePath(path);

            _speedupCategory.SaveToFile();
            //_rngCategory.SaveToFile();
            //_cheatyDay2Category.SaveToFile();
            //_cheatyDay1Category.SaveToFile();
        }
    }
}
