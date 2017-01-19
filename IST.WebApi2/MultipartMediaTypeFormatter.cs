using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace FullTextSearchProvider.WebBase.WebApi
{
    public class MultipartMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public MultipartMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/docx"));
        }

        public override bool CanReadType(Type type)
        {
            return true;

        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}
