using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMenuController : MonoBehaviour, MerchInfo.IClickable
{
    public abstract void OnClick(int pos);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
