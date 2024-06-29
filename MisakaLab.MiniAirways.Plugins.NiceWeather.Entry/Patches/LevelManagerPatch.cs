using HarmonyLib;
using MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Managers;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Patches;

[HarmonyPatch(typeof(LevelManager))]
internal class LevelManagerPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Start")]
    private static void StartPrefix(LevelManager __instance)
    {
        __instance.gameObject.AddComponent<WeatherAreaManager>();
    }
}