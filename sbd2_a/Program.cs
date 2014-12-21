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
          
            
            Random rnd = new Random();
            Page page = new Page();
            for (int i = 0 ; i < 10 ; i++ )
            {
                byte[] valueToTest = new byte[10];
               
                rnd.NextBytes(valueToTest);

                Record record = new Record(valueToTest, 1);
                

                bool ifInserted = false;
                try
                {
                     ifInserted = page.addRecordToPage(record);
                     String output = String.Format("{0}:{1}| inserted: {2}",i, record, ifInserted);
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
            using (BinaryWriter writer = new BinaryWriter(File.Open(primary_file,FileMode.Truncate))) 
            {
                writer.Write(write_buffer);
            }
            Console.WriteLine(page);
            
            if(File.Exists(primary_file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(primary_file,FileMode.Open)))
                {
                    int offset = 0;
                    while(true)
                    {
                        int bytes_read = 0;
                        byte[] read_buffer = new byte[Page.page_size_in_bytes];
                        try {
                            bytes_read = reader.Read(read_buffer, offset, Page.page_size_in_bytes);
                        }
                        catch (System.ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }

                        if (bytes_read == 0)
                        {
                            Page read_page = Page.deserializePage(read_buffer);
                            Console.WriteLine("No more pages. Closing.");
                            //print record
                            break;
                        }
                        else if(bytes_read < Page.page_size_in_bytes)
                        {
                            throw new PageReadFaultyException("Number of bytes is not aligned to size of Page! This is fatal.");
                        }
                        else
                        {
                            Page read_page = Page.deserializePage(read_buffer);
                            Console.WriteLine(read_page);
                            offset += Page.page_size_in_bytes;
                            
                            
                        }
                    }
                }
            }
            Console.WriteLine("Press any key to end..");
            Console.ReadKey();

        }
    }
}
