using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniUniversity.Logging
{
    public interface ILogger
    {
        // 로그의 상대적인 중요도를 나타내기 위한 세 가지 추적 수준으로 구분
        void Information(string message);
        void Information(string fmt, params object[] vars);
        void Information(Exception exception, string fmt, params object[] vars);

        void Warning(string message);
        void Warning(string fmt, params object[] vars);
        void Warning(Exception exception, string fmt, params object[] vars);

        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        // 데이터베이스 질의 같은 외부 서비스 호출에 대한 대기 시간 정보를 제공하기 위해 설계된 메서드
        // TraceApi() - SQL 데이터베이스 같은 외부 서비스들에 대한 각각의 호출 대기 시간을 추적
        void TraceApi(string componentName, string method, TimeSpan timespan);
        void TraceApi(string componentName, string method, TimeSpan timespan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars);

    }
}