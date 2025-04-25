using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    public TMP_Dropdown goalDropdown;  // TextMeshPro Dropdown 사용 시
    private string startId = "";
    private string goalId = "";

    void Start()
    {
        PopulateDropdownFromGraph();
    }

    public void SetStartFromQR(QRCodeInfo qrInfo)
    {
        startId = qrInfo.Node;
        TryNavigate();
    }

    public void OnGoalSelected(int index)
    {
        goalId = goalDropdown.options[index].text;
        TryNavigate();
    }

    private void TryNavigate()
    {
        if (!string.IsNullOrEmpty(startId) && !string.IsNullOrEmpty(goalId))
        {
            Debug.Log($"📍 TryNavigate() 호출됨 - Start: {startId}, Goal: {goalId}");
            
            var graph = FindObjectOfType<MapGraph>();
            var path = graph.FindPath(startId, goalId);
            if (path != null)
            {
                Debug.Log("경로 탐색 완료: " + string.Join(" → ", path));
                graph.lastPath = path; // Gizmo 경로 업데이트용
            }
            else
            {
                Debug.LogWarning("경로를 찾지 못했습니다.");
            }
        }
    }

    private void PopulateDropdownFromGraph()
    {
        var graph = FindFirstObjectByType<MapGraph>();
        if (graph == null) return;

        List<string> nodeIds = graph.nodes.Keys.ToList();

        goalDropdown.ClearOptions();
        goalDropdown.AddOptions(nodeIds);

        goalDropdown.onValueChanged.AddListener(OnGoalSelected);
    }

    // 선택적으로 사용할 수 있는 Dropdown 변화 대응 메서드
    public void OnDropdownChanged(int index)
    {
        goalId = goalDropdown.options[index].text;
        TryNavigate();
    }
}
