using System;

public interface IOpenable
{
    event Action<int> OnWordOpenEvent;
    void OpenTile();
    void OpenWord(string value);
}