using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWithMarkers : Map
{
    public GameObject markerPrefab; // 위치에 표시할 프리팹
    private List<GameObject> markers = new List<GameObject>();

    new void Start()
    {
        string fileName = "강원";
        string conditionHeader = "상호명";
        string conditionValue = "보건";

        List<(string, string)> values = BinaryDataReader.GetLatLon(fileName, conditionHeader, conditionValue);

        if (values.Count > 0)
        {
            float.TryParse(values[0].Item1, out latLast);
            float.TryParse(values[0].Item2, out lonLast);
        }

        List<(float, float)> coordinates = new List<(float, float)>
        {
            (33.856606f, 151.215007f), // 예시 데이터
            (34.052235f, -118.243683f), // 예시 데이터
        };

        // 프리팹 생성
        StartCoroutine(GenerateMarkers(coordinates));
    }

    IEnumerator GenerateMarkers(List<(float lat, float lon)> coordinates)
    {
        // 모든 기존 마커 제거
        foreach (var marker in markers)
        {
            Destroy(marker);
        }
        markers.Clear();

        foreach (var coord in coordinates)
        {
            GameObject marker = Instantiate(markerPrefab, transform);
            //marker.transform.position = MapToScreenPosition(coord.lat, coord.lon);

            markers.Add(marker);
        }

        // 마커가 모두 보이도록 줌 조정
        AdjustZoomToFitMarkers(coordinates);

        // 맵 업데이트
        updateMap = true;
        yield return GetStaticMap();
    }

    private void AdjustZoomToFitMarkers(List<(float lat, float lon)> coordinates)
    {
        // 최소 및 최대 위도, 경도 계산
        float minLat = float.MaxValue, maxLat = float.MinValue, minLon = float.MaxValue, maxLon = float.MinValue;

        foreach (var coord in coordinates)
        {
            if (coord.lat < minLat) minLat = coord.lat;
            if (coord.lat > maxLat) maxLat = coord.lat;
            if (coord.lon < minLon) minLon = coord.lon;
            if (coord.lon > maxLon) maxLon = coord.lon;
        }

        // 중앙 좌표 계산
        lat = (minLat + maxLat) / 2f;
        lon = (minLon + maxLon) / 2f;

        // 위도, 경도의 차이를 기반으로 적절한 줌 계산 
        //아직 조절x
        float latDiff = maxLat - minLat;
        float lonDiff = maxLon - minLon;
        float maxDiff = Mathf.Max(latDiff, lonDiff);

        zoom = Mathf.Clamp(20 - Mathf.CeilToInt(maxDiff * 10), 0, maxZoom);
    }
}
