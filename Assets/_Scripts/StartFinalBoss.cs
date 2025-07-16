using UnityEngine;

public class StartFinalBoss : MonoBehaviour
{
    [SerializeField] private GameObject wallF, WallB;

    [SerializeField] GameObject zonaFija;

    private bool activated = false;

    private CameraMovement cameraMovement;


    [SerializeField] private GameObject boss;
    [SerializeField] private Transform spawnBossLocation;


    private void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            cameraMovement.ActivarZonaFija(transform.parent);
            activated = true;

            GameObject currentBoss = Instantiate(boss, spawnBossLocation.position + new Vector3(0,10,0), Quaternion.identity);

            wallF.SetActive(true);
            WallB.SetActive(true);
        }
    }

}
