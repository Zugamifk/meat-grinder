using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabContainer : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI _titleText;

    public void ShowTab(TabContent content)
    {
        _titleText.text = content.Title;
        content.OnShowTab();
        gameObject.SetActive(true);
    }

    public void CloseTab_OnClick()
    {
        gameObject.SetActive(false);
    }
}
