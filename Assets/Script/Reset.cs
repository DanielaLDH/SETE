using UnityEngine;

public class Reset : MonoBehaviour
{
    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs redefinidos.");
    }
}
