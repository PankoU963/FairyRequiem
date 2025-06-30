using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "Wave Data")]
public class EnemySpawnData : ScriptableObject
{
    public int Zona;

    [Header("Wave1")]
    public List<GameObject> enemyPrefab1;
    public int[] amount1;

    [Header("Wave2")]
    public List<GameObject> enemyPrefab2;
    public List<int> amount2;

    [Header("Wave3")]
    public List<GameObject> enemyPrefab3;
    public List<int> amount3;


}