using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace POCFunctionToTestTLS
{
    //
    // Summary:
    //     Typed HTTP result wrapper for all http requests.
    //
    // Type parameters:
    //   T:
    //     The data return type.
    public class ClientResult<T> 
    {
        //
        // Summary:
        //     The result from the request.
        public string ResultContent;

        //
        // Summary:
        //     The strongly typed data returned in the HTTP request.
        public T Data;

        //
        // Summary:
        //     Error message if something went wrong.
        public string ExceptionText;

        //
        // Summary:
        //     The HTTP method used in the request.
        public HttpMethod HttpMethod
        {
            get;
            set;
        }

        //
        // Summary:
        //     The HttpStatusCode returned in the result.
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }

        //
        // Summary:
        //     Indicates if the request completed successfully.
        public bool IsSuccessStatusCode
        {
            get;
            set;
        }
    }
}
