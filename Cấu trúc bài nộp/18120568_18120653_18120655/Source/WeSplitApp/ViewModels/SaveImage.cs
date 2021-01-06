using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModels
{
    class SaveImage
    {
        public static string tempUriImage; //Ảnh được load
        public static List<string> listFiles = new List<string>(); //List files ảnh sẽ được lưu
        public static List<string> listImageName = new List<string>(); //List name ảnh đã được lưu theo thứ tự 1->n

        public static void Save_Image()
        {
            var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string uriImage = ""; //Đường dẫn của .//Images

            for (int i = 0; i < currentFolder.Length - 10; i++)
            {
                uriImage += currentFolder[i];
            }

            var files = listFiles.ToArray();

            //Lấy từng file ảnh copy vào Images của project
            foreach (var file in files)
            {
                var info = new FileInfo(file);
                var newName = $"{Guid.NewGuid()}{info.Extension}";
                listImageName.Add(newName);
                File.Copy(file, $"{uriImage}Images\\{newName}");
            }
        }

        public static void Free_Memory()
        {
            listFiles.Clear();
            tempUriImage = null;
            listImageName.Clear();
        }
    }
}
