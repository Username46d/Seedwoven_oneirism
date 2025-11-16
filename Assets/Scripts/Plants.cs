using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Plants/Plants Type")]
public class Plants : ScriptableObject
{
    public GameObject growtTile;
    public Vector3Int position;
    public int growtTime = 3;
    public Sized sized = new Sized(0.1f, 1f);
    public int maxStages = 1;
    public int currentStadia = 0;
}
public struct Sized
{
    public float startSize;
    public float endSize;

    public Sized(float s, float e) => (startSize, endSize) = (s, e);
}
