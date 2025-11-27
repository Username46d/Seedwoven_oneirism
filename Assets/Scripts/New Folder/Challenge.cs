using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Challenge", menuName = "Challenges/Default_Challenge")]
public class Challenge : ScriptableObject
{
    public string tname;
    public Sprite _sprite;
    public string _text;
    public virtual void Apply() { Debug.Log("אבהÿבהÿ"); }
}
