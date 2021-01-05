using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelIndexButtons : MonoBehaviour
{
    public MSManager msManager;
    public int index;
    public MSMenuImage MenuImage;
    public void OnPanelButtonClick()
    {
        msManager.PanelIndex = index;
        msManager.PerformUpdate(msManager.CurrentMonster, msManager.PanelIndex);
        MenuImage.MenuSlideButton();
    }
}
