using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

public class CloseSteamVRApplication : MonoBehaviour
{
    void CloseVRProcessFunc(List<Process> processes)
    {
        foreach (var item in processes)
        {
            if (item.ProcessName == "vrserver")
            {
                item.Kill();
            }
            if (item.ProcessName == "vrcompositor")
            {
                item.Kill();
            }
            if (item.ProcessName == "vrdashboard")
            {
                item.Kill();
            }
            if (item.ProcessName == "vrmonitor")
            {
                item.Kill();
            }
        }

    }
    // Update is called once per frame
    private void OnApplicationQuit()
    {
        List<Process> allProcesses = Process.GetProcesses().ToList();
        CloseVRProcessFunc(allProcesses);
    }
   
   
}
