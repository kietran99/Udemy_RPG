using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private Transform target;

    public Tilemap theMap;
    private Vector3 bottomLeftLimit, topRightLimit;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;

        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);

        PlayerController.instance.setBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        // make the camera follow the player
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // keep the camera inside the map
        transform.position = new Vector3(
                            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                            transform.position.z);  
    }
}
