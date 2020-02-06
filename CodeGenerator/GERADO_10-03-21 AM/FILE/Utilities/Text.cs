using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Text
/// </summary>
namespace Utilities
{
    public static class Text
    {
        public static string StripDiacritics(string value)
        {
            string formD = value.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char character in formD)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);

                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(character);
                }
            }

            return (stringBuilder.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string StripExtraWhitespaces(string value)
        {
            return Regex.Replace(value.Trim(), "\\s+", " ");
        }

        public static string ToHumanFriendly(string value)
        {
            return Utilities.Text.ToHumanFriendly(value, "-");
        }

        public static string ToHumanFriendly(string value, string separator)
        {
            value = Utilities.Text.StripExtraWhitespaces(value);
            value = Utilities.Text.StripDiacritics(value);
            value = Regex.Replace(value, "[\\W_]+", separator);
            value = value.ToLower();

            return value;
        }

        public static string CriptografiaMD5(string Valor)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(Valor));
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public static string CriptografiaSenha(string Valor)
        {
            return CriptografiaMD5(CriptografiaMD5("VIVA10-2018") + Valor);
        }

		public static string GetIPAddress()
		{
			System.Web.HttpContext context = System.Web.HttpContext.Current;
			string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (!string.IsNullOrEmpty(ipAddress))
			{
				string[] addresses = ipAddress.Split(',');
				if (addresses.Length != 0)
				{
					return addresses[0];
				}
			}
			return context.Request.ServerVariables["REMOTE_ADDR"];
		}

		public static string Right(this string sValue, int iMaxLength)
		{
			if (string.IsNullOrEmpty(sValue))
			{
				sValue = string.Empty;
			}
			else
			{
				if (sValue.Length > iMaxLength)
				{
					sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
				}
			}
			return sValue;
		}

		public static bool IsCNPJ(string cnpj)
		{
			int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int soma;
			int resto;
			string digito;
			string tempCnpj;
			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
			if (cnpj.Length != 14)
				return false;
			tempCnpj = cnpj.Substring(0, 12);
			soma = 0;
			for (int i = 0; i < 12; i++)
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCnpj = tempCnpj + digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();
			return cnpj.EndsWith(digito);
		}

		public static string SHA256(string s)
		{
			var crypt = new SHA256Managed();
			var hash = new StringBuilder();
			byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(s));
			foreach (byte theByte in crypto)
			{
				hash.Append(theByte.ToString("x2"));
			}
			return hash.ToString().ToUpper();
		}

		public static int GetRandomInt()
		{
			using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider()) 
			{
				byte[] rno = new byte[4];
				rg.GetBytes(rno);
				return BitConverter.ToInt32(rno, 0);
			}
		}

		public static bool IsCPF(string cpf)
		{
			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digito;
			int soma;
			int resto;
			cpf = cpf.Trim();
			cpf = cpf.Replace(".", "").Replace("-", "");
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring(0, 9);
			soma = 0;

			for (int i = 0; i < 9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCpf = tempCpf + digito;
			soma = 0;
			for (int i = 0; i < 10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();
			return cpf.EndsWith(digito);
		}
    }
}