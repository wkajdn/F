using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;

    public int Index = 1;
    public GameObject GameOver;

    public void SpawnTower(Transform tileTransform)
    {
        //타워 건설 가능 여부 확인
        // 1. 현재 타일의 위치에 이미 타워가 건설되어 있으면 차워 건설 X
        Tile tile = tileTransform.GetComponent<Tile>();

        if ( tile.IsBuildTower == true)
        {
            return;
        }

        //타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;
        // 선택한 타일의 위치에 타워 건설
        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
        //타워 무기에 enemySpawner 정보 전달
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
}
