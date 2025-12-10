using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance;
    [Header("Setting")]
    public List<Challenge> challenges;
    void Start()
    {
        Instance = this;
    }

    public void DoChallenge()
    {
        var c = challenges[Random.RandomRange(0, challenges.Count)];

        Challenge challenge1 = Instantiate(c);
        challenge1._sprite = c._sprite; challenge1.tname = c.tname; challenge1._text = c._text;

        var newC = new List<Challenge>(challenges);
        newC.Remove(c);

        var c1 = newC[Random.RandomRange(0, newC.Count)];
        Challenge challenge2 = Instantiate(c1);
        challenge2._sprite = c1._sprite; challenge2.tname = c1.tname; challenge2._text = c1._text;

        UIManager.Instance.Open(2);
        UIManager.Instance.ChangeChallengePalen(challenge1, challenge2);
    }
}