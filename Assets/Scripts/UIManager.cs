using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    public TextMeshProUGUI counterText;

    private void Awake()
    {
        inst = this;
    }
}
