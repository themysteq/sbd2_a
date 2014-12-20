using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbd2_a
{
    class Program
    {
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
            Page page = new Page();
            for (int i = 0 ; i < 10 ; i++ )
            {
                byte[] valueToTest = Encoding.ASCII.GetBytes("LE45y4yyL\0\0E");
                Console.WriteLine(i);
                valueToTest[0] = (byte)i;
                Record record = new Record(valueToTest, 0);
                bool ifInserted = false;
                try
                {
                     ifInserted = page.addRecordToPage(record);
                }
                catch(PageFullException e)
                {
                    // podejmij próbe wrzucenia na inną stronę
                    Console.WriteLine(e.Message);
                }
                
                Console.WriteLine(ifInserted);
            }
            Console.ReadLine();

        }
    }
}
