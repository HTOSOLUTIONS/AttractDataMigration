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

        public static string PascalToSnake(this string text)
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


        public static string PascalToCamel(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            var firstCharRaw = text[0];

            if (char.IsLower(firstCharRaw))
            {
                return text;
            }

            var firstLowerCasePos = 0;

            StringBuilder sb = new StringBuilder();
            //sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsLower(text[i]))
                {
                    firstLowerCasePos = i;
                    if (i == 1)
                    {
                        sb.Append(char.ToLowerInvariant(text[i - 1]));
                        //var firstCharLower = char.ToLowerInvariant(firstCharRaw);
                        //var fullName = firstCharLower.ToString() + text.Substring(1);
                        firstLowerCasePos = 2;
                    }
                    break;
                } else
                {
                    sb.Append(char.ToLowerInvariant(text[i-1]));
                }
            }
            if (firstLowerCasePos == 0)
            {
                //Never found a Lower case character, convert the entire string
                return text.ToLower();
            }
            else {
                var fullName = sb.ToString() + text.Substring(firstLowerCasePos - 1);
                var namelen = fullName.Length;
                if (namelen > 2 && fullName.Substring(namelen - 2, 2) == "ID")
                {
                    var prevChar = fullName[namelen - 3];
                    if (char.IsLower(prevChar))
                    {
                        fullName = fullName.Substring(0, namelen - 2) + "Id";
                    }
                }

                return fullName;
            }


        }

        public static string PascalToCamelOld(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var firstChar = char.ToLowerInvariant(text[0]);
            var fullName = firstChar.ToString() + text.Substring(1);
            var namelen = fullName.Length;
            if (namelen > 2 && fullName.Substring(namelen - 2, 2) == "ID")
            {
                var prevChar = fullName[namelen - 3];
                if (char.IsLower(prevChar))
                {
                    fullName = fullName.Substring(0, namelen - 2) + "Id";
                }
            }

            return fullName;

        }

        public static string ConvertPascal(this string text, string caseType)
        {
            if (caseType == CaseTypes.SnakeCase)
            {
                return text.PascalToSnake();
            }
            else if (caseType == CaseTypes.CamelCase)
            {
                return text.PascalToCamel();
            }
            return text;    
        }



    }

    public class CaseTypes
    {
        public const string SnakeCase = "S";
        public const string CamelCase = "C";



    }

}
