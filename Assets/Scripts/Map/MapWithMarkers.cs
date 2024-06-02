using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWithMarkers : Map
{
    public GameObject markerPrefab; // ��ġ�� ǥ���� ������
    private List<GameObject> markers = new List<GameObject>();

    new void Start()
    {
        string fileName = "����";
        string conditionHeader = "��ȣ��";
        string conditionValue = "����";

        List<(string, string)> values = BinaryDataReader.GetLatLon(fileName, conditionHeader, conditionValue);

        if (values.Count > 0)
        {
            float.TryParse(values[0].Item1, out latLast);
            float.TryParse(values[0].Item2, out lonLast);
        }

        List<(float, float)> coordinates = new List<(float, float)>
        {
            (33.856606f, 151.215007f), // ���� ������
            (34.052235f, -118.243683f), // ���� ������
        };

        // ������ ����
        StartCoroutine(GenerateMarkers(coordinates));
    }

    IEnumerator GenerateMarkers(List<(float lat, float lon)> coordinates)
    {
        // ��� ���� ��Ŀ ����
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

        // ��Ŀ�� ��� ���̵��� �� ����
        AdjustZoomToFitMarkers(coordinates);

        // �� ������Ʈ
        updateMap = true;
        yield return GetStaticMap();
    }

    private void AdjustZoomToFitMarkers(List<(float lat, float lon)> coordinates)
    {
        // �ּ� �� �ִ� ����, �浵 ���
        float minLat = float.MaxValue, maxLat = float.MinValue, minLon = float.MaxValue, maxLon = float.MinValue;

        foreach (var coord in coordinates)
        {
            if (coord.lat < minLat) minLat = coord.lat;
            if (coord.lat > maxLat) maxLat = coord.lat;
            if (coord.lon < minLon) minLon = coord.lon;
            if (coord.lon > maxLon) maxLon = coord.lon;
        }

        // �߾� ��ǥ ���
        lat = (minLat + maxLat) / 2f;
        lon = (minLon + maxLon) / 2f;

        // ����, �浵�� ���̸� ������� ������ �� ��� 
        //���� ����x
        float latDiff = maxLat - minLat;
        float lonDiff = maxLon - minLon;
        float maxDiff = Mathf.Max(latDiff, lonDiff);

        zoom = Mathf.Clamp(20 - Mathf.CeilToInt(maxDiff * 10), 0, maxZoom);
    }
}
