using UnityEngine;

public class UserPositionMapper : MonoBehaviour
{
    public Transform arCameraTransform;
    public float detectionRadius = 0.5f;

    private MapGraph mapGraph;

    void Start()
    {
        mapGraph = FindObjectOfType<MapGraph>();
    }

    void Update()
    {
        if (mapGraph == null || arCameraTransform == null)
            return;

        string closestNodeId = null;
        float minDist = float.MaxValue;

        foreach (var kvp in mapGraph.nodes)
        {
            Vector2 nodePos = kvp.Value.Position;
            Vector2 userPos = new Vector2(arCameraTransform.position.x, arCameraTransform.position.z);

            float dist = Vector2.Distance(userPos, nodePos);
            if (dist < minDist)
            {
                minDist = dist;
                closestNodeId = kvp.Key;
            }
        }

        if (minDist <= detectionRadius && closestNodeId != mapGraph.currentStartId)
        {
            mapGraph.currentStartId = closestNodeId;
            Debug.Log($"📍 사용자 현재 위치 → {closestNodeId} (거리: {minDist:F2})");
        }
    }
}
