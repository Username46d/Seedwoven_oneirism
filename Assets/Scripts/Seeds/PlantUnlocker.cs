using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plants", menuName = "Plants/UnlockerPlant")]
public class PlantUnlocker : Plants
{
    public bool isUsed = false;
    public override void Apply()
    {
        Debug.Log("Выполнено в PU");
        if (isUsed == true)
        {
            return;
        }
        GameManager.Instance.AddNewPlant();
        isUsed = true;
    }
}
