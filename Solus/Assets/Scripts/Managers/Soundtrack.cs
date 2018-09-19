using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public LayerMask layers;
    public bool playOnAwake;

    private void Start()
    {
        if (playOnAwake)
        {
            MusicManager.instance.PushTrack(this.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
            MusicManager.instance.PushTrack(this.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
            MusicManager.instance.PopTrack();
    }

    private void OnDestroy()
    {
        MusicManager.instance.PopTrack();
    }
}
