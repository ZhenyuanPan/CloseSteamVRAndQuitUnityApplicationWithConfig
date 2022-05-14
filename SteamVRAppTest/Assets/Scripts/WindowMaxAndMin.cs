
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
public class WindowMaxAndMin : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("User32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("User32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int SetCursorPos(int x, int y); //设置光标位置
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(ref int x, ref int y); //获取光标位置
    [DllImport("user32.dll")]
    static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo); //鼠标事件

    //这个枚举同样来自user32.dll
    [Flags]
    enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        Absolute = 0x8000
    }

    const int SW_SHOWMINIMIZED = 2; //{最小化, 激活}
    const int SW_SHOWMAXIMIZED = 3;//最大化
    const int SW_SHOWRESTORE = 1;//还原
    private void Awake()
    {

        if (GameObject.Find("SetWindow").gameObject != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        StartCoroutine(Setposition());
        OnClickMaximize();
    }
    public void OnClickMinimize()
    { //最小化 
        ShowWindow(GetForegroundWindow(), SW_SHOWMINIMIZED);
    }

    public void OnClickMaximize()
    {
        //最大化
        ShowWindow(GetForegroundWindow(), SW_SHOWMAXIMIZED);
    }

    public void OnClickRestore()
    {
        //还原
        ShowWindow(GetForegroundWindow(), SW_SHOWRESTORE);
    }

    //测试
    public void OnGUI()
    {
        //if (GUI.Button(new Rect(100, 100, 200, 100), "最大化"))
        //    OnClickMaximize();
        //if (GUI.Button(new Rect(100, 300, 200, 100), "最小化"))
        //    OnClickMinimize();
        //if (GUI.Button(new Rect(100, 500, 200, 100), "窗口还原"))
        //    OnClickRestore();
    }


    IEnumerator Setposition()
    {
        yield return new WaitForSeconds(0.1f);		//不知道为什么发布于行后，设置位置的不会生效，我延迟0.1秒就可以
        SetWindowPos(GetForegroundWindow(), -1, 0, 0, 0, 0, 1 | 2);       //设置屏幕大小和位置
    }
    float addTime = 0;
    void Update()
    {
        addTime += Time.deltaTime;
        if (addTime >= 5)
        {
            addTime = 0;

            // apptitle自己到查看进程得到，一般就是程序名不带.exe
            // 或者用spy++查看
            IntPtr hwnd = FindWindow(null, "SteamVRAppTest");

            // 如果没有找到，则不做任何操作（找不到一般就是apptitle错了）
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            IntPtr activeWndHwnd = GetForegroundWindow();

            // 当前程序不是活动窗口，则设置为活动窗口
            if (hwnd != activeWndHwnd)
            {
                SetWindowPos(hwnd, -1, 0, 0, 0, 0, 1 | 2);
                OnClickMaximize();

                SetCursorPos(100, 100);

                mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
                mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            }
        }
    }

}