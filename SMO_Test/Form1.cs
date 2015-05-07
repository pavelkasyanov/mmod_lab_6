using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Q_SchemeModuleProject;

namespace SMO_Test
{
    public partial class Form1 : Form
    {
        private QScheme _qScheme;
        private int _requestCount;
        private double _dt;
        private readonly CalcHelper _calcHelper = new CalcHelper();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _requestCount = int.Parse(_requestCountTextBox.Text);
            _dt = double.Parse(_steptextBox.Text);

            _qScheme = new QScheme(_dt, _requestCount);

            _qScheme.Start();

            _logRichTextBox.Clear();

            _logRichTextBox.AppendText(string.Format("simulate time: {0:0.##}\n", _qScheme.Time));
            _logRichTextBox.AppendText( string.Format("gen: {0}\n",_qScheme.RequestGenerate) );
            _logRichTextBox.AppendText(String.Format("check: {0}\n discarded: {1}\n", _qScheme.CheckRequests.Count,
                _qScheme.DiscardedRequests.Count));
            _logRichTextBox.AppendText(string.Format("last: {0:0.##}%\n",
                (double)_qScheme.DiscardedRequests.Count / (double)_qScheme.RequestGenerate * 100.0));

            MessageBox.Show("Done!", "result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Statistic()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            int intervalCount = 0;
            int.TryParse(_intervalCountTextBox.Text, out intervalCount);

            var arr = _calcHelper.CalcUniformity2(_qScheme.RequestsCheckList, intervalCount, _qScheme.Time);

            for (int i = 0; i < intervalCount; i++)
            {
                chart1.Series[0].Points.AddY(arr[i]);
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(i, i + 2, string.Format("{0}", i));
            }

            _logRichTextBox.AppendText("=====  statistic  =====\n");
            
            AverageServiceTime();
            
            LoadingChannels();
        }

        private void LoadingChannels()
        {
            int index = 0;
            //Phase1
            _logRichTextBox.AppendText("======= Phase 1 ======\n");
            _logRichTextBox.AppendText(String.Format("phase1 check: {0}\n", _qScheme.Phase1.RequestCheck));
            foreach (var item in _qScheme.Phase1.SChannels)
            {
                index++;
                _logRichTextBox.AppendText(String.Format("phase1 chanel#{0}: {1}({2:0.##}%) dt:({3:0.##})\n",
                    index, item.CheckedRequests, 
                    (int)(double)item.CheckedRequests / (double)_qScheme.Phase1.RequestCheck * 100.0,
                    (double)item.AllServiceTime / (double)item.CheckedRequests));
            }

            //Phase2
            index = 0;
            _logRichTextBox.AppendText("======= Phase 2 ======\n");
            _logRichTextBox.AppendText(String.Format("phase2 check: {0}\n", _qScheme.Phase2.RequestCheck));
            foreach (var item in _qScheme.Phase2.SChannels)
            {
                index++;
                _logRichTextBox.AppendText(String.Format("phase2 chanel#{0}: {1}({2:0.##}%) dt:({3:0.##})\n",
                    index, item.CheckedRequests,
                    (int)(double)item.CheckedRequests / (double)_qScheme.Phase2.RequestCheck * 100.0,
                    (double)item.AllServiceTime / (double)item.CheckedRequests));
            }

            //Phase3
            index = 0;
            _logRichTextBox.AppendText("======= Phase 3 ======\n");
            _logRichTextBox.AppendText(String.Format("phase3 check: {0}\n", _qScheme.Phase3.RequestCheck));
            foreach (var item in _qScheme.Phase3.SChannels)
            {
                index++;
                _logRichTextBox.AppendText(String.Format("phase3 chanel#{0}: {1}({2:0.##}%) dt:({3:0.##})\n",
                    index, item.CheckedRequests,
                    (int)(double)item.CheckedRequests / (double)_qScheme.Phase3.RequestCheck * 100.0,
                    (double)item.AllServiceTime / (double)item.CheckedRequests));
            }

            //Phase4
            index = 0;
            _logRichTextBox.AppendText("======= Phase 4 ======\n");
            _logRichTextBox.AppendText(String.Format("phase4 check: {0}\n", _qScheme.Phase4.RequestCheck));
            foreach (var item in _qScheme.Phase4.SChannels)
            {
                index++;
                _logRichTextBox.AppendText(String.Format("phase4 chanel#{0}: {1}({2:0.##}%) dt:({3:0.##})\n",
                    index, item.CheckedRequests,
                    (int)(double)item.CheckedRequests / (double)_qScheme.Phase4.RequestCheck * 100.0,
                    (double)item.AllServiceTime / (double)item.CheckedRequests));
            }

            //Phase5
            index = 0;
            _logRichTextBox.AppendText("======= Phase 5 ======\n");
            _logRichTextBox.AppendText(String.Format("phase5 check: {0}\n", _qScheme.Phase5.RequestCheck));
            foreach (var item in _qScheme.Phase5.SChannels)
            {
                index++;
                _logRichTextBox.AppendText(String.Format("phase5 chanel#{0}: {1}({2:0.##}%) dt:({3:0.##})\n",
                    index, item.CheckedRequests,
                    (int)(double)item.CheckedRequests / (double)_qScheme.Phase5.RequestCheck * 100.0,
                    (double)item.AllServiceTime / (double)item.CheckedRequests));
            }
        }

        private double AverageServiceTime()
        {
            //the average time spent in the application
            double sum = 0.0;
            double dt = 0.0;
            foreach (var item in _qScheme.RequestsCheckList)
            {
                sum += item.ServiceTime;
                dt += (item.EndTime - item.StartTime);
            }
            
            sum = sum /_qScheme.RequestsCheckList.Count;
            dt = dt/_qScheme.RequestsCheckList.Count;

            _logRichTextBox.AppendText(String.Format("average t.: {0:0.##}\n", sum));
            _logRichTextBox.AppendText(String.Format("average t. spent: {0:0.##}\n", dt));

            return sum;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Statistic();
            MessageBox.Show("Done!", "result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
