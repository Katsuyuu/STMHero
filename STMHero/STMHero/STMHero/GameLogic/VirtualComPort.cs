using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace STMHero.GameLogic
{
    public static class VirtualComPort
    {
        static SerialPort serialPort;
        public static STMKeyboardState keyboardStateSTM;

        static public bool isConnected;

        public static void Connect()
        {
            keyboardStateSTM = new STMKeyboardState();
            string[] portNames = SerialPort.GetPortNames();
            Thread readThread = new Thread(Read);

            foreach (string s in portNames)
            {

                serialPort = new SerialPort();
                serialPort.PortName = s;
                serialPort.Open();

                Thread connectThread = new Thread(Find);
                connectThread.Start();
                Thread.Sleep(1000);

                if (connectThread.ThreadState == ThreadState.Stopped)
                    break;
                else
                {
                    serialPort.Close();
                    serialPort.Dispose();
                    connectThread.Abort();
                }
            }
            if (serialPort.IsOpen)
            {
                isConnected = true;
                readThread.Start();
            }
            else
            {
                isConnected = false;
            }
                
        }

        static void Find()
        {
            char[] hi = { 'H' };

            serialPort.Write(hi, 0, 1);

            try
            {
                if (serialPort.ReadChar() == 'I')
                {
                    serialPort.Write(hi, 0, 1);
                    serialPort.ReadChar();
                }
            }
            catch (Exception) { }

        }


        static void Read()
        {
            while (isConnected)
            {
                try
                {
                    
                    if (0x38 == (char)serialPort.ReadChar())
                    {
                        keyboardStateSTM.A = ((char)serialPort.ReadChar() > 0x0F ? true : false);
                        keyboardStateSTM.S = ((char)serialPort.ReadChar() > 0x0F ? true : false);
                        keyboardStateSTM.D = ((char)serialPort.ReadChar() > 0x0F ? true : false);
                        keyboardStateSTM.F = ((char)serialPort.ReadChar() > 0x0F ? true : false);
                    }
                }
                catch (TimeoutException) { }
            }
        }

        public static STMKeyboardState GetState()
        {
            return keyboardStateSTM;
        }
    }
}
