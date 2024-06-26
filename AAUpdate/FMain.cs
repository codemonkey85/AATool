﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AAUpdate
{
    public partial class FMain : Form
    {
        private void SetBar(MinecraftBar bar, int percent) => 
            this.background.ReportProgress(0, (bar, percent));
        private void SetLabel(Label label, string text)    =>
            this.background.ReportProgress(0, (label, text));

        private Updater updater;

        public FMain(Updater updater)
        {
            this.InitializeComponent();
            this.updater = updater;
            updater.ProgressChanged += this.OnProgressChanged;
            updater.StatusChanged   += this.OnStatusChanged;
            this.background.RunWorkerAsync();
        }

        private void OnStatusChanged(int key, string value)
        {
            switch (key)
            {
                case Updater.STATUS_1:
                    this.SetLabel(this.statusGeneric, value);
                    break;
                case Updater.STATUS_2:
                    this.SetLabel(this.statusSpecific, value);
                    break;
                case Updater.STATUS_3:
                    this.SetLabel(this.statusFile, value);
                    break;
            }
        }

        private void OnProgressChanged(int key, (int numerator, int denominator) value)
        {
            MinecraftBar bar = null;
            Label labelTotal = null, labelPercent = null;
            if (key is Updater.PROGRESS_1)
            {
                bar          = this.barOverall;
                labelTotal   = this.totalOverall;
                labelPercent = this.percentOverall;
            }
            else if (key is Updater.PROGRESS_2)
            {
                bar          = this.barCurrent;
                labelTotal   = this.totalCurrent;
                labelPercent = this.percentCurrent;
            }

            //update label text
            int percent = (int)Math.Round(((double)value.numerator / value.denominator) * 100);
            if (bar.Value == 0 && percent == 100 || bar.Value == 100 && percent == 100)
                this.SetLabel(labelTotal, "");
            else
                this.SetLabel(labelTotal, value.numerator + " / " + value.denominator);
            this.SetLabel(labelPercent, percent + "%");

            //update progress bar fill
            this.SetBar(bar, percent);
        }

        private void UpdateProgress()
        { 
            
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            this.updater.TryUpdate();
        }

        private void OnReportProgress(object sender, ProgressChangedEventArgs e)
        {
            //update ui thread
            if (e.UserState == null)
                return;
            if (e.UserState.GetType() == typeof(ValueTuple<MinecraftBar, int>))
            {
                var state = (ValueTuple<MinecraftBar, int>)e.UserState;
                state.Item1.SetValue(state.Item2);
            }
            else if (e.UserState.GetType() == typeof(ValueTuple<Label, string>))
            {
                var state = (ValueTuple<Label, string>)e.UserState;
                state.Item1.Text = state.Item2;
            }
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (updater.ReturnWhenDone)
                Close();
        }
    }
}
