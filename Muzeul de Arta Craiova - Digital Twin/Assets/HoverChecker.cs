using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class HoverChecker : NetworkBehaviour
{
    [SerializeField] GameObject hand;
    GameObject textMeshObject;
    TextMeshPro textMeshComponent;

    void Start()
    {
        textMeshObject = new GameObject("MyTextMesh");
        textMeshComponent = textMeshObject.AddComponent<TextMeshPro>();
        textMeshObject.SetActive(false);
    }

    void Update()
    {
        if (IsOwner)
        {
            if (hand != null)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    GameObject gameObject = hit.collider.gameObject;

                    if (gameObject.CompareTag("Pic") || gameObject.CompareTag("Exponat"))
                    {
                        float distance = CalculateDistance(hand, gameObject);

                        if (distance < 2.2f)
                        {
                            Debug.Log("Hovered over object: " + gameObject.name + " distance between objects: " + distance);

                            // Load the file content and find the line with the artwork name
                            string filePath = "Assets/exponate.txt"; // Replace with the actual path to your file
                            string artworkName = gameObject.name;

                            if (File.Exists(filePath))
                            {
                                string[] lines = File.ReadAllLines(filePath);

                                foreach (string line in lines)
                                {
                                    if (line.Contains(artworkName))
                                    {
                                        // Split the line and extract the last three values
                                        string[] values = line.Split(',');
                                        if (values.Length >= 4)
                                        {
                                            string author = values[values.Length - 2];
                                            string year = values[values.Length - 1];
                                            string unit = values[values.Length - 3];

                                            // Do something with the extracted values
                                            Debug.Log($"Author: {author}, Year: {year}, Unit: {unit}");

                                            // Set the text on the TextMeshPro component with line breaks
                                            textMeshComponent.text = $"Author: {author}\nYear: {year}\nUnit: {unit}";

                                            // Move the textMeshObject closer to the camera/player and a bit to the right
                                            float distanceFromCamera = 2.0f; // Adjust this distance as needed
                                            float offsetRight = 0.5f; // Adjust this offset as needed
                                            Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera + Camera.main.transform.right * offsetRight;
                                            textMeshObject.transform.position = newPosition;

                                            textMeshObject.transform.rotation = hand.transform.rotation;
                                            textMeshObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                                            textMeshComponent.fontSize = 15;
                                            textMeshComponent.color = Color.red;

                                            textMeshObject.SetActive(true);
                                            break;
                                        }
                                    }
                                }
                            }
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
                }
                else
                {
                    textMeshObject.SetActive(false);
                }
            }
        }
    }

    float CalculateDistance(GameObject object1, GameObject object2)
    {
        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        return distance;
    }
}
