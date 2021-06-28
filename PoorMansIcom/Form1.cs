using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PoorMansIcom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ComPortNumber1.Items.AddRange(SerialPort.GetPortNames());

            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.WorkerReportsProgress = true;
            BackgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            BackgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;

            this.Text = Version.NameAndNumber;

        }
        public class RadioValue
        {
            public static int StatusRX;

            public byte[] SerialPortDataRX { get; set; }
            public static byte[] SerialPortData = new byte[0];

        }
        public static SerialPort SerialPort1 = new SerialPort();
        public static BackgroundWorker BackgroundWorker1 = new BackgroundWorker();


        private void DebugLogTextInsert(string program, string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string>(DebugLogTextInsert), new object[] { program, text });
                return;
            }

            textBox1.Text = textBox1.Text.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + program + " : " + text + "\r\n");
            DebugLogRemoveLines(50);
        }

        private void DebugLogRemoveLines(int rows)
        {

            if (textBox1.Lines.Length > rows)
            {
                textBox1.Lines = textBox1.Lines.Take(rows).ToArray();
            }
        }
        private void ComPortConnect1_Click(object sender, EventArgs e)
        {
            try
            {
                //      SaveSettings();

                SerialPort1.BaudRate = int.Parse(BaudComboBox.Text);
                SerialPort1.PortName = ComPortNumber1.Text;
                SerialPort1.DtrEnable = true;
                SerialPort1.Open();
                SerialPort1.DiscardInBuffer();
                SerialPort1.DiscardOutBuffer();

                BackgroundWorker1.RunWorkerAsync();

                ComPortConnect1.Enabled = false;
                ComPortDisconnect1.Enabled = true;
                
                ComPortNumber1.Enabled = false;
                BaudComboBox.Enabled = false;


            }
            catch (Exception ex)
            {
                if (SerialPort1.IsOpen) SerialPort1.Close();

                ComPortConnect1.Enabled = true;
                ComPortDisconnect1.Enabled = false;

                ComPortNumber1.Enabled = true;
                BaudComboBox.Enabled = true;

                Debug.WriteLine("ComPortConnect1_Click: " + ex.Message);
                MessageBox.Show(ex.Message, "ComPortError ");
            }

        }
        private void ComPortDisconnect1_Click(object sender, EventArgs e)
        {
            if (BackgroundWorker1.IsBusy)
            {
                BackgroundWorker1.CancelAsync();
                BackgroundWorker1.Dispose();

                if (BackgroundWorker1.IsBusy)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                }

            }

            SerialPort1.DtrEnable = false;
            SerialPort1.Close();
            ComPortConnect1.Enabled = true;
            ComPortDisconnect1.Enabled = false;

            ComPortNumber1.Enabled = true;
            BaudComboBox.Enabled = true;

        }
        private static void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var buffer = new byte[4096];

            while (!BackgroundWorker1.CancellationPending)
            {
                try
                {
                    if (SerialPort1.IsOpen)
                    {
                        var c = SerialPort1.Read(buffer, 0, buffer.Length);
                        BackgroundWorker1.ReportProgress(0, new RadioValue() { SerialPortDataRX = buffer.Take(c).ToArray() });
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("BackgroundWorker1_DoWork: " + ex.Message);
                }
            }

        }
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var sp = e.UserState as RadioValue;
            int startByte = 0;
            int recivingStatus = 0;

            for (int i = 0; i < sp.SerialPortDataRX.Length; i++)
            {
                if (recivingStatus == 0)
                {
                    startByte = i;

                    if (sp.SerialPortDataRX[i].ToString("x2").Equals("fe"))
                    {
                        recivingStatus = 1;
                    }
                }
                else
                {
                    if (SerialPort1.IsOpen && sp.SerialPortDataRX[i].ToString("x2").Equals("fd"))
                    {
                        recivingStatus = 0;



                        string TransceiverID = BitConverter.ToString(sp.SerialPortDataRX, (startByte + 2), 1).Replace("-", "");
                        string ControllerID = BitConverter.ToString(sp.SerialPortDataRX, (startByte + 3), 1).Replace("-", "");
                        string DataRX = BitConverter.ToString(sp.SerialPortDataRX, (startByte + 4), i - (startByte + 4)).Replace("-", "");
                        string Respond = getRadioAnswer(@"IcomProtocoll.xml", DataRX);
                        string DataTX = "FEFE" + ControllerID + TransceiverID + Respond + "FD";


                        if (checkBoxReadPortLog.Checked && Respond != "FA")
                        {
                            DebugLogTextInsert("Read", BitConverter.ToString(sp.SerialPortDataRX, startByte, i - startByte + 1).Replace("-", " "));
                        }
                        else if ((checkBoxReadPortLog.Checked ||checkBoxErrorLog.Checked) && Respond == "FA")
                        {
                            DebugLogTextInsert("Error", BitConverter.ToString(sp.SerialPortDataRX, startByte, i - startByte + 1).Replace("-", " "));
                        }


                        //  if (DataTX.Length > 2)
                        //  {
                        int NumberOfChars = DataTX.Length;
                            byte[] DataTXbytes = new byte[NumberOfChars / 2];
                            for (int DataTXHexNo = 0; DataTXHexNo < NumberOfChars; DataTXHexNo += 2)
                                DataTXbytes[DataTXHexNo / 2] = Convert.ToByte(DataTX.Substring(DataTXHexNo, 2).ToLower(), 16);
                            if (checkBoxSendPortLog.Checked)
                            {
                                DebugLogTextInsert("Send", BitConverter.ToString(DataTXbytes, 0, DataTXbytes.Length).Replace("-", " "));
                            }

                            SerialPort1.Write(sp.SerialPortDataRX,0, sp.SerialPortDataRX.Length);
                            SerialPort1.Write(DataTXbytes, 0, DataTXbytes.Length);
                       // }
                    }
                }
            }
            
        }

        public string getRadioAnswer(string xmlPath, string DataRX)
        {
            string returnValue = "";
            string returnValueOk = "FB";
            string returnValueNotOk = "FA";
            string subCommands = "No";

            string command = DataRX.Substring(0, 2);
            string orgCommand = command;
            string subCommand = "";
            string setRadioValue = "";
            string FixedSize = "0";
            XElement getRadioValue;





            XDocument doc = XDocument.Load(xmlPath);
            XElement xmlCommand;

            command = DataRX.Substring(0, 2);
            xmlCommand = doc.Element("hamradio")
                            .Elements("Command").Where(node => node.Attribute("code").Value == command)
                            .FirstOrDefault();

            #region Get ReadFrom & subCommands
            /* Get read from */
            if (xmlCommand == null && command != null)
            {
                doc.Element("hamradio").Add(new XElement("Command", new XAttribute("code", command)
                                                                  , new XAttribute("name", "NEW")
                                                                  , new XAttribute("subCommands", "No")
                                                                  , new XAttribute("ReadFrom", command)
                                                                  , new XAttribute("FixedSize","0")
                                                                  ));
                doc.Save(xmlPath);
            }
            else
            {
                if (xmlCommand.Attribute("ReadFrom") != null && xmlCommand.Attribute("ReadFrom").Value != null)
                    {command = xmlCommand.Attribute("ReadFrom").Value;}
            }

            /* update doc and xmlCommand */
            doc = XDocument.Load(xmlPath);
            xmlCommand = doc.Element("hamradio")
                            .Elements("Command").Where(node => node.Attribute("code").Value == command)
                            .FirstOrDefault();

            /* Get subCommand */
            if (xmlCommand == null && command != null)
            {
                doc.Element("hamradio").Add(new XElement("Command", new XAttribute("code", command)
                                                                  , new XAttribute("name", "NEW")
                                                                  , new XAttribute("subCommands", "No")
                                                                  , new XAttribute("ReadFrom", command)
                                                                  , new XAttribute("FixedSize", "0")
                                                                  ));
                doc.Save(xmlPath);
            }
            else
            {
                if (xmlCommand.Attribute("subCommands") != null && xmlCommand.Attribute("subCommands").Value != null)
                { subCommands = xmlCommand.Attribute("subCommands").Value; }

                if (xmlCommand.Attribute("FixedSize") != null && xmlCommand.Attribute("FixedSize").Value != null)
                { FixedSize = xmlCommand.Attribute("FixedSize").Value; }
            }
            #endregion
            int FixedSizeInt;

            if (! Int32.TryParse(FixedSize, out FixedSizeInt))
            {
                FixedSizeInt = 0;

            }
            #region Get type of data
            /* get type of data */
            if (DataRX.Length > 2 && subCommands == "No")
            {
                setRadioValue = DataRX.Substring(2, DataRX.Length - 2).PadRight(FixedSizeInt, '0');
            }
            else if (DataRX.Length > 4 && subCommands != "No")
            {
                int subCommandChar = 2;

                List<String> SubstringSixChar = new List<String>();

                SubstringSixChar.Add("1A05");

                if (SubstringSixChar.Contains(DataRX.Substring(0,4)))
                {
                    subCommandChar = 6;
                }
                else
                {
                    subCommandChar = 2;
                }

                if (DataRX.Length == 6 && subCommandChar == 6)
                {
                    subCommand = DataRX.Substring(2, 2) + "00" + DataRX.Substring(4, 2);
                }
                else
                {
                    subCommand = DataRX.Substring(2, subCommandChar);
                    setRadioValue = DataRX.Substring(2 + subCommandChar, DataRX.Length - (2 + subCommandChar)).PadRight(FixedSizeInt, '0');
                }

                
                
            }
            else if (DataRX.Length > 2 && subCommands != "No")
            {
                subCommand = DataRX.Substring(2, 2);
            }
            else if (subCommands != "No")
            {
                subCommand = "00";
            }
            #endregion



            if (command != "" && subCommands == "No")
            {
                getRadioValue = doc.Element("hamradio")
                                    .Elements("Command").Where(node => node.Attribute("code").Value == command)
                                    .Elements("RadioValue")
                                    .FirstOrDefault();

                if (getRadioValue == null && setRadioValue != "")
                {
                    doc.Element("hamradio")
                       .Elements("Command").Where(node => node.Attribute("code").Value == command)
                       .FirstOrDefault().Add(new XElement("RadioValue", new XAttribute("code", setRadioValue)));
                    doc.Save(xmlPath);

                    returnValue = returnValueOk;
                }
                else if (getRadioValue != null && setRadioValue != "")
                {
                    if (getRadioValue.Attribute("code").Value != setRadioValue)
                    {
                        getRadioValue.Attribute("code").Value = setRadioValue;
                        doc.Save(xmlPath);
                    }
 
                    returnValue = returnValueOk;
                }
                else if (setRadioValue== "" && getRadioValue != null )
                {
                    returnValue = orgCommand + getRadioValue.Attribute("code").Value;
                }
                else
                {
                    returnValue = returnValueNotOk;
                }

            }
            else if (command != "" && subCommand != "")
            {

                getRadioValue = doc.Element("hamradio")
                                    .Elements("Command").Where(node => node.Attribute("code").Value == command)
                                    .Elements("SubCommand").Where(node => node.Attribute("code").Value == subCommand)
                                    .FirstOrDefault();

                if (getRadioValue == null)
                {
                    doc.Element("hamradio")
                       .Elements("Command").Where(node => node.Attribute("code").Value == command)
                       .FirstOrDefault().Add(new XElement("SubCommand"
                                            , new XAttribute("code", subCommand)
                                            , new XAttribute("PreFix", "No")
                                            ));
                    doc.Save(xmlPath);
                } else if (getRadioValue.Attribute("PreFix") != null && getRadioValue.Attribute("PreFix").Value != "No" && setRadioValue != "")
                {
                    setRadioValue = getRadioValue.Attribute("PreFix").Value + setRadioValue;
                    
                }

                getRadioValue = doc.Element("hamradio")
                                    .Elements("Command").Where(node => node.Attribute("code").Value == command)
                                    .Elements("SubCommand").Where(node => node.Attribute("code").Value == subCommand)
                                    .Elements("RadioValue")
                                    .FirstOrDefault();

                if (getRadioValue == null && setRadioValue != "")
                {
                    doc.Element("hamradio")
                       .Elements("Command").Where(node => node.Attribute("code").Value == command)
                       .Elements("SubCommand").Where(node => node.Attribute("code").Value == subCommand)
                       .FirstOrDefault().Add(new XElement("RadioValue", new XAttribute("code", setRadioValue)));
                    doc.Save(xmlPath);

                    returnValue = returnValueOk;
                }
                else if (getRadioValue != null && setRadioValue != "")
                {
                    if (getRadioValue.Attribute("code").Value != setRadioValue)
                    {
                        getRadioValue.Attribute("code").Value = setRadioValue;
                        doc.Save(xmlPath);
                    }

                    returnValue = returnValueOk;
                }
                else if (setRadioValue == "" && getRadioValue != null)
                {
                    returnValue = orgCommand + getRadioValue.Attribute("code").Value;

                }
                else
                {
                    returnValue = returnValueNotOk;
                }

            }
            else
            {
                returnValue = returnValueNotOk;
            }

            return returnValue;
        }
 
        private void TrashButton_Click(object sender, EventArgs e)
        {
            DebugLogRemoveLines(0);
        }
    }
}
