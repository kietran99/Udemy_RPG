using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject shopMenu = null;

    private bool canActivate = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canActivate)
        {
            GameManager.Instance.ShopMenuActive = true;
            shopMenu.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canActivate = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canActivate = false;
    }
}
