using Excel.Convert;
using Excel.Convert.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Plugs
{
    public class ProtobufToCSharp : IOutPutPlugs
    {

        string DefaultOutDir = "d:\\out\\protobuf\\cs";

        public PlugEnum plugEnum()
        {
            return PlugEnum.Protobuf;
        }

        public string PlugsName()
        {
            return "Protobuf 导出 C#";
        }

        public void DoAction(List<string> files)
        {
            string basepath = AppDomain.CurrentDomain.BaseDirectory + "Protobuf\\";
            //string basepath = AppDomain.CurrentDomain.BaseDirectory;

            if (!System.IO.Directory.Exists(basepath)) { System.IO.Directory.CreateDirectory(basepath); }

            foreach (var filePath in files)
            {
                File.Copy(filePath, basepath + Path.GetFileName(filePath), true);
                string fileDir = Path.GetDirectoryName(filePath);
                IEnumerable<string> dfiles = Directory.EnumerateFiles(fileDir);
                foreach (var dfile in dfiles)
                {
                    if (dfile.ToLower().EndsWith(".proto"))
                    {
                        File.Copy(dfile, basepath + Path.GetFileName(dfile), true);
                    }
                }
            }

            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.FileName = "Protobuf/protogen.exe";
                //protogen -i:input.proto -o:output.cs  
                //protogen -i:input.proto -o:output.xml -t:xml  
                //protogen -i:input.proto -o:output.cs -p:datacontract -q  
                //protogen -i:input.proto -o:output.cs -p:observable=true  
                string str2 = "";
                foreach (var filePath in files)
                {
                    FormMain.ShowLog("开始处理 Protobuf 文件：" + Path.GetFileName(filePath) + " 生成 CSharp 文件");
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                    string dir = Path.GetDirectoryName(filePath);

                    if (!System.IO.Directory.Exists(DefaultOutDir))
                    {
                        System.IO.Directory.CreateDirectory(DefaultOutDir);
                    }
                    process.StartInfo.Arguments = " -i:" + basepath + Path.GetFileName(filePath) + " -o:" + DefaultOutDir + "/" + fileNameWithoutExtension + ".cs -p:observable=true";

                    process.Start();
                    str2 = process.StandardError.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(str2))
                    {
                        FormMain.ShowLog("C#文件:" + fileNameWithoutExtension + " 错误:" + str2);
                        break;
                    }
                }
                //process.WaitForExit();
                process.Close();
            }

            IEnumerable<string> bDFiles = Directory.EnumerateFiles(basepath);

            foreach (var dfile in bDFiles)
            {
                if (dfile.ToLower().EndsWith(".proto"))
                {
                    File.Delete(dfile);
                }
            }

            //LoadProtoMessage.Instance.LoadCSharpFile(new string[] { CreateBase.protobufnetMessage });
            FormMain.ShowLog("");
            FormMain.ShowLog("");
            FormMain.ShowLog("创建 protobuffer CSharp 代码完成 目录：" + DefaultOutDir);
            FormMain.ShowLog("");
            //System.Diagnostics.Process.Start(CreateBase.protobufnet);
        }

    }
}
