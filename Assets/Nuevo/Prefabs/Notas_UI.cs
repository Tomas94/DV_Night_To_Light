using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notas_UI : MonoBehaviour
{
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _description;
    public Image _Background;

    private void Start()
    {
        Invoke(nameof(SetToManager), 0.5f);
    }

    void SetToManager()
    {
        GameManager.Instance.SetTextUIBox(this);
    }
}
