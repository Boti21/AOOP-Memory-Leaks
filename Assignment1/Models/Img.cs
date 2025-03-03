using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment1.Models;

public class Img
{
    // Public variables that are set at initialization
    public string FilePath { get; init; }
    public string FileName { get; init; }
    
    public string FileContent { get; init; }
    
    // Matrix dimensions
    public byte RowDimension { get; init; }
    public byte ColumnDimension { get; init; }
    
    // Matrix to store pixel values
    public List<List<byte>> ImgMatrix { get; init; }

    // Prints parsed matrix to console
    public void DisplayImgMatrix()
    {
        Console.WriteLine("{");
        for (int i = 0; i < this.RowDimension; i++)
        {
            Console.Write("{");
            for (int j = 0; j < this.ColumnDimension; j++)
            {
                Console.Write($"{this.ImgMatrix[i][j]} ");
            }
            Console.Write("}\n");
        }
        Console.WriteLine("}");
    }
    
    
    // Checks if digit is a valid hexadecimal value
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

    // Converts valid hex digit to it's integer value
    private byte HexToByte(char hex)
    {
        if(hex >= '0' && hex <= '9') return (byte)(hex - '0');
        if(hex >= 'a' && hex <= 'f') return (byte)(hex - 'a' + 10);
        if(hex >= 'A' && hex <= 'F') return (byte)(hex - 'A' + 10);
        throw new ArgumentException();
    }

    public Img(string filePath)
    {
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
        this.FilePath = filePath;
        this.FileContent = File.ReadAllText(filePath);
        this.FileName = Path.GetFileName(filePath);
        this.ImgMatrix = new List<List<byte>>();

        // List to store only valid hexadecimal digits
        List<byte> hexDigits = new List<byte>();
        
        // Temporary variables for reading process
        int dimensions = 2;
        int dimensionsPos = 0;
        bool dimensionsFound = false;
        
        // Counting variable
        int filePos = 0;
        while (dimensionsFound == false && filePos < this.FileContent.Length)
        {
            if (this.IsHexDigit(this.FileContent[filePos]))
            {
                // Parsing dimension values
                if (dimensions == 2)
                {
                    dimensions--;
                    this.RowDimension = HexToByte(this.FileContent[filePos]);
                } else if (dimensions == 1)
                {
                    dimensions--;
                    this.ColumnDimension = HexToByte(this.FileContent[filePos]);
                }
                else
                {
                    // Dimensions found
                    dimensionsPos = filePos;
                    dimensionsFound = true;
                }
            }        
            filePos++;
        } 

        // Extracting valid hex digits of the file after the dimensions
        for (int i = dimensionsPos; i < this.FileContent.Length; i++)
        {
            if (this.IsHexDigit(this.FileContent[i]))
            {
                hexDigits.Add(HexToByte(this.FileContent[i]));
            }
        }

        // Adding values of pixels into matrix
        for (int i = 0; i < this.RowDimension; i++)
        {
            this.ImgMatrix.Add(new List<byte>());
            for (int j = 0; j < this.ColumnDimension; j++)
            {
                this.ImgMatrix[i].Add(hexDigits[(i * this.ColumnDimension) + j]);
            }
        }
    }
    
    public void SerializeToFile(string path)
    {
        List<byte> serializedImgMatrix = new List<byte>();
        serializedImgMatrix.Add(this.RowDimension);
        serializedImgMatrix.Add(this.ColumnDimension);
        for (int i = 0; i < this.RowDimension; i++)
        {
            for (int j = 0; j < this.ColumnDimension; j++)
            {
                serializedImgMatrix.Add(this.ImgMatrix[i][j]);
            }
        }

        for (int i = 0; i < serializedImgMatrix.Count; i++)
        {
            Console.Write(serializedImgMatrix[i]);
        }
    }
}