using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private Tilemap theMap = null;
    private Vector3 bottomLeftLimit, topRightLimit;

    [SerializeField]
    private bool smallScene = false;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.Instance.transform;

        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);

        PlayerController.Instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        if (smallScene) return;

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = new Vector3(
                            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                            transform.position.z);  
    }
}
