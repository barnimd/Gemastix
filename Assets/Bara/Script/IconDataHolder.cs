using UnityEngine;

public class IconDataHolder : MonoBehaviour
{
    public string iconName;
    public int id;
    public string source;
    public string function;
    public bool isRealIcon = true; // true = asli, false = palsu

    public string GetFormattedData()
    {
        return $"Nama: {iconName}\nID: {id}\nSource: {source}\nFunction: {function}";
    }
}
