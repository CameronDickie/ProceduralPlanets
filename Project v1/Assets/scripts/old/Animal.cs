using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    public float energy;
    public float size;
    public float speed;
    public float sense;
    public float sex;
    public Vector3 toGo;

    
    public Animal()
    {
        GameObject me = new GameObject();
        me.AddComponent<MeshFilter>();
        me.AddComponent<Rigidbody>();
        me.AddComponent<MeshCollider>().convex = true;
        me.AddComponent<MeshRenderer>();
        toGo = new Vector3(0, 0, 0);
    }
    public abstract float Eat(GameObject food);
    public abstract GameObject[] Search(float radius);
    public abstract GameObject Reproduce(GameObject partner);
    public abstract void Think();
    public abstract float cost();
    
}
