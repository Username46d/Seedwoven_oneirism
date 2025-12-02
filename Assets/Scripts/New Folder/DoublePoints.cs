using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Challenge", menuName = "Challenges/Point_Challenge")]
public class DoublePoints : Challenge
{
    public override void Apply()
    {
        Debug.Log("DP");
        GameManager.Instance.scoreManager.ChangeFine(50);
    }
}
