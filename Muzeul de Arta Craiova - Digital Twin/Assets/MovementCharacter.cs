using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class MovementCharacter : NetworkBehaviour
{
    //[SerializeField] Transform m_transform;
    private Animator mAnimator;
    public GameObject egg;
    bool checker;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = egg.GetComponent<Animator>();
        checker = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (mAnimator != null)
            {
                if (!checker)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        mAnimator.SetTrigger("WalkingTrig");
                        checker = true;
                    }
                }
                else
                {
                    if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    {
                        mAnimator.SetTrigger("IdleTrig");
                        checker = false;
                    }
                }

            }
        }
    }
}
