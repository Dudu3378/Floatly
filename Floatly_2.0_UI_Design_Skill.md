# Floatly 2.0 UI Design Skill & Visual Lock

## 产品定位

Floatly 是一个：

> Windows 桌面常驻 Today Dashboard（今日信息条）

核心目标：

- 一眼获取今天最重要的信息
- 不打扰、轻量、可常驻
- 高级感 + 专注感
- 不像后台系统面板
- 更像 Windows 11 / Arc / macOS Widget 的融合

视觉关键词：

```text
Glassmorphism
Fluent 2
Minimal
Elegant
Breathing Space
Productivity
Floating
```

---

# 1. 整体设计原则（最高优先级）

## Rule 1：扫一眼就懂（Glanceable）

用户打开后：

**3 秒内必须获取核心信息。**

首页优先级：

```text
P0 时间 / 日期 / 天气
P1 今日待办
P2 倒数日 / 年进度
P3 番茄钟
P4 速记
P5 月历
```

禁止：

- 首页信息权重平均
- 所有模块一样大
- 文字密度过高

---

## Rule 2：模块化而非后台工具

Floatly 是：

> 漂浮式桌面助手

不是：

> 系统后台控制台

禁止：

❌ 大量边框  
❌ 高对比色块  
❌ 密集列表  
❌ Win10 风格面板感

推荐：

✅ 半透明卡片  
✅ 呼吸留白  
✅ 柔和边框  
✅ 模块折叠

---

## Rule 3：默认折叠，按需展开

首页：

> 只显示最重要信息

复杂模块默认 Compact 模式。

### 黄历

默认：

```text
📅 四月廿六
宜：沟通、推进项目
⌄ 展开
```

展开后：

```text
宜 忌 冲煞 吉神 凶煞 五行
```

---

# 2. 布局规则（Layout System）

采用：

### Smart Grid Layout

```text
┌──────────────┐
│ Hero Header  │
├──────────────┤
│ 黄历入口      │
├──────┬───────┤
│倒数日│年进度 │
├──────┼───────┤
│待办  │番茄钟 │
├──────────────┤
│便签区         │
├──────┬───────┤
│周历  │月历   │
└──────┴───────┘
```

Grid 比例：

```text
100%
100%
50% | 50%
50% | 50%
100%
50% | 50%
```

---

# 3. Hero Header 规则

时间：64~72px

日期：20px

天气：16px

辅助信息：13px

必须：

- 时间是唯一最大元素
- 不允许多个大标题竞争视觉
- 黄历不得抢时间焦点

---

# 4. 卡片设计规则

统一：

- 圆角：18px
- Padding：14~16px
- 模块间距：12px

卡片背景：

```css
background: rgba(22,25,34,.72);
backdrop-filter: blur(32px);
border: 1px solid rgba(255,255,255,.05);
```

高度规则：

### Small Card
120~140px

### Medium Card
280~340px

### Wide Card
140~180px

### Calendar Card
260~320px

---

# 5. 颜色系统

背景：

```css
#131722
```

主蓝：

```css
#5C8DFF
```

节日橙：

```css
#FF8A72
```

绿色：

```css
#55D38A
```

一级文字：

```css
#FFFFFF
```

二级文字：

```css
rgba(255,255,255,.65)
```

---

# 6. 风格锁定（必须）

Use a Fluent 2 + Arc Browser inspired floating desktop dashboard style.

Must follow:

- dark glassmorphism
- soft ambient glow
- rounded cards
- hidden borders
- breathable spacing
- asymmetric smart grid
- strong typography hierarchy
- single-focus cards

Avoid:

- crowded dashboard
- admin panel feeling
- hard borders
- dense information
- bright saturated colors
- too many controls visible at once

---

# 7. 动效规则

动画时长：

```text
180~250ms
```

缓动：

```css
ease-out
```

Hover：

```css
translateY(-2px)
```

Expand：

```css
max-height + opacity
```

禁止夸张缩放。

---

# 最终体验目标

用户看到 Floatly 时：

```text
轻
透
高级
有呼吸感
像系统自带
```

而不是：

```text
后台系统
工具箱
拥挤
廉价毛玻璃
```
