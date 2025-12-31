using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Tapbuttonscript : Button
{
    public static Tapbuttonscript instance;

    protected override void Awake()
    {
        instance = this;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        ///gamemanager should start the game
        GameManager.instance.StartGame();

        ///diactivate the start panel
        gameObject.SetActive(false);
    }
    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }
}
