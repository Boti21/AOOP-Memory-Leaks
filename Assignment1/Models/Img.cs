using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment1.Models;

//Console.WriteLine("Hello, World!");
//var fp = new FileInfo("../../../hello.txt");
//var path = "/Users/boti/Uni/SDU/Semester6/AOOP/assignment_test/FileReading/hello.txt";
//string readText = File.ReadAllText(path);

//Console.WriteLine(readText);

//File.WriteAllText(path, "another Hello world");

//Console.WriteLine(File.ReadAllText(path));
/*
FileStream fp = File.Open("/Users/boti/Uni/SDU/Semester6/AOOP/assignment_test/FileReading/hello.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
Console.WriteLine(fp.Position);
fp.Position = 0;

var readByte = fp.ReadByte();
Console.WriteLine(readByte);
Console.WriteLine(fp.Position);


fp.Position = 0;

readByte = fp.ReadByte();
Console.WriteLine(readByte);
Console.WriteLine(fp.Position);

fp.Close();

string FileText = File.ReadAllText("/Users/boti/Uni/SDU/Semester6/AOOP/assignment_test/FileReading/hello.txt");
Console.WriteLine(FileText);
Console.WriteLine(FileText.Length);

for (int i = 0; i < FileText.Length; i++)
{
    Console.WriteLine(FileText[i]);
    Console.WriteLine("IsHexDigit:");
    //Console.WriteLine(IsHexDigit(FileText[i]));
}
*/


public class Img
{
    public string FilePath { get; init; }
    public string FileName { get; init; }
    
    public string FileContent { get; init; }
    
    public byte RowDimension { get; init; }
    public byte ColumnDimension { get; init; }
    
    public List<List<byte>> ImgMatrix { get; init; }

    public void DisplayImgMatrix()
    {
        Console.Write("{");
        for (int i = 0; i < this.RowDimension; i++)
        {
            Console.Write("{");
            for (int j = 0; j < this.ColumnDimension; j++)
            {
                Console.Write($"{this.ImgMatrix[i][j]} ");
            }
            Console.Write("}\n");
        }
    }
    
    
    private bool IsHexDigit(char c)
    {
    if (c >= '0' && c <= '9')
    {
        return true;
    }

    if (c >= 'A' && c <= 'F')
    {
        return true;
    }

    if (c >= 'a' && c <= 'f')
    {
        return true;
    }
    return false;
    }

    public Img(string filePath)
    {
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
        this.FilePath = filePath;
        this.FileContent = File.ReadAllText(filePath);
        this.FileName = Path.GetFileName(filePath);
        this.ImgMatrix = new List<List<byte>>();

        int dimensions = 2;
        
        List<byte> hexDigits = new List<byte>();

        for (int i = 0; i < this.FileContent.Length; i++)
        {
            if (this.IsHexDigit(this.FileContent[i]))
            {
                if (dimensions == 2)
                {
                    dimensions--;
                    this.RowDimension = (byte)Convert.ToInt16(this.FileContent[i].ToString(), 16);
                } else if (dimensions == 1)
                {
                    dimensions--;
                    this.ColumnDimension = (byte)Convert.ToInt16(this.FileContent[i].ToString(), 16);
                }
                hexDigits.Add(Convert.ToByte(this.FileContent[i].ToString(), 16));
            }        
        }

        for (int i = 0; i < this.RowDimension; i++)
        {
            this.ImgMatrix.Add(new List<byte>());
            for (int j = 0; j < this.ColumnDimension; j++)
            {
                this.ImgMatrix[i].Add(hexDigits[(i * this.RowDimension) + j]);
            }
        }
    }




}

//Img img = new Img("/Users/boti/Uni/SDU/Semester6/AOOP/assignment_test/FileReading/smile.b2img.txt");
