using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BankManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> PageButtons;
        List<Grid> AppGrids;
        MediaPlayer player1 = new MediaPlayer();
        string PageState = "Регистрация";
        bool IsLogged = false;
        SolidColorBrush lightColorBrush;
        double TickCounter = 1;
        Client LoggedClient = new Client();

        public MainWindow()
        {
            InitializeComponent();
            PageButtons = new List<Button>() { RegPageButton, LogPageButton, DepositPageButton, ObtainPageButton, ProfilePageButton };
            AppGrids = new List<Grid>() { RegGrid, EnterGrid, DepositGrid, ObtainGrid, ProfileGrid};
            RegPageButton.Margin = new Thickness(50, 15, -35, 15);
            Color lightColor = (Color)ColorConverter.ConvertFromString("#FF707070");
            lightColorBrush = new SolidColorBrush(lightColor);
            foreach (var grid in AppGrids)
                foreach (var element in grid.Children)
                    if (element.GetType().Equals(typeof(TextBox)))
                        (element as TextBox).Foreground = lightColorBrush;
                    else if (element.GetType().Equals(typeof(PasswordBox)))
                        (element as PasswordBox).Foreground = lightColorBrush;
            KeyText_Reg.Text = RequestSender.Execute(@"https://localhost:44343/Home/GetNextKey", "").ToString();
        }
       
        private void CloseImage_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void MinimizeImage_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RegPageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Button RButton = sender as Button;
            if(RButton.Margin.Left != 50)
            {
                PageButtons.ForEach(q => q.Margin = new Thickness(15, 15, 15, 15));
                RButton.Margin = new Thickness(50, 15, -35, 15);
                PageState = RButton.Content.ToString();
                TextPrinter(StateLabel, RButton.Content.ToString());
                switch (PageState)
                {
                    case "Регистрация":
                        AppGrids.ForEach(q => q.Visibility = Visibility.Hidden);
                        RegGrid.Visibility = Visibility.Visible;
                        break;
                    case "Вход":
                        AppGrids.ForEach(q => q.Visibility = Visibility.Hidden);
                        EnterGrid.Visibility = Visibility.Visible;
                        break;
                    case "Внесение средств":
                        AppGrids.ForEach(q => q.Visibility = Visibility.Hidden);
                        DepositGrid.Visibility = Visibility.Visible;
                        break;
                    case "Вывод средств":
                        AppGrids.ForEach(q => q.Visibility = Visibility.Hidden);
                        ObtainGrid.Visibility = Visibility.Visible;
                        break;
                    case "Профиль":
                        AppGrids.ForEach(q => q.Visibility = Visibility.Hidden);
                        ProfileGrid.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            string info = string.Empty;
            using (ClientContext database = new ClientContext())
            {
                int res;
                if (int.TryParse(KeyText_Reg.Text, out res))
                {
                    if (Convert.ToBoolean(RequestSender.Execute(@"https://localhost:44343/Home/SignUp", $"FirstName={FirstNameText_Reg.Text}&SecondName={SecondNameText_Reg.Text}&ThirdName={ThirdNameText_Reg.Text}&Key={int.Parse(KeyText_Reg.Text)}&Password={PassText_Reg.Password}")))
                        new MessageForm(true);
                    else
                        new MessageForm(false);
                }
            }
        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            using (ClientContext database = new ClientContext())
            {
                var Clients = database.Clients.Where(q => q.FirstName.Equals(FirstNameText_Log.Text) & q.SecondName.Equals(SecondNameText_Log.Text) & q.ThirdName.Equals(ThirdNameText_Log.Text) &
                    q.Key.Equals(int.Parse(KeyText_Log.Text)) & q.Password.Equals(PassText_Log.Password));
                string response = RequestSender.Execute(@"https://localhost:44343/Home/LogIn", $"FirstName={FirstNameText_Log.Text}&SecondName={SecondNameText_Log.Text}&ThirdName={ThirdNameText_Log.Text}&Key={int.Parse(KeyText_Log.Text)}&Password={PassText_Log.Password}").ToString();
                bool IsLogged = bool.Parse(response.Split(new char[] { ';' })[0]);
                if (IsLogged)
                {
                    string AccountMoney = response.Split(new char[] { ';' })[1];
                    foreach (var q in PageButtons)
                        q.IsEnabled = true;
                    LoggedClient.FirstName = FirstNameText_Log.Text;
                    LoggedClient.SecondName = SecondNameText_Log.Text;
                    LoggedClient.ThirdName = ThirdNameText_Log.Text;
                    LoggedClient.Key = int.Parse(KeyText_Log.Text);
                    LoggedClient.Money = int.Parse(AccountMoney);
                    LoggedClient.Password = PassText_Log.Password;
                    SmallName.Text = $"{LoggedClient.FirstName} {LoggedClient.SecondName}";
                    SmallestName.Text = LoggedClient.ThirdName;
                    FirstNameText_Profile.Text = LoggedClient.FirstName;
                    SecondNameText_Profile.Text = LoggedClient.SecondName;
                    ThirdNameText_Profile.Text = SmallestName.Text;
                    KeyText_Profile.Text = LoggedClient.Key.ToString();
                    MoneyText_Profile.Text = LoggedClient.Money.ToString();
                    new MessageForm(true).Show();
                }
                else
                    new MessageForm(false).Show();
            }
        }

        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameText_Deposit.Text.Equals(LoggedClient.FirstName) & SecondNameText_Deposit.Text.Equals(LoggedClient.SecondName) & KeyText_Deposit.Text.Equals(LoggedClient.Key.ToString()) & PassText_Deposit.Password.Equals(LoggedClient.Password))
            {
                if (Convert.ToBoolean(RequestSender.Execute(@"https://localhost:44343/Home/DepositMoney", $"FirstName={LoggedClient.FirstName}&SecondName={LoggedClient.SecondName}&ThirdName={LoggedClient.ThirdName}&Key={LoggedClient.Key}&Password={LoggedClient.Password}&Money={MoneyText_Deposit.Text}")))
                {
                    new MessageForm(true).Show();
                    LoggedClient.Money += int.Parse(MoneyText_Deposit.Text);
                    MoneyText_Profile.Text = LoggedClient.Money.ToString();
                }
                else
                {
                    new MessageForm(false).Show();
                }
            }
        }

        private void ObtainButton_Click(object sender, RoutedEventArgs e)
        {
            using (ClientContext database = new ClientContext())
            {
                if (FirstNameText_Deposit.Text.Equals(LoggedClient.FirstName) & SecondNameText_Deposit.Text.Equals(LoggedClient.SecondName) & KeyText_Deposit.Text.Equals(LoggedClient.Key.ToString()) & PassText_Deposit.Password.Equals(LoggedClient.Password))
                {
                    if (Convert.ToBoolean(RequestSender.Execute(@"https://localhost:44343/Home/ObtainMoney", $"FirstName={LoggedClient.FirstName}&SecondName={LoggedClient.SecondName}&ThirdName={LoggedClient.ThirdName}&Key={LoggedClient.Key}&Password={LoggedClient.Password}&Money={MoneyText_Obtain.Text}")))
                    {
                        new MessageForm(true).Show();
                        LoggedClient.Money -= int.Parse(MoneyText_Obtain.Text);
                        MoneyText_Profile.Text = LoggedClient.Money.ToString();
                    }
                    else
                    {
                        new MessageForm(false).Show();
                    }
                }
            }
        }

        async public void TextPrinter(Label Source, object Content)
        {
            string stringSource = "";
            for (int i = 0; i < Content.ToString().Length; i++)
            {
                stringSource = stringSource.Insert(i, Content.ToString()[i].ToString());
                Source.Content = stringSource;
                await Task.Delay(20);
            }
        }

        private void PlaceHolder(object sender, MouseButtonEventArgs e)
        {
            Color darkColor = (Color)ColorConverter.ConvertFromString("#FF505050");
            SolidColorBrush darkColorBrush = new SolidColorBrush(darkColor);
            try
            {
                if ((sender as TextBox).Foreground == lightColorBrush)
                {
                    (sender as TextBox).Foreground = darkColorBrush;
                    (sender as TextBox).Text = string.Empty;
                }
            } catch (Exception)
            {
                if ((sender as PasswordBox).Foreground == lightColorBrush)
                {
                    (sender as PasswordBox).Foreground = darkColorBrush;
                    (sender as PasswordBox).Password = string.Empty;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string info = RequestSender.Execute(@"https://localhost:44343/Home/GetUsers");

            // MessageBox.Show(info);
        }
    }
}
