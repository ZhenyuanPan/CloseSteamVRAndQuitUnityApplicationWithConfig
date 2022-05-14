using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class ReadConfig : MonoBehaviour
{
    public Text appPath;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReadConfigConrotine());
        appPath.text = Application.dataPath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReadConfigConrotine() 
    {
        while (true)
        {
            
            using (StreamReader sr = new StreamReader(Application.dataPath+"/ApplicationQuit.config"))
            {
                string line= sr.ReadLine();
                if (line == "isStartQuit:1")
                {
                    Application.Quit();
                }
            }
            yield return new WaitForSeconds(1f);
        }
       
    }
}
