using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemo1.Dtos;
using WebApiDemo1.Entities;

namespace WebApiDemo1.Formatters
{
    public class CSVOutputFormatter : TextOutputFormatter
    {

        public CSVOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var sb = new StringBuilder();
            sb.AppendLine("Id-Fullname-SeriaNo-Age-Score");
            if (context.Object is IEnumerable<StudentDto> list)
            {
                foreach ( var item in list)
                {
                    FormatVCard(sb, item);
                }
            }
            else if (context.Object is StudentDto student)
            {
                FormatVCard(sb, student);
            }
            await response.WriteAsync(sb.ToString());
        }




        private void FormatVCard(StringBuilder sb, StudentDto item)
        {
            sb.AppendLine($"{item.Id}-{item.Fullname}-{item.SerioNo}-{item.Age}-{item.Score}");
        }
    }
}
