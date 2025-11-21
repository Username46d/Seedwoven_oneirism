using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plants", menuName = "Plants/Score Plant")]
public class ScorePlant : Plants
{
    public int points;
    public override void Apply(int combinationCoef)
    {
        GameManager.Instance.scoreManager.AddScore(combinationCoef > combinatiedIndex ? points * combinationCoef : points);
    }
}
