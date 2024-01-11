using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject obj;
    public float size = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.0f, 0.0f, 0.0f, Space.World);
            this.transform.Rotate(new Vector3(0.0f, 0.0f, 50.0f) * Time.deltaTime, Space.World);
            size += 0.001f;
            obj.GetComponent<Point>().BrushSizeUp();

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.0f, Space.World);
            this.transform.Rotate(new Vector3(0.0f, 0.0f, -50.0f) * Time.deltaTime, Space.World);
            size += 0.001f;
            obj.GetComponent<Point>().BrushSizeUp();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.0f, Space.World);
            this.transform.Rotate(new Vector3(50.0f, 0.0f, 0.0f) * Time.deltaTime, Space.World);
            size += 0.001f;
            obj.GetComponent<Point>().BrushSizeUp();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.0f, Space.World);
            this.transform.Rotate(new Vector3(-50.0f, 0.0f, 0.0f) * Time.deltaTime, Space.World);
            size += 0.001f;
            obj.GetComponent<Point>().BrushSizeUp();
        }
        if (size >= 5)
            size = 5;
        obj.GetComponent<Renderer>().material.SetFloat("_DisHeight", size);
    }
}
