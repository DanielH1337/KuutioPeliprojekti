using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    bool reversed = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(PlayerRot());       
    }

    IEnumerator PlayerRot()
    {
        for (float i = 0; i <= 1 * 179; i++)
        {
            gameObject.transform.Rotate(-1, 0, 0 * 1);
            yield return null;
            Debug.Log(i);
        }

        enabled = false;
    }
}
