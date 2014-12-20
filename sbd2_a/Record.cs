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
    
        public Record()
        {

        }
        public Record(byte[] _body, int _pointer)
        {
            if (_body.Length > howManyElementsInRecord) { throw new OverflowException("Record is too long!"); }
            this.body = _body;
            this.pointer = _pointer;
            this.key = this.getKeyValue();
        }
        public static byte[] intToBytes(int _val)
        {

            byte[] intBytes = BitConverter.GetBytes(_val);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            byte[] result = intBytes;
            return result;
        }
        public byte[] getRecordByteStream()
        {
            byte[] keyBytes = Record.intToBytes(this.key);
            byte[] pointerBytes = Record.intToBytes(this.pointer);
            byte[] bodyBytes = this.body;
            byte[] buffer = new byte[keyBytes.Length + pointerBytes.Length + bodyBytes.Length];
            System.Buffer.BlockCopy(keyBytes, 0, buffer, 0, keyBytes.Length);
            System.Buffer.BlockCopy(pointerBytes, 0, buffer, keyBytes.Length, pointerBytes.Length);
            System.Buffer.BlockCopy(bodyBytes, 0, buffer, keyBytes.Length + pointerBytes.Length, bodyBytes.Length);
            return buffer;
        }

        public int getKeyValue()
        {

           int count = 0;
            foreach (byte elem in this.body){
                byte val = elem;
                while (val != 0) {
                    if ((val & 0x1) == 0x1) count++;
                    val >>= 1;
                }
            
            }
            return count;
            
        }
        public override string ToString()
        {
            return String.Format("{0}", this.key);
        }
    }
}
