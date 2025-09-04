using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    bool ending = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.playerTag) && !ending) 
        {
            ending = true;
            SceneManager.LoadScene("Ending", LoadSceneMode.Additive);
        }
    }
}
