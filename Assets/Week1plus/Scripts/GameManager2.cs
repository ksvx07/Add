using UnityEngine;
using System.Collections;

public class GameManager2 : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager2 instance { get; private set; }

    [SerializeField] private Transform levelsTransform;
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 playerResetPosition;

    public float actionDelayTime = 0.1f;

    private int thisLevel = 1;
    private int actionsInProgress = 0;
    private bool _busy = false; // 실제 외부에서 보는 값
    public bool busy => _busy;
    public bool gravityEnabled = true;

    private Coroutine delayCoroutine;

    private void Awake()
    {
        // 싱글톤 체크
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // 이미 다른 인스턴스가 있으면 제거
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 유지하고 싶으면 활성
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            thisLevel += 1;
            MakeLevel();
            ResetPlayerPosition();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MakeLevel();
            ResetPlayerPosition();
        }
    }

    private void MakeLevel()
    {
        for (int i = 0; i < levelsTransform.childCount; i++)
        {
            Transform child = levelsTransform.GetChild(i);
            Destroy(child.gameObject);
        }

        for (int i = 0; i < thisLevel; i++)
        {
            Instantiate(levelPrefabs[i], levelsTransform);
        }
    }

    private void ResetPlayerPosition()
    {
        playerTransform.position = playerResetPosition;
    }

    public void StartAction()
    {
        actionsInProgress++;
        _busy = true; // 행동 시작하면 무조건 true

        // 유예 코루틴 실행 중이면 취소
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
            delayCoroutine = null;
        }

        //Debug.Log("Action started, count: " + actionsInProgress);
    }

    // 행동 끝날 때
    public void EndAction()
    {
        actionsInProgress--;
        if (actionsInProgress < 0) actionsInProgress = 0;

        //Debug.Log("Action ended, count: " + actionsInProgress);

        if (actionsInProgress == 0)
        {
            // 모든 행동 끝 → 유예 후 doingSomething false
            delayCoroutine = StartCoroutine(DelayDoingSomethingFalse(actionDelayTime));
        }
    }

    private IEnumerator DelayDoingSomethingFalse(float delay)
    {
        yield return new WaitForSeconds(delay);
        _busy = false;
        delayCoroutine = null;
        //Debug.Log("All actions done after delay, doingSomething = false");
    }

    public void EnableGravity(bool enable) => gravityEnabled = enable;
}
