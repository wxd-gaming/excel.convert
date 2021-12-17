using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Convert
{
    public static class Objects
    {
        public static String CodeString(this String str)
        {
            string code = "";
            string[] vs = str.Replace("=", "_").Replace("-", "_").Split('_');
            foreach (string v in vs)
            {
                if (code.Length < 1)
                    code += FirstLower(v);
                else
                    code += FirstUpper(v);
            }
            return code;
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String FirstLower(this String str)
        {
            if (str == null || str.Length < 1)
            {
                return str;
            }
            char[] chars = str.ToCharArray();
            chars[0] = Char.ToLower(chars[0]);
            str = new String(chars);
            return str;
        }


        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String FirstUpper(this String str)
        {
            if (str == null || str.Length < 1)
            {
                return str;
            }
            char[] chars = str.ToCharArray();
            chars[0] = Char.ToUpper(chars[0]);
            str = new String(chars);
            return str;
        }

    }
}
