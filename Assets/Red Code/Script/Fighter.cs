using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public float coolDownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClick = 0;
    private float lastClickTime = 0;
    private float maxComDelay = 1;

    public bool AttackInProgress { get; private set; } = false;

    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    
    void Update()
    {
        RightAttack();
    }

    private void RightAttack()
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
            if (Input.GetMouseButtonDown(0))
            {
                lastClickTime = Time.time;
                noOfClick++;

                if (noOfClick == 1)
                {
                    anim.SetBool("hit2", true);
                }

                noOfClick = Mathf.Clamp(noOfClick, 0, 3);

            }

            else
            {
                anim.SetBool("hit2", false);
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
