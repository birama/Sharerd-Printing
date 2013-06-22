using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using general;
using server;


namespace ClientGUI
{
    public partial class FrmClient : Form
    {
        
        public FrmClient()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (terminal != null)
            {
                MessageBox.Show("Client is already connected!");
                return;
            }



            try
            {
                remoteProxy = m_Terminal.Connect<IRemotingDistributableObject>(
                    "localhost", 1234, Constants.remoteableObjectUri, Constants.RemotingChannelName);

                try
                {
                    remoteProxy.TestConnection();
                }
                catch
                {
                    terminal.Disconnect();

                    throw;
                }

                

                ShowMessage("Connection Established!");
            }
            catch (TargetInvocationException ex)
            {
                MessageBox.Show("Connection cannot be established!\n" + ex.InnerException);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection cannot be established!\n" + ex);
            }
        }

        private void MessageRecivedHandler(string message)
        {
            ShowMessage(message);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (terminal == null)
            {
                MessageBox.Show("You must connect first!");
                return;
            }

            string mes = textBox1.Text;

            try
            {
                Thread[] threads = new Thread[1];
                for (int i = 0; i < 1; i++)
                {
                    threads[i] = new Thread(sendMessageCallback);
                }

                for (int i = 0; i < 1; i++)
                {
                    threads[i].Start(mes);
                }

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot be sent!\n" + ex);
            }
        }

        private void sendMessageCallback(object obj)
        {
            remoteProxy.SendMessage((string)obj);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
            ShowMessage("Disconnected");
        }

        private void FrmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();

        }

        private void Disconnect()
        {
            if (terminal == null)
            {
                return;
            }

            try
            {
            

                terminal.Disconnect();

                m_Terminal = null;
            }
            catch (Exception ex)
            {
                ShowMessage("Disconnection Failed!\n" + ex);
            }

        }


        private void ShowMessage(string message)
        {
            if (InvokeRequired)
            {
               
                BeginInvoke((ThreadStart)delegate
                {
                    ShowMessage(message);
                });

                return;
            }

            listBox1.Items.Add(message);
            Refresh();
        }
    }
}
