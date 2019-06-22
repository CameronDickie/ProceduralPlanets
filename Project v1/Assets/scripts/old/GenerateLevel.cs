using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    int width = 4;
    public GameObject prefab;
    Grid level;
    // Start is called before the first frame update
    void Start()
    {
        level = makeGridAtPos(prefab, new Vector3(-1, 0, -1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Grid makeGridAtPos(GameObject prefab, Vector3 pos)
    {
        //make a grid of prefab size centered at pos
        int rad = 8;
        //make a list of gameobject lists with length 16
        GameObject[][] n = new GameObject[2 * rad][];
        for(int x = 0; x < rad; x++)
        {
            n[rad + x] = new GameObject[2 * rad];
            for(int z = 0; z < rad; z++)
            {
                GameObject make = Instantiate(prefab);
                make.transform.position = pos + new Vector3(x*width, 0, z*width);
                make.transform.parent = this.transform;
                n[rad + x][rad + z] = make;
                make = Instantiate(prefab);
                make.transform.position = pos - new Vector3(x * width + width, 0, z * width);
                make.transform.parent = this.transform;
                n[rad + x][rad - z] = make;
            }
        }
        

        Grid platform = new Grid(0);
        platform.setObj(n);
        platform.setSize(2 * rad);
        return platform;
    }

}
