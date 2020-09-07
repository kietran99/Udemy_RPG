using UnityEngine;
using UnityEngine.UI;
using Functional;
using System.Collections.Generic;
using System;

namespace RPG.Inventory
{
    [RequireComponent (typeof(ObjectPool))]
    public class ItemDetailsView : MonoBehaviour, ItemDetailsInterface
    {      
        [SerializeField]
        private Text nameText = null, descriptionText = null;

        private IObjectPool spritesPool;

        private GameObject[] sprites;

        void Awake()
        {
            spritesPool = GetComponent<IObjectPool>();
            sprites = Array.Empty<GameObject>();
        }

        public void Show(string name, string description, Sprite[] equippablesSprites)
        {
            nameText.text = name;
            descriptionText.text = description;
            HOF.Map(sprite => spritesPool.Push(sprite), sprites);
            sprites = new GameObject[equippablesSprites.Length];
            HOF.Map((sprite, idx) =>
            {
                GameObject obj = spritesPool.Pop();
                sprites[idx] = obj;
                obj.GetComponent<Image>().sprite = sprite;
            }, equippablesSprites);
        }
    }
}
