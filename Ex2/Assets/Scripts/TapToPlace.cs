using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{

    public GameObject gameObjectToPlace;

    private GameObject placedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 position;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetPosition(out Vector2 position)
    {
        if(Input.touchCount > 0)
        {
            position = Input.GetTouch(0).position;
            return true;
        }
        else
        {
            position = default;
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetPosition(out Vector2 position))
        {
            return;
        }
        if(_arRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(placedObject == null)
            {
                placedObject = Instantiate(gameObjectToPlace, hitPose.position, hitPose.rotation);
            }
            else
            {
                placedObject.transform.position = hitPose.position;
            }
        }
    }
}
