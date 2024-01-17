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
                        //print("Walks");
                        checker = true;
                    }
                }
                else
                {
                    if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    {
                        mAnimator.SetTrigger("IdleTrig");
                        //print("Stops");
                        checker = false;
                    }
                }

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsOwner)
        {
            if(collision.gameObject.tag.Contains("Door"))
            {
                //print("I touched the door");
                if (collision.gameObject.tag == "DoorToRoom2")
                {
                   // movingToAnotherRoom(2); //going to room 2
                   // print("YES, checking");
                }
            }
        }
    }
    /*private void movingToAnotherRoom(int room)
    {
        if (room == 1)
            transform.position = new Vector3(0, 0, 0);
        else
        {
            Vector3 pos = new Vector3(13f, 0f, -1f);
            m_transform.position = pos;
            transform.parent.position = pos;
            showChildren(m_transform, pos);
        }
    }

    private void showChildren(Transform parent, Vector3 position)
    {
        foreach (Transform child in parent)
        {
            print(child.name);
            child.position += position;
            showChildren(child, position);
        }
    }*/
}
