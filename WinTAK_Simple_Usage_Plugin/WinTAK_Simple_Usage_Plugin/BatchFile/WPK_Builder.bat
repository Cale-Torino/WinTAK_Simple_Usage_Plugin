::Run App With Args
@ECHO OFF
::cd /D %~dp0
cd /D "C:\Program Files\WinTAK"
cmd /k WpkBuilder.exe -v -i "WinTAK_Simple_Usage_Plugin" -d "WinTAK Simple Usage Plugin By C.A Torino" --plugins64 "C:\Users\User\Documents\GitHub\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin.dll" --toolsData "C:\Users\User\Documents\GitHub\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin.pdb" --icon "C:\Users\User\Documents\GitHub\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin\WinTAK_Simple_Usage_Plugin\Img\tech.png"
pause