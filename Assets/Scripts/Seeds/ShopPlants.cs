using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plants", menuName = "Plants/ShopPlant")]
public class ShopPlants : Plants
{
    public bool isUsed = false;
    public override void Apply()
    {
        Debug.Log("Выполнено в PU");
        if (Time.timeScale == 0)
        {
            return;
        }
        UIManager.Instance.Shop();
    }
}
