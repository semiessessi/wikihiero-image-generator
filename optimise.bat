@echo off
REM Remember to place optipng.exe in the same directory as this batch file
copy recoloured-tuxscribe-hieroglyphs\optipng.exe optipng.exe
set target=%1

IF "%target:~-4%" == ".png" (
    "%~dp0optipng.exe" -nc -o7 -strip "all" %target%
) ELSE (
    FOR /F "tokens=*" %%G IN ('dir %target%\*.png /s /b') DO "%~dp0optipng.exe" -nc -o7 -strip "all" %%G
)