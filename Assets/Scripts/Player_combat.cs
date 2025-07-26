using UnityEngine;

public class Player_combat : MonoBehaviour
{
    public Transform attackPoint;
    public StatsUi statsUi;
    public LayerMask enemyLayer;
    
    public Animator anim;

    public float cooldown = 2;
    private float timer;


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);


            timer = cooldown;

        }
    }

    public void DealDamage()
    {
        StatsManager.Instance.damage = 5;
        statsUi.UpdateDamage();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            if (enemies[0].isTrigger)
            {
                return;
            }
            {
                enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-StatsManager.Instance.damage);
                enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
            }
        }
    }
    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }
}
