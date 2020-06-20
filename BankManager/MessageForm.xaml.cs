using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BankManager
{
    /// <summary>
    /// Interaction logic for MessageForm.xaml
    /// </summary>
    public partial class MessageForm : Window
    {
        int Tick = 0;
        public MessageForm(bool mark)
        {
            InitializeComponent();
            if(mark)
            {
                MarkImage.Source = new ImageSourceConverter().ConvertFromString(@"C:\Users\Yoga\Desktop\ТРПО\Проект\BankManager\BankManager\Res\Images\confirm.png") as ImageSource;
                MessageBlock.Text = "Operation confirmed!";
            }
            else
            {
                MarkImage.Source = new ImageSourceConverter().ConvertFromString(@"C:\Users\Yoga\Desktop\ТРПО\Проект\BankManager\BankManager\Res\Images\denied.png") as ImageSource;
                MessageBlock.Text = "Operation failed!";
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(DispathcerTick);
            timer.Start();
        }
        private void DispathcerTick(object sender, EventArgs e)
        {
            if(Tick == 1)
                this.Close();
            Tick++;
        }
    }
}
