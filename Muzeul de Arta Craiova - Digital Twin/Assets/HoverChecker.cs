using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class HoverChecker : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject hand;

    // for info
    GameObject textMeshObject;
    TextMeshPro textMeshComponent;
    void Start()
    {
        textMeshObject = new GameObject("MyTextMesh");
        textMeshComponent = textMeshObject.AddComponent<TextMeshPro>();
        textMeshObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (hand != null)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                // Check for intersections with objects
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Object is being hovered over
                    GameObject gameObject = hit.collider.gameObject;
                    if (gameObject.tag == "Pic" || gameObject.tag == "Exponat")
                    {
                        float distance = CalculateDistance(hand, gameObject);
                        if (distance < 2f)
                        {
                            Debug.Log("Hovered over object: " + gameObject.name + "distance berween rtgen " + distance);
                            //here we will need a function for data =)

                            textMeshComponent.text = gameObject.name;
                            textMeshObject.transform.position = gameObject.transform.position;
                            textMeshObject.transform.rotation = hand.transform.rotation;
                            textMeshObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                            textMeshComponent.fontSize = 15;
                            textMeshComponent.color = Color.red;

                            textMeshObject.SetActive(true);

                        }
                        else
                        {
                            textMeshObject.SetActive(false);
                        }
                    }
                    else
                    {
                        textMeshObject.SetActive(false);
                    }

                    // You can perform actions like highlighting, displaying information, etc.
                    // Example: hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;

                    // Implement your custom interaction logic here
                }
            }
        }
    }
    float CalculateDistance(GameObject object1, GameObject object2)
    {
        // Use Vector3.Distance to calculate the distance between the two objects
        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        return distance;
    }
}
