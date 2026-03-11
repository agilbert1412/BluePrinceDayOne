using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace KaitoKid.BluePrinceDayOne
{
    public class Day1Variables
    {
        private MelonLogger.Instance _logger;
        private DayOneConfig _config;
        private PlayMakerFSM _globalPersistentManager;
        private PlayMakerFSM _globalManager;

        public Day1Variables(MelonLogger.Instance logger, DayOneConfig config)
        {
            _logger = logger;
            _config = config;
        }

        public void ApplyDay1Variables(string sceneName)
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
            _globalPersistentManager = globalPersistentManagerFsm;
            _globalManager = GameObject.Find("Global Manager")?.GetComponent<PlayMakerFSM>();

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
            SetFsmBool(_globalPersistentManager, name, value);
        }

        private void SetFsmBool(PlayMakerFSM fsm, string name, bool value = true)
        {
            try
            {
                var variables = fsm.FsmVariables;
                var boudoirBool = variables.FindFsmBool(name);
                boudoirBool.Value = value;
                _logger.Msg($"Successfully set `{name}` to {value}");
            }
            catch (Exception ex)
            {
                _logger.Error($"\tFailed to set the Fsm Bool `{name}` to {value}. Message: {ex.Message}");
            }
        }

        private void SetFsmInt(string name, int value)
        {
            SetFsmInt(_globalPersistentManager, name, value);
        }

        private void SetFsmInt(PlayMakerFSM fsm, string name, int value)
        {
            try
            {
                var variables = fsm.FsmVariables;
                var boudoirBool = variables.FindFsmInt(name);
                boudoirBool.Value = value;
                _logger.Msg($"Successfully set `{name}` to {value}");
            }
            catch (Exception ex)
            {
                _logger.Error($"\tFailed to set the Fsm Int `{name}` to {value}. Message: {ex.Message}");
            }
        }
    }
}
