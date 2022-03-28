using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownStartup : MonoBehaviour
{
    // Dropdown menu for pipe separation difficulty
    private void Awake()
    {
        Dropdown dropdown = gameObject.GetComponent<Dropdown>();
        dropdown.value = PlayerPrefs.GetInt("difficulty", 0);
    }
}
