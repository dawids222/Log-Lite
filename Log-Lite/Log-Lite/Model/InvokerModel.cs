﻿namespace Log_Lite.Model
{
    public class InvokerModel : IInvokerModel
    {
        public string Class { get; private set; }
        public string Method { get; private set; }


        public InvokerModel(string @class, string method)
        {
            Class = @class;
            Method = method;
        }

        public override string ToString()
        {
            return $"{Class}.{Method}";
        }
    }
}
