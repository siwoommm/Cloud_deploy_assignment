using UnityEngine;
using ZXing; 

public class QRCodeScanner : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private IBarcodeReader reader;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("Detected devices:");
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log($"Camera {i}: {devices[i].name}");
        }

        webcamTexture = new WebCamTexture(devices[0].name); // 640x480이 안 되니 일단 320x240
        webcamTexture.Play();
        reader = new BarcodeReader();

        GetComponent<Renderer>().material.mainTexture = webcamTexture;

    }
    void Update()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            try
            {
                Color32[] pixels = webcamTexture.GetPixels32();
                var result = reader.Decode(pixels, webcamTexture.width, webcamTexture.height);
                if (result != null)
                {
                    QRCodeInfo qrInfo = QRCodeParser.Parse(result.Text);
                    Debug.Log($"[QR Info] {qrInfo}");

                    // 예시: 위치 초기화
                    var navManager = FindFirstObjectByType<NavigationManager>();
                    navManager?.SetStartFromQR(qrInfo);
                    // 예시: node ID 활용 A* 탐색 시작 가능
                    // AStarManager.Instance.SetStartNode(info.Node);
                }
            }
            catch {}
        }
    }
}


