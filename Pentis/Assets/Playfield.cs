using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield: MonoBehaviour
{
    // The tank
    public static int length = 10;
    public static int width = 10;
    public static int height = 25;
    public static Transform[,,] grid = new Transform[length, height, width];

    public static Vector3 roundVec3(Vector3 vector)
    {
        return new Vector3
            (
            Mathf.Round(vector.x),
            Mathf.Round(vector.y),
            Mathf.Round(vector.z)
            );
    }
    
    public static bool insideBorder(Vector3 position)
    {
        return (
            (int)position.x >= 0 &&
            (int)position.x < length &&
            (int)position.y >= 0 &&
            (int)position.z >= 0 &&
            (int)position.z < width
            );
    }

    public static void deleteLayer(int y)
    {
        for (int x = 0; x < length; ++length)
        {
            for (int z = 0; z < width; ++z)
            {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
    }

    public static void decreaseLayer(int y)
    {
        for (int x = 0; x < length; ++x)
        {
            for (int z = 0; z < width; ++z)
            {
                if (grid[x,y,z] != null)
                {
                    grid[x,y - 1,z] = grid[x,y,z];
                    grid[x, y, z] = null;

                    grid[x, y - 1, z].position += new Vector3(0,-1,0);
                }
            }
        }
    }

    public static void decreaseLayersAbove(int y)
    {
        for (int i = y; i < height; ++i)
        {
            decreaseLayer(i);
        }
    }

    public static bool isLayerFull(int y)
    {
        for (int x = 0; x < length; ++x)
        {
            for (int z = 0; z < width; ++z)
            {
                if (grid[x,y,z] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void deleteFullLayers()
    {
        for (int y = 0; y < height; ++y)
        {
            if (isLayerFull(y))
            {
                deleteLayer(y);
                decreaseLayersAbove(y + 1);
                --y;
            }
        }
    }
}
