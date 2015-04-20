using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wox.Stash.Settings
{
    /// <summary>
    /// Interaction logic for PluginSettingsControl.xaml
    /// </summary>
    public partial class PluginSettingsControl : UserControl
    {
        public PluginSettingsControl()
        {
            InitializeComponent();
            CloneDestination.Text = PluginSettings.Instance.CloneDestination;
            StashUrl.Text = PluginSettings.Instance.StashUrl;
            ((RadioButton)this.FindName(PluginSettings.Instance.CloneMethod)).IsChecked = true;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                this.CloneDestination.Text = dialog.SelectedPath;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = (RadioButton)sender;
            PluginSettings.Instance.CloneMethod = radio.Name;
            PluginSettings.Instance.Save();
        }

        private void CloneDestination_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            PluginSettings.Instance.CloneDestination = textBox.Text;
            PluginSettings.Instance.Save();
        }

        private void StashUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            PluginSettings.Instance.StashUrl = textBox.Text;
            PluginSettings.Instance.Save();
        }
    }
}
