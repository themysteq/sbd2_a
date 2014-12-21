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
            List<Page> list_of_pages = new List<Page>();

            for (int i = 0 ; i < 3 ; i++ )
            {
                page = Page.generateRandomPage(rnd,1,200);
                list_of_pages.Add(page);
                Console.WriteLine(page);
                
            }
           PageFileHandler pagehandler = new PageFileHandler(primary_file, overflow_file, null);
           
           Console.WriteLine("Zapisać do pliku {0}? [t/n]", primary_file);
           ConsoleKeyInfo key =  Console.ReadKey(true);
           if (key.KeyChar == 't')
           {
               int write_offset = 0;
               foreach (var item in list_of_pages)
	            {
		            pagehandler.WritePageToPrimary(write_offset, (Page)item);
	                write_offset += Page.allocate_size;
                }
           }


           Console.WriteLine("Odczytać z pliku {0}? [t/n]", primary_file);
           key =  Console.ReadKey(true);
           if (key.KeyChar == 't')
           {
               List<Page> read_page_list = new List<Page>();
               if (File.Exists(primary_file))
               {
                   int read_pages_count = 0;
                   page = null;
                   do
                   {
                       page = pagehandler.ReadPageFromPrimary(read_pages_count * Page.allocate_size);
                       Console.WriteLine(page);
                       if (page != null)
                       {
                           read_page_list.Add(page);
                           read_pages_count++;
                       }
                       
                   } while (page != null);
                   Console.WriteLine(String.Format("pages read: {0}", read_page_list.Count));
               }
            }
            Console.WriteLine("Press any key to end..");
            Console.ReadKey();

        }
    }
}
