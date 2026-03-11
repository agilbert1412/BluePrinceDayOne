using Il2Cpp;
using KaitoKid.BluePrinceDayOne;
using MelonLoader;

[assembly: MelonInfo(typeof(DayOneMod), "DayOne", "1.0.0", "Kaito Kid")]
[assembly: MelonGame("Dogubomb", "BLUE PRINCE")]
namespace KaitoKid.BluePrinceDayOne
{
    public class DayOneMod : MelonMod
    {
        public static DayOneMod Instance;

        public DayOneConfig _config;
        public DayOneInput _input;
        private Day1Variables _day1Vars;
        private ParlorSolver _parlorSolver;
        private DartsSolver _dartsSolver;
        public PlayMakerFSM ParlorGameToSolve;
        public PlayMakerFSM DartboardToSolve;

        public override void OnInitializeMelon()
        {
            Instance = this;

            LoggerInstance.Msg($"Initializing DayOne...");
            base.OnInitializeMelon();
            ParlorGameToSolve = null;
            DartboardToSolve = null;
            _config = new DayOneConfig();
            _input = new DayOneInput(LoggerInstance);
            _day1Vars = new Day1Variables(LoggerInstance, _config);
            _parlorSolver = new ParlorSolver(LoggerInstance);
            _dartsSolver = new DartsSolver(LoggerInstance);
            RoomPatches.Initialize(LoggerInstance);
            Events.Initialize(LoggerInstance);

            HarmonyInstance.PatchAll();
            LoggerInstance.Msg($"Initialized DayOne!");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _input.Update();

            if (ParlorGameToSolve != null && _config.SolveParlor.Value)
            {
                if (_parlorSolver.SolveParlorGame(ParlorGameToSolve))
                {
                    ParlorGameToSolve = null;
                }
            }

            if (DartboardToSolve != null && _config.SolveDarts.Value)
            {
                if (_dartsSolver.SolveDartboard(DartboardToSolve))
                {
                    DartboardToSolve = null;
                }
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _day1Vars.ApplyDay1Variables(sceneName);

            base.OnSceneWasLoaded(buildIndex, sceneName);
        }
    }
}
