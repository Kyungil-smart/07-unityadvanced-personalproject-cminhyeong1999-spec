using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int maxHealth;
    public int giveExp;
    public int damage;
    public float baseSpeed;
}
