using UnityEngine;
using System.Collections;

public class LockCursorOnFocus : MonoBehaviour {

    private void OnApplicationFocus(bool focusStatus) {
        if (focusStatus)
            Cursor.lockState = CursorLockMode.Locked;
    }

}
