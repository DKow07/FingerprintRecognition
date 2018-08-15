using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerprintRecognition.Utils
{
    public class ConnectionUtils
    {

        [DllImport("SFM_SDK.dll",
           CharSet = CharSet.Ansi,
           EntryPoint = "UF_InitCommPort")]
        static extern int UF_InitCommPort(string commPort, int baudrate, int asciiMode);

        [DllImport("SFM_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "UF_CloseCommPort")]
        static extern int UF_CloseCommPort();

        private static ConnectionUtils instance;

        public bool IsConnectedToScanner { get; set; }
     
        private ConnectionUtils() 
        {
            IsConnectedToScanner = false;
        }

        public static ConnectionUtils GetInstance()
        {
            if(instance == null)
            {
                instance = new ConnectionUtils();
            }
            return instance;
        }

        public int ConnectToScanner(bool isBinaryMode, string portCom, string baudrateString)
        {
            int isASCIIMode = Convert.ToInt32(!isBinaryMode);
            int baudrate = Convert.ToInt32(baudrateString);
            Debug.Print("Connection: " + portCom + " " + baudrateString + " " + isBinaryMode);
            int result = UF_InitCommPort(portCom, baudrate, isASCIIMode);
            Debug.Print("Connection status = " + result);
            if (result != -1)
            {
                IsConnectedToScanner = true;
            }
            else
            {
                Debug.Print("Can't connect to scanner", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public void Disconnect()
        {
            int result = UF_CloseCommPort();
            if (result != -1)
            {
                Debug.Print("Disconnected succesfull");
                IsConnectedToScanner = false;
            }
        }

    }
}
