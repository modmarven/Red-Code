using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicate : MonoBehaviour
{
    private Animator animator;
    public float health = 100f;
    private int damageAmount = 20;
    public BoxCollider hand;
    public EnemyController enemyController;
   
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            if (health > 0)
            {
                health -= damageAmount;
            }

            else if (health <= 0)
            {
                animator.SetTrigger("isDie");
                GetComponent<BoxCollider>().enabled = false;
                enemyController.enabled = false;
                hand.enabled = false;
            }

            else
            {
                animator.SetTrigger("isHit");
            }
        }
    }
}
