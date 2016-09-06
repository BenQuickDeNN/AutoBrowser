using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class Form1 : Form
    {
        private String FilePath;
        private Thread thread_runCommand;
        private object flag_is_document_complete = false;//判断网页是否加载完成
        private String data_for_crawl = "";//用于记录从网页上爬取的数据
        private String[] varMemery = new String[256];//临时开200个存储变量
        private String[] commandMemery;//代码段
        private String[] commandInfo;//代码信息
        private int commandIndex = 0;//代码段指针
        private int commandIndex2 = 0;
        private int commandIndex3 = 0;
        public Form1()
        {
            InitializeComponent();
            initial_widget();
        }
        //控件初始化
        private void initial_widget()
        {
            try
            {
                webBrowser_MainWeb.ScriptErrorsSuppressed = true;//屏蔽页面错误
                webBrowser_MainWeb.AllowNavigation = true;
                button_goWebPage.Click += new EventHandler(btn_GoWebPage_Click);
                toolStripMenuItem_文件_保存指令.Click += new EventHandler(mst_saveFile_Click);
                toolStripMenuItem_文件_另存指令.Click += new EventHandler(mst_saveFileAs_Click);
                toolStripMenuItem_文件_打开指令文件.Click += new EventHandler(mst_openFile_Click);
                toolStripMenuItem_文件_新建指令.Click += new EventHandler(mst_newFile_Click);
                toolStripMenuItem_指令_运行指令.Click += new EventHandler(mst_runCommand_Click);
                toolStripMenuItem_指令_停止运行.Click += new EventHandler(mst_abortThreadRunningCommand_Click);
                openFileDialog_Command.RestoreDirectory = true;
                saveFileDialog_Command.RestoreDirectory = true;
                //初始保存不可用
                toolStripMenuItem_文件_保存指令.Enabled = false;
                toolStripMenuItem_文件_另存指令.Enabled = false;
            }catch(Exception e)
            {
                richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
            }
        }
        //载入页面
        private void btn_GoWebPage_Click(object obj,EventArgs ea)
        {
            try
            {
                flag_is_document_complete = false;
                webBrowser_MainWeb.Navigate(textBox_url.Text);
                //webBrowser_MainWeb.Document.Body.Style = "zoom:30%";
            }
            catch(Exception e)
            {
                richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
            }
        }
        //新建指令文件
        private void mst_newFile_Click(object obj, EventArgs ea)
        {
            try
            {
                if (saveFileDialog_Command.ShowDialog() == DialogResult.OK)
                {
                    richTextBox_CommandWindow.Text = "";
                    FilePath = saveFileDialog_Command.FileName;
                    StreamWriter command_writer = new StreamWriter(saveFileDialog_Command.FileName, false, Encoding.Default);
                    command_writer.Write(richTextBox_CommandWindow.Text);
                    command_writer.Close();
                    toolStripMenuItem_文件_保存指令.Enabled = true;
                    toolStripMenuItem_文件_另存指令.Enabled = true;
                }
            }catch(Exception e)
            {
                richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
            }
        }
        //打开指令文件
        private void mst_openFile_Click(object obj, EventArgs ea)
        {
            try
            {
                if (openFileDialog_Command.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog_Command.FileName;
                    richTextBox_CommandWindow.Text = "";
                    StreamReader command_reader = new StreamReader(FilePath, Encoding.Default);
                    String line = "";
                    while ((line = command_reader.ReadLine()) != null)
                    {
                        richTextBox_CommandWindow.AppendText(line + "\n");
                    }
                    command_reader.Close();
                    toolStripMenuItem_文件_保存指令.Enabled = true;
                    toolStripMenuItem_文件_另存指令.Enabled = true;
                }
            }catch(Exception e)
            {
                richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
            }
        }
        //保存指令文件
        private void mst_saveFile_Click(object obj, EventArgs ea)
        {
            try
            {
                StreamWriter command_writer = new StreamWriter(FilePath, false, Encoding.Default);
                command_writer.Write(richTextBox_CommandWindow.Text);
                command_writer.Close();
            }
            catch(Exception e)
            {
                richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
            }
        }
        //另存指令文件
        private void mst_saveFileAs_Click(object obj, EventArgs ea)
        {
            try
            {
                if (saveFileDialog_Command.ShowDialog() == DialogResult.OK)
                {
                    FilePath = saveFileDialog_Command.FileName;
                    StreamWriter command_writer = new StreamWriter(saveFileDialog_Command.FileName, false, Encoding.Default);
                    command_writer.Write(richTextBox_CommandWindow.Text);
                    command_writer.Close();
                }
            }catch(Exception e)
            {
                richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
            }
        }
        //运行指令
        private void mst_runCommand_Click(object obj, EventArgs ea)
        {
            try
            {
                mst_saveFile_Click(obj, ea);//保存指令文件
                commandIndex = 0;//代码指针清零
                commandMemery = new String[0];
                varMemery = new String[256];
                thread_runCommand = new Thread(runThread_RunningCommand);
                thread_runCommand.Start();//开启执行指令的线程
            }catch(Exception e)
            {
                richTextBox_CommandWindow.AppendText(e.ToString() + "\n");
                return;//一旦有异常，即退出运行
            }
        }
        //终止指令运行
        private void mst_abortThreadRunningCommand_Click(object obj, EventArgs ea)
        {
            try
            {
                if (thread_runCommand != null)
                {
                    thread_runCommand.Abort();
                }
            }catch(Exception e)
            {
                richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
            }
        }
        //指令运行线程
        private void runThread_RunningCommand()
        {
            try
            {
                //统计行数
                StreamReader command_reader = new StreamReader(FilePath, Encoding.Default);
                String line_command = "";
                int command_row = 0;
                while ((line_command = command_reader.ReadLine()) != null)
                {
                    ++command_row;
                }
                commandMemery = new String[command_row];//申请代码存储空间
                commandInfo = new String[command_row];//申请代码信息存储空间
                command_reader.Close();
                //装载指令
                command_reader = new StreamReader(FilePath, Encoding.Default);
                line_command = "";
                command_row = 0;
                while ((line_command = command_reader.ReadLine()) != null)
                {
                    if (command_row < commandMemery.Length)
                    {
                        commandMemery[command_row] = line_command;
                        commandInfo[command_row] = "";
                    }
                    ++command_row;
                }
                command_reader.Close();
                //检查代码正确性
                bool flag_command_allright = true;//记录代码是否有误
                String erroInfo = "";
                for (int i = 0; i < commandMemery.Length; i++)
                {
                    if ((erroInfo = checkCommand(commandMemery[i], i, commandMemery)) != CodeAnalysis.COMMAND_CORRECT)
                    {
                        flag_command_allright = false;
                        commandInfo[i] = "第" + (i + 1) + "行: " + erroInfo;
                    }
                }
                //检查循环体和条件体
                if ((erroInfo = CodeAnalysis.ProgressCommand.checkLoopBody(commandMemery)) != CodeAnalysis.COMMAND_CORRECT)
                {
                    flag_command_allright = false;
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText(erroInfo + "\n");
                    }));
                }
                //检查条件体
                if ((erroInfo = CodeAnalysis.ProgressCommand.checkConditionBody(commandMemery)) != CodeAnalysis.COMMAND_CORRECT)
                {
                    flag_command_allright = false;
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText(erroInfo + "\n");
                    }));
                }
                //输出错误信息并终止运行
                if (!flag_command_allright)
                {
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        try {
                            for (int i = 0; i < commandInfo.Length; i++)
                            {
                                if (commandInfo[i] != "")
                                {
                                    richTextBox_ApplicationInfo.AppendText(commandInfo[i] + "\n");
                                }
                            }
                        }
                        catch
                        {

                        }
                    }));
                    return;
                }
                //执行指令
                while (commandIndex < commandMemery.Length)
                {
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText("running row:" + (commandIndex + 1) + "\n");
                    }));
                    handleByCommand(commandMemery[commandIndex]);
                    ++commandIndex;
                }
                richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                {
                    richTextBox_ApplicationInfo.AppendText("programme terminate!\n");
                }));
            }
            catch(Exception e)
            {
                richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                {
                    richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                }));
                //command_Terminate();
                return;//一旦出现异常，即终止线程
            }
        }
        //根据指令执行相应的操作
        private void handleByCommand(String line)
        {
            try
            {
                String command = CodeAnalysis.getCommandString(line);
                String[] value = CodeAnalysis.getValueString(line);
                switch (command)
                {
                    case CodeAnalysis.ApplicationCommand.COMMAND_CLEAR:
                        command_Clear();
                        return;
                    case CodeAnalysis.ApplicationCommand.COMMAND_CLOSE_APPLICATION:
                        command_CloseApplication();
                        return;
                    case CodeAnalysis.ApplicationCommand.COMMAND_LOAD_WEB_PAGE:
                        command_LoadWebPage(value[0]);
                        return;
                    case CodeAnalysis.ApplicationCommand.COMMAND_SHUT_DOWN:
                        command_ShutDown();
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_BREAK_LOOP:
                        command_BreakLoop();
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_END_IF:
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_END_LOOP:
                        command_EndLoop();
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_IF:
                        command_If(value[0], value[1], value[2]);
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_LOOP:
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_PAUSE:
                        command_Pause(int.Parse(value[0]), value[1]);
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_TERMINATE:
                        command_Terminate();
                        return;
                    case CodeAnalysis.ProgressCommand.COMMAND_WAIT_FOR_DOCUMENT:
                        command_WaitForDocument();
                        return;
                    case CodeAnalysis.VariableCommand.COMMAND_ADD:
                        command_Add(value[0], value[1]);
                        return;
                    case CodeAnalysis.VariableCommand.COMMAND_SET_VAR:
                        command_SetVar(value[0], value[1]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_CLICK_ELEMENT:
                        command_ClickElement(value[0]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_CRAWL_ELEMENT:
                        command_CrawlElement(value[0], value[1]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_RUN_SCRIPT_FUNC:
                        command_RunScriptFunc(value[0], value[1]);
                        return;
                    case CodeAnalysis.VariableCommand.COMMAND_SAVE_VAR:
                        command_SaveVar(value[0], value[1], value[2]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_SET_CHECK_BOX_VALUE:
                        command_SetCheckBoxValue(value[0], value[1]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_SET_ELEMENT_VALUE:
                        command_SetElementValue(value[0], value[1]);
                        return;
                    case CodeAnalysis.VariableCommand.COMMAND_MUL:
                        command_Mul(value[0], value[1]);
                        return;
                    case CodeAnalysis.VariableCommand.COMMAND_CONNECT_VAR:
                        command_connectVar(value[0], value[1]);
                        return;
                    case CodeAnalysis.VariableCommand.COMMAND_LOAD_DATA_TO_VAR:
                        command_LoadDataToVar(value[0], value[1]);
                        return;
                    case CodeAnalysis.VariableCommand.COMMAND_PRINT:
                        command_Print(value[0]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_CRAWL_CONTENT:
                        command_CrawlContent(value[0], value[1], value[2]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_CLICK_ELEMENT_BY_VALUE:
                        command_ClickElementByType(value[0]);
                        return;
                    case CodeAnalysis.WebElementCommand.COMMAND_RUN_PARAM_FUNC:
                        command_RunParamFunc(value[0], value[1], value);
                        return;
                    case "":return;
                }
                richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                {
                    richTextBox_ApplicationInfo.AppendText("undefine command!\n");
                }));
                command_Terminate();
            }
            catch (Exception e)
            {
                richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                {
                    richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                }));
            }
        }
        //指令-清屏
        private void command_Clear()
        {
            richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
            {
                richTextBox_ApplicationInfo.Text = "";
            }));
        }
        //指令-关闭程序
        private void command_CloseApplication()
        {
            Application.Exit();
        }
        //指令-载入网页
        private void command_LoadWebPage(String url)
        {
            webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
            {
                try {
                    lock (flag_is_document_complete)
                    {
                        flag_is_document_complete = false;
                    }
                    if (url.Contains("var["))
                    {
                        int temp_index = int.Parse(CodeAnalysis.getStringBetween(url, "[", "]"));
                        webBrowser_MainWeb.Navigate(varMemery[temp_index]);
                    }
                    else
                    {
                        webBrowser_MainWeb.Navigate(url);
                    }
                }catch(Exception e)
                {
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                    }));
                }
            }));
        }
        //指令-关机
        private void command_ShutDown()
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-p");
        }
        //指令-设置页面元素值
        private void command_SetElementValue(String elementID, String value)
        {
            try {
                HtmlElement element;
                webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
                {
                    try {
                        element = webBrowser_MainWeb.Document.GetElementById(CodeAnalysis.getStringBefore(elementID, '.'));
                        //检查元素的父元素和子元素
                        element = CodeAnalysis.WebElementCommand.getRelation(element, elementID);
                        if (!value.Contains("var["))
                        {
                            element.SetAttribute("value", value);//给元素赋值
                        }
                        else
                        {
                            int temp_index = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));
                            element.SetAttribute("value", varMemery[temp_index]);
                        }
                    }catch(Exception e)
                    {
                        richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                        {
                            richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                        }));
                    }
                }));
            }catch(Exception e)
            {
                richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                {
                    richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                }));
            }
        }
        //指令-点击页面元素
        private void command_ClickElement(String elementID)
        {
            HtmlElement element;
            webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    element = webBrowser_MainWeb.Document.GetElementById(CodeAnalysis.getStringBefore(elementID, '.'));
                    //检查元素的父元素和子元素
                    element = CodeAnalysis.WebElementCommand.getRelation(element, elementID);
                    element.InvokeMember("Click");
                }
                catch (Exception e)
                {
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                    }));
                }
            }));
        }
        //指令-根据元素值点击页面元素
        private void command_ClickElementByType(String elementType)
        {
            webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    HtmlElementCollection allelements = webBrowser_MainWeb.Document.All;
                    foreach (HtmlElement element in allelements)
                    {
                        if (element.GetAttribute("type") == elementType)
                        {
                            element.InvokeMember("Click");
                        }
                    }
                }
                catch (Exception e)
                {
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                    }));
                }
            }));
        }
        //指令-抓取元素的值
        private void command_CrawlElement(String elementID, String varX)
        {
            if (!varX.Contains("var[")) return;
            int memeryIndex = int.Parse(CodeAnalysis.getStringBetween(varX, "[", "]"));//获取变量索引
            HtmlElement element;
            webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    element = webBrowser_MainWeb.Document.GetElementById(CodeAnalysis.getStringBefore(elementID, '.'));
                    //检查元素的父元素和子元素
                    element = CodeAnalysis.WebElementCommand.getRelation(element, elementID);
                    varMemery[memeryIndex] = element.GetAttribute("value").ToString();
                }
                catch (Exception e)
                {
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                    }));
                }
            }));
        }
        //指令-抓取html内容
        private void command_CrawlContent(String keyword1, String keyword2, String value)
        {
            //检查格式
            if (!value.Contains("var[")) return;
            String leftbound = CodeAnalysis.getStringBetween(keyword1, "@@", "^^");
            String rightbound = CodeAnalysis.getStringBetween(keyword2, "@@", "^^");
            int tempindex = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));
            String str_content = "";
            bool flag_hasInfo = false;
                webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            //webBrowser_MainWeb.Refresh();//更新文档
                            //command_WaitForDocument();
                            //webBrowser_MainWeb.Navigate(webBrowser_MainWeb.Url);
                            str_content = webBrowser_MainWeb.DocumentText;
                        }
                    }
                    catch
                    {

                    }
                }));
            
                //逐行读取
                byte[] temp_byte = Encoding.Default.GetBytes(str_content);
                MemoryStream myStream = new MemoryStream(temp_byte);
                //逐行读取，并分析其中的元素
                StreamReader myStreamReader = new StreamReader(myStream, Encoding.Default);
                String line = "";
                while ((line = myStreamReader.ReadLine()) != null)
                {
                    if (line.Contains(leftbound) && line.Contains(rightbound))
                    {
                        flag_hasInfo = true;
                        String tempcontent = CodeAnalysis.getStringBetween(line, leftbound, rightbound);
                        if (tempcontent != "")
                        {
                            varMemery[tempindex] += tempcontent + "\t";
                        }
                    }
                }
                myStreamReader.Close();
                myStream.Close();
        }
        //指令-设置CheckBox元素选中
        private void command_SetCheckBoxValue(String elementID, String value)
        {
            HtmlElement element;
            webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    element = webBrowser_MainWeb.Document.GetElementById(CodeAnalysis.getStringBefore(elementID, '.'));
                    //检查元素的父元素和子元素
                    element = CodeAnalysis.WebElementCommand.getRelation(element, elementID);
                    element.SetAttribute("Checked", value);//设置CheckBox
                }
                catch (Exception e)
                {
                    richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                    }));
                }
            }));
        }
        //指令-执行js函数
        private void command_RunScriptFunc(String funcName, String value)
        {
            if (!value.Contains("var[")) return;
            int tempindex = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));
            webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    webBrowser_MainWeb.Document.InvokeScript(funcName);
                }
                catch 
                {
                }
            }));
        }
        //指令-执行带参的js函数
        private void command_RunParamFunc(String funcName, String value, String[] Param)
        {
            if (!value.Contains("var[")) return;
            if (Param.Length <= 2) return;
            int store_index = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));
            Object[] param_array = new Object[Param.Length - 2];//JS参数
            for(int i = 2; i < Param.Length; i++)
            {
                if (Param[i].Contains("var["))
                {
                    int temp_index = int.Parse(CodeAnalysis.getStringBetween(Param[i], "[", "]"));
                    param_array[i - 2] = varMemery[temp_index];
                }
                else
                {
                    param_array[i - 2] = Param[i];
                }
            }
            webBrowser_MainWeb.Invoke(new MethodInvoker(delegate
            {
                webBrowser_MainWeb.Document.InvokeScript(funcName, param_array);
            }));
        }
        //指令-等待网页加载完毕
        private void command_WaitForDocument()
        {
            lock (flag_is_document_complete)
            {
                while (flag_is_document_complete.Equals(false))
                {
                    Thread.Sleep(20);
                }
            }
        }
        //指令-暂停
        private void command_Pause(int number, String unit)
        {
            switch (unit)
            {
                case "ms":Thread.Sleep(number);break;
                case "s":
                    for(int i=0;i< number; i++)
                    {
                        Thread.Sleep(1000);
                    }
                    break;
                case "min":
                    for(int i=0;i< number; i++)
                    {
                        Thread.Sleep(60000);
                    }
                    break;
                case "h":
                    for(int i=0;i< number; i++)
                    {
                        for(int j = 0; j < 60; j++)
                        {
                            Thread.Sleep(60000);
                        }
                    }
                    break;
            }
        }
        //指令-终止程序
        private void command_Terminate()
        {
            if (thread_runCommand != null) thread_runCommand.Abort();
        }
        //指令-设置变量值
        private void command_SetVar(String index, String value)
        {
            if (!value.Contains("var["))
            {
                if (index.Contains("var["))
                {
                    int memeryIndex = int.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                    varMemery[memeryIndex] = value;
                }
            }
            else
            {
                if (index.Contains("var["))
                {
                    int memeryIndex = int.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                    int memeryIndex2 = int.Parse(CodeAnalysis.getStringBetween(value, "[","]"));
                    varMemery[memeryIndex] = varMemery[memeryIndex2];
                }
            }
        }
        //指令-连接变量
        private void command_connectVar(String index, String value)
        {
            if (!value.Contains("var["))
            {
                if (index.Contains("var["))
                {
                    int memeryIndex = int.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                    varMemery[memeryIndex] += value;
                }
            }
            else
            {
                if (index.Contains("var["))
                {
                    int memeryIndex = int.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                    int memeryIndex2 = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));
                    varMemery[memeryIndex] += varMemery[memeryIndex2];
                }
            }
        }
        //指令-变量值相加
        private void command_Add(String index, String addNum)
        {
            if (!index.Contains("var[")) return;
            if (!addNum.Contains("var["))
            {
                long memeryIndex = int.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                long temp_a = long.Parse(varMemery[memeryIndex]);
                long temp_b = long.Parse(addNum);
                long temp_c = temp_a + temp_b;
                varMemery[memeryIndex] = temp_c.ToString();
            }
            else
            {
                long memeryIndex1 = long.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                long memeryIndex2 = long.Parse(CodeAnalysis.getStringBetween(addNum, "[", "]"));//获取变量索引
                long temp_a = long.Parse(varMemery[memeryIndex1]);
                long temp_b = long.Parse(varMemery[memeryIndex2]);
                long temp_c = temp_a + temp_b;
                varMemery[memeryIndex1] = temp_c.ToString();
            }
        }
        //指令-变量值相乘
        private void command_Mul(String index, String addNum)
        {
            if (!index.Contains("var[")) return;
            if (!addNum.Contains("var["))
            {
                int memeryIndex = int.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                int temp_a = int.Parse(varMemery[memeryIndex]);
                int temp_b = int.Parse(addNum);
                int temp_c = temp_a * temp_b;
                varMemery[memeryIndex] = temp_c.ToString();
            }
            else
            {
                int memeryIndex1 = int.Parse(CodeAnalysis.getStringBetween(index, "[", "]"));//获取变量索引
                int memeryIndex2 = int.Parse(CodeAnalysis.getStringBetween(addNum, "[", "]"));//获取变量索引
                int temp_a = int.Parse(varMemery[memeryIndex1]);
                int temp_b = int.Parse(varMemery[memeryIndex2]);
                int temp_c = temp_a * temp_b;
                varMemery[memeryIndex1] = temp_c.ToString();
            }
        }
        //指令-保存变量值
        private void command_SaveVar(String path, String value, String tag)
        {
            if (!value.Contains("var[")) return;
            int memeryIndex = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));//获取变量索引
            StreamWriter varWriter = new StreamWriter(path, true, Encoding.Default);//不覆盖原文件
            varWriter.Write(varMemery[memeryIndex]);
            switch (tag)
            {
                case "n":varWriter.Write("\n");break;
                case "t":varWriter.Write("\t");break;
                case "d":varWriter.Write(",");break;
            }
            varWriter.Close();
        }
        //指令-载入变量值
        private void command_LoadDataToVar(String path, String value)
        {
            if (!value.Contains("var[")) return;
            StreamReader varReader = new StreamReader(path, Encoding.Default);
            int tempIndex = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));
            varMemery[tempIndex] = varReader.ReadToEnd();
            varReader.Close();
        }
        //指令-显示变量
        private void command_Print(String value)
        {
            if (!value.Contains("var[")) return;
            int tempindex = int.Parse(CodeAnalysis.getStringBetween(value, "[", "]"));
            richTextBox_ApplicationInfo.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    richTextBox_ApplicationInfo.AppendText(varMemery[tempindex] + "\n");
                }
                catch (Exception e)
                {
                    richTextBox_ApplicationInfo.AppendText(e.ToString() + "\n");
                }
            }));
        }
        //指令-循环体结束
        private void command_EndLoop()
        {
            int temp_index = commandIndex;//记住当前指令段的索引
            int endloopcount = 0;//循环体计数器
            while (temp_index > 0)
            {
                --temp_index;
                if (CodeAnalysis.getCommandString(commandMemery[temp_index]) == CodeAnalysis.ProgressCommand.COMMAND_LOOP
                    && endloopcount == 0)
                {
                    commandIndex = temp_index;//使指令指针指向对应的loop语句的下一条
                    break;
                }
                if (CodeAnalysis.getCommandString(commandMemery[temp_index]) == CodeAnalysis.ProgressCommand.COMMAND_END_LOOP) ++endloopcount;
                if (CodeAnalysis.getCommandString(commandMemery[temp_index]) == CodeAnalysis.ProgressCommand.COMMAND_LOOP) --endloopcount;
            }
        }
        //指令-跳出循环体
        private void command_BreakLoop()
        {
            int temp_index = commandIndex;//记住当前指令的索引
            int loopcount = 0;//循环体计数器
            while (temp_index < commandMemery.Length)
            {
                ++temp_index;
                if (CodeAnalysis.getCommandString(commandMemery[temp_index]) == CodeAnalysis.ProgressCommand.COMMAND_END_LOOP
                     && loopcount == 0)
                {
                    commandIndex = temp_index;//下次从endloop后执行
                    break;
                }
                if (CodeAnalysis.getCommandString(commandMemery[temp_index]) == CodeAnalysis.ProgressCommand.COMMAND_LOOP) ++loopcount;
                if (CodeAnalysis.getCommandString(commandMemery[temp_index]) == CodeAnalysis.ProgressCommand.COMMAND_END_LOOP) --loopcount;
            }
        }
        //指令-条件判断
        private void command_If(String value1, String symbol, String value2)
        {
            if (!(value1.Contains("var[") && value2.Contains("var["))) return;//比较的必须是变量
            int tempIndex1 = int.Parse(CodeAnalysis.getStringBetween(value1, "[", "]"));//获取变量索引
            int tempIndex2 = int.Parse(CodeAnalysis.getStringBetween(value2, "[", "]"));
            bool flag_compare_result = false;//比较结果
            switch (symbol)
            {
                case "<":
                    if (int.Parse(varMemery[tempIndex1]) < int.Parse(varMemery[tempIndex2])) flag_compare_result = true;
                    break;
                case "<=":
                    if (int.Parse(varMemery[tempIndex1]) <= int.Parse(varMemery[tempIndex2])) flag_compare_result = true;
                    break;
                case ">=":
                    if (int.Parse(varMemery[tempIndex1]) >= int.Parse(varMemery[tempIndex2])) flag_compare_result = true;
                    break;
                case ">":
                    if (int.Parse(varMemery[tempIndex1]) > int.Parse(varMemery[tempIndex2])) flag_compare_result = true;
                    break;
                case "==":
                    if (varMemery[tempIndex1] == varMemery[tempIndex2]) flag_compare_result = true;
                    break;
                case "contains":
                    if (varMemery[tempIndex1].Contains(varMemery[tempIndex2])) flag_compare_result = true;
                    break;
            }
            if (!flag_compare_result)
            {
                //如果没有一个条件成立，则跳到下方对应的endif
                int tempIndex = commandIndex;
                int ifcount = 0;
                while (commandIndex < commandMemery.Length)
                {
                    ++tempIndex;
                    if(CodeAnalysis.getCommandString(commandMemery[tempIndex]) == CodeAnalysis.ProgressCommand.COMMAND_END_IF
                         && ifcount == 0)
                    {
                        commandIndex = tempIndex;
                        break;
                    }
                    if (CodeAnalysis.getCommandString(commandMemery[tempIndex]) == CodeAnalysis.ProgressCommand.COMMAND_IF) ++ifcount;
                    if (CodeAnalysis.getCommandString(commandMemery[tempIndex]) == CodeAnalysis.ProgressCommand.COMMAND_END_IF) --ifcount;
                }
            }
        }
        //判断html是否载入完成
        private void webBrowser_MainWeb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            flag_is_document_complete = true;
            webBrowser_MainWeb.Document.Body.Style = "zoom:70%";
        }
        //检查代码正确性
        private String checkCommand(String line, int index, String[] memery)
        {
            String command = CodeAnalysis.getCommandString(line);
            String[] value = CodeAnalysis.getValueString(line);
            switch (command)
            {
                case CodeAnalysis.ApplicationCommand.COMMAND_CLEAR:
                    return CodeAnalysis.ApplicationCommand.checkApplicationCommand(command, value);
                case CodeAnalysis.ApplicationCommand.COMMAND_CLOSE_APPLICATION:
                    return CodeAnalysis.ApplicationCommand.checkApplicationCommand(command, value);
                case CodeAnalysis.ApplicationCommand.COMMAND_LOAD_WEB_PAGE:
                    return CodeAnalysis.ApplicationCommand.checkLoadWebPage(command, value);
                case CodeAnalysis.ApplicationCommand.COMMAND_SHUT_DOWN:
                    return CodeAnalysis.ApplicationCommand.checkApplicationCommand(command, value);
                case CodeAnalysis.ProgressCommand.COMMAND_BREAK_LOOP:
                    return CodeAnalysis.ProgressCommand.checkBreakLoop(memery, index);
                case CodeAnalysis.ProgressCommand.COMMAND_END_IF:
                    return CodeAnalysis.COMMAND_CORRECT;
                case CodeAnalysis.ProgressCommand.COMMAND_END_LOOP:
                    return CodeAnalysis.COMMAND_CORRECT;
                case CodeAnalysis.ProgressCommand.COMMAND_IF:
                    return CodeAnalysis.ProgressCommand.checkIfSymbol(command, value);
                case CodeAnalysis.ProgressCommand.COMMAND_LOOP:
                    return CodeAnalysis.COMMAND_CORRECT;
                case CodeAnalysis.ProgressCommand.COMMAND_PAUSE:
                    return CodeAnalysis.ProgressCommand.checkPause(command, value);
                case CodeAnalysis.ProgressCommand.COMMAND_TERMINATE:
                    return CodeAnalysis.COMMAND_CORRECT;
                case CodeAnalysis.ProgressCommand.COMMAND_WAIT_FOR_DOCUMENT:
                    return CodeAnalysis.COMMAND_CORRECT;
                case CodeAnalysis.VariableCommand.COMMAND_ADD:
                    return CodeAnalysis.VariableCommand.checkParameter2(command, value);
                case CodeAnalysis.VariableCommand.COMMAND_SET_VAR:
                    return CodeAnalysis.VariableCommand.checkParameter2(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_CLICK_ELEMENT:
                    return CodeAnalysis.WebElementCommand.checkParameter1(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_CRAWL_ELEMENT:
                    return CodeAnalysis.WebElementCommand.checkCrawlElement(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_RUN_SCRIPT_FUNC:
                    return CodeAnalysis.WebElementCommand.checkRunScriptFunc(command, value);
                case CodeAnalysis.VariableCommand.COMMAND_SAVE_VAR:
                    return CodeAnalysis.VariableCommand.checkSaveVar(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_SET_CHECK_BOX_VALUE:
                    return CodeAnalysis.WebElementCommand.checkParameter2(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_SET_ELEMENT_VALUE:
                    return CodeAnalysis.WebElementCommand.checkParameter2(command, value);
                case CodeAnalysis.VariableCommand.COMMAND_MUL:
                    return CodeAnalysis.VariableCommand.checkParameter2(command, value);
                case CodeAnalysis.VariableCommand.COMMAND_CONNECT_VAR:
                    return CodeAnalysis.VariableCommand.checkParameter2(command, value);
                case CodeAnalysis.VariableCommand.COMMAND_LOAD_DATA_TO_VAR:
                    return CodeAnalysis.VariableCommand.checkLoadDataToVar(command, value);
                case CodeAnalysis.VariableCommand.COMMAND_PRINT:
                    return CodeAnalysis.VariableCommand.checkPrint(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_CRAWL_CONTENT:
                    return CodeAnalysis.WebElementCommand.checkCrawlContent(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_CLICK_ELEMENT_BY_VALUE:
                    return CodeAnalysis.WebElementCommand.checkParameter1(command, value);
                case CodeAnalysis.WebElementCommand.COMMAND_RUN_PARAM_FUNC:
                    return CodeAnalysis.WebElementCommand.checkRunParamFunc(command, value);
                case "": return CodeAnalysis.COMMAND_CORRECT;
            }
            return "未定义的指令“"+command+"”";
        }
    }
}
