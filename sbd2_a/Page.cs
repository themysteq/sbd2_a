using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbd2_a
{
    class Page
    {
        static public int records_per_page = 5;
        public int howManyRecordsInPage = 0;
        private byte[] _buffer;
        private Record[] _records;
        public Page()
        {
           _records = new Record[records_per_page];
           for (int i = 0; i < records_per_page;i++ )
           {
               _records[i] = null;
           }
        }
        public bool addRecordToPage(Record _record)
        {
            if (howManyRecordsInPage < records_per_page)
            {
                _records[ howManyRecordsInPage ] = _record;
                howManyRecordsInPage++;
                return true;
            }
            else
            {
                throw new PageFullException("Page is full!");

            }
        }
        Record[] getRecords()
        {
            return _records;
        }
        
        
        public byte[] serializePageToBytes()
        {
            /* 
            int counter = 0;
            int howManyElementsInPage = this.
            int howManyToAllocate = records_per_page * (Record.howManyElementsInRecord + sizeof(int) + sizeof(int));
            byte[] serializingBuffer = new byte[howManyToAllocate];
             * */

        }
         
    }
}
