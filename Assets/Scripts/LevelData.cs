using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickSpacing
{
    public float x;
    public float y;
}

[System.Serializable]
public class BrickRow
{
    public List<int> row;  // Wrap each row in a class
}

[System.Serializable]
public class LevelData
{
    public int rows;
    public int columns;
    public BrickSpacing brickSpacing;
    public List<BrickRow> brickLayout;  // List of BrickRow objects
}
