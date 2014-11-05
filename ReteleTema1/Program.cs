using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReteleTema1
{
    class Program
    {
        const string B = "01111110";

        static void Main(string[] args)
        {
            

            int k=int.Parse(Console.ReadLine());
            char[] buffer = new char[k];
            List<string> data = new List<string>();
            List<string> originalData = new List<string>();
            using(StreamReader reader=new StreamReader("fisier1.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string s="";
                    while (s.Length < k)
                    {
                        reader.ReadBlock(buffer, 0, k-s.Length);
                        s += new string(buffer, 0, k - s.Length).Trim();
                        int ind = 0;
                        while(ind!=-1)
                        {
                            ind = s.IndexOfAny(new char[] { '\n', '\r' });
                            if(ind!=-1)
                            s = s.Remove(ind, 1);
                        }
                    }
                    data.Add(s);
                    originalData.Add(s);
                }
            }
            int index=0;

            for(int i=0;i<data.Count;i++)
            {
                index = 0;
                while (index != -1)
                {
                    index = data[i].IndexOf("11111", index);
                    if (index != -1)
                    {
                        index += 5;
                        data[i] = data[i].Insert(index, "0");
                    }

                }
                data[i] = B + data[i] + B;
                
            }
            string fulldata="";
            using(StreamWriter writer=new StreamWriter("fisier_codat.txt"))
            {
                foreach (var s in data)
                {
                    writer.Write(s);
                    fulldata += s;
                }
            }

            int sIndex = 0;
            int fIndex = 0;
            List<string> codedData = new List<string>();
            while(sIndex !=-1 || fIndex!=-1)
            {
                sIndex = fulldata.IndexOf(B, sIndex)+B.Length;
                fIndex = fulldata.IndexOf(B, sIndex);
                if (fIndex == -1) break;
                string s = fulldata.Substring(sIndex, fIndex - (sIndex));
                if(s!="")
                    codedData.Add(s);
            }
            List<string> decodedData = new List<string>();

            foreach(var s in codedData)
            {
                index = 0;
                string temp=s;
                while (index != -1)
                {
                    index = temp.IndexOf("11111", index);
                    if (index != -1)
                    {
                        index += 5;
                        temp = temp.Remove(index,1);
                    }

                }
                decodedData.Add(temp);
            }
            using (StreamWriter writer = new StreamWriter("fisier_decodat.txt"))
            {
                foreach (var s in decodedData)
                {
                    writer.Write(s);
                }
            }

            foreach (var s in data)
                Console.Write(s);

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();

            foreach (var s in codedData)
                Console.WriteLine(s);

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();

            foreach (var s in decodedData)
                Console.WriteLine(s);

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();

            bool ok = true;

            ok = originalData.SequenceEqual(decodedData);

            if (ok)
                Console.WriteLine("It worked!");
            else
                Console.WriteLine("Something failed!");


            Console.Read();
            
        }
    }
}
