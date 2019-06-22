using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;
    ShapeGenerator ShapeGenerator = new ShapeGenerator();
    ColourGenerator ColourGenerator = new ColourGenerator();
    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    Terrainface[] terrainFaces;
    
    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;
    private void OnValidate()
    {
        GeneratePlanet();
    }
    void Initialize()
    {
        ShapeGenerator.updateSettings(shapeSettings);
        ColourGenerator.UpdateSettings(colourSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new Terrainface[6];

        Vector3[] directions = { Vector3.up, Vector3.down,Vector3.left,Vector3.right, Vector3.forward, Vector3.back };
        for(int i = 0; i < 6; i++)
        {
            if(meshFilters[i]==null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMat;

            terrainFaces[i] = new Terrainface(ShapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
       
    }
    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        generateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        if(autoUpdate) { 
        Initialize();
        GenerateMesh();
        }
    }
    public void OnColourSettingsUpdated()
    {
        if (autoUpdate) { 
        Initialize();
        generateColours();
        }
    }
    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++) 
        {
            if(meshFilters[i].gameObject.activeSelf)
            {

                terrainFaces[i].ConstructMesh();
            }
        }

        ColourGenerator.UpdateElevation(ShapeGenerator.elevationMinMax);
    }
    void generateColours()
    {
        ColourGenerator.updateColours();
    }
}
