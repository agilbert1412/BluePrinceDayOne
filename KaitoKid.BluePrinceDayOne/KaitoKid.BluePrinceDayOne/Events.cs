using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MelonLoader.MelonLogger;
using Object = UnityEngine.Object;

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
            if (room != null)
            {
                _logger.Msg($"Item: {room.name}");
            }

            if (roomTransform != null)
            {
                _logger.Msg($"Transform: {roomTransform.name} - {roomTransform.transform.position.ToString()}");
            }

            var roomName = room.name;

            _logger.Msg($"Spawned: {roomName}");
        }
    }
}
