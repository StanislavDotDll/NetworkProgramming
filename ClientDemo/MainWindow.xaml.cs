using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Xml.Serialization;

namespace ClientDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContactDTO contact = new ContactDTO
            {
                Name = name.Text,
                Phone = phone.Text
            };

            TcpClient client = new TcpClient("127.0.0.1", 2020);

            using (NetworkStream stream = client.GetStream())
            {
                XmlSerializer serializer = new XmlSerializer(contact.GetType());
                serializer.Serialize(stream, contact);
            }
            client.Close();
        }

        [Serializable]
        public class ContactDTO
        {
            public string Name { get; set; }
            public string Phone { get; set; }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
