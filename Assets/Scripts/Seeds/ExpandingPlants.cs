using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Plants/ExpandingPlants")]
public class ExpandingPlants : Plants
{
    public override void Apply(int combinationCoef)
    {
        TIleManager.Instance.OpenTiles(position, combinationCoef > combinatiedIndex ? 2 : 1);
    }
}
