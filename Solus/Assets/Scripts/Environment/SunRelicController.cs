using UnityEngine;

public class SunRelicController : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public delegate void RelicHandler();
    public static event RelicHandler OnRelicObtained;

    Vector3 posOffset;
    Vector3 tempPos;

    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() != null && collider.gameObject.GetComponent<PlayerController>() == PlayerController.instance)
        {
            if (OnRelicObtained != null) OnRelicObtained();
        }
    }
}