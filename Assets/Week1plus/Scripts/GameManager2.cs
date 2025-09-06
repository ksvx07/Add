using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] private Transform levelsTransform;
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 playerResetPosition;


    private int thisLevel = 1;

    // Update is called once per frame
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
}
