using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapGraph : MonoBehaviour
{
    public TMP_Dropdown goalNodeDropdown;

    public Dictionary<string, MapNode> nodes = new Dictionary<string, MapNode>();
    public List<string> lastPath = new List<string>();  // A* 경로 저장용
    public string currentStartId = null;  // QR로 설정됨
    public string goalId = null;          // 도착지 수동 지정

    void Start()
    {
        // 임시 QR 위치 지정 (테스트용)
        //SetStartNodeFromQR("S06-0401");
        // 노드 추가
        // ✅ 왼쪽 복도 (서쪽 복도)
        AddNode("S06-0635", new Vector2(0, 0), 6);
        AddNode("S06-0634", new Vector2(1, 0), 6);
        AddNode("S06-0633", new Vector2(2, 0), 6);
        AddNode("S06-0632", new Vector2(3, 0), 6);
        AddNode("S06-0631", new Vector2(4, 0), 6);
        AddNode("S06-0630", new Vector2(5, 0), 6);
        AddNode("S06-0629", new Vector2(6, 0), 6);
        AddNode("S06-0628", new Vector2(7, 0), 6);
        AddNode("S06-0627", new Vector2(8, 0), 6);
        AddNode("S06-0626", new Vector2(9, 0), 6);
        AddNode("S06-0625", new Vector2(10, 0), 6);
        AddNode("S06-0624", new Vector2(11, 0), 6);
        AddNode("S06-0623", new Vector2(12, 0), 6);
        AddNode("S06-0622", new Vector2(13, 0), 6); // 중앙 코너

        // ✅ 아래 복도 (남쪽 복도)
        AddNode("S06-0621", new Vector2(13, -1), 6);
        AddNode("S06-0620", new Vector2(13, -2), 6);
        AddNode("S06-0619", new Vector2(13, -3), 6);
        AddNode("S06-0618", new Vector2(13, -4), 6);
        AddNode("S06-0617", new Vector2(13, -5), 6);
        AddNode("S06-0616", new Vector2(13, -6), 6);
        AddNode("S06-0615", new Vector2(13, -7), 6);
        AddNode("S06-0614", new Vector2(13, -8), 6);

        // ✅ 오른쪽 복도 (동쪽 대각선 복도)
        AddNode("S06-0601", new Vector2(14, 1), 6);
        AddNode("S06-0602", new Vector2(15, 2), 6);
        AddNode("S06-0603", new Vector2(16, 3), 6);
        AddNode("S06-0604", new Vector2(17, 4), 6);
        AddNode("S06-0605", new Vector2(18, 5), 6);
        AddNode("S06-0606", new Vector2(19, 6), 6);
        AddNode("S06-0607", new Vector2(20, 7), 6);
        AddNode("S06-0608", new Vector2(21, 8), 6);
        AddNode("S06-0609", new Vector2(22, 9), 6);

        // ✅ 왼쪽 복도 연결
        AddEdge("S06-0635", "S06-0634");
        AddEdge("S06-0634", "S06-0633");
        AddEdge("S06-0633", "S06-0632");
        AddEdge("S06-0632", "S06-0631");
        AddEdge("S06-0631", "S06-0630");
        AddEdge("S06-0630", "S06-0629");
        AddEdge("S06-0629", "S06-0628");
        AddEdge("S06-0628", "S06-0627");
        AddEdge("S06-0627", "S06-0626");
        AddEdge("S06-0626", "S06-0625");
        AddEdge("S06-0625", "S06-0624");
        AddEdge("S06-0624", "S06-0623");
        AddEdge("S06-0623", "S06-0622");

        // ✅ 아래 복도 연결
        AddEdge("S06-0622", "S06-0621");
        AddEdge("S06-0621", "S06-0620");
        AddEdge("S06-0620", "S06-0619");
        AddEdge("S06-0619", "S06-0618");
        AddEdge("S06-0618", "S06-0617");
        AddEdge("S06-0617", "S06-0616");
        AddEdge("S06-0616", "S06-0615");
        AddEdge("S06-0615", "S06-0614");

        // ✅ 오른쪽 복도 연결
        AddEdge("S06-0622", "S06-0601");
        AddEdge("S06-0601", "S06-0602");
        AddEdge("S06-0602", "S06-0603");
        AddEdge("S06-0603", "S06-0604");
        AddEdge("S06-0604", "S06-0605");
        AddEdge("S06-0605", "S06-0606");
        AddEdge("S06-0606", "S06-0607");
        AddEdge("S06-0607", "S06-0608");
        AddEdge("S06-0608", "S06-0609");
        
        PopulateDropdowns();


        // A* 테스트
        //SetStartNodeFromQR("S06-0401"); 

    }

    public void AddNode(string id, Vector2 pos, int floor)
    {
        nodes[id] = new MapNode(id, pos, floor);
    }

    public void AddEdge(string from, string to)
    {
        if (nodes.ContainsKey(from) && nodes.ContainsKey(to))
        {
            if (!nodes[from].Neighbors.Contains(to))
                nodes[from].Neighbors.Add(to);
            if (!nodes[to].Neighbors.Contains(from))
                nodes[to].Neighbors.Add(from);
        }
    }
    public void SetStartNodeFromQR(string qrNodeId)
    {
        if (!nodes.ContainsKey(qrNodeId))
        {
            Debug.LogWarning($"QR 노드 '{qrNodeId}'가 존재하지 않습니다.");
            return;
        }

        currentStartId = qrNodeId;
        lastPath = FindPath(currentStartId, goalId);

        Debug.Log($"📍 QR 위치 초기화: {currentStartId} → {goalId}");
        Debug.Log("🧭 A* 경로: " + string.Join(" → ", lastPath));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var node in nodes.Values)
        {
            Vector3 from = new Vector3(node.Position.x, node.Position.y, 0);
            Gizmos.DrawSphere(from, 0.05f);

            #if UNITY_EDITOR
            Handles.Label(from + Vector3.up * 0.1f, node.Id);
            #endif

            foreach (var neighborId in node.Neighbors)
            {
                if (nodes.TryGetValue(neighborId, out var neighbor))
                {
                    Vector3 to = new Vector3(neighbor.Position.x, neighbor.Position.y, 0);
                    Gizmos.DrawLine(from, to);
                }
            }
        }

        // 🔴 A* 경로 시각화 (빨간 선)
        if (lastPath != null && lastPath.Count >= 2)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < lastPath.Count - 1; i++)
            {
                Vector3 from = new Vector3(nodes[lastPath[i]].Position.x, nodes[lastPath[i]].Position.y, 0);
                Vector3 to = new Vector3(nodes[lastPath[i + 1]].Position.x, nodes[lastPath[i + 1]].Position.y, 0);
                Gizmos.DrawLine(from, to);
            }
        }
    }

    // ===============================
    // A* 알고리즘
    // ===============================
    public List<string> FindPath(string startId, string goalId)
    {
        if (!nodes.ContainsKey(startId) || !nodes.ContainsKey(goalId))
            return null;

        var openSet = new PriorityQueue<string>();
        var cameFrom = new Dictionary<string, string>();
        var gScore = new Dictionary<string, float>();
        var fScore = new Dictionary<string, float>();

        foreach (var node in nodes.Keys)
        {
            gScore[node] = float.PositiveInfinity;
            fScore[node] = float.PositiveInfinity;
        }

        gScore[startId] = 0f;
        fScore[startId] = Heuristic(startId, goalId);
        openSet.Enqueue(startId, fScore[startId]);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current == goalId)
                return ReconstructPath(cameFrom, current);

            foreach (var neighborId in nodes[current].Neighbors)
            {
                float tentativeG = gScore[current] + Vector2.Distance(nodes[current].Position, nodes[neighborId].Position);

                if (tentativeG < gScore[neighborId])
                {
                    cameFrom[neighborId] = current;
                    gScore[neighborId] = tentativeG;
                    fScore[neighborId] = tentativeG + Heuristic(neighborId, goalId);
                    if (!openSet.Contains(neighborId))
                        openSet.Enqueue(neighborId, fScore[neighborId]);
                }
            }
        }

        return null;
    }

    float Heuristic(string a, string b)
    {
        return Vector2.Distance(nodes[a].Position, nodes[b].Position);
    }

    List<string> ReconstructPath(Dictionary<string, string> cameFrom, string current)
    {
        List<string> path = new List<string> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }
    public void SetStartAndGoal(string start, string goal)
    {
        Debug.Log($"🔍 요청된 Start: {start}, Goal: {goal}");
        Debug.Log($"Start 존재함? {nodes.ContainsKey(start)}");
        Debug.Log($"Goal 존재함? {nodes.ContainsKey(goal)}");
        if (!nodes.ContainsKey(start) || !nodes.ContainsKey(goal))
        {
            Debug.LogWarning("⚠️ 유효하지 않은 start 또는 goal 노드입니다.");
            return;
        }

        currentStartId = start;
        goalId = goal;

        lastPath = FindPath(currentStartId, goalId);
        Debug.Log($"📍 경로 요청: {currentStartId} → {goalId}");
        Debug.Log("🧭 A* 경로: " + string.Join(" → ", lastPath));
    }
    void PopulateDropdowns()
    {
        if (goalNodeDropdown == null)
        {
            Debug.LogWarning("Goal Dropdown reference not set in Inspector.");
            return;
        }

        List<string> nodeIds = nodes.Keys.ToList();
        goalNodeDropdown.ClearOptions();
        goalNodeDropdown.AddOptions(nodeIds);

        // 드롭다운에서 목표 노드 선택 시
        goalNodeDropdown.onValueChanged.AddListener((index) =>
        {
            goalId = goalNodeDropdown.options[index].text;

            // QR로 startId가 설정되어 있으면 바로 경로 계산
            if (!string.IsNullOrEmpty(currentStartId))
            {
                SetStartAndGoal(currentStartId, goalId);
            }
        });
    }

}

// ===============================
// 간단한 우선순위 큐
// ===============================
public class PriorityQueue<T>
{
    private List<(T item, float priority)> elements = new List<(T, float)>();

    public int Count => elements.Count;

    public void Enqueue(T item, float priority)
    {
        elements.Add((item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;
        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].priority < elements[bestIndex].priority)
                bestIndex = i;
        }

        T bestItem = elements[bestIndex].item;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }

    public bool Contains(T item)
    {
        return elements.Any(e => EqualityComparer<T>.Default.Equals(e.item, item));
    }
    

}
