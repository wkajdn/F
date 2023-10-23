using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    //[SerializeField]
    //private float spawnmTime;
    [SerializeField]
    private Transform[] wayPoints;
    private int currentEnemyCount;          //���� ���̺꿡 �����ִ� �� ���� (���̺� ���۽� max�� ����, �� ��� �� -1)
    private Wave currentWave;
    private List<Enemy> enemyList;

    //���� ������ ���Դ� EnemySpawner���� �ϱ� ������ Set�� �ʿ� ����.
    public List<Enemy> EnemyList => enemyList;

    //���� ������ ������ EnmeySpawner���� �ϱ� ������ Set�� �ʿ� ����.
    public int CurrentEnemyCount => currentEnemyCount;
    public int maxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        // �� ������ �ڷ�ƾ �Լ� ȣ��
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        //�Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;
        //���� ���̺��� �ִ� �� ���ڸ� ����
        currentEnemyCount = currentWave.maxEnemyCount;
        //���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        //���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        //while (true)
        while ( spawnEnemyCount < currentWave.maxEnemyCount )
        {
            //GameObject clone = Instantiate(enemyPrefab);
            //���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ����
            int enemyIndex =Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enmey = clone.GetComponent<Enemy>();

            enmey.Setup(this, wayPoints);
            enemyList.Add(enmey);                               //����Ʈ�� ��� ������ �� ���� ����

            SpawnEnemyHPSlider(clone);


            //���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount ++;

            //yield return new WaitForSeconds(spawnmTime);
            //�� ���̺긶��  spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺�(currentWave)�� spawnTime ���
            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }

    public void DestroyEnemy(Enemy enemy)
    {

        //���� ����� ������ ���� ���̺��� ���� �� ���� ���� (UI ǥ�ÿ�)
        currentEnemyCount --;
        //����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        //�� ������Ʈ ����
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        //Slider UI ������Ʈ�� parent("Canvas"' ������Ʈ)�� �ڽ����� ����
        //Tip. UI�� ĵ������ �ڽĿ�����Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�.
        sliderClone.transform.SetParent(canvasTransform);
        //���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Silder UI�� �ڽ��� ü�������� ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

}
