using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;

[Serializable]
public class Level{
    [Range(0,11)]
    public int partCount = 11;
    [Range(0,11)]
    public int deathPartCount = 1;
}
[CreateAssetMenu(fileName = "New Stage")]
public class Stages : ScriptableObject
{
    public Color  stageBackgroundColor = Color.white;
    public Color stageLevelPartColor = Color.white;
    public Color stageBallColor = Color.white;
    public List<Level> level = new List<Level>();

}
