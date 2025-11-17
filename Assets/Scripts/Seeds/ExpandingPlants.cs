using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Plants/ExpandingPlants")]
public class ExpandingPlants : Plants
{
    public override void Apply()
    {
        TIleManager.Instance.OpenTiles(position, 1);
    }
}
