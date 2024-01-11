using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int resolution = 512;
    private Texture2D whiteMap; 
    public float brushSize; 
    public float brushRand; 
    public Texture2D brushTexture;
    private Vector2 stored;

    public static Dictionary<Collider, RenderTexture> paintTextures = new Dictionary<Collider, RenderTexture>();

    // Start is called before the first frame update
    void Start()
    {
        MakeWhiteTexture();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            Collider coll = hit.collider;
            if (coll != null && hit.transform.tag == "Floor")
            {
                if (!paintTextures.ContainsKey(coll))
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    paintTextures.Add(coll, GetWhiteRT());
                    rend.material.SetTexture("_PaintMap", paintTextures[coll]);
                }
                if (stored != hit.textureCoord)
                {
                    stored = hit.textureCoord;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.y *= resolution;
                    pixelUV.x *= resolution;
                    DrawTexture(paintTextures[coll], pixelUV.x, pixelUV.y);
                }

            }

        }

    }

    public void BrushSizeUp()
    {
        brushSize -= 0.1f;
        if (brushSize <= 85)
            brushSize = 85.0f;
    }

    void DrawTexture(RenderTexture rt, float posX, float posY)
    {
        RenderTexture.active = rt;
        GL.PushMatrix();  
        GL.LoadPixelMatrix(0, resolution, resolution, 0);  
        float t_Size = brushSize * Random.Range(1f - brushRand, 1f + brushRand);  
        Rect t_rect = new Rect(posX - brushTexture.width / t_Size, (rt.height - posY) - brushTexture.height / t_Size, brushTexture.width / (t_Size * 0.5f), brushTexture.height / (t_Size * 0.5f));
        Graphics.DrawTexture(t_rect, brushTexture);
        GL.PopMatrix();
        RenderTexture.active = null;
    }

    void MakeWhiteTexture()
    {
        whiteMap = new Texture2D(1, 1);
        whiteMap.SetPixel(0, 0, Color.white);
        whiteMap.Apply();
    }

    RenderTexture GetWhiteRT()
    {
        RenderTexture rt = new RenderTexture(resolution, resolution, 32);
        Graphics.Blit(whiteMap, rt);
        return rt;
    }

}
