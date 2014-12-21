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
        private Record[] records_container;
        public static int page_size_in_bytes = Record.recordSizeInBytes * records_per_page;
        public static int allocate_size = Page.records_per_page * (Record.howManyElementsInRecord + sizeof(int) + sizeof(byte));
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
            int offset = 0;
            Page page = new Page();
            byte[] serialize_buffer = new byte[allocate_size];
            //possible buffer overflow! allocate_size < _streamToSerialize.Length
            System.Buffer.BlockCopy(_streamToSerialize,0,serialize_buffer,0,_streamToSerialize.Length);
            byte[] record_buffer = new byte[Record.recordSizeInBytes];
            
            for (int i = 0 ;i < Page.records_per_page; i++)
            {
                System.Buffer.BlockCopy(serialize_buffer, i * Record.recordSizeInBytes,record_buffer, 0, Record.recordSizeInBytes);
                Record rec = Record.createRecordFromBytes(record_buffer);
                page.addRecordToPage(rec);
            }

            return page;
        }

        public override string ToString()
        {
            String output = "";
            int count = 0;
            foreach (Record record in this.records_container)
            {
                //output += String.Format("[{0}] : key({1})",count,record.getKeyValue()) + Environment.NewLine;
               // count++;
                output += record.ToString()+Environment.NewLine;
            }
            return output;
        }
    }
}
