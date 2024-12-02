using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class TigerSenser : SingletonMonoBehaviour<TigerSenser>
{
    [SerializeField] private string portName = "COM8";
    private SerialPort sp;
    public string s = "";

    private string value = "0,0";
    private Thread readThread;
    private bool isRunning = false;

    void Start()
    {
        // シリアルポートを定義
        sp = new SerialPort(portName, 9600);
        sp.ReadTimeout = 2000;

        OpenSerialPort();
        StartReadingThread();
    }

    void OpenSerialPort()
    {
        if (sp != null && !sp.IsOpen)
        {
            sp.Open();
        }
    }

    void StartReadingThread()
    {
        isRunning = true;
        readThread = new Thread(ReadSerialPort);
        readThread.Start();
    }

    void ReadSerialPort()
    {
        while (isRunning)
        {
            if (sp != null && sp.IsOpen)
            {
                try
                {
                    string tmp = sp.ReadLine();
                    string[] tmpAr = tmp.Split(',');
                    if (tmpAr[0] == "0" || tmpAr[0] == "1" || tmpAr[0] == "2" ||tmpAr[0] == "3" )
                    {
                        value = tmp;
                    }
                    else
                    {
                        value = "-1,0";
                    }
                }
                catch (System.Exception)
                {
                    Debug.LogWarning("Failed to read from serial port");
                }
            }
        }
    }

    void Update()
    {
        // Update内では特にシリアルポート操作はしない
    }

    public string GetValue()
    {
        return value;
    }

    public void SendWriteUpNum(string num)
    {
        sp.Write(num);
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join();
        }

        if (sp != null && sp.IsOpen)
        {
            sp.Close();
        }

        Debug.Log("senser off");
    }
}
