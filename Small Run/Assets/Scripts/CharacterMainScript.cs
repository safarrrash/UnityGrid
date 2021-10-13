using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMainScript : MonoBehaviour
{
    public class characterAtt
    {
        public string Cname, description;
        public float health, speed, damage;
        public float cooldown, AttSpeed;
        public int cost, range;
    }

    [SerializeField] CharactersSO CharacterSO;
    public characterAtt Attributes;
    public characterAtt SelfAttribute;
    

    private void Awake()
    {
        Attributes = new characterAtt();
        SelfAttribute = new characterAtt();

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
        Attributes.range = CharacterSO.Range;
        Attributes.AttSpeed = CharacterSO.ShotSpeed;

        SelfAttribute.Cname = CharacterSO.Cname;
        SelfAttribute.description = CharacterSO.description;
        SelfAttribute.health = CharacterSO.health;
        SelfAttribute.speed = CharacterSO.speed;
        SelfAttribute.damage = CharacterSO.damage;
        SelfAttribute.cooldown = CharacterSO.cooldown;
        SelfAttribute.cost = CharacterSO.cost;
        SelfAttribute.range = CharacterSO.Range;
        SelfAttribute.AttSpeed = CharacterSO.ShotSpeed;
    }

    public void SelfDamage(float amount)
    {
        SelfAttribute.health -= amount;
    }

}
