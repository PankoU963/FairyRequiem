using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossStage {Fall, Idle, Attack }
    [SerializeField]private BossStage stage;

    private Rigidbody rb;

    public GameObject tronco;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurrentStage()
    {
        if(stage == BossStage.Fall)
        {
            rb.Move()
        }
    }
}
