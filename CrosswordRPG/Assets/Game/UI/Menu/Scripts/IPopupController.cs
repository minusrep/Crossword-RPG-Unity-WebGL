
public interface IPopupController 
{
    public void OpenPopup<T>() where T : Popup;
    public void OpenPopup();
}
