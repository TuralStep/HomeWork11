using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemo1.Dtos;
using WebApiDemo1.Entities;

namespace WebApiDemo1.Formatters
{
    public class VCardInputFormatter : TextInputFormatter
    {
        public VCardInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }


        protected override bool CanReadType(Type type)
            => type == typeof(StudentAddDto);


        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            using var reader = new StreamReader(httpContext.Request.Body, encoding);
            var stDto = new StudentAddDto();

            try
            {
                await ReadLineAsync("Fullname", reader, context);

                var data = ReadLineAsync("",reader, context).Result;
                stDto.Fullname = data[0];
                stDto.SerioNo = data[1];
                stDto.Age = int.Parse(data[2]);
                stDto.Score = double.Parse(data[3]);

                return await InputFormatterResult.SuccessAsync(stDto);

            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }

        }

        private static async Task<List<string>> ReadLineAsync(string expected, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();
            
            if(line is null || !line.StartsWith(expected))
            {
                var errMsg = $"Looked for '{expected}' and got '{line}'";

                context.ModelState.TryAddModelError(context.ModelName, errMsg);

                throw new Exception(errMsg);
            }

            var fullName = line.Split("-")[0].Trim();
            var sno = line.Split("-")[1].Trim();
            var age = line.Split("-")[2].Trim();
            var score = line.Split("-")[3].Trim();
            return new List<string>{  fullName, sno, age, score };

        }
    }
}
