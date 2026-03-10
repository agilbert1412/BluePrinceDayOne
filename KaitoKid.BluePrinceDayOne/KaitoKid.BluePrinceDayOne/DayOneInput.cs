using Il2Cpp;
using MelonLoader;
using System.Drawing;
using UnityEngine;

namespace KaitoKid.BluePrinceDayOne
{
    public class DayOneInput
    {
        private MelonLogger.Instance _logger;

        public DayOneInput(MelonLogger.Instance logger)
        {
            _logger = logger;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                PrintFsmStates(GetParlorGameFsm());
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                PrintFsmStates("Blue");
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                PrintFsmStates("White");
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                PrintFsmStates("Black");
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                DisableFsmStateAction("Blue", "State 8", 2);
                DisableFsmStateAction("Blue", "State 4", 0);
            }

            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                DisableFsmStateAction("White", "State 8", 2);
                DisableFsmStateAction("White", "State 10", 0);
            }

            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                DisableFsmStateAction("Black", "State 8", 2);
                DisableFsmStateAction("Black", "State 4", 0);
            }

            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                OpenParlorBox("Blue");
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                OpenParlorBox("White");
            }

            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                OpenParlorBox("Black");
            }
        }

        private void OpenParlorBox(string color)
        {
            var gameFsm = GetParlorGameFsm();
            var boxFsm = GetBoxFsm(color);

            if (gameFsm == null)
            {
                _logger.Msg($"Could not find an active Parlor Game with an FSM.");
                return;
            }

            if (boxFsm == null)
            {
                _logger.Msg($"Could not find a {color} Box with an FSM.");
                return;
            }

            _logger.Msg($"Found a {color} Box with an FSM, State is: {boxFsm.ActiveStateName}");

            PrintFsmStates(boxFsm);

            //if (new[] { "Prestate", "Prestate 2" }.Contains(boxFsm.ActiveStateName))
            //{
                SendEventAndLog(boxFsm, "hov");
                SendEventAndLog(boxFsm, "cont");
                SendEventAndLog(boxFsm, "click");
                //if (new[] { "Inventory Remove", "State 3", "State 2", "State 7" }.Contains(boxFsm.ActiveStateName))
                //{
                //    _solvedParlor = true;
                //}
            //}
        }

        private static PlayMakerFSM? GetBoxFsm(string color)
        {
            var box = GameObject.Find($"_GAMEPLAY/PARLOR GAME/ParlorBox {color}/Parlor Box/Keyhole");

            if (box == null)
            {
                return null;
            }

            var boxFsm = box.GetComponent<PlayMakerFSM>();
            if (boxFsm == null)
            {
                return null;
            }

            return boxFsm;
        }

        private void DisableFsmStateAction(string color, string stateName, int actionIndex)
        {
            var gameFsm = GetParlorGameFsm();
            var boxFsm = GetBoxFsm(color);

            if (gameFsm == null)
            {
                _logger.Msg($"Could not find an active Parlor Game with an FSM.");
                return;
            }

            if (boxFsm == null)
            {
                _logger.Msg($"Could not find a {color} Box with an FSM.");
                return;
            }

            DisableFsmStateAction(boxFsm, stateName, actionIndex);
        }

        private void DisableFsmStateAction(PlayMakerFSM fsm, string stateName, int actionIndex)
        {
            _logger.Msg($"Disabling Action {actionIndex} for state {stateName} in {fsm.FsmName}");
            foreach (var state in fsm.FsmStates)
            {
                if (state.Name == stateName)
                {
                    var action = state.Actions[actionIndex];
                    action.Enabled = false;
                    _logger.Msg($"Disabled Action {action.Name} for state {state.Name} in {fsm.FsmName}");
                }
            }
        }

        private void PrintFsmStates(string color)
        {
            var gameFsm = GetParlorGameFsm();
            var boxFsm = GetBoxFsm(color);

            if (gameFsm == null)
            {
                _logger.Msg($"Could not find an active Parlor Game with an FSM.");
                return;
            }

            if (boxFsm == null)
            {
                _logger.Msg($"Could not find a {color} Box with an FSM.");
                return;
            }

            PrintFsmStates(gameFsm);
            PrintFsmStates(boxFsm);
        }

        private void PrintFsmStates(PlayMakerFSM fsm)
        {
            if (fsm == null)
            {
                _logger.Msg($"Could not find requested FSM.");
                return;
            }

            _logger.Msg($"Fsm {fsm.FsmName} states:");
            foreach (var state in fsm.FsmStates)
            {
                var message = $"\t{state.Name}";
                if (state.Name == fsm.ActiveStateName)
                {
                    message += $"(Active State)";
                }
                _logger.Msg(message);
            }
        }

        private void SendEventAndLog(PlayMakerFSM fsm, string eventName)
        {
            fsm.SendEvent(eventName);
            _logger.Msg($"Sent {eventName} event, State is now: {fsm.ActiveStateName}");
        }

        private void SetStateAndLog(PlayMakerFSM fsm, string stateName)
        {
            fsm.SetState(stateName);
            _logger.Msg($"Set state to {stateName}, State is now: {fsm.ActiveStateName}");
        }
    }
}
