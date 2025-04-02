using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject miniMenu;
    public Button menuButton;
    public Button exitButton;

    void Start()
    {
        miniMenu.SetActive(false);

        menuButton.onClick.AddListener(ToggleMenu);
        exitButton.onClick.AddListener(ExitApp);
    }

    void ToggleMenu()
    {
        miniMenu.SetActive(!miniMenu.activeSelf);
    }

    void ExitApp()
    {
        Application.Quit();
    }
}
