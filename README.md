# DeskLite

轻量级 Windows 桌面小组件（Phase 1 MVP）。

## 功能

- 无边框透明面板：时间、公历、农历/节气
- 天气：Open-Meteo（默认北京，托盘可改城市）
- 迷你周历：本周 7 天，今天高亮
- 今日待办：本地 JSON 存储
- 系统托盘：显示/隐藏、置顶、开机自启、模块开关
- 双击标题栏：切换鼠标穿透

## 运行

需要 [.NET 8 桌面运行时](https://dotnet.microsoft.com/download/dotnet/8.0)。

```powershell
cd DeskLite
dotnet run
```

## 数据位置

`%AppData%\DeskLite\data.json`  
`%AppData%\DeskLite\settings.json`
