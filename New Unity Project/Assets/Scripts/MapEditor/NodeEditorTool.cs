// // NodeEditorTool.cs
// using UnityEngine;
// using System.Collections.Generic;

// public class NodeEditorTool : MonoBehaviour
// {
//     public List<MapNode> nodes = new List<MapNode>();
//     public int currentFloor = 6;
//     public string nodePrefix = "A";

//     private int counter = 1;

//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0))  // 왼쪽 클릭
//         {
//             Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             string id = $"{nodePrefix}{counter:D2}";
//             nodes.Add(new MapNode(id, worldPos, currentFloor));
//             Debug.Log($"Node Added: {id} at {worldPos}");
//             counter++;
//         }

//         if (Input.GetKeyDown(KeyCode.S)) // S키 눌러서 저장
//         {
//             SaveNodesToJson();
//         }
//     }

//     void SaveNodesToJson()
//     {
//         string json = JsonHelper.ToJson(nodes.ToArray(), true);
//         System.IO.File.WriteAllText(Application.dataPath + "/Resources/nodes.json", json);
//         Debug.Log("노드 저장 완료");
//     }
// }
