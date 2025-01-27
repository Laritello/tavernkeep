@echo off
REM Detecting script directory
set scriptDir=%~dp0

REM Switching to server directory
cd /d "%scriptDir%.."
cd /d "src\Tavernkeep.Server"

echo Server is launching...
dotnet run

REM Switching to client directory
cd /d "%scriptDir%.."
cd /d "src\tavernkeep.client"

echo Installing dependencies...
npm install

echo Client is launching...
npm start

echo Script executed
pause