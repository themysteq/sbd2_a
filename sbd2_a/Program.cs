using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace sbd2_a
{
    class Program
    {
        static public String primary_file = @"primary.bin";
        static public String overflow_file = @"overflow.bin";
        static void Main(string[] args)
        {
            /*
            byte[] valueToTest = Encoding.ASCII.GetBytes("LEL\0\0E");
            uint enabledBits = 12;
            Console.WriteLine("HELLO WORLDDDDDDDD!");
            RecordTester test = new RecordTester();
            Record record = new Record(valueToTest, 0);
            bool test_result = test.testIt(record, enabledBits);
            Console.WriteLine(test_result);
            Console.ReadLine();
             */
            Random rnd = new Random();
            Page page = new Page();
            for (int i = 0 ; i < 10 ; i++ )
            {
                byte[] valueToTest = new byte[10];
               
                rnd.NextBytes(valueToTest);

                Record record = new Record(valueToTest, 0);
                

                bool ifInserted = false;
                try
                {
                     ifInserted = page.addRecordToPage(record);
                     String output = String.Format("{0}:{1}| inserted: {1}",i, record, ifInserted);
                     Console.WriteLine(output);
                }
                catch(PageFullException e)
                {
                    // podejmij próbe wrzucenia na inną stronę
                    Console.WriteLine(e.Message);
                    break;
                }
                
            }
            // page is full, wat to do ?
            Console.WriteLine("Serializing...");
            byte[] write_buffer = page.serializePageToBytes();
            Console.WriteLine("Done");
            using (BinaryWriter writer = new BinaryWriter(File.Open(primary_file,FileMode.OpenOrCreate))) 
            {
                writer.Write(write_buffer);
            }
            Console.WriteLine(page);
            if(File.Exists(primary_file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(primary_file,FileMode.Open)))
                {
                   // reader.Read()
                }
            }
            Console.ReadLine();

        }
    }
}
