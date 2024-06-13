using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public int id;
    public Text idText;

    public void Initialize(int newId)
    {
        id = newId;
        if (idText != null)
        {
            idText.text = id.ToString();
        }
    }
}
