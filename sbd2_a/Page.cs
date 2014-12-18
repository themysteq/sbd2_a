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
        private byte[] _buffer;
        private Record[] _records = new Record[records_per_page];
        public Page()
        {

        }
        public void addRecordToPage()
        {

        }
        /*
        public byte[] serializePageToBytes()
        {
            byte[] serializingBuffer = null;
            foreach (Record record in this._records)
            {
                
            }
        }
         */
    }
}
