using System;

namespace App.Common.Message
{
    public class Response
        : IResponse
    {
        public EResponse Type { get; }
        public EError Error { get; }
        public Guid RequestId { get; set; }
        public bool Success => Type == EResponse.Ok;
        public string Text { get; }

        public static Response NotImplemented = new Response(EResponse.NotImplemented);
        public static Response Ok = new Response(EResponse.Ok);
        public static Response Fail = new Response(EResponse.Fail);

        public Response(EResponse response = EResponse.Ok, EError err = EError.Error, string text = "")
        {
            Type = response;
            Error = EError.None;
            Text = text;
        }
    }

    public class Response<TPayload>
        : Response
        , IResponse<TPayload>
    {
        public TPayload Payload { get; }

        public Response(TPayload load, EResponse response = EResponse.Ok, EError err = EError.None)
            : base(response, err)
        {
            Payload = load;
        }
    }
}