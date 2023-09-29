using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    public GameObject model;
    private Animator animator;

    // Start is called before the first frame update

    public void Start()
    {
        animator = model.GetComponent<Animator>();
    }
    public void chopAttackAnim()
    {
        animator.SetTrigger("chopAttack");
        Debug.Log("anime1");
    }
    public void sliceAttackAnim()
    {
        animator.SetTrigger("sliceAttack");
        Debug.Log("anime2");
    }
    public void stabAttackAnim()
    {
        animator.SetTrigger("stabAttack");
        Debug.Log("anime3");
    }
    public void spinAttackAnim()
    {
        animator.SetTrigger("spinAttack");
        Debug.Log("anime4");
    }
}
