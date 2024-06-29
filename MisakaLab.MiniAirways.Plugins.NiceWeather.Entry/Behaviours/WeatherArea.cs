using MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Managers;
using MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Utils;
using UnityEngine;

namespace MisakaLab.MiniAirways.Plugins.NiceWeather.Entry.Behaviours;

internal class WeatherArea : MonoBehaviour
{
    private Vector3 _moveDirection;
    private Vector3 _targetPosition;

    private WeatherAreaManager _weatherAreaManager;

    private bool _hasEnterMap;

    private void Start()
    {
        Debug.Log("Weather Area Added");

        var mapHalfSize = MapUtils.GetMapHalfSize();

        _targetPosition = new Vector3(Random.Range(-mapHalfSize.x, mapHalfSize.x),
            Random.Range(-mapHalfSize.y, mapHalfSize.y));
        _moveDirection = transform.position - _targetPosition;
    }

    public void SetWeatherAreaManager(WeatherAreaManager weatherAreaManager)
    {
        _weatherAreaManager = weatherAreaManager;
    }

    private void FixedUpdate()
    {
        transform.position += _moveDirection.normalized * -0.001f;

        var mapHalfSize = MapUtils.GetMapHalfSize();
        var distanceToTarget = transform.position - _targetPosition;

        if (distanceToTarget.magnitude < mapHalfSize.magnitude)
        {
            if (!_hasEnterMap)
            {
                Debug.Log("Weather Area Enter Map");
                _hasEnterMap = true;
            }
        }
        else if (_hasEnterMap)
        {
            _weatherAreaManager.CreateNewWeatherArea();
            Destroy(gameObject);
        }
    }
}