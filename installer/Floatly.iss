#define MyAppName "Floatly"
#define MyAppDisplayName "Floatly（浮岛）"
#define MyAppVersion "2.0.2"
#define MyAppPublisher "cass-2003"
#define MyAppURL "https://github.com/cass-2003/Floatly"
#define MyAppExeName "Floatly.exe"

[Setup]
AppId={{A8F3C2E1-9B4D-4A7E-8F1C-2D5E6A9B0C3D}
AppName={#MyAppDisplayName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppDisplayName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppDisplayName}
AllowNoIcons=yes
OutputDir=..\release
OutputBaseFilename=Floatly-Setup-{#MyAppVersion}
SetupIconFile=..\release\Floatly\app.ico
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
PrivilegesRequired=lowest
DisableProgramGroupPage=yes
ShowLanguageDialog=no

[Languages]
Name: "chinesesimp"; MessagesFile: "ChineseSimplified.isl"

[CustomMessages]
chinesesimp.LaunchProgram=安装完成后运行 {#MyAppDisplayName}

[Tasks]
Name: "desktopicon"; Description: "创建桌面快捷方式"; GroupDescription: "附加任务："; Flags: unchecked

[Files]
Source: "..\release\Floatly\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppDisplayName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\卸载 {#MyAppDisplayName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppDisplayName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram}"; Flags: nowait postinstall skipifsilent

