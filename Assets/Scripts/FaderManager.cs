using UnityEngine;

public class FaderManager : MonoBehaviour
{
    public SEvent fadedIn;
    public SEvent fadedOut;

    public void FadedOut()
    {
        fadedOut.Raise();
    }
    public void FadedIn()
    {
        fadedIn.Raise();
    }
}
