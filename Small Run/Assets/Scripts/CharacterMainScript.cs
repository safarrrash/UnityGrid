using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMainScript : MonoBehaviour
{
    public class characterAtt
    {
        public string Cname, description;
        public float health, speed, damage;
        public float cooldown;
        public int cost;
    }

    [SerializeField] CharactersSO CharacterSO;
    public characterAtt Attributes;

    private void Awake()
    {
        Attributes = new characterAtt();
        assignAttributes();

    }

    void assignAttributes()
    {
        Attributes.Cname = CharacterSO.Cname;
        Attributes.description = CharacterSO.description;
        Attributes.health = CharacterSO.health;
        Attributes.speed = CharacterSO.speed;
        Attributes.damage = CharacterSO.damage;
        Attributes.cooldown = CharacterSO.cooldown;
        Attributes.cost = CharacterSO.cost;
    }

}
