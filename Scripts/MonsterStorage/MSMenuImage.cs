using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSMenuImage : MonoBehaviour
{
    public GameObject MenuImage;
    public bool MenuShowing = false;
    public bool MenuMoving = false;
    public bool MenuMovingBack = false;
    public Sprite ForwardArrow;
    public Sprite BackwardsArrow;
    public Button MenuButton;
    private void Update()
    {
        if(MenuMoving && MenuImage.transform.localPosition.x < -445)
        {
            MenuImage.transform.localPosition = new Vector3(MenuImage.transform.localPosition.x+2, MenuImage.transform.localPosition.y, MenuImage.transform.localPosition.z);
        }
        else if (MenuMoving && MenuImage.transform.localPosition.x > -445){
            MenuShowing = true;
            MenuMoving = false;
            //MenuButton.GetComponent<Image>().sprite = ForwardArrow;
            MenuButton.interactable = true;
        }
        if (MenuMovingBack && MenuImage.transform.localPosition.x > -620)
        {
            MenuImage.transform.localPosition = new Vector3(MenuImage.transform.localPosition.x - 4, MenuImage.transform.localPosition.y, MenuImage.transform.localPosition.z);
        }
        else if (MenuMovingBack && MenuImage.transform.localPosition.x <= -620)
        {
            MenuMovingBack = false;
            //MenuButton.GetComponent<Image>().sprite = ForwardArrow;
            MenuButton.interactable = true;
        }

    }
    public void MenuSlideButton()
    {
        if (MenuShowing)
        {
            MenuShowing = false;
            MenuMovingBack = true;
            MenuButton.interactable = false;
            //MenuButton.GetComponent<Image>().sprite = BackwardsArrow;
            return;
        }
        MenuMoving = true;
        MenuButton.interactable = false;
        
    }
    //-620,-445
}
