using System;

public interface IAdvertisement
{
    event Action OnAdvShowEvent;
    event Action OnAdvCloseEvent;
    event Action OnRewardedEvent;

    public void ShowFullScreenAdv();
    public void ShowRewardedAdv();
    public void RateGame();

}