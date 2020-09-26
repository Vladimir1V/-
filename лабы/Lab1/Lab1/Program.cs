using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Control_File
    {
        public string path_way, name, note,created, modified, new_modified, size, new_size;
    }
    class Control_Folder
    {
        public string path_way;
    }
    class Program
    {
        private static List<Control_File> files = new List<Control_File>();
        private static List<Control_Folder> folders = new List<Control_Folder>();

        static string Read()     //Чтение данных из файла.
        {
            string path1 = Environment.CurrentDirectory + "\\folders.txt";
            string path2 = Environment.CurrentDirectory + "\\files.txt";
            string active_path_way;
            FileInfo fileInf = new FileInfo(path1);
            if (fileInf.Exists == true)
            {
                if (fileInf.Length > 0)
                {
                    int j1, j2;
                    bool check_j1, check_j2, check = true;
                    using (StreamReader sr = new StreamReader(path1, Encoding.Default))
                    {
                        check_j1 = int.TryParse(sr.ReadLine(), out j1);
                        active_path_way = sr.ReadLine();
                        for (int i = 0; i < j1 && check_j1 == true; i++)
                        {
                            folders.Add(new Control_Folder() { path_way = sr.ReadLine() });
                        }
                    }
                    using (StreamReader sr = new StreamReader(path2, Encoding.Default))
                    {
                        check_j2 = int.TryParse(sr.ReadLine(), out j2);
                        for (int i = 0; i < j2 && check_j1 == true && check_j2 == true; i++)
                        {
                            files.Add(new Control_File() { path_way = sr.ReadLine(), name = sr.ReadLine(), size = sr.ReadLine(), new_size = sr.ReadLine(), created = sr.ReadLine(), modified = sr.ReadLine(), new_modified = sr.ReadLine(), note = sr.ReadLine() });
                        }
                    }
                    if (check_j1 == true && check_j2 == true)
                    {
                        foreach (Control_Folder u in folders)
                        {
                            if (u.path_way == null)
                                check = false;
                        }
                        foreach (Control_File u in files)
                        {
                            if (u.path_way == null || u.name == null || u.size == null || u.created == null || u.modified == null)
                                check = false;
                        }
                    }
                    else {
                        check = false;
                    }
                    if (check == false)
                    {
                        folders.Clear();
                        files.Clear();
                    }
                    return active_path_way;
                }
                else
                {
                    Console.WriteLine("Нет отслеживаемых файлов.");
                    return null;
                }
            }
            else {
                Console.WriteLine("Файл для хранения данных находится в директории {0}",Environment.CurrentDirectory);
                return null;
            }
        }

        static void Write(string active_path_way)     //Запись данных в файл.
        {
            string path1 = Environment.CurrentDirectory + "\\folders.txt";
            string path2 = Environment.CurrentDirectory + "\\files.txt";
            using (StreamWriter sr = new StreamWriter(path1, false, Encoding.Default))
            {
                sr.WriteLine(folders.Count);
                sr.WriteLine(active_path_way);
                foreach (Control_Folder u in folders)
                {
                    sr.WriteLine(u.path_way);
                }
            }
            using (StreamWriter sr = new StreamWriter(path2, false, Encoding.Default))
            {
                sr.WriteLine(files.Count);
                foreach (Control_File u in files)
                {
                    sr.WriteLine(u.path_way);
                    sr.WriteLine(u.name);
                    sr.WriteLine(u.size);
                    sr.WriteLine(u.new_size);
                    sr.WriteLine(u.created);
                    sr.WriteLine(u.modified);
                    sr.WriteLine(u.new_modified);
                    sr.WriteLine(u.note);
                }
            }
        }

        static int Status(string active_path_way)       //ОБНОВЛЕНИЕ ДАННЫХ О ФАЙЛАХ АКТИВНОЙ ДИРЕКТОРИИ.
        {
            if (Directory.Exists(active_path_way) == true)
            {
                string[] new_files = Directory.GetFiles(active_path_way);   //Проверяем данные и статусы файлов.
                foreach (string s in new_files)
                {
                    FileInfo fileInf = new FileInfo(s);
                    int count = files.FindIndex(x => string.Equals(x.name, s, StringComparison.CurrentCultureIgnoreCase));
                    if (count >= 0)
                    {
                        if (files[count].size != Convert.ToString(fileInf.Length) || files[count].new_modified != Convert.ToString(fileInf.LastWriteTime))   //Проверка на изменение размера и время модификации файла.
                        {
                            files.Add(new Control_File() { path_way = active_path_way, name = files[count].name, size = files[count].size, new_size = Convert.ToString(fileInf.Length), created = files[count].created, modified = files[count].modified, new_modified = Convert.ToString(fileInf.LastWriteTime)});
                            files.RemoveAt(count);
                        }
                    }
                    else            //Добавляем NEW файл.
                    {
                        files.Add(new Control_File() { path_way = active_path_way, name = s, size = Convert.ToString(fileInf.Length), created = Convert.ToString(fileInf.CreationTime), modified = Convert.ToString(fileInf.LastWriteTime), new_modified = Convert.ToString(fileInf.LastWriteTime), note = "new" });
                    }
                }
                for(int i = files.Count - 1; i >= 0; i--)       //Определяем файлы DELETE.
                {
                    if (files[i].path_way == active_path_way)
                    {
                        if (Array.FindIndex(new_files, x => x.Equals(files[i].name)) < 0)
                        {
                            int count = files.IndexOf(files[i]);
                            files.Add(new Control_File() { path_way = active_path_way, name = files[i].name, size = files[i].size, new_size = files[i].new_size, created = files[i].created, modified = files[i].modified, new_modified = files[i].new_modified, note = "delete" });
                            files.RemoveAt(count);
                        }
                    }
                }
                return 0;
            }
            else
            {
                Console.WriteLine("Активная директория исчезла.");
                active_path_way = null;
                return 1;
            }
        }


        //ГЛАВНАЯ ФУНКЦИЯ
        static void Main(string[] args)
        {
            string active_path_way;
            active_path_way=Read();
            do
            {
                Console.Clear();
                Console.WriteLine("СИСТЕМА КОНТРОЛЯ ВЕРСИЙ.");
                Console.WriteLine("Введите команду(help - описание команд, exit - выход):");
                link:
                    Console.Write(">");
                    string str = Console.ReadLine();
                    str = str.Trim();
                    //КОМАНДА HELP
                    if (str == "help")
                    {
                        Console.WriteLine("init [dir_path] – инициализация СКВ для папки с файлами (или без), путь к которой указан в dir_path.\n");
                        Console.WriteLine("status – отображение статуса отслеживаемых файлов последней проинициализированной папки (какие файлы отслеживаются, краткая информация по ним).\n");
                        Console.WriteLine("add [file_path] – добавить файл под версионный контроль, где file_path имя файла с расширением.\n");
                        Console.WriteLine("remove [file_path] – удалить файл из-под версионного контроля.\n");
                        Console.WriteLine("apply [dir_path] – сохранить все изменения в отслеживаемой папке (удалить все метки к файлам и сохранить изменения в них).\n");
                        Console.WriteLine("list branch -  показать все отслеживаемые папки.\n");
                        Console.WriteLine("checkout [dir_path] OR [dir_number] – перейти к указанной отслеживаемой директории.");
                        goto link;
                    }
                    //КОМАНДА INIT
                    if (str.IndexOf("init ") == 0)
                    {
                        str = str.Remove(0,5);
                        if (Directory.Exists(str) == true)
                        {
                            if (folders.Exists(x => string.Equals(x.path_way, str, StringComparison.CurrentCultureIgnoreCase)) != true)
                            {
                                folders.Add(new Control_Folder() { path_way = str });
                                active_path_way = str;
                                string[] new_files = Directory.GetFiles(str);
                                foreach (string s in new_files)
                                {
                                    FileInfo fileInf = new FileInfo(s);
                                    files.Add(new Control_File() { path_way = str, name = s, size = Convert.ToString(fileInf.Length), created = Convert.ToString(fileInf.CreationTime), modified = Convert.ToString(fileInf.LastWriteTime), new_modified = Convert.ToString(fileInf.LastWriteTime) });
                                }
                                Console.WriteLine("Директория инициализирована.");
                            }
                            else
                                Console.WriteLine("Папка с введенным путём уже проинициализирована.");
                        }
                        else
                            Console.WriteLine("Директория с введенным путём не существует.");
                        Write(active_path_way);
                        goto link;
                    }
                    //КОМАНДА STATUS
                    if (str == "status")
                    {
                        if (active_path_way != null)
                        {
                            if (Status(active_path_way) == 0)
                            {
                            Console.WriteLine("Directory: {0}\\", active_path_way);
                                int mark = 0;
                                foreach (Control_File u in files)  //Вывод данных о файлах на консоль.
                                    {
                                        if (active_path_way == u.path_way)
                                        {
                                            if (u.new_size==null || u.modified == u.new_modified)
                                                Console.ForegroundColor = ConsoleColor.Green;
                                            else
                                                Console.ForegroundColor = ConsoleColor.Red;
                                            if (String.IsNullOrEmpty(u.note))
                                                Console.WriteLine("file: {0}", u.name.Replace(u.path_way + "\\", ""));
                                            else
                                                Console.WriteLine("file: {0} <<-- {1}", u.name.Replace(u.path_way + "\\",""), u.note);
                                            int size = Convert.ToInt32(u.size);     //Конвертирование размера файла.
                                            string name_size = "Byte";
                                            if ((size / 1024) > 1)
                                            {
                                                size /= 1024;
                                                name_size = "Kb";
                                                if ((size / 1024) > 1)
                                                {
                                                    size /= 1024;
                                                    name_size = "Mb";
                                                }
                                            }
                                            if (!String.IsNullOrEmpty(u.new_size))
                                            {
                                                int new_size = Convert.ToInt32(u.new_size); //Конвертирование нового размера файла.
                                                string name_new_size = "Byte";
                                                if ((new_size / 1024) > 1)
                                                {
                                                    new_size /= 1024;
                                                    name_new_size = "Kb";
                                                    if ((new_size / 1024) > 1)
                                                    {
                                                        new_size /= 1024;
                                                        name_new_size = "Mb";
                                                    }
                                                }
                                                Console.WriteLine("  size: {0} {1} <<-- {2} {3}", size, name_size, new_size, name_new_size);
                                            }
                                            else
                                            {
                                                Console.WriteLine("  size: {0} {1}", size, name_size);
                                            }
                                            Console.WriteLine("  created: {0}", u.created);
                                            Console.WriteLine("  modified: {0}\n", u.new_modified);
                                            mark++;
                                        }
                                    }
                                    Console.ResetColor();
                                    if (mark == 0)
                                        Console.WriteLine("Нет в файлов в активной директории.");
                            }
                            else
                            {
                                Console.WriteLine("Активная директория исчезла.");
                                active_path_way = null;
                            }
                        }
                        else
                            Console.WriteLine("Не выбрана активная директория(используйте команду checkout).");
                        Write(active_path_way);
                        goto link;
                    }
                    //КОМАНДА ADD
                    if (str.IndexOf("add ") == 0)
                    {
                        str = str.Remove(0, 4);
                        if (active_path_way != null)
                        {
                            Status(active_path_way);
                            if (File.Exists(active_path_way + "\\" + str) == true)
                            {
                                int count = files.FindIndex(s => string.Equals(s.name, active_path_way + "\\" + str, StringComparison.CurrentCultureIgnoreCase));
                                if (count >= 0)
                                {
                                    files.RemoveAt(count);
                                    FileInfo fileInf = new FileInfo(active_path_way + "\\" + str);
                                    files.Add(new Control_File() { path_way = active_path_way, name = active_path_way + "\\" + str, size = Convert.ToString(fileInf.Length), created = Convert.ToString(fileInf.CreationTime), modified = Convert.ToString(fileInf.LastWriteTime), new_modified = Convert.ToString(fileInf.LastWriteTime), note = "added" });
                                    Console.WriteLine("Файл {0} добавлен под версионный контроль.", str);
                                }
                                else
                                    Console.WriteLine("Файл не удалось добавить");
                            }
                            else
                                Console.WriteLine("Файл с таким названием в директории не существует.");
                        }
                        else
                            Console.WriteLine("Не выбрана активная директория(команда checkout).");
                        Write(active_path_way);
                        goto link;
                    }
                    //КОМАНДА REMOVE
                    if (str.IndexOf("remove ") == 0)
                    {
                        str = str.Remove(0, 7);
                        if (active_path_way != null)
                        {
                            Status(active_path_way);
                            if (File.Exists(active_path_way + "\\" + str) == true)
                            {
                                int count = files.FindIndex(s => string.Equals(s.name, active_path_way + "\\" + str, StringComparison.CurrentCultureIgnoreCase));
                                string mod =files[count].modified;
                                files.RemoveAt(count);
                                FileInfo fileInf = new FileInfo(active_path_way + "\\" + str);
                                files.Add(new Control_File() { path_way = active_path_way, name = active_path_way + "\\" + str, size = Convert.ToString(fileInf.Length), created = Convert.ToString(fileInf.CreationTime), modified = mod, new_modified = Convert.ToString(fileInf.LastWriteTime), note = "removed" });
                                Console.WriteLine("Файл {0} убран из под версионного контроля.", str);
                            }
                            else
                                Console.WriteLine("Файл с таким названием не существует.");
                            Write(active_path_way);
                        }
                        else
                            Console.WriteLine("Не выбрана активная директория(команда checkout).");
                        goto link;
                    }
                    //КОМАНДА APPLY
                    if (str.IndexOf("apply ") == 0)
                    {
                        str = str.Remove(0, 6);
                        if (Directory.Exists(str) == true)
                        {
                            if (folders.Exists(x => x.path_way == str ) == true)
                            {
                                for(int i = files.Count()-1; i>=0; i--)
                                {
                                    if (files[i].path_way == str)
                                    {
                                        if(files[i].note != "delete")
                                        {
                                            string size = !String.IsNullOrEmpty(files[i].new_size) ? files[i].new_size : files[i].size;
                                            files.Add(new Control_File() { path_way = str, name = files[i].name, size = size, created = files[i].created, modified = files[i].modified, new_modified = files[i].modified, note = "" });
                                        }
                                        files.Remove(files[i]);
                                    }
                                }
                                Console.WriteLine("Все изменения в данной директории сохранены.");
                                Write(active_path_way);
                            }
                            else
                                Console.WriteLine("Данная директория не отслеживается (для добавления используйте команду init).");
                        }
                        else
                            Console.WriteLine("Директория с введенным путём не существует.");
                        goto link;
                    }
                    //КОМАНДА LIST BRANCH
                    if (str == "list branch")
                    {
                        if (folders.Count > 0)
                        {
                            Console.WriteLine("Отслеживаемые директории:");
                            foreach (Control_Folder u in folders)
                            {
                                Console.WriteLine(u.path_way);
                                System.Diagnostics.Process.Start(u.path_way);
                            }
                        }
                        else
                            Console.WriteLine("Отслеживаемые директории отсутствуют.");
                        goto link;
                    }
                    //КОМАНДА CHECKOUT
                    if (str.IndexOf("checkout ") == 0)
                    {
                        str = str.Remove(0, 9);
                        if (Directory.Exists(str) == true)
                        {
                            if (folders.Exists(x => x.path_way == str) == true)
                            {
                                active_path_way = str;
                                Console.WriteLine("Директория {0} активна", active_path_way);
                            }
                            else
                                Console.WriteLine("Данная директория не отслеживается (для добавления используйте команду init).");
                        }
                        else
                            Console.WriteLine("Директория с введенным путём не существует.");
                        goto link;
                    }
                    //КОМАНДА CHECKOUT БЕЗ СЛОВА "CHECKOUT"
                    if (Directory.Exists(str) == true)
                    {
                        if (Directory.Exists(str) == true)
                        {
                            if (folders.Exists(x => x.path_way == str) == true)
                            {
                                active_path_way = str;
                                Console.WriteLine("Директория {0} активна", active_path_way);
                            }
                            else
                                Console.WriteLine("Данная директория не отслеживается (для добавления используйте команду init).");
                        }
                        else
                            Console.WriteLine("Директория с введенным путём не существует.");
                        goto link;
                    }
                    // КОМАНДА EXIT
                    if (str == "exit")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Команда введена неверно.");
                        goto link;
                    }
            } while(Console.ReadKey().Key!=ConsoleKey.Escape);
        }
    }
}