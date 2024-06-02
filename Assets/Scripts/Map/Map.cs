using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Map : MonoBehaviour
{
    //apiKey와 url
    public string apiKey = "";
    private string url = "";

    //맵 크기와 확대
    private int mapWidth = 640;
    private int mapHeight = 640;
    public int zoom = 12;
    public int maxZoom = 20;

    //위도와 경도 
    public float lat = 33.85660618894087f;
    public float lon = 151.21500701957325f;

    private Rect rect;
    public enum resolution { low = 1, high = 2 };
    public resolution mapResolution = resolution.low;

    public enum type
    {
        roadmap,
        satellite,
        gybrid,
        terrain,
    }
    public type mapType = type.roadmap;

    //값이 변경되면 업데이트 하기 위한 최근값
    private string apiKeyLast;
    protected float latLast = -33.85660618894087f;
    protected float lonLast = 151.21500701957325f;
    private int zoomLast = 12;
    private resolution mapResolutionLast = resolution.low;
    private type mapTypeLast = type.roadmap;
    protected bool updateMap = true;

    // Touch handling variables
    private bool isDragging = false;
    private Vector2 lastTouchPosition;
    private Vector2 lastMousePosition;
    public float dragSpeed = 0.1f;

    protected void Start()
    {
        //맵 불러오기
        StartCoroutine(GetStaticMap());

        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;

        //맵 크기를 rectTransform과 같게 만듦
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int)Math.Round(rect.height);
    }

    void Update()
    {
        HandleInput();
        UpdateMap();
    }

    void UpdateMap()
    {
        //값에 변화가 있으면 업데이트
        if (updateMap && (apiKeyLast != apiKey ||
            !Mathf.Approximately(latLast, lat) ||
            !Mathf.Approximately(lonLast, lon) ||
            zoomLast != zoom ||
            mapResolutionLast != mapResolution ||
            mapTypeLast != mapType))
        {
            rect = gameObject.GetComponent<RawImage>().rectTransform.rect;

            mapWidth = (int)Math.Round(rect.width);
            mapHeight = (int)Math.Round(rect.height);

            StartCoroutine(GetStaticMap());

            updateMap = false;
        }
    }



    //구글API를 통해 구글맵을 가져오는 코루틴
    protected IEnumerator GetStaticMap()
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?" +
            "&center=" + lat + "," + lon +
            "&zoom=" + zoom +
            "&size=" + mapWidth + "x" + mapHeight +
            "&scale=" + mapResolution +
            "&maptype=" + mapType +
            "&key=" + apiKey;

        //query에 현재위치정보를 단말기에서 가져온다.
        var query = "";
        //query += "&center =" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));
        //query += "&marker =" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url + query);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            //오류가 없으면
            Destroy(GetComponent<RawImage>().texture);
            GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            apiKeyLast = apiKey;
            latLast = lat;
            lonLast = lon;
            zoomLast = zoom;
            mapResolutionLast = mapResolution;
            mapTypeLast = mapType;
            updateMap = true;

            Debug.Log("GoogleMap 불러오기 성공" + query);
            
        }
        else
        {
            //오류가 있으면

            Debug.Log("GoogleMap 불러오기 실패" + query);
        }
    }

    //버튼에 할당할 확대 축소 버튼
    public void ZoomButton(bool _plusMinus)
    {
        if(_plusMinus)
        {

            zoom++;
            if (zoom > maxZoom)
                zoom = maxZoom;
        }
        else
        {
            zoom--;
            if (zoom < 0)
                zoom = 0;
        }
    }
    //TODO : 마우스 드래그 이동거리로 LOn 0.001씩 이동시키기 

    void HandleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 touchDelta = touch.position - lastTouchPosition;
                lastTouchPosition = touch.position;

                // Convert touch delta to map movement
                float latMovement = touchDelta.y * dragSpeed / 1000;
                float lonMovement = touchDelta.x * dragSpeed / 1000;

                lat -= latMovement;
                lon -= lonMovement;

                // Clamp latitude to [-85, 85] range to avoid polar distortion
                lat = Mathf.Clamp(lat, -85f, 85f);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            // Convert mouse delta to map movement
            float latMovement = mouseDelta.y * dragSpeed / 1000;
            float lonMovement = mouseDelta.x * dragSpeed / 1000;

            lat -= latMovement;
            lon -= lonMovement;

            // Clamp latitude to [-85, 85] range to avoid polar distortion
            lat = Mathf.Clamp(lat, -85f, 85f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void HandleInput()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }
}
