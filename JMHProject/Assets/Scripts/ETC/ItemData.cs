using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int baseDamage;
    public float baseCooltime;
    public float baseSpeed;
    public int baseAmount;
    public int basePierce;
}
