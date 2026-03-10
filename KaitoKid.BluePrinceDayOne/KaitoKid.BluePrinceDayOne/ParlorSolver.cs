using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KaitoKid.BluePrinceDayOne
{
    public class ParlorSolver
    {
        private MelonLogger.Instance _logger;
        private PlayMakerFSM _parlorGameToSolve;

        public ParlorSolver(MelonLogger.Instance logger)
        {
            _logger = logger;
        }

        public bool SolveParlorGame(PlayMakerFSM parlorGameToSolve)
        {
            if (parlorGameToSolve.ActiveStateName != "Waiting for selection")
            {
                return false;
            }

            _parlorGameToSolve = parlorGameToSolve;
            OpenCorrectParlorBox();

            return true;
        }

        public void OpenCorrectParlorBox()
        {
            if (_parlorGameToSolve == null)
            {
                _logger.Msg($"Could not find an active Parlor Game with an FSM.");
                return;
            }

            SendEventAndLog(_parlorGameToSolve, "Event 0");
            var correctBoxColor = _parlorGameToSolve.ActiveStateName.Split(" ")[0];
            _logger.Msg($"Parlor Solution is {correctBoxColor} because state is `{_parlorGameToSolve.ActiveStateName}`");

            var boxFsm = GetBoxFsm(correctBoxColor);

            if (boxFsm == null)
            {
                _logger.Msg($"Could not find a {correctBoxColor} Box with an FSM.");
                return;
            }

            _logger.Msg($"Found a {correctBoxColor} Box with an FSM, State is: {boxFsm.ActiveStateName}");

            SetStateAndLog(boxFsm, "Click");
            SendEventAndLog(_parlorGameToSolve, correctBoxColor.ToLower());
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

        private void SendEventAndLog(List<PlayMakerFSM> fsms, string eventName)
        {
            foreach (var playMakerFsm in fsms)
            {
                SendEventAndLog(playMakerFsm, eventName);
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
