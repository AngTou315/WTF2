using System.Collections;
using System.Collections.Generic;
using ClockStone;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioToolKit/Create AudioItemData ")]
public class AudioItemData : ScriptableObject
{
    #if UNITY_EDITOR
    public string Des = "";
    #endif
    public AudioItem audioItem;
}