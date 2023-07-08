using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/ThemeInfoSO")]
public class ThemeInfo : ScriptableObject
{
    public string ThemeName;
    public List<Stage> StageList;
}
