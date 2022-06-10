using System.Security.Cryptography;
using System.Text;

namespace GerenciadorDeTarefas.API.Utils
{
    public class MD5Utils
    {
        public static string GerarHashMD5(string input)
        {
            MD5 mD5Hash = MD5.Create();
            var bytes = mD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            foreach (var b in bytes)
            {
                sBuilder.Append(b.ToString("X2"));
            }

            return sBuilder.ToString();
        }
    }
}
