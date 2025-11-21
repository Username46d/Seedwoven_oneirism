using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "Plants", menuName = "Plants/CustomExpandingPlants")]
public class CustomExpandingPlants : ExpandingPlants
{
    public Vector3Int[] positions;
    public Vector3Int[] extraPositions;
    public override void Apply(int combinationCoef)
    {
        TIleManager.Instance.OpenTiles(position, (combinationCoef > combinatiedIndex ? positions.Concat(extraPositions).ToArray<Vector3Int>() : positions));
    }
}
