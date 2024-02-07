using UnityEngine;

public class TeleportOnKeyPress : MonoBehaviour
{
    public float teleportDistance = 2f; // Distance to teleport the player
    public float teleportStepSize = 0.5f; // Step size for teleportation direction adjustment

    void Update()
    {
        // Check for arrow key input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TeleportPlayer(transform.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TeleportPlayer(-transform.forward);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TeleportPlayer(-transform.right);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TeleportPlayer(transform.right);
        }
    }

    private void TeleportPlayer(Vector3 direction)
    {
        // Calculate the teleport destination by adding the teleportDistance multiplied by the specified direction
        Vector3 teleportDestination = transform.position + direction * teleportDistance;

        // Teleport the player to the calculated destination
        transform.position = teleportDestination;
    }
}
