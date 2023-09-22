using StringGenerator;
using System.Text.RegularExpressions;

namespace WebApplication5.Tools
{
    public class TokenGeneratorByStringLib : ITokenGenerator
    {
        public TokenGeneratorByStringLib() {_pattern = @"\d+";  }
        private readonly string _pattern;
        public int Length = 5;
        public string GenerateToken()
        {
           
            var input = new StringGenerator.PseudoRandomStringGenerator().Next(32);
            string result = Regex.Replace(input, _pattern, "");
            if (result.Length >= Length)
               result =  result[..5].ToUpper();
            return result;
        }
    }
}
