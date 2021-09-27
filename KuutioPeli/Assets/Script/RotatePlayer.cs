using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public SC_PickItem gravityrev,gravityrev1,gravityrev2;
    public int buttonclicks=0;
    bool reversed = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(PlayerRot());
        buttonclicks += 1;
        if (buttonclicks == 1)
        {
            gravityrev2.flip = true;
            gravityrev1.flip = true;
            gravityrev.flip = true;
            Debug.Log("FalseTest");

        }
        if (buttonclicks == 2)
        {
            gravityrev2.flip = false;
            gravityrev1.flip = false;
            gravityrev.flip = false;
            Debug.Log("TrueTest");
            //Debug.Log(buttonclicks);
            buttonclicks = 0;
        }
    }

    IEnumerator PlayerRot()
    {
        for (float i = 0; i <= 1 * 179; i++)
        {
            gameObject.transform.Rotate(-1, 0, 0 * 1);
            yield return null;
            //Debug.Log(i);
        }

        enabled = false;
    }
}
