using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainface
{
    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public Terrainface(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x); //swapping coords of localup
        axisB = Vector3.Cross(localUp, axisA); //vector perpendicular to localup and a
        
    }

    public void GeneratePlanet()
    {

    }
    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6]; //imagine a grid of vertices (square) which have two triangles with three vertices (six)

        int triIndex = 0;
        for(int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1); //when x = 0, the percent is 0; when x is max then percent is 1 on x axis
                Vector3 pointOnUnitCube = localUp + (percent.x -.5f)*2*axisA + (percent.y -0.5f)*2*axisB;//starting at 0 find our pos on the cube btw -1, 1
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);


                if(x != resolution-1 && y != resolution -1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex+1] = i+resolution +1;
                    triangles[triIndex+2] = i+resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                }

            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


    }
}
