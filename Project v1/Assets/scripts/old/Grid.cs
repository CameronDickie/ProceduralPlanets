using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    int size;
    GameObject[][] obj;
    public GameObject prefab;
    public int prefabWidth = 4;
    public Grid()
    {
        this.size = 0;
    }
    public Grid(GameObject prefab)
    {
        this.prefab = prefab;
        this.size = 0;
    }
    public Grid(int dim)
    {
        //make an empty dim*dim grid 
        obj = new GameObject[dim][];
        for(int i = 0; i < obj.Length; i++)
        {
            obj[i] = new GameObject[dim];
        }
        this.size = dim * dim;
    }
    public Grid (int l, int w)
    {
        //make an empty l*w grid
        obj = new GameObject[l][];
        this.size = l * w;
        for(int i = 0; i < l; i++)
        {
            obj[i] = new GameObject[w];
        }
    }
    public Grid (int dim, GameObject p)
    {
        //make a square populated grid of type p and of size dim*dim
        this.prefab = p;
        obj = new GameObject[dim][];
        for(int x =0; x < obj.Length; x++)
        {
            obj[x] = new GameObject[dim];
            for(int z = 0; z < obj[x].Length; z++)
            {
                GameObject block = p;
                block.transform.position = new Vector3(x*prefabWidth, 0 , z*prefabWidth);
                obj[x][z] = block;
                Debug.Log(x * prefabWidth + "" + z*prefabWidth);
            }
        }
        this.size = dim * dim;
    }
    public Grid (int l, int w, GameObject p)
    {
        //make a rect grid of dimensions l*w populated with type p
        this.prefab = p;
        obj = new GameObject[l][];
        for(int x = 0; x < l; x++)
        {
            obj[x] = new GameObject[w];
            for(int z = 0; z < w; z++)
            {
                GameObject block = p;
                block.transform.position = new Vector3(x * prefabWidth, 0, z * prefabWidth);
                obj[x][z] = block;
            }
        }
        this.size = l * w;
    }
    public int getSize()
    {
        return this.size;
    }
    public void RenderGrid()
    {
        if(obj == null)
        {
            this.obj = new GameObject[0][];
        }
        for(int x = 0; x < obj.Length; x++)
        {
            for(int z = 0; z < obj.Length; z++)
            {
                GameObject toMake = obj[x][z];
                Instantiate(toMake, toMake.transform);
            }
        }
    }
    public GameObject[][] setObj(GameObject[][] n)
    {
        obj = n;
        return n;
    }
    public int setSize(int s)
    {
        this.size = s;
        return s;
    }

}
