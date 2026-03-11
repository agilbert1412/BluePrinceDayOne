using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace KaitoKid.BluePrinceDayOne
{
    public static class Events
    {
        private static MelonLogger.Instance _logger;

        public static void Initialize(MelonLogger.Instance logger)
        {
            _logger = logger;
        }

        public static void OnRoomSpawned(GameObject room, GameObject roomTransform)
        {
            if (room == null)
            {
                return;
            }

            // _logger.Msg($"Item: {room.name}");

            if (roomTransform == null)
            {
                return;
            }

            // _logger.Msg($"Transform: {roomTransform.name} - {roomTransform.transform.position.ToString()}");

            var roomName = room.name;

            // _logger.Msg($"Spawned: {roomName}");


            RegisterParlor(roomName);
            RegisterDarts(roomName);
        }

        private static void RegisterParlor(string roomName)
        {
            if (roomName.StartsWith("Parlor"))
            {
                var parlorGame = GameObject.Find($"{roomName}/_GAMEPLAY/PARLOR GAME");
                var gameFsm = parlorGame.GetComponent<PlayMakerFSM>();
                if (gameFsm == null)
                {
                    _logger.Msg($"Parlor GameObject did not contain a `{nameof(PlayMakerFSM)}`");
                    return;
                }

                DayOneMod.Instance.ParlorGameToSolve = gameFsm;
            }
        }

        private static void RegisterDarts(string roomName)
        {
            if (roomName.StartsWith("Billiard Room"))
            {
                var parlorGame = GameObject.Find($"{roomName}/_GAMEPLAY/Dartboard/Dartboard System");
                var gameFsm = parlorGame.GetComponent<PlayMakerFSM>();
                if (gameFsm == null)
                {
                    _logger.Error($"Dartboard GameObject did not contain a `{nameof(PlayMakerFSM)}`");
                    return;
                }

                DayOneMod.Instance.DartboardToSolve = gameFsm;
            }
        }
    }
}
