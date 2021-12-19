﻿
using System.Collections.Generic;

namespace Convert.Tools.Code
{
    public interface IOutPutPlugs
    {

        PlugEnum plugEnum();

        string PlugsName();

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void DoAction(List<string> files);

    }
}