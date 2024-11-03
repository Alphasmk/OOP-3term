using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab05
{
    public abstract partial class Client
    {
        public virtual string Info()
        {
            return ($"Имя: {FIO}, Адрес: {Address}, Баланс: {Balance}, Идентификатор: {Id}");
        }

        public override string ToString()
        {
            return Info();
        }

        public abstract void GetAccountType();

        public void BlockAccount()
        {
            isBlocked = true;
        }

        public void UnblockAccount()
        {
            isBlocked = false;
        }
    }
}
