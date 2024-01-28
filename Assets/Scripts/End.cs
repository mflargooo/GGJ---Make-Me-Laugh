using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private SceneLoader sl;
    // Start is called before the first frame update
    void Start()
    {
        sl = GetComponent<SceneLoader>();
        sl.LoadSceneAfterTime(2, 3f);
    }
}
