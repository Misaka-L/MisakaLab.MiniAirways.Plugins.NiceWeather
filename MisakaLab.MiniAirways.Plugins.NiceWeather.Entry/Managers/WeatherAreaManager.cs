using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Behaviours;
using MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Utils;
using UnityEngine;
using Logger = BepInEx.Logging.Logger;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Managers;

internal class WeatherAreaManager : MonoBehaviour
{
    private const int RestrictedAreaLayer = 25;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private readonly ManualLogSource _logger = Logger.CreateLogSource(typeof(WeatherAreaManager).FullName);

    private GameObject _currentWeather;

    private void Start()
    {
        _logger.LogInfo("GameStart!");

        _currentWeather = CreateWeatherArea(GetRandomWeatherPosition(), RandomUtils.GetRandomPolygonPoints());
    }

    private static Vector2 GetRandomWeatherPosition()
    {
        var mapHalfSize = MapUtils.GetMapHalfSize();

        var x = RandomUtils.RandomNotInRange(-mapHalfSize.x, mapHalfSize.x);
        var y = RandomUtils.RandomNotInRange(-mapHalfSize.y, mapHalfSize.y);

        return new Vector2(x, y);
    }

    public void CreateNewWeatherArea()
    {
        CreateWeatherArea(GetRandomWeatherPosition(), RandomUtils.GetRandomPolygonPoints());
    }

    private GameObject CreateWeatherArea(Vector2 position, Vector2[] points)
    {
        // GameObject Creation
        var restrictedAreaGameObject = new GameObject("WeatherArea");
        var restrictedAreaColliderGameObject = new GameObject("WeatherAreaCollider")
        {
            layer = RestrictedAreaLayer,
            transform = { parent = restrictedAreaGameObject.transform, localPosition = new Vector3(0, 0, 0) }
        };

        restrictedAreaGameObject.transform.position = position;

        // Create Physics
        var polygonCollider = restrictedAreaColliderGameObject.AddComponent<PolygonCollider2D>();
        var rigidBody = restrictedAreaColliderGameObject.AddComponent<Rigidbody2D>();

        polygonCollider.isTrigger = true;
        polygonCollider.tag = "CollideCheck";

        rigidBody.isKinematic = true;

        polygonCollider.points = points;

        // Graphics
        var lineRenderer = restrictedAreaGameObject.AddComponent<LineRenderer>();

        var lineMaterial = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));
        lineRenderer.material = lineMaterial;
        lineRenderer.loop = true;
        lineRenderer.widthMultiplier = 0.08f;
        lineRenderer.useWorldSpace = false;

        var vector3Points = points.Select(point => new Vector3(point.x, point.y)).ToArray();
        lineRenderer.positionCount = 4;
        lineRenderer.SetPositions(vector3Points);

        var mesh = CreateMesh(points);

        var meshFilter = restrictedAreaGameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        using var textureStream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(
                "MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Assets.MdiWeatherLightningRainy.png");
        var textureBytes = new byte[textureStream.Length];
        textureStream.Read(textureBytes, 0, (int)textureStream.Length);

        var meshMaterial = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));

        var texture = new Texture2D(512, 512);
        texture.LoadImage(textureBytes);

        meshMaterial.SetTexture(MainTex, texture);

        var meshRenderer = restrictedAreaGameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(meshMaterial);

        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateTangents();

        // Scripts
        var weatherArea = restrictedAreaGameObject.AddComponent<WeatherArea>();

        weatherArea.SetWeatherAreaManager(this);

        return restrictedAreaGameObject;
    }

    private Mesh CreateMesh(Vector2[] points)
    {
        var mesh = new Mesh();

        var vertices = points.Select(point => new Vector3(point.x, point.y)).ToArray();
        var triangulates = new Triangulator(points).Triangulate();

        mesh.vertices = vertices;
        mesh.triangles = triangulates;
        mesh.uv = points;

        return mesh;
    }

    public void GameOver()
    {
        _logger.LogInfo("GameOver!");
    }
}