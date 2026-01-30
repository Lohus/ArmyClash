using System.Collections;
using UnityEngine;

public class BaseUnit : MonoBehaviour, IDamageable
{
    [SerializeField] public Army army { get; private set;}
    private const float distanceSearch = 20;
    private const float distanceAttack = 2f;
    public int HP = 100;
    public int ATK = 10;
    public int SPEED = 10;
    public float ATKSPD = 1;
    public BaseUnit target = null;
    bool isAttacking = false;
    private Coroutine attackCoroutine;
    void Update()
    {
        Move();
    }
    public void ApplyProperty()
    {
        if (army.properties.Count != 0)
        {
            foreach (Property property in army.properties)
            {
                property.ApplyProperty(this);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            army.KillUnit(gameObject);
            Destroy(gameObject);
        }
    }
    public void Move()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
            if((target.transform.position - transform.position).magnitude > distanceAttack)
            {
                if (isAttacking)
                {
                    StopAttack();
                }
                transform.Translate(new Vector3(0,0, SPEED) * Time.deltaTime);
            }
            else
            {
                if (!isAttacking)
                {
                    attackCoroutine = StartCoroutine(AttackTarget());
                }
            }
        }
        else
        {
            StopAttack();
            Search();
        }
    }
    public void Attack(IDamageable target, int damage)
    {
        target.TakeDamage(damage);
    }
    public void Search()
    {
        if (target == null)
        {
            if (Physics.CheckSphere(transform.position, distanceSearch))
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, distanceSearch);
                float closestDistance = float.MaxValue; 
                foreach (Collider col in colliders)
                {
                    if (col.TryGetComponent<BaseUnit>(out BaseUnit unit))
                    {
                        if (unit != this && army.id != unit.army.id)
                        {
                            float distanceToUnit = Vector3.Distance(col.transform.position, transform.position);
                            if (distanceToUnit < closestDistance)
                            {
                                closestDistance = distanceToUnit;
                                target = unit;
                            }
                        }
                    }
                }
            }
        }
    }
    public void BattleRage()
    {
        ATK *= 2;
    }

    IEnumerator AttackTarget()
    {
        isAttacking = true;
        while (target != null && (target.transform.position - transform.position).magnitude <= distanceAttack)
        {
            Attack(target, ATK);
            yield return new WaitForSeconds(ATKSPD);
        }
    }
    private void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        isAttacking = false;
    }
    public void SetArmy(Army army)
    {
        this.army = army;
    }
    void OnDestroy()
    {
        StopAttack();
    }
}
