using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public interface ITcpSender
    {
        void Send(byte[] bytes);
    }
}
