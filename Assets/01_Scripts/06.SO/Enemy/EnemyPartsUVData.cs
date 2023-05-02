using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/EnemyUVData")]
public class EnemyPartsUVData : ScriptableObject
{
    [SerializeField]
    int headX, headY, headWidth, headHeight, textureWidth, textureHeight;
    [SerializeField]
    string EnemyName;

    public int GetHeadX => headX;
    public int GetHeadY => headY;
    public int GetHeadWidth => headWidth;
    public int GetHeadHeight => headHeight;
    public int GetTextureWidth => textureWidth;
    public int GetTextureHeight => textureHeight;
}
