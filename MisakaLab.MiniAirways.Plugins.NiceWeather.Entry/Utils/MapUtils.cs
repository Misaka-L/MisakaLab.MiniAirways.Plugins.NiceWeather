using UnityEngine;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Utils;

internal static class MapUtils
{
    public static Vector2 GetMapHalfSize()
    {
        var mapYSize = Camera.main.orthographicSize;
        // var mapXSize = Camera.main.aspect * mapYSize;
        var mapXSize = mapYSize / 9 * 16;

        return new Vector2(mapXSize, mapYSize);
    }
}