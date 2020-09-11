using CSharpLab_4.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpLab_4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowThis();
        }
        string[] fileInfo;
        public void ShowThis()
        {
            Service1Client client = new Service1Client();
            fileInfo = client.GetFilesInfo();
            for(int i = 0; i < fileInfo.Length; i++)
            {
                string[] newstr = fileInfo[i].Split(',');
                listbox.Items.Add("文件名："+newstr[0]+",文件大小："+newstr[1]+"B");
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = "";
            tb2.Text = "";
            string[] ss = fileInfo[listbox.SelectedIndex].Split(',');
            tb1.Text = "正在下载："+ss[0];
            Service1Client client = new Service1Client();
            Stream stream = client.DownloadStrem(ss[0]);
            string filePath = System.IO.Path.Combine(@"E:\", ss[0]); 
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            byte[] bytes = new byte[1024];
            int num;
            int count0 = 0;
            do
            {
                num = stream.Read(bytes, 0, bytes.Length);
                fs.Write(bytes, 0, num);
                count0 += num;
            } while (num > 0);
            fs.Close();
            stream.Close();
            tb2.Text = "下载完成，共下载"+count0+"字节，文件保存到"+filePath;
        }
    }
}
