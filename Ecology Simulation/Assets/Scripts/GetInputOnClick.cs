using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Essentials.Variables;
public class GetInputOnClick : MonoBehaviour
{
    public Button btnClick;
    public TMP_InputField inputUser;
    public IntVariable input;
    // Start is called before the first frame update
    void Start()
    {
        btnClick.onClick.AddListener(GetInputOnClickHandler);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetInputOnClickHandler()
    {
        int tmp;
        int.TryParse(inputUser.text, out tmp);
        input.Value = tmp;
        Debug.Log(input.Value);
    }

}