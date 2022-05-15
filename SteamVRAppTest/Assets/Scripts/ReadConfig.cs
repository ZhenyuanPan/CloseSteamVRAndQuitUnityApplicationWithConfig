using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text;

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
    /// <summary>
    /// 根据配置来关闭程序
    /// </summary>
    /// <returns></returns>
    IEnumerator ReadConfigConrotine() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("do quit Application");
            var content = File.ReadAllText(Application.dataPath + "/QuitApplication.config");
            if (content.Contains("QuitApplication"))
            {
                File.WriteAllText(Application.dataPath + "/QuitApplication.config", "");
                Application.Quit();
            }
        }
    }
}
