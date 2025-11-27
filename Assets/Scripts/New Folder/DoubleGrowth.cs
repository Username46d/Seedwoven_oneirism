using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Challenge", menuName = "Challenges/GrowthChallenge")]
public class DoubleGrowth : Challenge
{
    public override void Apply()
    {
        Debug.Log("DG");
        GameManager.Instance.SetChall(2f);
    }
}
