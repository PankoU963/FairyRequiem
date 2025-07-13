using UnityEngine;

public class Ability : MonoBehaviour
{
    public RootPool rootPool;
    public int numberOfRoots = 10;
    public float radius = 3f;
    public float riseSpeed = 1f;
    public float rotationSpeed = 100f;
    public float duration = 3f;

    [SerializeField] private int damageAmount; 

    public GameObject areaTriggerPrefab;

    bool abilityActive;

    [SerializeField] private Animator animator;

    private AnimatorStateInfo stateInfo;

    private Movement playerMovement;

    private Mana playerMana;
    [SerializeField] int cost;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        playerMana = GetComponent<Mana>();
    }

    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Ability"))
        {
            abilityActive = false;
            if(playerMana.CurrentMana >= cost)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    animator.SetTrigger("Ability");
                    playerMana.CurrentMana -= cost;
                }
            }
        }

        if (stateInfo.IsName("Ability"))
        {
            playerMovement.isAttack = true;
            if (stateInfo.normalizedTime >= 0.5f && !abilityActive)
            {
                abilityActive = true;
                SpawnRoots();
            }
        }

    }

    void SpawnRoots()
    {
        GameObject area = Instantiate(areaTriggerPrefab, transform.position, Quaternion.identity);
        area.transform.SetParent(transform);
        area.GetComponent<RootArea>().damageAmount = damageAmount;
        area.transform.localScale = Vector3.one * radius * 2f;
        for (int i = 0; i < numberOfRoots; i++)
        {
            Vector2 randomPos2D = Random.insideUnitCircle * radius;
            Vector3 spawnPos = transform.position + new Vector3(randomPos2D.x, -2, randomPos2D.y);

            GameObject root = rootPool.GetRoot(spawnPos);
            root.GetComponent<RootMovement>().Init(riseSpeed, rotationSpeed, duration);
        }
    }
}