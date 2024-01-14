using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public Animator anim;
    public float health = 100;
    [SerializeField] private int damageAmount = 20;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        Debug.Log(health);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            health -= damageAmount;

            if (health <= 0)
            {
                AudioManager.instance.Play("Dead");
                anim.SetTrigger("isDie");
                GetComponent<BoxCollider>().enabled = false;

            }
            else
            {
                anim.SetTrigger("isHit");
            }
        }
    }
           
}
