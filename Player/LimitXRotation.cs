using UnityEngine;
using System.Collections;

public class LimitXRotation : MonoBehaviour {

    [SerializeField]
    private float min = 0, max = 0;

    private void Update() {
        Limit();
    }

    public void Limit() {//because of the AI span bullet script changing its angle.
        float x = transform.localEulerAngles.x;

        //convert rotation to between 0 and 360
        x %= 360;
        if (x < 0)
            x += 360;

        //convert rotation to between -180 and 180
        if (x >= 180)
            x -= 360;

        x = Mathf.Clamp(x, min, max);

        transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
