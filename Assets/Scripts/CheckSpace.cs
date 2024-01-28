using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpace : MonoBehaviour
{
    [SerializeField] private SceneLoader sl;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            sl.LoadScene(2);
    }
}
