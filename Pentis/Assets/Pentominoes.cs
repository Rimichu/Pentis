using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentominoes : MonoBehaviour
{
    public float lastFall = 0;

    void Start()
    {
        // Default position not valid, game over
        if (!isValidGridPosition())
        {
            Debug.Log("Game Over");
            Debug.Log(gameObject);
            Destroy(gameObject);
        }
    }

    bool isValidGridPosition()
    {
        foreach (Transform child in transform)
        {
            Vector3 vector = Playfield.roundVec3(child.position);

            // check if inside border
            if (!Playfield.insideBorder(vector))
            {
                return false;
            }

            if (Playfield.grid[(int)vector.x, (int)vector.y, (int)vector.z] != null && Playfield.grid[(int)vector.x, (int)vector.y, (int)vector.z].parent != transform)
            { 
                    return false;
            }
        }
        return true;
    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Playfield.height; ++y)
        {
            for (int x = 0; x < Playfield.length; ++x)
            {
                for (int z = 0; z < Playfield.width; ++z)
                {
                    if (Playfield.grid[x, y, z] != null)
                    {
                        if (Playfield.grid[x, y, z].parent == transform)
                        {
                            Playfield.grid[x, y,z] = null;
                        }
                    }
                }
            }
        }
        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector3 vector = Playfield.roundVec3(child.position);
            Playfield.grid[(int)vector.x, (int)vector.y, (int)vector.z] = child;
        }
    }

    void Update()
    {
        // Movement for testing in Unity Only
        {
            // Move left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Modify position
                transform.position += new Vector3(-1, 0, 0);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.position += new Vector3(1, 0, 0);
                }
                // Move right
            } 
            // Move right
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Modify position
                transform.position += new Vector3(1, 0, 0);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.position += new Vector3(-1, 0, 0);
                }
            }
            // Move back
            else if (Input.GetKeyDown(KeyCode.A))
            {
                // Modify position
                transform.position += new Vector3(0, 0, -1);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.position += new Vector3(0, 0, 1);
                }
                // Move forward
            }
            // Move forward
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // Modify position
                transform.position += new Vector3(0, 0, 1);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.position += new Vector3(0, 0, -1);
                }
            }

            // Rotate right
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(0, 0, -90);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.Rotate(0, 0, 90);
                }
            }
            // Rotate left
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.Rotate(0, 0, 90);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.Rotate(0, 0, -90);
                }
            }
            // Rotate forward
            else if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(-90, 0, 0);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.Rotate(90, 0, 0);
                }
            }
            // Rotate back
            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.Rotate(90, 0, 0);

                // Check if Valid
                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.Rotate(-90, 0, 0);
                }
            }

            // Fall
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Modify position
                transform.position += new Vector3(0, -1, 0);

                if (isValidGridPosition())
                {
                    // Yes it is a valid position
                    updateGrid();
                }
                else
                {
                    // It is not a valid position
                    transform.position += new Vector3(0, 1, 0);

                    // clear filled lines
                    Playfield.deleteFullLayers();

                    // Spawn next pentomino
                    FindObjectOfType<Spawner>().spawnNext();

                    // Disable script
                    enabled = false;
                }
            }
        }

        // Move Left
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
       {  
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.position += new Vector3(1, 0, 0);
            }
            // Move right
       } 
        // Move Right
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
       {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.position += new Vector3(-1, 0, 0);
            }
            }
        // Move back
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            // Modify position
            transform.position += new Vector3(0, 0, -1);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.position += new Vector3(0, 0, 1);
            }
            // Move forward
        }
        // Move forward
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            // Modify position
            transform.position += new Vector3(0, 0, 1);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.position += new Vector3(0, 0, -1);
            }
        }

        // Rotate right
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            transform.Rotate(0, 0, -90);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.Rotate(0, 0, 90);
            }
        }
        // Rotate left
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            transform.Rotate(0, 0, 90);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.Rotate(0, 0, -90);
            }
        } 
        // Rotate forward
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            transform.Rotate(-90, 0, 0);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.Rotate(90, 0, 0);
            }
        }
        // Rotate back
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            transform.Rotate(90, 0, 0);

            // Check if Valid
            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.Rotate(-90, 0, 0);
            }
        }

        // Fall
        if (OVRInput.Get(OVRInput.Button.One))
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            if (isValidGridPosition())
            {
                // Yes it is a valid position
                updateGrid();
            }
            else
            {
                // It is not a valid position
                transform.position += new Vector3(0, 1, 0);

                // clear filled lines
                Playfield.deleteFullLayers();

                // Spawn next pentomino
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }
        }
        lastFall = Time.time;
    }
    }
