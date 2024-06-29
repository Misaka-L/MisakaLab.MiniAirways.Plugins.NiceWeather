using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;
using MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Managers;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Patches;

[HarmonyPatch]
internal class GameOverManagerGameOverMethodPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        return AccessTools.GetTypesFromAssembly(typeof(GameOverManager).Assembly)
            .SelectMany(type => type.GetMethods())
            .Where(method => method.Name == nameof(GameOverManager.GameOver));
    }

    private static void Prefix()
    {
        var weatherAreaManager = LevelManager.Instance.gameObject.GetComponent<WeatherAreaManager>();
        weatherAreaManager.GameOver();
    }
}