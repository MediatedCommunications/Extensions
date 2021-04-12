using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http {
    public static class HttpStatusCodeExtensions {

        public static bool IsSuccess(this HttpStatusCode This) {
            return IsSuccess((HttpStatusCode?)This);
        }

        public static bool IsSuccess(this HttpStatusCode? This) {
            var ret = false;
            if(This is { } V1 && (int) V1 >= 200 && (int)V1 <= 299){
                ret = true;
            }

            return ret;
        }

        public static bool IsRedirect(this HttpStatusCode This) {
            return IsRedirect((HttpStatusCode?)This);
        }

        public static bool IsRedirect(this HttpStatusCode? This) {
            var ret = false
                || This == HttpStatusCode.MovedPermanently
                || This == HttpStatusCode.Found
                || This == HttpStatusCode.SeeOther
                || This == HttpStatusCode.TemporaryRedirect
                || This == (HttpStatusCode)308
                ;

            return ret;
        }

        public static bool IsContentRedirect(this HttpStatusCode This) {
            return IsContentRedirect((HttpStatusCode?)This);
        }

        public static bool IsContentRedirect(this HttpStatusCode? This) {
            return false
                || This == HttpStatusCode.TemporaryRedirect
                || This == (HttpStatusCode)308
                ;
        }

    }
}
