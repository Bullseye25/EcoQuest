using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DummyAppOperator : MonoBehaviour
{
    public void UponReset() 
    {
        SceneManager.LoadScene(0);
    }
}
