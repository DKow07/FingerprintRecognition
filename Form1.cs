using FingerprintRecognition.ImageOperations;
using FingerprintRecognition.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerprintRecognition
{
    public partial class Form1 : Form
    {

        #region DLL_IMPORTS
        [DllImport("SFM_SDK.dll",
            CharSet = CharSet.Auto,
            EntryPoint = "UF_ReadImage")]
        static extern int UF_ReadImage(IntPtr image);

        [DllImport("SFM_SDK.dll",
            CharSet = CharSet.Auto,
            EntryPoint = "UF_ConvertToBitmap")]
        static extern IntPtr UF_ConvertToBitmap(IntPtr image);

        [DllImport("SFM_SDK.dll",
             CharSet = CharSet.Auto,
             EntryPoint = "UF_SetIdentifyCallback")]
        static extern void UF_SetIdentifyCallback(IdentifyCallback callback);

        [DllImport("SFM_SDK.dll",
             CharSet = CharSet.Auto,
             EntryPoint = "UF_SetEnrollCallback")]
        static extern void UF_SetEnrollCallback(EnrollCallback callback);

        [DllImport("SFM_SDK.dll",
             CharSet = CharSet.Auto,
             EntryPoint = "UF_SetReceiveDataPacketCallback")]
        static extern void UF_SetReceiveDataPacketCallback(DataCallback callback);

        [DllImport("SFM_SDK.dll",
             CharSet = CharSet.Auto,
             EntryPoint = "UF_SetReceiveRawDataCallback")]
        static extern void UF_SetReceiveRawDataCallback(RawDataCallback callback);

        [DllImport("SFM_SDK.dll",
             CharSet = CharSet.Auto,
             EntryPoint = "UF_Enroll")]
        static extern int UF_Enroll(uint userID, int option, ref uint enrollID, ref byte imageQuality);

        [DllImport("SFM_SDK.dll",
             CharSet = CharSet.Auto,
             EntryPoint = "UF_ReadLog")]
        static extern int UF_ReadLog(int startIndex, int count, IntPtr logRecords, ref int readCount);

        [DllImport("SFM_SDK.dll",
         CharSet = CharSet.Auto,
         EntryPoint = "UF_Identify")]
        static extern int UF_Identify(ref int userID, ref byte subID);

        public delegate void IdentifyCallback(byte errorCode);
        public delegate void EnrollCallback(byte errorCode, int enrollMode, int numOfSuccess);
        public delegate void DataCallback(int packetIndex, int numOfPacket);
        public delegate void RawDataCallback(int transferred, int totalSize);

        private IdentifyCallback m_IdentifyCallback;
        private EnrollCallback m_EnrollCallback;
        private IntPtr m_Image;


        private void identifyButton_Click(object sender, System.EventArgs e)
        {
            labelScannInfo.Text = "Place a finger";
            labelScannInfo.Refresh();

            int userID = 0;
            byte subID = 0;

           // toolStripProgressBar1.Value = 0;

            int result = UF_Identify(ref userID, ref subID);

            if (result == 0)
            {
                labelScannInfo.Text = "Scann Succeed: " + userID + "(" + subID + ")";
                labelScannInfo.Refresh();
            }
            else
            {
                labelScannInfo.Text = "Scann Succes - ";
                labelScannInfo.Refresh();
            }

            result = UF_ReadImage(m_Image);

            if (result == 0)
            {
                IntPtr hbitmap = UF_ConvertToBitmap(m_Image);

              //  pcbFingerprintScann.SizeMode = PictureBoxSizeMode.Zoom;
               // pcbFingerprintScann.Image = Image.FromHbitmap(hbitmap);
            }
            else if (result == -1)
            {
                MessageBox.Show("Error while reading image");
            }
        }

        private void enrollButton_Click(object sender, System.EventArgs e)
        {
            labelScannInfo.Text = "Place a finger";
            labelScannInfo.Refresh();

            uint userID = 0;
            int option = 0x79;
            uint enrolledID = 0;
            byte imageQuality = 0;

            //toolStripProgressBar1.Value = 0;

            int result = UF_Enroll(userID, option, ref enrolledID, ref imageQuality);

            if (result == 0)
            {
                labelScannInfo.Text = "Enroll Succeed: " + enrolledID + "(" + imageQuality + ")";
                labelScannInfo.Refresh();
            }
            else
            {
                labelScannInfo.Text = "Enroll Failed";
                labelScannInfo.Refresh();
            }

            result = UF_ReadImage(m_Image);

            if (result == 0)
            {
                IntPtr hbitmap = UF_ConvertToBitmap(m_Image);

                //pcbFingerprintScann.Image = Image.FromHbitmap(hbitmap);
            }

        }

        public void identifyCallback(byte errorCode)
        {
            if (errorCode == 0x62)
            {
                labelScannInfo.Text = "Scan Success";
                labelScannInfo.Refresh();
            }
        }

        public void enrollCallback(byte errorCode, int enrollMode, int numOfSuccess)
        {
            switch (enrollMode)
            {
                case 0x30:
                    break;

                case 0x31:
                case 0x41:
                    if (numOfSuccess == 0)
                    {
                        label3.Text = "Place the finger again";
                        label3.Refresh();
                    }

                    break;

                case 0x32:
                case 0x42:
                    break;
            }
        }

        public void dataCallback(int packetIndex, int numOfPacket)
        {
           // toolStripProgressBar1.Value = (packetIndex + 1) * 100 / numOfPacket;
        }

        public void rawDataCallback(int transferred, int totalSize)
        {
           // toolStripProgressBar1.Value = (transferred + 1) * 100 / totalSize;

        }

        #endregion

        public Form1()
        {
            InitializeComponent();

            InitForms();

            m_Image = Marshal.AllocHGlobal(256 * 1024);
            m_IdentifyCallback = new IdentifyCallback(identifyCallback);
            m_EnrollCallback = new EnrollCallback(enrollCallback);

            UF_SetIdentifyCallback(m_IdentifyCallback);
            UF_SetEnrollCallback(m_EnrollCallback);

            UF_SetReceiveRawDataCallback(new RawDataCallback(rawDataCallback));
        }

        private void InitForms()
        {
            comboBoxBaudrate.SelectedIndex = 0;
            comboBoxPortCom.SelectedIndex = 0;

            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNew.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            bool isBinaryMode = radioButtonBinaryMode.Checked;
            string port = comboBoxPortCom.Text;
            string baudrate = comboBoxBaudrate.Text;

            if (Connection.GetInstance().IsConnectedToScanner == false)
            {
                if (Connection.GetInstance().ConnectToScanner(isBinaryMode, port, baudrate) == -1)
                {
                    MessageBox.Show("Can't connect to scanner");
                }
            }
            else
            {
                Connection.GetInstance().Disconnect();
            }

            HandleConnectionForms(Connection.GetInstance().IsConnectedToScanner);
        }

        private void HandleConnectionForms(bool isConnected)
        {
            panelConnectionStatus.BackColor = isConnected ? Color.Lime : Color.Red;
            buttonConnect.Text = isConnected ? "Disconnect" : "Connect";
            buttonScann.Enabled = isConnected ? true : false;
        }

        private void openFingerprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG|*.jpg|PNG|*.png"; 
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            Bitmap bitmap = (Bitmap)Image.FromFile(path);
            Bitmap newBitmap = ImageUtils.Binarized(bitmap, 100);
            pictureBoxOriginal.Image = newBitmap;
            Bitmap thinBitmap = Thinning.Thin(newBitmap);
            pictureBoxNew.Image = thinBitmap;
        }
    }
}