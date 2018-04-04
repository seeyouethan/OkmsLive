using System;
using System.IO;
using Aspose.Cells;//Excel
using Aspose.Slides;//PPT
using Aspose.Words;//Word
namespace OkmsLive.OkmsLiveTools
{
    /// <summary>
    /// Aspose操作帮助类
    /// </summary>
    public static class AsposeHandlerHelper
    {
        /// <summary>
        /// 将Word转换为Pdf（在相同目录下生成.pdf文件）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        public static void Word2Pdf(string filePath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            Document doc = new Document(filePath);
            var pdfFileName = filePath.Substring(0, filePath.LastIndexOf('.') + 1) + "pdf";
            doc.Save(pdfFileName, Aspose.Words.SaveFormat.Pdf);
        }

        /// <summary>
        /// 将Word转换为pdf（在制定目录下生成.pdf）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <param name="destinationPath">指定生成pdf的路径（包括后缀.pdf）</param>
        public static void Word2Pdf(string filePath, string destinationPath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            Document doc = new Document(filePath);
            doc.Save(destinationPath, Aspose.Words.SaveFormat.Pdf);
        }

        /// <summary>
        /// 将PPT转换为Pdf（在相同目录下生成.pdf文件）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        public static void Ppt2Pdf(string filePath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            Presentation ppt = new Presentation(filePath);
            var pdfFileName = filePath.Substring(0, filePath.LastIndexOf('.') + 1) + "pdf";
            ppt.Save(pdfFileName, Aspose.Slides.Export.SaveFormat.Pdf);
        }

        /// <summary>
        /// 将PPT转换为Pdf（在指定目录下生成.pdf文件）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <param name="destinationPath">指定生成pdf的路径（包括后缀.pdf）</param>
        public static void Ppt2Pdf(string filePath, string destinationPath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            Presentation ppt = new Presentation(filePath);
            ppt.Save(destinationPath, Aspose.Slides.Export.SaveFormat.Pdf);
        }

        /// <summary>
        /// 将Excel转换为Pdf（在相同目录下生成.pdf文件）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        public static void Excel2Pdf(string filePath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            Workbook excel = new Workbook(filePath);
            var pdfFileName = filePath.Substring(0, filePath.LastIndexOf('.') + 1) + "pdf";
            excel.Save(pdfFileName, Aspose.Cells.SaveFormat.Pdf);
        }

        /// <summary>
        /// 将Excel转换为Pdf（在指定目录下生成.pdf文件）
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <param name="destinationPath">指定生成pdf的路径（包括后缀.pdf）</param>
        public static void Excel2Pdf(string filePath, string destinationPath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            Workbook excel = new Workbook(filePath);
            excel.Save(destinationPath, Aspose.Cells.SaveFormat.Pdf);
        }

        /// <summary>
        /// 将Pdf文件转换为图片，将转换的图片存放在相同目录下新建一个以该Pdf文件名为名称的文件夹中
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        public static void Pdf2Images(string filePath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            string directoryPath = filePath.Substring(0, filePath.LastIndexOf('.'));
            Pdf2Images(filePath, directoryPath);
        }

        /// <summary>
        /// 将Pdf文件转换为图片，将转换的图片存放在指定目录中
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <param name="destinationPath">指定存放图片的目录</param>
        public static void Pdf2Images(string filePath, string destinationPath)
        {
            ModifyInMemory.ActivateMemoryPatching();
            Aspose.Pdf.Document document = new Aspose.Pdf.Document(filePath);
            var device = new Aspose.Pdf.Devices.JpegDevice();
            int quality = 100;
            Directory.CreateDirectory(destinationPath);
            //默认质量为100，设置质量的好坏与处理速度不成正比，甚至是设置的质量越低反而花的时间越长，怀疑处理过程是先生成高质量的再压缩
            device = new Aspose.Pdf.Devices.JpegDevice(quality);
            //遍历每一页转为jpg
            for (var i = 1; i <= document.Pages.Count; i++)
            {
                string filePathOutPut = destinationPath + "\\" + string.Format("{0}.jpg", i);
                FileStream fs = new FileStream(filePathOutPut, FileMode.OpenOrCreate);
                try
                {
                    device.Process(document.Pages[i], fs);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    fs.Close();
                    File.Delete(filePathOutPut);
                    throw;
                }
            }
        }
    }
}
