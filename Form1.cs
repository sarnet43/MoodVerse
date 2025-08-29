using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoodVerse
{
    public partial class Form1 : Form
    {
        List<string> results;
        public Form1()
        {
            InitializeComponent();
        }

        private void 끝내기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 내역목록보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistory form = Application.OpenForms["FormHistory"] as FormHistory;
            if (form != null)
            {
                form.Activate();
            }
            else
            {
                form = new FormHistory();
                form.Show();
            }
        }

        private void moodVerse정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            string mood = tbMood.Text.Trim();
            string result = GetMoodResponse(mood);
            string verse = result.Split('|')[0];
            string message = result.Split('|')[1];
            tbResult.Text = $"{mood}{Environment.NewLine}"
                                + $"{verse}{Environment.NewLine}" +
                                $"{message}";
            
        }

        private string GetMoodResponse(string mood)
        {
            Random rand = new Random();
            int index = rand.Next(0, results.Count);
            return results[index];
        }
    }
}
