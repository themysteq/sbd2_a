using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace sbd2_a
{
    class PageFileHandler
    {

        private String _primary_file_name;
        private String _overflow_file_name;
        private String _index_file_name;
        
        public PageFileHandler( String primary_file_name, String overflow_file_name, String index_file_name) {
            this._index_file_name = index_file_name;
            this._overflow_file_name = overflow_file_name;
            this._primary_file_name = primary_file_name;

           
            
        }

        private Page ReadFromFile(String filename, int offset_in_bytes)
        {
            Page page = null;
            using (FileStream filestream = new FileStream(filename, FileMode.Open))
            {
                byte[] read_buff = new byte[Page.allocate_size];
                filestream.Seek(offset_in_bytes, SeekOrigin.Begin);
                int bytes_read = filestream.Read(read_buff, 0, read_buff.Length);

                //wyczytałeś zero, co teraz?
                if (bytes_read == 0)
                {
                    Console.WriteLine("No more pages.");
                    return null;
                }
                else if( bytes_read == Page.allocate_size)
                {
                    page = Page.deserializePage(read_buff);
                    return page;
                }
                else
                {
                    throw new PageReadFaultyException("Bytes read is not aligned to size of Page. This is fatal.");
                }    
            }
            
        }
        private void WriteToFile(String filename, int offset_in_bytes, Page page)
        {
            using (FileStream filestream = new FileStream(filename, FileMode.Open))
            {
                byte[] write_buffer = null;
                write_buffer = page.serializePageToBytes();
                filestream.Seek(offset_in_bytes, SeekOrigin.Begin);
                filestream.Write(write_buffer, 0, write_buffer.Length);
            }

        }
        public Page ReadPageFromPrimary(int offset_in_bytes){

            return ReadFromFile(_primary_file_name, offset_in_bytes);
          
        }
        public Page ReadPageFromOverflow(int offset_in_bytes)
        {
            return ReadFromFile(_overflow_file_name, offset_in_bytes);
        }
        public void WritePageToPrimary(int offset_in_bytes, Page page)
        {
            WriteToFile(_primary_file_name, offset_in_bytes, page);
        }
        public void WritePageToOverflow(int offset_in_bytes, Page page)
        {
            WriteToFile(_overflow_file_name, offset_in_bytes, page);
        }

    }
}
