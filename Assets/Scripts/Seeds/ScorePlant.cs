using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Plants", menuName = "Plants/Score Plant")]
public class ScorePlant : Plants
{
    public override void Apply()
    {
        GameManager.Instance.scoreManager.AddScore(10);
    }
}
