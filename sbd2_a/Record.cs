using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbd2_a
{
    class Record
    {

        private int key;
        private byte[] body;
        private int pointer;
        public static int howManyElementsInRecord = 10;
        public static int recordSizeInBytes = Record.howManyElementsInRecord + sizeof(int) + sizeof(int);

        public Record()
        {

        }
        public Record(byte[] _body, int _pointer, int _key)
        {
            if (_body.Length > howManyElementsInRecord) { throw new OverflowException("Record is too long!"); }
            this.body = _body;
            this.pointer = _pointer;
            this.key = _key;
        }
        public static byte[] numberToBytes(byte _val)
        {

            byte[] result = new byte[1];
            result[0] = _val;
            return result;
        }
        public static byte[] numberToBytes(int _val)
        {
           
            byte[] intBytes = BitConverter.GetBytes(_val);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            byte[] result = intBytes;
            return result;
        }
        public static int FourBytesToInt(byte[] _bytes, int offset)
        {
            byte[] intbytes = new byte[sizeof(int)];
            System.Buffer.BlockCopy(_bytes, offset, intbytes, 0, sizeof(int));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intbytes);
            return BitConverter.ToInt32(intbytes, 0);
        }
        public byte[] getRecordByteStream()
        {
            byte[] keyBytes = Record.numberToBytes(this.key);
            byte[] pointerBytes = Record.numberToBytes(this.pointer);
            byte[] bodyBytes = this.body;
            byte[] buffer = new byte[keyBytes.Length + pointerBytes.Length + bodyBytes.Length];
            System.Buffer.BlockCopy(keyBytes, 0, buffer, 0, keyBytes.Length);
            System.Buffer.BlockCopy(pointerBytes, 0, buffer, keyBytes.Length, pointerBytes.Length);
            System.Buffer.BlockCopy(bodyBytes, 0, buffer, keyBytes.Length + pointerBytes.Length, bodyBytes.Length);
            return buffer;
        }

        public static Record createRecordFromBytes(byte[] _record_in_bytes)
        {
            int key = Record.FourBytesToInt(_record_in_bytes, 0);
            int pointer = Record.FourBytesToInt(_record_in_bytes, 4);
            byte[] record_body = new byte[ Record.howManyElementsInRecord ];
            System.Buffer.BlockCopy(_record_in_bytes, sizeof(int) + sizeof(int), record_body, 0, Record.howManyElementsInRecord);
            Record record = new Record(record_body, pointer, key);
            return record;


        }
        
        public int getKeyValue()
        {
            return this.key;
            /*
           int count = 0;
            foreach (byte elem in this.body){
                byte val = elem;
                while (val != 0) {
                    if ((val & 0x1) == 0x1) count++;
                    val >>= 1;
                }
            
            }
            return count;
            */
        }

        public override string ToString()
        {
            return String.Format("{0} ptr({1})", this.key,this.pointer);
        }
        public static Record generateRandomRecord(Random rnd, int min, int max)
        {
            Record record = null;
            byte[] valueToTest = new byte[10];
            rnd.NextBytes(valueToTest);
            record = new Record(valueToTest, -1,rnd.Next(min, max));
            return record; 
        }
    }
}
