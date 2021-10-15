using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMainScript : MonoBehaviour
{
    public enum CharacterType
    {
        Troop,
        Building,
        Spell
    }

    public class TimerClass
    {
        public float Unit; //must increament with Time.DeltaTime every frame
        public bool isWorking = true;
        

        public bool isPassed(float Compared)
        {
            if (Unit >= Compared) return true;
            else if (Unit < Compared) return false;
            else
            {
                Debug.LogWarning("Error in Timer Class");
                return false;
            }
        }

        public void StopTimer(bool stop)
        {
            if (stop) isWorking = false;
            else if (!stop) isWorking = true;
        }

        public void Increase(float Amount)
        {
            if(isWorking)
            Unit += Amount;
        }
        public void resetTimer()
        {
            Unit = 0;
        }

    }

    public class TargetManager
    {
        public GameObject target;

        public bool haveTarget = false;

        public GameObject getTarget()
        {
            if (haveTarget)
                return target;
            else
                return null;
        }

        public void checkforTarget(Detector detector)
        {
            if (detector.IsDetected())
            {
                haveTarget = true;
                target = detector.GetTarget();
            }
            else if (!detector.IsDetected())
            {
                haveTarget = false;
                target = null;
            }
        }

        public void clearTarget()
        {
            target = null;
        }

    }

    public class characterAtt
    {
        public string cName, description;
        public float health, speed, damage;
        public float cooldown, attSpeed;
        public int cost, range;
        public CharacterType charType;
    }

    [SerializeField] CharactersSO CharacterSO;
    public Rigidbody2D rigidBody;
    [SerializeField] GameObject healthBar;
    [SerializeField] Detector targetDetector;

    public characterAtt Attributes;
    public characterAtt SelfAttribute;
    public TimerClass timerClass;
    public TargetManager targetManager;

    bool isAlive;
    bool canAttack;
    bool canMove;

    private void Awake()
    {
        targetDetector = GetComponentInChildren<Detector>();
        targetManager = new TargetManager();
        timerClass = new TimerClass();
        Attributes = new characterAtt();
        SelfAttribute = new characterAtt();
        assignAttributes();
        
    }

    private void Start()
    {
        isAlive = true;
    }

    private void Update()
    {
        timerClass.Increase(Time.deltaTime);
        targetManager.checkforTarget(targetDetector);   
        canAttack = attCheck(timerClass.isPassed(SelfAttribute.attSpeed));

        isAlive = checkAlive();
        
        if (!isAlive) dead();

        setHealthBar(SelfAttribute.health);

    }

    void assignAttributes()
    {
        Attributes.cName = CharacterSO.Cname;
        Attributes.description = CharacterSO.description;
        Attributes.health = CharacterSO.health;
        Attributes.speed = CharacterSO.speed;
        Attributes.damage = CharacterSO.damage;
        Attributes.cooldown = CharacterSO.cooldown;
        Attributes.cost = CharacterSO.cost;
        Attributes.range = CharacterSO.Range;
        Attributes.attSpeed = CharacterSO.ShotSpeed;
        Attributes.charType = CharacterSO.CharacterType;

        SelfAttribute.cName = CharacterSO.Cname;
        SelfAttribute.description = CharacterSO.description;
        SelfAttribute.health = CharacterSO.health;
        SelfAttribute.speed = CharacterSO.speed;
        SelfAttribute.damage = CharacterSO.damage;
        SelfAttribute.cooldown = CharacterSO.cooldown;
        SelfAttribute.cost = CharacterSO.cost;
        SelfAttribute.range = CharacterSO.Range;
        SelfAttribute.attSpeed = CharacterSO.ShotSpeed;
        SelfAttribute.charType = CharacterSO.CharacterType;
    }

    bool checkAlive()
    {
        if (SelfAttribute.health <= 0) return false;
        else return true;
    }

    void dead()
    {
        timerClass.StopTimer(true);
        GameObject.Destroy(gameObject, 0.5f);
        gameObject.SetActive(false);
    }

    bool attCheck(bool isTimerPassed)
    {
        if (isTimerPassed)
        {
            timerClass.resetTimer();
            return true;
        }
        else if (!isTimerPassed) return false;
        else
        {
            Debug.LogWarning("Error in attCheck()");
            return false;
        }
    }


    void setHealthBar(float amount)
    {
        if ((SelfAttribute.health / Attributes.health) < 0.2) healthBar.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        if ((SelfAttribute.health / Attributes.health) < 0.5) healthBar.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        if (amount>0) healthBar.transform.localScale = new Vector3(amount / Attributes.health, healthBar.transform.localScale.y);
        else if(amount<=0) healthBar.transform.localScale = new Vector3(0, healthBar.transform.localScale.y);

    }


    public void SelfDamage(float amount)
    {
        SelfAttribute.health -= amount;
    }
    
    public bool CanAttack()
    {
        return canAttack;
    }
        
    public bool CanMove()
    {
        return canMove;
    }   

    public bool IsAlive()
    {
        return isAlive;
    }

    public void setCanMove(bool _canMove)
    {
        canMove = _canMove;
    }

    public void setCanAttack(bool _canAttack)
    {
        canAttack = _canAttack;
    }

}
