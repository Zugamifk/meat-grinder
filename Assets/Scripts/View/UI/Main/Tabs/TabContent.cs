using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TabContent : MonoBehaviour
{
    [SerializeField]
    string _title;

    public string Title => _title;

    public abstract void OnShowTab();
}
