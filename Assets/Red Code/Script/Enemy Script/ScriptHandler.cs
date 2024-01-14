using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptHandler : MonoBehaviour
{
    public Animator anim;
    public TakeDamage takeDamage;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (takeDamage.health < 0)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }
    }
}
