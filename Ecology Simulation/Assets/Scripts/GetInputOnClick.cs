using System;
using UnityEngine;
using TMPro;
using Essentials.Variables;

[RequireComponent(typeof(TMP_InputField))]
public class GetInputOnClick : MonoBehaviour
{
    private TMP_InputField inputUser;
    public IntVariable input;

    private void Awake()
    {
        inputUser = GetComponent<TMP_InputField>();
    }

    public void GetInputOnClickHandler()
    {
        int.TryParse(inputUser.text, out int tmp);
        input.Value = tmp;
    }
}