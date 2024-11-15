using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

}
