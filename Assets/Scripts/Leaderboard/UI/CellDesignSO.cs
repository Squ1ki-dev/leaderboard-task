using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell Config")]
public class CellDesignSO : ScriptableObject
{
    [System.Serializable]
    public struct TypeConfigPair
    {
        public string type;
        public Color color;
        public float height;
    }

    public TypeConfigPair[] cellConfigPairs;

    public Color GetColorForType(string type)
    {
        foreach (var pair in cellConfigPairs)
        {
            if (pair.type == type)
                return pair.color;
        }
        return Color.white; // Default color if type not found
    }

    public float GetHeightForType(string type)
    {
        foreach (var pair in cellConfigPairs)
        {
            if (pair.type == type)
                return pair.height;
        }
        return 116f; // Default height if type not found
    }
}