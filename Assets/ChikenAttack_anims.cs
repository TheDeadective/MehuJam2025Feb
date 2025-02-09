using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChikenAttack_anims : MonoBehaviour
{
    [SerializeField] Animator animatorCiken;
    [SerializeField] Animator animatorGril;
    private Animator endingAnimator;

    [SerializeField] Animator grannyAnim;

    private bool visited = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grandma") && !visited)
        {
            grannyAnim.Play("Granny_ThrowingEgg");
        }
    }

    public void ThrowEgg () 
    {
        endingAnimator.Play("Ending");
    }


    public void FightingLoop ()
    {
        animatorCiken.Play("Ciken_attack");
        animatorGril.Play("BadGril_IsAttacked");
        endingAnimator.Play("Ending_Idle");
    }
}
