using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading; //スレッド通信

public class SerialHandler : MonoBehaviour
{
    // シリアルデータを受信したときに呼び出されるイベントハンドラ
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived=delegate{};

     // シリアルポートの名前とボーレート
    public string portName = "COM6";
    public int baudRate    = 9600;
    string Conectports = string.Join("", SerialPort.GetPortNames());

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        // 利用可能なポートを検索し、指定されたポートを開く
        if(Conectports.Contains(portName))
            OpenB();
        else if(Conectports.Contains("COM3"))
            Open();
       
    }

    void Update()
    {
         // 新しいメッセージが受信されたときにイベントを発生させる
        if (isNewMessageReceived_) {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        Close();
    }
    // 指定されたプロペラ1のポートを開く
    private void Open()
    {
        serialPort_ = new SerialPort("COM3", baudRate, Parity.None, 8, StopBits.One);
       
        serialPort_.ReadTimeout = 20;
        serialPort_.NewLine = "\n";

        serialPort_.Open();

        isRunning_ = true;

       

        thread_ = new Thread(Read);
        thread_.Start();
    }

    // 指定されたプロペラ2のポートを開く
     private void OpenB()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
     
        serialPort_.ReadTimeout = 20;
        serialPort_.NewLine = "\n";

        serialPort_.Open();

        isRunning_ = true;

       

        thread_ = new Thread(Read);
        thread_.Start();
    }

    //ポートを閉じる
    private void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive) {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen) {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

 // ポートからのデータを読み取る
    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
            try {
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            } catch (System.Exception e) {
                UnityEngine.Debug.LogWarning(e.Message);
                 UnityEngine.Debug.LogWarning(Conectports);
            }
        }
    }

// ポートにデータを書き込む
    public void Write(string message)
    {
        try {
            serialPort_.Write(message);
        } catch (System.Exception e) {
            UnityEngine.Debug.LogWarning(e.Message);
            UnityEngine.Debug.LogWarning(Conectports);
           
        }
    }
}