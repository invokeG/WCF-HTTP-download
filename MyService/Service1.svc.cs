using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MyService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class Service1 : IService1
    {
        public static List<string> filesInfo;
        public static string path;
        public List<string> GetFilesInfo()//获取文件列表信息
        {
            if(filesInfo==null)//只在调第一次时获取
            {
                filesInfo = new List<string>();
                //获得路径信息，可执行文件目录与Foler文件夹进行拼接
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Folder");
                //构造DirectoryInfo对象，获得路径信息
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                //用GetFiles方法获得所有文件路径信息
                FileInfo[] q = directoryInfo.GetFiles();
                foreach (FileInfo v in q)
                {
                    filesInfo.Add(string.Format("{0},{1}",v.Name,v.Length));
                }
            }
            return filesInfo;
        }

        public Stream DownloadStrem(string fileName)//返回基类的Stream
        {
            //用文件名拼出来全文件路径信息
            string filePath = System.IO.Path.Combine(path, fileName);
            try
            {
                //构造FileStream获取文件流对象也可以
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return fs;
                //FileStream imageFile = File.OpenRead(filePath);
                //return imageFile;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
