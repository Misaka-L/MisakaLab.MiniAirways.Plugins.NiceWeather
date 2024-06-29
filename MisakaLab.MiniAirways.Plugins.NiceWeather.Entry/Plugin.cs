using BepInEx;
using HarmonyLib;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInProcess("MiniAirways.exe")]
public class Plugin : BaseUnityPlugin
{
    private const string PluginGuid = "MisakaLab.MiniAirways.Plugins.NiceWeather.Entry";
    private const string PluginName = "MisakaLab.MiniAirways.Plugins.NiceWeather.Entry";
    private const string PluginVersion = "0.0.1";

    private void Awake()
    {
        var harmony = new Harmony(PluginGuid);
        harmony.PatchAll();
    }
}