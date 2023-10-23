using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textWave;       // Text - TextMashPro UI [현재 웨이브 / 총 웨이브]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;

    [SerializeField]
    private WaveSystem waveSystem;          // 웨이브 정보
    [SerializeField]
    private EnemySpawner enemySpawner;      //적 정보

    private void Update()
    {
        textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.maxEnemyCount;

    }
}
