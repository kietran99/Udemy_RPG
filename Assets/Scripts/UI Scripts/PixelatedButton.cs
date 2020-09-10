﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class PixelatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Sprite unpressedSprite = null;

    [SerializeField] Sprite pressedSprite = null;

    Button myButton;

    void Awake()
    {
        myButton = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        myButton.image.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        myButton.image.sprite = unpressedSprite;
    }
}