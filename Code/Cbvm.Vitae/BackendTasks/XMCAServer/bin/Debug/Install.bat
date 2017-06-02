@echo off
echo install start....
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /i XMCAServer.exe
Net Start "XMCAServerV1"
pause