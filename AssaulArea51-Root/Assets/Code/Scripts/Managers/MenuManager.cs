using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private string _inputOpenMainMenu = "Fill with proper input axis";
    [SerializeField] private string _inputOpenPauseMenu = "Fill with proper input axis";

    [Header("References")]
    [SerializeField] private IGameMenu _mainMenu;
    [SerializeField] private IGameMenu _pauseMenu;

    private IGameMenu _displayedMenu;
    
    private void Update()
    {
        if (Input.GetAxisRaw(_inputOpenMainMenu) != 0f)
            if (TrySetAsDisplayedMenu(_mainMenu))
                _displayedMenu.Display();

        if (Input.GetAxisRaw(_inputOpenPauseMenu) != 0f)
            if (TrySetAsDisplayedMenu(_pauseMenu))
                _displayedMenu.Display();
    }

    private bool TrySetAsDisplayedMenu(IGameMenu menuToDisplay)
    {
        if(_displayedMenu.Equals(null))
        {
            _displayedMenu = menuToDisplay;
            return true;
        }

        return false;   
    }

    public void HideDisplayedMenu()
    {
        _displayedMenu.Hide();
        _displayedMenu = null;
    }
}
