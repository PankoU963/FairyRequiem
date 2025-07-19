using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private RootPool rootPool;
    [SerializeField] private int numberOfRoots = 20;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float riseSpeed = 2f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private int damageAmount = 50; 
    [SerializeField] private GameObject areaTriggerPrefab;
    [SerializeField] private int cost;

    private bool abilityActive;
    private bool bufferAbility;
    private AnimatorStateInfo stateInfo;

    private Animator animator;
    private Movement playerMovement;
    private Mana playerMana;
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
                if (Input.GetKeyDown(KeyCode.Z) && !bufferAbility)
                {
                    bufferAbility = true;
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
                bufferAbility = false;
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