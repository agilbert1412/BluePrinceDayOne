using HarmonyLib;
using Il2CppHutongGames.PlayMaker.Actions;
using MelonLoader;

namespace KaitoKid.BluePrinceDayOne
{
    [HarmonyPatch(typeof(PmtSpawn), "OnEnter")]
    public class RoomPatches
    {
        private static MelonLogger.Instance _logger;

        public static void Initialize(MelonLogger.Instance logger)
        {
            _logger = logger;
        }

        public static void Postfix(PmtSpawn __instance)
        {
            try
            {
                // _logger.Msg($"Executing DayOne.{nameof(RoomPatches)}.{nameof(Postfix)}");
                if (__instance == null) return;

                var obj = __instance.gameObject.value;
                var poolName = __instance.poolName.value;
                var transformObj = __instance.spawnTransform.value;
                //_logger.Msg($"obj: {obj}");
                //_logger.Msg($"poolName: {poolName}");
                if (true || poolName == "Rooms")
                {
                    Events.OnRoomSpawned(obj, transformObj);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed in `{nameof(RoomPatches)}.{nameof(Postfix)}`: {ex.Message}. Stack Trace: {ex.StackTrace}");
            }
        }
    }


    //[HarmonyPatch(typeof(StatsLogger), nameof(StatsLogger.BeginDay), new []{typeof(int)})]
    //public class StatsLoggerBeginDayPatch
    //{
    //    private static MelonLogger.Instance _logger;

    //    public static void Initialize(MelonLogger.Instance logger)
    //    {
    //        _logger = logger;
    //    }

    //    public static void Postfix(StatsLogger __instance)
    //    {
    //        try
    //        {
    //            _logger.Msg($"Executing DayOne.{nameof(StatsLoggerBeginDayPatch)}.{nameof(Postfix)}");

    //            var dayStats = __instance.CurrentDayStats;
    //            dayStats.Allowance = 10;
    //            foreach (var a in __instance.CurrentData.GlobalEvents.EventsCount)
    //            {
    //                _logger.Msg($"Event already there: {a.Key} ({a.Value})");
    //            }
    //            __instance.CurrentData.GlobalEvents.AddEvent(EventID.Boudoir_Safe_Opened);
    //            __instance.CurrentData.GlobalEvents.AddEvent(EventID.Drafting_Studio_Safe_Opened);
    //            __instance.CurrentData.GlobalEvents.AddEvent(EventID.Drawing_Room_Safe_Opened);
    //            __instance.CurrentData.GlobalEvents.AddEvent(EventID.Office_Safe_Opened);
    //            __instance.CurrentData.GlobalEvents.AddEvent(EventID.Shelter_Safe_Opened);
    //            __instance.CurrentData.GlobalEvents.AddEvent(EventID.Study_Safe_Opened);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.Error($"Failed in `{nameof(StatsLoggerBeginDayPatch)}.{nameof(Postfix)}`: {ex.Message}. Stack Trace: {ex.StackTrace}");
    //        }
    //    }
    //}
}
