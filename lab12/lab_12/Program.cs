using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace lab12
{
    public static class SGVLog
    {
        private static StreamWriter _writer;
        private static string pathLog = "E:\\unic\\ООП\\lab12\\lab_12\\SGVlogfile.txt";

        public static void WriteInLog(string action, string fileName = "", string path = "")
        {
            if (fileName.Length != 0 && path.Length != 0)
            {
                using (_writer = new StreamWriter(pathLog, true))
                {
                    _writer.WriteLine($"{DateTime.Now.ToString()}");
                    _writer.WriteLine($"Действие: {action}");
                    _writer.WriteLine($"Имя файла: {fileName}");
                    _writer.WriteLine($"Путь к файлу: {path}");
                    _writer.WriteLine("----------");
                }
            }
            else
            {
                using (_writer = new StreamWriter(pathLog, true))
                {
                    _writer.WriteLine($"{DateTime.Now}");
                    _writer.WriteLine($"Действие: {action}");
                    _writer.WriteLine("----------");
                }
            }
        }

        public static void FindInfo()
        {
            var output = new StringBuilder();

            using (var stream = new StreamReader(pathLog))
            {
                string textline;
                bool isActual = false;
                while (!stream.EndOfStream)
                {
                    textline = stream.ReadLine();
                    isActual = false;
                    if (DateTime.TryParse(textline, out DateTime date) && date.Day == DateTime.Now.Day)
                    {
                        isActual = true;
                        textline += Environment.NewLine;
                        output.AppendFormat(textline);
                    }

                    textline = stream.ReadLine();
                    while (textline != "----------" && !stream.EndOfStream)
                    {
                        if (isActual)
                        {
                            textline += Environment.NewLine;
                            output.AppendFormat(textline);
                        }
                        textline = stream.ReadLine();
                    }

                    if (isActual)
                    {
                        output.AppendFormat("----------");
                        output.AppendFormat(Environment.NewLine);
                    }
                }
            }

            using (var stream = new StreamWriter("E:\\unic\\ООП\\lab12\\lab_12\\Log2.txt"))
            {
                stream.WriteLine(output.ToString());
            }
        }
    }

    public static class SGVDiskInfo
    {
        public static void WriteDiskInfo(string driverName)
        {
            var driver = DriveInfo.GetDrives().Single(d => d.Name == driverName);
            Console.WriteLine($"Имя диска: {driver.Name}");
            Console.WriteLine($"Свободное место на диске: {driver.TotalFreeSpace}");
            Console.WriteLine($"Файловая система: {driver.DriveFormat}");
            Console.WriteLine($"Тип диска: {driver.DriveType}");
            Console.WriteLine($"Метка тома: {driver.VolumeLabel}");
            Console.WriteLine($"Объём: {driver.TotalSize}");
            Console.WriteLine($"Доступный объём: {driver.AvailableFreeSpace}");
        }
    }

    public static class SGVFileInfo
    {
        public static void WriteFileInfo(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                Console.WriteLine($"Полный путь к файлу {fileInfo.Name}: {fileInfo.FullName}\n" +
                    $"Размер: {fileInfo.Length}\n" +
                    $"Расширение: {fileInfo.Extension}\n" +
                    $"Дата создания: {fileInfo.CreationTime}\n" +
                    $"Дата изменения: {fileInfo.LastWriteTime}");
            }
            else
                throw new FileNotFoundException();
        }
    }

    public static class SGVDirInfo
    {
        public static void WriteDirInfo(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                Console.WriteLine($"Количество файлов: {dirInfo.GetFiles().Length}\n" +
                    $"Время создания: {dirInfo.CreationTime}\n" +
                    $"Количество поддиректориев: {dirInfo.GetDirectories().Length}\n" +
                    $"Список родительских директориев: {dirInfo.Parent}");
            }
            else
                throw new ArgumentException();
        }
    }

    public static class SGVFileManager
    {
        public static void InspectDriver(string driverName)
        {
            Directory.CreateDirectory("E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect");
            File.Create("E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVdirinfo.txt").Close();
            var currentDrive = DriveInfo.GetDrives().Single(x => x.Name == driverName);

            using (StreamWriter file = new StreamWriter("E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVdirinfo.txt"))
            {
                file.WriteLine("Список папок:");
                foreach (var s in currentDrive.RootDirectory.GetDirectories())
                {
                    file.WriteLine(s);
                }
                file.WriteLine("Список файлов:");
                foreach (var f in currentDrive.RootDirectory.GetFiles())
                {
                    file.WriteLine(f);
                }
            }

            File.Copy("E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVdirinfo.txt", "E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVdirinfocopy.txt", true);
            File.Delete("E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVdirinfo.txt");
        }

        public static void CopyFiles(string path, string extention)
        {
            Directory.CreateDirectory("E:\\unic\\ООП\\lab12\\lab_12\\SGVFiles");
            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryInfo directory2 = new DirectoryInfo("E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVFiles\\");
            foreach (var f in directory.GetFiles())
            {
                if (f.Extension == extention)
                    f.CopyTo("E:\\unic\\ООП\\lab12\\lab_12\\SGVFiles\\" + f.Name + extention, true);
            }
            if (!directory2.Exists)
                Directory.Move("E:\\unic\\ООП\\lab12\\lab_12\\SGVFiles\\", "E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVFiles\\");
            else
                Directory.Delete("E:\\unic\\ООП\\lab12\\lab_12\\SGVFiles", true);
        }

        public static void CreateArchive(string dir)
        {
            string name = "E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\SGVFiles";
            if (new DirectoryInfo("E:\\unic\\ООП\\lab12\\lab_12\\SGVInspect\\").GetFiles(" *.zip").Length == 0)
            {
                ZipFile.CreateFromDirectory(name, name + ".zip");
                DirectoryInfo direct = new DirectoryInfo(dir);
                foreach (var innerFile in direct.GetFiles())
                    innerFile.Delete();
                direct.Delete();
                ZipFile.ExtractToDirectory(name + ".zip", dir);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("*****************************************************************************************************");
                SGVDiskInfo.WriteDiskInfo("C:\\");
                SGVLog.WriteInLog("RNADiskInfo.getFreeDrivesSpace()");

                Console.WriteLine("******************************************************************************************************");
                SGVFileInfo.WriteFileInfo("E:\\unic\\ООП\\12_Потоки_файловая система.pdf");
                SGVLog.WriteInLog("RNAFileInfo.WriteFileInfo()", "RNALogfile.txt", "E:\\unic\\ООП\\lab12\\RNAlogfile.txt");

                Console.WriteLine("******************************************************************************************************");
                SGVDirInfo.WriteDirInfo("E:\\unic\\ООП");
                SGVLog.WriteInLog("RNADirInfo.WriteDirInfo()", "E:\\unic\\ООП");

                SGVFileManager.InspectDriver("C:\\");
                SGVLog.WriteInLog("RNAFileManager.InspectDriver()", "C:\\");

                SGVFileManager.CopyFiles("E:\\unic\\ООП", ".pdf");
                SGVLog.WriteInLog("RNAFileManager.CopyFiles()", "E:\\unic\\ООП");
                SGVFileManager.CreateArchive("E:\\unic\\ООП\\forarchive");
                SGVLog.WriteInLog("RNAFileManager.CreateArchive()");

                SGVLog.FindInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
