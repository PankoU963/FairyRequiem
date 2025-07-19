using System.Collections.Generic;
using UnityEngine;

public class RootPool : MonoBehaviour
{
    [SerializeField] private GameObject rootPrefab;
    [SerializeField] private int poolSize = 20;
    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(rootPrefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetRoot(Vector3 position)
    {
        foreach (GameObject root in pool)
        {
            if (!root.activeInHierarchy)
            {
                root.transform.position = position;
                root.transform.rotation = Quaternion.identity;
                root.SetActive(true);
                return root;
            }
        }

        GameObject newRoot = Instantiate(rootPrefab, position, Quaternion.identity);
        pool.Add(newRoot);
        return newRoot;
    }
}