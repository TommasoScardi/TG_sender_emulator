﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TG_sender_emulator.Models;

namespace TG_sender_emulator
{
    public partial class FormBotData : Form
    {
        BindingList<BotConfig> _botsConfig;
        public BotConfig SelectedConfig { get; set; }

        public FormBotData(BindingList<BotConfig> botsConfig, BotConfig selectedConfig)
        {
            _botsConfig = botsConfig;
            SelectedConfig = selectedConfig;
            InitializeComponent();
        }

        private async void BotDataForm_Load(object sender, EventArgs e)
        {
            cbo_BotsConfig.DataSource = _botsConfig;
            cbo_BotsConfig.DisplayMember = "ConfigName";
            if (SelectedConfig != null)
            {
                cbo_BotsConfig.SelectedItem = SelectedConfig;
            }
        }

        private void btn_NewConfig_Click(object sender, EventArgs e)
        {
            cbo_BotsConfig.SelectedIndex = -1;
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (cbo_BotsConfig.SelectedIndex == -1)
            {
                if (txt_ConfigName.Text.Length > 0 && txt_EndpointsURL.Text.Length > 0)
                {
                    BotConfig newConfig = new BotConfig();
                    newConfig.ConfigName = txt_ConfigName.Text;
                    newConfig.Url = txt_EndpointsURL.Text;
                    newConfig.UrlWebhookEndpoint = txt_WebhookEP.Text;
                    newConfig.UrlWebhookSet = txt_WebhookSetEP.Text;
                    newConfig.UrlCronEndpoint = txt_CronEP.Text;
                    _botsConfig.Add(newConfig);
                    SelectedConfig = newConfig;
                }
                else
                {
                    MessageBox.Show("Campi vuoti");
                }
            }
            else
            {
                SelectedConfig = cbo_BotsConfig.SelectedItem as BotConfig;

                if (SelectedConfig.ConfigName != txt_ConfigName.Text
                    || SelectedConfig.Url != txt_EndpointsURL.Text
                    || SelectedConfig.UrlWebhookEndpoint != txt_WebhookEP.Text
                    || SelectedConfig.UrlCronEndpoint != txt_CronEP.Text)
                {
                    SelectedConfig.ConfigName = txt_ConfigName.Text;
                    SelectedConfig.Url = txt_EndpointsURL.Text;
                    SelectedConfig.UrlWebhookEndpoint = txt_WebhookEP.Text;
                    SelectedConfig.UrlWebhookSet = txt_WebhookSetEP.Text;
                    SelectedConfig.UrlCronEndpoint = txt_CronEP.Text;

                    int configIndex = _botsConfig.IndexOf(SelectedConfig);
                    _botsConfig[configIndex] = SelectedConfig;
                }
            }
            this.Close();
        }

        private void cbo_BotsConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_BotsConfig.SelectedIndex >= 0)
            {
                SelectedConfig = cbo_BotsConfig.SelectedItem as BotConfig;
                txt_ConfigName.Text = SelectedConfig.ConfigName;
                txt_EndpointsURL.Text = SelectedConfig.Url;
                txt_WebhookEP.Text = SelectedConfig.UrlWebhookEndpoint;
                txt_WebhookSetEP.Text = SelectedConfig.UrlWebhookSet;
                txt_CronEP.Text = SelectedConfig.UrlCronEndpoint;
            }
            else
            {
                txt_ConfigName.Text = null;
                txt_EndpointsURL.Text = null;
                txt_WebhookEP.Text = null;
                txt_WebhookSetEP.Text = null;
                txt_CronEP.Text = null;
            }
        }
    }
}