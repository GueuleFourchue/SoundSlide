using UnityEngine;
using System.Collections;

public class InputsManager : MonoBehaviour
{
    public static InputsManager IM;

    public KeyCode left1 { get; set; }
    public KeyCode left2 { get; set; }
    public KeyCode right1 { get; set; }
    public KeyCode right2 { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }
       
        left1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey1", "Q"));
        left2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey2", "LeftArrow"));
        right1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey1", "D"));
        right2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey2", "RightArrow"));

    }
}