using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ScreenEffect : MonoBehaviour
{
    public Camera shadowCamera;
    public Material shadowMaterial;
    //private void Start()
    //{
    //    RenderTexture rt = new RenderTexture(128, 128, 16);
    //    rt.useMipMap = false;
    //    rt.format = RenderTextureFormat.Default;

    //    shadowCamera.targetTexture = rt;
    //    shadowMaterial.SetTexture("_ShadowTex", rt);
    //}

    public Material mat;
    void OnRenderImage(RenderTexture sourceImage, RenderTexture destImage)
    {
        Graphics.Blit(sourceImage, destImage, mat);
    }
}
