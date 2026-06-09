
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class ARPlacementManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject contentPrefab;

    private GameObject spawnedContent;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
    
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

      
        if (!touch.press.wasPressedThisFrame)
            return;

        Vector2 touchPosition = touch.position.ReadValue();

        Debug.Log($"Touch detected at: {touchPosition}");

        
        if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
        {
            ARRaycastHit hit = hits[0];

           
            ARPlane plane = planeManager.GetPlane(hit.trackableId);

            if (plane == null)
                return;

            Debug.Log($"Plane hit - Alignment: {plane.alignment}");

            Pose hitPose = hit.pose;

            
            Vector3 offsetPosition =
                hitPose.position +
                hitPose.rotation * Vector3.forward * 0.05f;

            
            if (spawnedContent == null)
            {
                Debug.Log("Spawning content");

                spawnedContent = Instantiate(
                    contentPrefab,
                    offsetPosition,
                    hitPose.rotation
                );
            }
           
            else
            {
                Debug.Log("Moving content");

                spawnedContent.transform.SetPositionAndRotation(
                    offsetPosition,
                    hitPose.rotation
                );
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any plane");
        }
    }
}