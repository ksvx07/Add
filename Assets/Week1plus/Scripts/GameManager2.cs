using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using JetBrains.Annotations;

public class GameManager2 : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager2 instance { get; private set; }

    [SerializeField] private Transform levelsTransform;
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private Vector3 playerResetPosition;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private ReverseRoom reverseRoom;
    [SerializeField] private TextMeshProUGUI reverseText;
    [SerializeField] private TextMeshProUGUI dashText;
    [SerializeField] private TextMeshProUGUI reverseAlertText;
    [SerializeField] private TextMeshProUGUI dashAlertText;
    [SerializeField] private TextMeshProUGUI end;

    public GameObject player;

    public float actionDelayTime = 0.1f;

    public int thisLevel = 1;
    private int actionsInProgress = 0;
    private bool _busy = false; // 실제 외부에서 보는 값
    public bool busy => _busy;
    public bool gravityEnabled = true;
    private bool Died;

    private Coroutine delayCoroutine;

    private void Start()
    {
        Die();
    }
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

    public bool keyW;
    public bool keyA;
    public bool keyS;
    public bool keyD;
    public bool keyQ;
    public bool keyE;
    public bool keyR;
    public bool keyT;

    void Update()
    {
        keyW = keyS = keyA = keyD = keyQ = keyE = keyR = keyT = false;

        if (Input.GetKeyDown(KeyCode.W)) keyW = true;
        else if (Input.GetKeyDown(KeyCode.S)) keyS = true;
        else if (Input.GetKeyDown(KeyCode.A)) keyA = true;
        else if (Input.GetKeyDown(KeyCode.D)) keyD = true;
        else if (Input.GetKeyDown(KeyCode.Q)) keyQ = true;
        else if (Input.GetKeyDown(KeyCode.E)) keyE = true;
        else if (Input.GetKeyDown(KeyCode.R)) keyR = true;
        else if (Input.GetKeyDown(KeyCode.T)) keyT = true;

        //Debug.Log(actionsInProgress);
        if (GameManager2.instance.keyT)
        {
            thisLevel += 1;
            ResetLevel();
            //NextLevel();
        }

        if (GameManager2.instance.keyR)
        {
            Die();
        }
    }

    bool alertReverse;
    bool alertDash;

    // 이거쓰면됨 담레밸
    async public void NextLevel()
    {
        if (!Died)
        {
            StartAction();
            thisLevel += 1;
            Died = true;
            await Task.Delay(500);
            StartCoroutine(MoveToPosition(player.transform, new Vector3(0, 0, 5), new Vector3(0, 0, 6), 0.75f));
            await Task.Delay(1000);

            UIMAnager.Instance.FadeOut();
            await Task.Delay(UIMAnager.fadeOutTime / 2);
            ResetLevel();
            LevelStart();
        }

        if (thisLevel > 3)
        {
            reverseText.gameObject.SetActive(true);

            if (!alertReverse)
            {
                alertReverse = true;
                alertSomething(reverseAlertText.gameObject);
            }
        }
        if (thisLevel > 8)
        {
            dashText.gameObject.SetActive(true);

            if (!alertDash)
            {
                alertDash = true;
                alertSomething(dashAlertText.gameObject);
            }
        }

        if (thisLevel > 11)
        {
            end.gameObject.SetActive(true);
        }
    }

    async void alertSomething(GameObject thisObj)
    {
        thisObj.SetActive(true);
        await Task.Delay(5000);
        thisObj.SetActive(false);

    }



    private void ResetLevel()
    {
        reverseRoom.ResetRoom();

        MakeLevel();
        ResetPlayer();
        ResetBusy();
    }

    async public void Die()
    {
        if (!Died)
        {
            playerDie();
            Died = true;
            StartAction();
            UIMAnager.Instance.FadeOut();
            await Task.Delay(UIMAnager.fadeOutTime / 2);
            ResetLevel();
            LevelStart();
        }
    }

    async public void LevelStart()
    {
        StartAction();
        LevelStartPlayerPos();
        await Task.Delay(UIMAnager.fadeOutTime / 2);
        ResetBusy();
        Died = false;
    }

    private void LevelStartPlayerPos()
    {
        StartCoroutine(MoveToPosition(player.transform, new Vector3(0, 0, 0), new Vector3(0, 0, 1), 0.75f));
    }

    IEnumerator MoveToPosition(Transform obj, Vector3 startPos, Vector3 targetPos, float time)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            obj.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPos; // 마지막 위치 보정
    }

    private void ResetBusy()
    {
        // 코루틴이 돌고 있으면 중단
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
            delayCoroutine = null;
        }

        // 상태 초기화
        actionsInProgress = 0;
        _busy = false;
    }

    private void MakeLevel()
    {
        MovePlatform thisButton = null;
        MovablePlatform movable = null;
        Laser laser = null;

        // 기존 레벨 삭제
        for (int i = 0; i < levelsTransform.childCount; i++)
        {
            Destroy(levelsTransform.GetChild(i).gameObject);
        }

        int levelss = thisLevel;
        if (levelss > levelPrefabs.Length)
            levelss = levelPrefabs.Length;
        // 새 레벨 생성
        for (int i = 0; i < levelss; i++)
        {
            GameObject thisLevelObj = Instantiate(levelPrefabs[i], levelsTransform);

            if (i == 0)
            {
                Transform child = thisLevelObj.transform.Find("HalfButton");
                if (child != null)
                {
                    thisButton = child.GetComponent<MovePlatform>();
                }
            }

            if (i == 2)
            {
                Transform child = thisLevelObj.transform.Find("MovablePlatform");
                if (child != null)
                {
                    
                    movable = child.GetComponent<MovablePlatform>();

                }
            }

            if (thisLevel > 2)
            {
                if (i == 1)
                {
                    Transform child = thisLevelObj.transform.Find("Laser");
                    if (child != null)
                    {

                        laser = child.GetComponent<Laser>();
                        laser.blink = false;

                    }
                }
            }

            if (i == 6)
            {
                Transform child = thisLevelObj.transform.Find("PushableBlock");
                if (child != null)
                {
                    reverseRoom.pushable1 = child.transform;
                }
            }

            if (i == 8)
            {
                Transform child = thisLevelObj.transform.Find("PushableBlock");
                if (child != null)
                {
                    reverseRoom.pushable2 = child.transform;
                }
            }


        }

        // 둘 다 확보됐을 때만 연결
        if (thisButton != null && movable != null)
        {
            thisButton.platform = movable;
        }


    }
    [SerializeField] private ParticleSystem playerParticle;
    [SerializeField] private ParticleSystem boxParticle;

    public void DestroyBox(GameObject thisBox)
    {
        Vector3 thisPos = thisBox.transform.position;
        Instantiate(boxParticle, thisPos, boxParticle.transform.rotation);
        Destroy(thisBox);
    }

    private void playerDie()
    {
        Vector3 thisPos = player.transform.position;
        Instantiate(playerParticle, thisPos, playerParticle.transform.rotation);
        player.SetActive(false);
    }

    private void ResetPlayer()
    {
        if (player != null)
        {
            //Vector3 thisPos = player.transform.position;
            //Instantiate(playerParticle, thisPos, playerParticle.transform.rotation);
            Destroy(player);
        }
        player = Instantiate(playerPrefab, playerResetPosition, playerPrefab.transform.rotation);

        reverseRoom.player = player.transform;
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
