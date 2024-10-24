using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
		Cursor.visible = false;
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        cursor.transform.position = mousePos;
    }
}
