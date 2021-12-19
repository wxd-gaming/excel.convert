using Convert.Tools;
using Convert.Tools.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Plugs
{
    public class Protobuf2ToJava : IOutPutPlugs
    {

        string DefaultOutDir = "d:\\out\\protobuf\\java";

        public PlugEnum plugEnum()
        {
            return PlugEnum.Protobuf;
        }

        public string PlugsName()
        {
            return "Protobuf 2 导出 Java";
        }

        public void DoAction(List<string> files)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.FileName = "Protobuf/protoc.exe";
                    string in_put_dir = "";
                    foreach (var filePath in files)
                    {
                        string fileName = Path.GetFileName(filePath);
                        FormMain.ShowLog("开始处理 Protobuf 2 文件：" + fileName + " 生成 Java 文件");
                        string fileNameWithoutExtension = Path.GetFileName(filePath);
                        in_put_dir = Path.GetDirectoryName(filePath);

                        if (!System.IO.Directory.Exists(DefaultOutDir))
                        {
                            System.IO.Directory.CreateDirectory(DefaultOutDir);
                        }
                        process.StartInfo.Arguments = "--proto_path=" + in_put_dir + " --java_out=" + DefaultOutDir + "   " + filePath;

                        process.Start();
                        string error = process.StandardError.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(error))
                        {
                            FormMain.ShowLog("java文件:" + fileNameWithoutExtension + "   错误:" + error);
                            return;
                        }
                    }
                    //process.WaitForExit();
                    process.Close();
                    FormMain.ShowLog("");
                    FormMain.ShowLog("");
                    FormMain.ShowLog("创建 protobuf 2 Java 代码完成 目录：" + DefaultOutDir);
                    FormMain.ShowLog("");
                    //System.Diagnostics.Process.Start(CreateBase.protobufjavaMessage);
                }
            }
            catch (Exception e)
            {
                FormMain.ShowLog(e.Message);
            }
        }

    }
}
