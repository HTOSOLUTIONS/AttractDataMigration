using System.Text;

namespace DataMigration.Services.HTOTools
{
    public static class CaseConverters
    {


        public static string ToSnakeCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text.ToLowerInvariant();
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string ToSnakeCase2(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 3)
            {
                return text.ToLowerInvariant();
            }

            List<char> backwardsList = new List<char>();
            var txtLen = text.Length;

            var lastChar = text[txtLen - 1];
            bool lastwasunderscore = false;

            backwardsList.Add(char.ToLowerInvariant(lastChar));

            var sb = new StringBuilder();

            for (int i = txtLen - 2 ; i >= 0; --i)
            {
                char c = text[i];
                //|| char.IsUpper(c) && char.IsLower(lastChar)
                if ((char.IsLower(c) && char.IsUpper(lastChar) ) && i > 0 && !lastwasunderscore)
                {
                    backwardsList.Add('_');
                    backwardsList.Add(char.ToLowerInvariant(c));
                    lastwasunderscore = false;
                }
                else if (char.IsUpper(c) && char.IsLower(lastChar) && i > 0 )
                {
                    backwardsList.Add(char.ToLowerInvariant(c));
                    backwardsList.Add('_');
                    lastwasunderscore = true;
                }
                else
                {
                    backwardsList.Add(char.ToLowerInvariant(c));
                    lastwasunderscore = false;
                }
                lastChar = c;
            }
            var reversedArray = backwardsList.ToArray();

            for (int i = reversedArray.Length -1; i >= 0 ; i--)
            {
                sb.Append(backwardsList[i]);
            }
            //var returnString = (sb.ToString()).Replace("__","_");

            return sb.ToString();
        }

    }



}
