using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbd2_a
{
    class Page
    {
        static public uint records_per_page = 5;
        public int counter = 0;
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
            if (counter < records_per_page)
            {
                _records[counter] = _record;
                counter++;
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
            int to_allocate = 0;
            to_allocate = sizeof()
            byte[] serializingBuffer = new 
            foreach (Record record in this._records)
            {
                
            }
        }
         */
    }
}
