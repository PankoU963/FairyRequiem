using System.Collections.Generic;
using UnityEngine;

public class RootPool : MonoBehaviour
{
    public GameObject rootPrefab;
    public int poolSize = 20;
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

        // Opcional: crear más si se necesita
        GameObject newRoot = Instantiate(rootPrefab, position, Quaternion.identity);
        pool.Add(newRoot);
        return newRoot;
    }
}