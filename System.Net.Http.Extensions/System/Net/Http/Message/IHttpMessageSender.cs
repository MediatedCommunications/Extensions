﻿using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message {
    public interface IHttpMessageSender {
        Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token);
    }

}
