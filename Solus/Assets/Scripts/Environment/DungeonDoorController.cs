using System.Text;
using UnityEngine;

public class DungeonDoorController : MonoBehaviour
{
    public Vector3 entranceDoorFinalPosition;

    private Camera mainCamera;
    private bool allEnemiesDestroyed;

    private void Awake()
    {
        mainCamera = Camera.main;

        allEnemiesDestroyed = false;

        LevelManager.OnEnemiesDestroyed += OpenDoor;
    }

    private void OnDestroy()
    {
        LevelManager.OnEnemiesDestroyed -= OpenDoor;
    }

    private void Update()
    {
        if (allEnemiesDestroyed && (Vector3.Distance(gameObject.transform.position, entranceDoorFinalPosition) > 1f))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, entranceDoorFinalPosition, Time.deltaTime * 2);

            mainCamera.GetComponent<CameraShake>().ShakeCamera(1f);
        }
    }


    private void OpenDoor()
    {
        allEnemiesDestroyed = true;
    }
}
