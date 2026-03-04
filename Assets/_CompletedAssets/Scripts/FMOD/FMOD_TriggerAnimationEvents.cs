using UnityEngine;
using FMODUnity;

public class FMOD_TriggerAnimationEvents : MonoBehaviour
{
    void TriggerFmodEvent (string fmodEventPath)
    {
        RuntimeManager.PlayOneShot(fmodEventPath, transform.position);
    }
}
