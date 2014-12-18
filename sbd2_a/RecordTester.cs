using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbd2_a
{
    class RecordTester
    {
        public bool testIt(Record _item, uint _enabled_bits)
        {
            
            //Record item = new Record(Encoding.ASCII.GetBytes("hehe"), 0);
            
            if (_item.key == _enabled_bits)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
