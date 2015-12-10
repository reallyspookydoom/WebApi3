using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await DoServerSideAsyncWork(button1, label1);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await DoServerSideAsyncWork(button2, label2);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await DoServerSideAsyncWork(button3, label3);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await DoServerSideAsyncWork(button4, label4);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            label5.Text = "Running 10 async calls to server... ";

            await Task.WhenAll(GetServerSideTasks());

            button5.Enabled = true;
            label5.Text += "done.";
        }

        private async Task DoServerSideAsyncWork(int counter, Label lbl)
        {
            lbl.Text += "U" + counter + ", ";

            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync("http://localhost:54360/api/heavy");
            }

            lbl.Text += "D" + counter + ", ";
        }

        private async Task DoServerSideAsyncWork(Button btn, Label lbl)
        {
            btn.Enabled = false;
            lbl.Text = "Calling service...";

            DateTime retval;
            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync("http://localhost:54360/api/heavy");
                retval = JsonConvert.DeserializeObject<DateTime>(response);
            }

            btn.Enabled = true;
            lbl.Text += retval != DateTime.MinValue ? " finished server side at " + retval : " FAILED";
        }
        private IEnumerable<Task> GetServerSideTasks()
        {
            yield return DoServerSideAsyncWork(1, label5);
            yield return DoServerSideAsyncWork(2, label5);
            yield return DoServerSideAsyncWork(3, label5);
            yield return DoServerSideAsyncWork(4, label5);
            yield return DoServerSideAsyncWork(5, label5);
            yield return DoServerSideAsyncWork(6, label5);
            yield return DoServerSideAsyncWork(7, label5);
            yield return DoServerSideAsyncWork(8, label5);
            yield return DoServerSideAsyncWork(9, label5);
            yield return DoServerSideAsyncWork(10, label5);
        }
    }
}