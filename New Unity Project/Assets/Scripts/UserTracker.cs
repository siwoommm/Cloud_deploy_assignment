// using UnityEngine;
// using UnityEngine.XR.ARFoundation;

// public class UserTracker : MonoBehaviour
// {
//     public Transform userMarker;
//     private XROrigin origin;

//     void Start()
//     {
//         origin = FindObjectOfType<XROrigin>();
//     }

//     void Update()
//     {
//         if (origin != null && userMarker != null)
//         {
//             userMarker.position = origin.Camera.transform.position;
//             userMarker.rotation = origin.Camera.transform.rotation;
//         }
//     }
// }
