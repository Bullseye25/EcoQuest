using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject l1, l2, l3;
    [SerializeField] private GameObject s1 , s2 , s3 , s4 , s5, TextComp;
  
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(L1S1());
    }
    public void complete()
    {
        StartCoroutine(comp());
    }
    private IEnumerator comp()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second
        TextComp.SetActive(false);
    }
    public void NextStage()
    {
        if (AntidotHelp.score == 15)
        {
            StartCoroutine(L1S2());
        } else if (AntidotHelp.score == 30)
        {
            StartCoroutine(L1S3());
        } else if (AntidotHelp.score == 45)
        {
            StartCoroutine(L1S4());
        } else if (AntidotHelp.score == 60)
        {
            StartCoroutine(L1S5());
        } else if (AntidotHelp.score == 75)
        {
            StartCoroutine(L2S1());
        } else if (AntidotHelp.score == 90)
        {
            StartCoroutine(L2S2());
        } else if (AntidotHelp.score == 105)
        {
            StartCoroutine(L2S3());
        } else if (AntidotHelp.score == 120)
        {
            StartCoroutine(L2S4());
        } else if (AntidotHelp.score == 135)
        {
            StartCoroutine(L2S5());
        } else if (AntidotHelp.score == 150)
        {
            StartCoroutine(L3S1());
        }else if (AntidotHelp.score == 165)
        {
            StartCoroutine(L3S2());
        } else if (AntidotHelp.score == 180)
        {
            StartCoroutine(L3S3());
        } else if (AntidotHelp.score == 200)
        {
            StartCoroutine(L3S4());
        } else if (AntidotHelp.score == 215)
        {
            StartCoroutine(L3S5());
        }
    }
     private IEnumerator L1S1()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l1.gameObject.SetActive(false);
        s1.gameObject.SetActive(false);
        s2.gameObject.SetActive(false); 
    }
     private IEnumerator L1S2()
    {   
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l1.gameObject.SetActive(true);
        s2.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
     private IEnumerator L1S3()
    {  
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l1.gameObject.SetActive(true);
        //s3.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
    private IEnumerator L1S4()
    {  
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l1.gameObject.SetActive(true);
        //s4.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
     private IEnumerator L1S5()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l1.gameObject.SetActive(false);
        //s5.gameObject.SetActive(false);
        StartCoroutine(L1S1());
    }
     private IEnumerator L2S1()
    {   
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l2.gameObject.SetActive(true);
        s1.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
     private IEnumerator L2S2()
    {  
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l2.gameObject.SetActive(true);
        s2.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
    private IEnumerator L2S3()
    {  
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l2.gameObject.SetActive(true);
        s3.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    } private IEnumerator L2S4()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l2.gameObject.SetActive(false);
        s4.gameObject.SetActive(false);
        StartCoroutine(L1S1());
    }
     private IEnumerator L2S5()
    {   
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l2.gameObject.SetActive(true);
        s5.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
     private IEnumerator L3S1()
    {  
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l3.gameObject.SetActive(true);
        s1.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
    private IEnumerator L3S2()
    {  
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l3.gameObject.SetActive(true);
        s2.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    } private IEnumerator L3S3()
    {
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l3.gameObject.SetActive(false);
        s3.gameObject.SetActive(false);
        StartCoroutine(L1S1()); 
    }
     private IEnumerator L3S4()
    {   
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l3.gameObject.SetActive(true);
        s4.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
     private IEnumerator L3S5()
    {  
        yield return new WaitForSeconds(2f); // Wait for 1 second
        l3.gameObject.SetActive(true);
        s5.gameObject.SetActive(true);
        StartCoroutine(L1S1());
       
    }
    



   
}
