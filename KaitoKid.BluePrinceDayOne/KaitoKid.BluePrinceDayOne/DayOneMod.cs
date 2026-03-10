using Il2Cpp;
using Il2CppHutongGames.PlayMaker;
using Il2CppPathologicalGames;
using KaitoKid.BluePrinceDayOne;
using MelonLoader;
using UnityEngine;
using UnityEngine.Windows;

[assembly: MelonInfo(typeof(DayOneMod), "DayOne", "1.0.0", "Kaito Kid")]
[assembly: MelonGame("Dogubomb", "BLUE PRINCE")]
namespace KaitoKid.BluePrinceDayOne
{
    public class DayOneMod : MelonMod
    {
        public DayOneConfig _config;
        public DayOneInput _input;
        public PlayMakerFSM GlobalPersistentManager;
        public PlayMakerFSM GlobalManager;
        public List<PlayMakerFSM> ParlorGames;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg($"Initializing DayOne...");
            base.OnInitializeMelon();
            ParlorGames = new List<PlayMakerFSM>();
            _config = new DayOneConfig();
            _input = new DayOneInput(LoggerInstance);
            RoomPatches.Initialize(LoggerInstance);
            Events.Initialize(LoggerInstance);
            HarmonyInstance.PatchAll();
            LoggerInstance.Msg($"Initialized DayOne!");
        }

        public override void OnDeinitializeMelon()
        {
            base.OnDeinitializeMelon();
        }

        private int _frameCounter = 0;
        private bool _solvedParlor = false;

        public override void OnUpdate()
        {
            base.OnUpdate();

            _input.Update();

            _frameCounter++;
            if (_frameCounter % 30 != 0)
            {
                return;
            }

            //if (_solvedParlor)
            //{
            //    return;
            //}
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            ApplyDay1Variables(sceneName);

            base.OnSceneWasLoaded(buildIndex, sceneName);
        }

        private void ApplyDay1Variables(string sceneName)
        {
            if (!sceneName.Equals("Mount Holly Estate"))
            {
                return;
            }

            ApplyDay1Variables();
        }


        private void ApplyDay1Variables()
        {
            var globalPersistentManagerName = "Global Persitent Manager";
            var globalPersistentManagerGameObject = GameObject.Find(globalPersistentManagerName);
            var globalPersistentManagerFsm = globalPersistentManagerGameObject?.GetComponent<PlayMakerFSM>();
            GlobalPersistentManager = globalPersistentManagerFsm;
            GlobalManager = GameObject.Find("Global Manager")?.GetComponent<PlayMakerFSM>();

            if (_config.OpenAllSafes.Value)
            {
                SetFsmBool("Red Envelope Boudoir");
                // SetFsmBool("Red Envelope Drafting");
                SetFsmBool("Red Envelope Drawing");
                SetFsmBool("Red Envelope Office");
                SetFsmBool("Red Envelope Shelter");
                SetFsmBool("Red Envelope Study");
                // SetFsmBool("Red Envelope Underpass");
            }

            if (_config.SolveBoilerRoom.Value)
            {
                SetFsmBool("Boiler A On");
                SetFsmBool("Boiler B On");
                SetFsmBool("Boiler C On");
                SetFsmBool("Boiler Gate Up");
                SetFsmBool("Boiler Switcher 1");
                SetFsmInt("Boiler Switcher 2", 3);
            }
        }

        private void SetFsmBool(string name, bool value = true)
        {
            SetFsmBool(GlobalPersistentManager, name, value);
        }

        private void SetFsmBool(PlayMakerFSM fsm, string name, bool value = true)
        {
            try
            {
                var variables = fsm.FsmVariables;
                var boudoirBool = variables.FindFsmBool(name);
                boudoirBool.Value = value;
                LoggerInstance.Msg($"Successfully set `{name}` to {value}");
            }
            catch (Exception ex)
            {
                LoggerInstance.Error($"\tFailed to set the Fsm Bool `{name}` to {value}. Message: {ex.Message}");
            }
        }

        private void SetFsmInt(string name, int value)
        {
            SetFsmInt(GlobalPersistentManager, name, value);
        }

        private void SetFsmInt(PlayMakerFSM fsm, string name, int value)
        {
            try
            {
                var variables = fsm.FsmVariables;
                var boudoirBool = variables.FindFsmInt(name);
                boudoirBool.Value = value;
                LoggerInstance.Msg($"Successfully set `{name}` to {value}");
            }
            catch (Exception ex)
            {
                LoggerInstance.Error($"\tFailed to set the Fsm Int `{name}` to {value}. Message: {ex.Message}");
            }
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasUnloaded(buildIndex, sceneName);
        }

        public override void OnPreferencesSaved()
        {
            base.OnPreferencesSaved();
        }

        public override void OnPreferencesSaved(string filepath)
        {
            base.OnPreferencesSaved(filepath);
        }

        public override void OnPreferencesLoaded()
        {
            base.OnPreferencesLoaded();
        }

        public override void OnPreferencesLoaded(string filepath)
        {
            base.OnPreferencesLoaded(filepath);
        }
    }
}
