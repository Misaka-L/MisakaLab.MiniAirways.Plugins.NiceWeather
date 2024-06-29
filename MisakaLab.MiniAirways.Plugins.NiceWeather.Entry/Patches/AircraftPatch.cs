using HarmonyLib;
using UnityEngine;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Patches;

[HarmonyPatch(typeof(Aircraft))]
internal class AircraftPatch
{
    [HarmonyPatch("Start"), HarmonyPrefix]
    private static void StartPrefix(Aircraft __instance)
    {
        var tailRenderer = __instance.gameObject.AddComponent<TrailRenderer>();

        var lineMaterial = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));

        tailRenderer.material = lineMaterial;
        tailRenderer.widthMultiplier = 0.08f;
        tailRenderer.time = 15f;

        lineMaterial.color = ColorCode.GetColor(__instance.colorCode);
    }
}