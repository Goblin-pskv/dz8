using delegates;
using System;
using System.IO;

namespace delegates
{
    public class FileSearcher
    {

        public event EventHandler<FileArgs> FileFound;

        public bool Search(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Каталог не найден: {directoryPath}");

            return SearchRecursive(directoryPath);
        }

        private bool SearchRecursive(string directoryPath)
        {
            // Обходим файлы текущего каталога
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                var args = new FileArgs(Path.GetFileName(file));
                OnFileFound(args);

                // Если Cancel — прекращаем поиск
                if (args.Cancel)
                    return true;
            }
          
            foreach (var subDir in Directory.GetDirectories(directoryPath))
            {
                if (SearchRecursive(subDir))
                    return true;
            }

            return false;
        }


        protected virtual void OnFileFound(FileArgs e)
        {
            FileFound?.Invoke(this, e);
        }
    }
}