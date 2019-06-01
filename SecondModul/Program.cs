using SecondModul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondModul
{
    interface IResolution
    {
        string GetResolution(int i);
    }

    class ParseString
    {
        private string str;
        private string name;
        private string category;
        private string extension;
        private string size;

        public string[] files;

        public string this[int index]
        {
            get { return files[index]; }
            set { files[index] = value; }
        }

        public string GetString()
        {
            str = @"Text:file.txt(6B);Some string content
Image:img.bmp(19MB);1920x1080
Text:data.txt(12B);Anothe string
Text:data1.txt(7B);Yet another string
Movie:logan.2017.mkv(19GB);1920x1080;2h12m";

            return str;
        }

        public int CountFiles()
        {
            int countFiles = 1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\n')
                {
                    countFiles++;
                }
            }

            return countFiles;
        }

        public string[] GetFiles()
        {
            GetString();
            int x = 0;
            string[] file = new string[CountFiles()];
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\n')
                {
                    x++;
                }
                else
                {
                    file[x] += str[i];
                }
            }
            files = file;
            return files;
        }

        public string GetCategory(int i)
        {
            int j = 0;
            category = "";
            while (j < files[i].Length)
            {
                if (files[i][j] == ':')
                {
                    break;
                }
                else
                {
                    category += files[i][j];
                }
                j++;
            }
            return category;
        }

        public string GetName(int i)
        {
            int index = 0;
            int index1 = 0;
            name = "";
            for (int j = 0; j < files[i].Length; j++)
            {
                if (files[i][j] == ':')
                {
                    index = j;
                }
                if (files[i][j] == '(')
                {
                    index1 = j;
                }
            }
            for (int k = index + 1; k < index1; k++)
            {
                name += files[i][k];
            }
            return name;
        }

        public string GetExtension(int i)
        {
            GetName(i);
            extension = "";
            int index = 0;
            for (int j = 0; j < GetName(i).Length; j++)
            {
                if (GetName(i)[j] == '.')
                {
                    index = j;
                }
            }

            for (int k = index; k < GetName(i).Length; k++)
            {
                extension += GetName(i)[k];
            }

            return extension;
        }

        public string GetSize(int i)
        {
            int index = 0;
            int index1 = 0;
            size = "";
            for (int j = 0; j < files[i].Length; j++)
            {
                if (files[i][j] == '(')
                {
                    index = j;
                }
                if (files[i][j] == ')')
                {
                    index1 = j;
                }
            }
            for (int k = index + 1; k < index1; k++)
            {
                size += files[i][k];
            }
            return size;
        }
    }

    class Text : ParseString
    {
        private string content;

        public string GetContent(int i)
        {
            GetFiles();
            content = "";
            int index = 0;

            for (int j = 0; j < files[i].Length; j++)
            {
                if (files[i][j] == ';')
                {
                    index = j;
                }
            }

            for (int k = index + 1; k < files[i].Length; k++)
            {
                content += files[i][k];
            }

            return content;
        }

        public void OutText()
        {
            GetFiles();
            Console.WriteLine("Text files:");
            for (int i = 0; i < CountFiles(); i++)
            {
                if (GetCategory(i) == "Text")
                {
                    Console.WriteLine($@"            {GetName(i)}
                    Extension: {GetExtension(i)}
                         Size: {GetSize(i)}
                      Content:{GetContent(i)}");
                }
            }
        }
    }

    class Image : ParseString, IResolution
    {
        private string resolution;

        public string GetResolution(int i)
        {
            GetFiles();
            int index = 0;
            resolution = "";
            for (int j = 0; j < files[i].Length; j++)
            {
                if (files[i][j] == ';')
                {
                    index = j;
                }
            }

            for (int k = index + 1; k < files[i].Length; k++)
            {
                resolution += files[i][k];
            }

            return resolution;
        }

        public void OutImage()
        {
            GetFiles();
            Console.WriteLine("Image files:");
            for (int i = 0; i < CountFiles(); i++)
            {
                if (GetCategory(i) == "Image")
                {
                    Console.WriteLine($@"            {GetName(i)}
                    Extension: {GetExtension(i)}
                         Size: {GetSize(i)}
                   Resolution: {GetResolution(i)}");
                }
            }
        }

    }

    class Movie : ParseString, IResolution
    {
        private string resolution;
        private string length;

        public string GetResolution(int i)
        {
            GetFiles();
            int index = 0;
            int index1 = 0;
            resolution = "";

            for (int j = 0; j < files[i].Length; j++)
            {
                if (files[i][j] == ';')
                {
                    index = j;
                }
            }
            while (index1 <= files[i].Length)
            {
                if (files[i][index1] == ';')
                {
                    break;
                }
                index1++;
            }

            for (int k = index1 + 1; k < index; k++)
            {
                resolution += files[i][k];
            }

            return resolution;
        }

        public string GetLength(int i)
        {
            GetFiles();
            int index = 0;
            length = "";

            for (int j = 0; j < files[i].Length; j++)
            {
                if (files[i][j] == ';')
                {
                    index = j;
                }
            }
            for (int k = index + 1; k < files[i].Length; k++)
            {
                length += files[i][k];
            }

            return length;
        }

        public void OutMovie()
        {
            GetFiles();
            Console.WriteLine("Move files:");
            for (int i = 0; i < CountFiles(); i++)
            {
                if (GetCategory(i) == "Movie")
                {
                    Console.WriteLine($@"            {GetName(i)}
                    Extension: {GetExtension(i)}
                         Size: {GetSize(i)}
                   Resolution: {GetResolution(i)}
                       Length: {GetLength(i)}");
                }
            }
        }

    }
}


class Program
{
    static void Main(string[] args)
    {
        Text b = new Text();
        Image c = new Image();
        Movie d = new Movie();

        b.OutText();
        c.OutImage();
        d.OutMovie();
    }
}

