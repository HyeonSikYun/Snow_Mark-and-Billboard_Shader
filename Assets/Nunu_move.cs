using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nunu_move : MonoBehaviour
{
    public GameObject obj;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.2f, 0.0f, 0.0f, Space.World);
            this.transform.localEulerAngles = new Vector3(0, -90, 0);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.2f, 0.0f, 0.0f, Space.World);
            this.transform.localEulerAngles = new Vector3(0, -250, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.2f, Space.World);
            this.transform.localEulerAngles = new Vector3(0, 20, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.2f, Space.World);
            this.transform.localEulerAngles = new Vector3(0, -170, 0);
        }
    }
}
