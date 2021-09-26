using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Custom/Character")]
public class CharactersSO : ScriptableObject
{
    public Sprite Icon, Ghost;
    [Space]
    public string Cname;
    [Multiline] public string description;
    public float health, speed, damage;
    public float cooldown;
    public int cost;

}
