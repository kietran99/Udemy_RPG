using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMenuController : MonoBehaviour, MerchInfo.IClickable
{
    public abstract void OnMerchClick(int pos);
}
