// using UnityEngine;

// public class MapDisplay : MonoBehaviour
// {
//     public string spriteName = "floormap"; // ← 현재 파일명에 맞게

//     void Start()
//     {
//         Debug.Log("✅ MapDisplay.Start() 실행됨");

//         // 시도 1: 직접 로딩
//         Sprite sprite = Resources.Load<Sprite>(spriteName);

//         if (sprite == null)
//         {
//             Debug.LogError($"❌ Resources.Load<Sprite>(\"{spriteName}\") 실패");

//             // 시도 2: 전체 Resources에 로드된 Sprite들 출력
//             Sprite[] allSprites = Resources.LoadAll<Sprite>("");
//             Debug.Log($"🔎 Resources.LoadAll<Sprite>(\"\") 결과: {allSprites.Length}개");

//             foreach (var s in allSprites)
//             {
//                 Debug.Log($"✅ 발견된 Sprite: {s.name}");
//             }

//             return;
//         }

//         SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
//         sr.sprite = sprite;
//         sr.sortingOrder = -10;

//         Debug.Log("✅ Sprite 성공적으로 표시됨");
//     }
// }
