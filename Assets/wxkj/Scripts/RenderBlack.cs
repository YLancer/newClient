using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class RenderBlack : MonoBehaviour
{
    public Camera shadowCamera = null;
    public Material shadowMaterial = null;
    private Material originalMaterial = null;

    private Renderer ObjRenderer = null;

    // Use this for initialization
    void Awake()
    {
        ObjRenderer = GetComponent<Renderer>();
        originalMaterial = ObjRenderer.sharedMaterial;
    }

    void OnWillRenderObject()
    {
        if ((Camera.current == shadowCamera) && (ObjRenderer != null))
        {
            originalMaterial = ObjRenderer.sharedMaterial;
            ObjRenderer.sharedMaterial = shadowMaterial;
        }
    }

    void OnRenderObject()
    {
        if ((Camera.current == shadowCamera) && (ObjRenderer != null))
        {
            ObjRenderer.sharedMaterial = originalMaterial;
        }
    }
}
