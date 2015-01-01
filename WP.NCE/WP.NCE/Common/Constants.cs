using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NCE.Common
{
    public class Constants
    {
        //
        // MessageId: WININET_E_NAME_NOT_RESOLVED
        //
        // MessageText:
        //
        // The server name or address could not be resolved
        //
        // #define WININET_E_NAME_NOT_RESOLVED      _HRESULT_TYPEDEF_(0x80072EE7L)
        public const string WININET_E_NAME_NOT_RESOLVED = "网络异常，请检查网络设置!";
        //
        // MessageId: WININET_E_TIMEOUT
        //
        // MessageText:
        //
        // The operation timed out
        //
        // #define WININET_E_TIMEOUT                _HRESULT_TYPEDEF_(0x80072EE2L)
        public const string WININET_E_TIMEOUT = "连接超时";
        public const string UNKNOWN_ERROR = "未知错误!";
        public const string NETWORK_CONNECTION = "网络连接";
    }
}
