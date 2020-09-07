using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string AreaTransitionName { get; set; }

    void Start()
    {
        if (AreaTransitionName.Equals(PlayerController.Instance.AreaTransitionName))
        {
            PlayerController.Instance.transform.position = GetComponent<BoxCollider2D>().bounds.center;
        }

        GameManager.Instance.fadingBetweenAreas = false;
    }    
}
