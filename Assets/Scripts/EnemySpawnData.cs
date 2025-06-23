using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "Wave Data")]
public class EnemySpawnData : ScriptableObject
{
    public int Zona;
    public List<GameObject> enemyPrefab;
    public List<int> amount;    
    
    
}