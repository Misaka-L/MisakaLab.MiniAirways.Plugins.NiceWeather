using UnityEngine;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Utils;

internal static class RandomUtils
{
    public static float RandomNotInRange(float min, float max)
    {
        var number1 = Random.Range(min - 6, min);
        var number2 = Random.Range(max, max + 6);

        var chose = Random.Range(0, 1);

        return chose < 0.5f ? number1 : number2;
    }

    public static Vector2[] GetRandomPolygonPoints()
    {
        var count = Random.RandomRangeInt(3, 10);

        var positions = new Vector2[count];

        for (var i = 0; i < count; i++)
        {
            var angle = 2 * Mathf.PI / count * i;
            var radius = Random.Range(1f, 3f); // Random radius
            positions[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        }

        return positions;
    }
}