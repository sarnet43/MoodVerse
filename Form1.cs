using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            LoadResults();
        }

        private void LoadResults()
        {
            try
            {
                string filename = "results.csv";
                results = File.ReadAllLines(filename).ToList();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"파일이 없어요. \n{ex.Message}", "파일 없음");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"권한이 없어요. \n{ex.Message}", "권한 오류");
            }
            catch (Exception ex)
            {
                MessageBox.Show($" 알수 없는 오류가 발생했어요: \n{ex.Message}", "알 수 없는 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                form = new FormHistory(this);
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
            tbResult.Text = $"{mood}{Environment.NewLine}{Environment.NewLine}"
                                + $"{verse}{Environment.NewLine}{Environment.NewLine}" +
                                $"{message}";

            saveHistory($"{mood}|{result}");
        }

        private void saveHistory(string history)
        {
            try
            {
                string filename = "history.csv";
                File.AppendAllText(filename, history + Environment.NewLine);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"권한 없음 오류 발생! \n{ex.Message}", "권한 오류");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"알 수 없는 오류 발생! \n{ex.Message}", "알 수 없는 오류");
            }
        }

        private string GetMoodResponse(string mood)
        {
            Random rand = new Random();
            int index = rand.Next(0, results.Count);
            return results[index];
        }

        public void LoadHistory(string history)
        {
            string mood = history.Split('|')[0].Split(' ')[0];
            tbMood.Text = mood;

            string verse = history.Split('|')[1];
            string message = history.Split('|')[2];

            tbResult.Text = $"{mood}{Environment.NewLine}{Environment.NewLine}"
                                + $"{verse}{Environment.NewLine} {Environment.NewLine}" +
                                $"{message}";

        }
    }
}
