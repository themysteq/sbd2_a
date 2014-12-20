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
        private Record[] records_container;
        public Page()
        {
           records_container = new Record[records_per_page];
           for (int i = 0; i < records_per_page;i++ )
           {
               records_container[i] = null;
           }
        }
        public bool addRecordToPage(Record _record)
        {
            if (howManyRecordsInPage < records_per_page)
            {
                records_container[ howManyRecordsInPage ] = _record;
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
            return records_container;
        }
        
        
        public byte[] serializePageToBytes()
        {
            

            int offset = 0;
            // int allocate_size = Page.records_per_page * (Record.howManyElementsInRecord + KEY_SIZE + POINTER_SIZE);
            int allocate_size = Page.records_per_page * (Record.howManyElementsInRecord + sizeof(int) + sizeof(int));
            byte[] serialize_buffer = new byte[allocate_size];
            foreach (Record item in this.records_container)
            {
                byte[] byte_record = item.getRecordByteStream();
                System.Buffer.BlockCopy(byte_record, 0, serialize_buffer, offset, byte_record.Length);
                offset += byte_record.Length;
            }
            //strona uległa serializacji
            return serialize_buffer;

        }
        public static Page deserializePage(byte[] _streamToSerialize)
        {
            return null;
        }
        public override string ToString()
        {
            String output = "";
            int count = 0;
            foreach (Record record in this.records_container)
            {
                output += String.Format("[{0}] : key({1})",count,record.getKeyValue()) + Environment.NewLine;
                count++;
            }
            return output;
        }
    }
}
