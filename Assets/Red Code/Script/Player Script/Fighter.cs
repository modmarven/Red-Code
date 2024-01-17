using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public float coolDownTime = 0f;
    private float nextFireTime = 0f;
    public static int noOfClick = 0;
    private float lastClickTime = 0;
    private float maxComDelay = 1;

    public bool AttackInProgress { get; private set; } = false;

    public MeshCollider sword;
    public MeshCollider dagger;

    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    
    void Update()
    {
        RightAttack();
        LeftAttack();
    }

    public void RightAttack()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }

        if (Time.time - lastClickTime > maxComDelay)
        {
            noOfClick = 0;
        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButton(0))
            {
                lastClickTime = Time.time;
                noOfClick++;

                if (noOfClick == 1)
                {
                    anim.SetBool("hit2", true);
                }

                noOfClick = Mathf.Clamp(noOfClick, 0, 10);

            }

            else
            {
                anim.SetBool("hit2", false);
            }
        }
    }

    private void LeftAttack()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
        }

        if (Time.time - lastClickTime > maxComDelay)
        {
            noOfClick = 0;
        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButton(1))
            {
                lastClickTime = Time.time;
                noOfClick++;

                if (noOfClick == 1)
                {
                    anim.SetBool("hit1", true);
                    sword.enabled = true;
                }

                noOfClick = Mathf.Clamp(noOfClick, 0, 10);

            }

            else
            {
                anim.SetBool("hit1", false);
                sword.enabled = false;
            }
        }
    }
      private void SetAttackStart()
        {
            AttackInProgress = true;
        }

        private void SetAttackEnd()
        {
            AttackInProgress = false;
        }
}
