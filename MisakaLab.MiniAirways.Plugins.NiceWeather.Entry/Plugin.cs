using System;
using BepInEx;
using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("MiniAirways.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "MisakaLab.MiniAirways.Plugins.NiceWeather.Entry";
        private const string PluginName = "MisakaLab.MiniAirways.Plugins.NiceWeather.Entry";
        private const string PluginVersion = "0.0.1";

        internal static IServiceProvider ServiceProvider { get; private set; }

        private void Awake()
        {
            var services = new ServiceCollection();

            ServiceProvider = services.BuildServiceProvider();

            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll();
        }
    }
}