﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Spider
{
    public class GlobalRedirectHandler : DelegatingHandler
    {
        public GlobalRedirectHandler(HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<HttpResponseMessage>();

            base.SendAsync(request, cancellationToken)
                .ContinueWith(t =>
                {
                    HttpResponseMessage response;
                    try
                    {
                        response = t.Result;
                    }
                    catch (Exception e)
                    {
                        response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable) { ReasonPhrase = e.Message };
                    }
                    if (response.StatusCode == HttpStatusCode.MovedPermanently
                        || response.StatusCode == HttpStatusCode.Moved
                        || response.StatusCode == HttpStatusCode.Redirect
                        || response.StatusCode == HttpStatusCode.Found
                        || response.StatusCode == HttpStatusCode.SeeOther
                        || response.StatusCode == HttpStatusCode.RedirectKeepVerb
                        || response.StatusCode == HttpStatusCode.TemporaryRedirect

                        || (int)response.StatusCode == 308)
                    {

                        var newRequest = CopyRequest(response.RequestMessage);

                        if (response.StatusCode == HttpStatusCode.Redirect
                            || response.StatusCode == HttpStatusCode.Found
                            || response.StatusCode == HttpStatusCode.SeeOther)
                        {
                            newRequest.Content = null;
                            newRequest.Method = HttpMethod.Get;

                        }
                        newRequest.RequestUri = response.Headers.Location;

                        SendAsync(newRequest, cancellationToken);
                    }
                    else
                    {
                        tcs.SetResult(response);
                    }
                }, cancellationToken);

            return tcs.Task;
        }

        private static HttpRequestMessage CopyRequest(HttpRequestMessage oldRequest)
        {
            var newrequest = new HttpRequestMessage(oldRequest.Method, oldRequest.RequestUri);

            foreach (var header in oldRequest.Headers)
            {
                newrequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            foreach (var property in oldRequest.Properties)
            {
                newrequest.Properties.Add(property);
            }
            if (oldRequest.Content != null) newrequest.Content = new StreamContent(oldRequest.Content.ReadAsStreamAsync().Result);
            return newrequest;
        }
    }

}
