using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    private Button button;


    // Start is called before the first frame update
    private void Awake()
    {
        Button button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(SceneMenuController.Instance.LoadConvaiDemo);
    }
}
