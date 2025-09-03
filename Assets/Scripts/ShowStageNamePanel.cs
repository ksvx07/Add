using TMPro;
using UnityEngine;

public class ShowStageNamePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    public void ShowStageName(string name)
    {
        text.text = name;
        GetComponent<Animator>().SetTrigger("ShowStageName");
    }
}