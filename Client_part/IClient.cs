using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_part
{
    //public interface IClient
    //{
    //    void SendMessage(string message);
    //    void Close();
    //}
    public interface IClient : IDisposable
    {
        void SendMessage(string message);
        void Close();
    }
}
