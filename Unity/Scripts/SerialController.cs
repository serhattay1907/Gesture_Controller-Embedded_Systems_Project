using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SerialController : MonoBehaviour
{
    [Header("Port Settings")]
    public string portName = "COM3";
    public int baudRate = 9600;

    private SerialPort port;
    private Thread readThread;
    private string lastCommand = "IDLE";
    private bool isRunning = false;
    private readonly object lockObject = new object();

    public string LastCommand
    {
        get
        {
            lock (lockObject)
            {
                return lastCommand;
            }
        }
    }

    void Start()
    {
        try
        {
            port = new SerialPort(portName, baudRate);
            port.ReadTimeout = 100;
            port.Open();
            isRunning = true;

            readThread = new Thread(ReadLoop);
            readThread.IsBackground = true;
            readThread.Start();

            Debug.Log("Serial port opened: " + portName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not open port: " + e.Message);
        }
    }

    void ReadLoop()
    {
        while (isRunning && port != null && port.IsOpen)
        {
            try
            {
                string line = port.ReadLine().Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    lock (lockObject)
                    {
                        lastCommand = line;
                    }
                }
            }
            catch (System.TimeoutException) { }
            catch (System.Exception e)
            {
                Debug.LogWarning("Read error: " + e.Message);
            }
        }
    }

    void OnDestroy()
    {
        isRunning = false;
        readThread?.Join(300);
        if (port != null && port.IsOpen)
        {
            port.Close();
        }
    }
}