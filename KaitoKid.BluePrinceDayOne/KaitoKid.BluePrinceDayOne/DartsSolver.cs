using Il2Cpp;
using MelonLoader;

namespace KaitoKid.BluePrinceDayOne
{
    public class DartsSolver
    {
        private MelonLogger.Instance _logger;
        private PlayMakerFSM _dartsGameToSolve;

        public DartsSolver(MelonLogger.Instance logger)
        {
            _logger = logger;
        }

        public bool SolveDartboard(PlayMakerFSM dartsGameToSolve)
        {
            //if (dartsGameToSolve.ActiveStateName != "Waiting for selection")
            //{
            //    return false;
            //}

            _dartsGameToSolve = dartsGameToSolve;
            SolveDartboard();

            return true;
        }

        public void SolveDartboard()
        {
            if (_dartsGameToSolve == null)
            {
                _logger.Msg($"Could not find an active Dartboard Game with an FSM.");
                return;
            }

            _logger.Msg($"Trying to solve Dartboard game (Current State: `{_dartsGameToSolve.ActiveStateName}`)");

            SetStateAndLog(_dartsGameToSolve, "Switch 10");
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
