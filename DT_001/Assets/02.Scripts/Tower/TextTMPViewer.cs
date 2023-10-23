using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textWave;       // Text - TextMashPro UI [���� ���̺� / �� ���̺�]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;

    [SerializeField]
    private WaveSystem waveSystem;          // ���̺� ����
    [SerializeField]
    private EnemySpawner enemySpawner;      //�� ����

    private void Update()
    {
        textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.maxEnemyCount;

    }
}
