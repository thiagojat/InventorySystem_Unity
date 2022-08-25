using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] GameObject goldenApple;
    [SerializeField] GameObject chicken;
    public GameObject getItem(ItemType item)
    {
        switch (item)
        {
            case ItemType.GoldenApple:
                {
                    //Debug.Log(goldenApple);
                    return goldenApple;
                }
            case ItemType.Chicken:
                {
                    //Debug.Log(chicken);
                    return chicken;
                }
            default: return null;
        }

    }
}
