/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
Copyright © 2019 chibayuki@foxmail.com

2048
Version 7.1.17000.7114.R19.190525-1400

This file is part of 2048

2048 is released under the GPLv3 license
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace WinFormApp
{
    public partial class Form_Main : Form
    {
        #region 版本信息

        private static readonly string ApplicationName = Application.ProductName; // 程序名。
        private static readonly string ApplicationEdition = "7.1.19"; // 程序版本。

        private static readonly Int32 MajorVersion = new Version(Application.ProductVersion).Major; // 主版本。
        private static readonly Int32 MinorVersion = new Version(Application.ProductVersion).Minor; // 副版本。
        private static readonly Int32 BuildNumber = new Version(Application.ProductVersion).Build; // 版本号。
        private static readonly Int32 BuildRevision = new Version(Application.ProductVersion).Revision; // 修订版本。
        private static readonly string LabString = "R19"; // 分支名。
        private static readonly string BuildTime = "190525-1400"; // 编译时间。

        //

        private static readonly string RootDir_Product = Environment.SystemDirectory.Substring(0, 1) + @":\ProgramData\AppConfig\2048"; // 根目录：此产品。
        private static readonly string RootDir_CurrentVersion = RootDir_Product + "\\" + BuildNumber + "." + BuildRevision; // 根目录：当前版本。

        private static readonly string ConfigFileDir = RootDir_CurrentVersion + @"\Config"; // 配置文件所在目录。
        private static readonly string ConfigFilePath = ConfigFileDir + @"\settings.cfg"; // 配置文件路径。

        private static readonly string LogFileDir = RootDir_CurrentVersion + @"\Log"; // 存档文件所在目录。
        private static readonly string DataFilePath = LogFileDir + @"\userdata.cfg"; // 用户数据文件路径（包含最佳成绩与游戏时长）。
        private static readonly string RecordFilePath = LogFileDir + @"\lastgame.cfg"; // 上次游戏文件路径（包含最后一次游戏记录）。

        //

        private static readonly List<Version> OldVersionList = new List<Version> // 兼容的版本列表，用于从最新的兼容版本迁移配置设置。
        {
            new Version(7, 1, 17000, 0),
            new Version(7, 1, 17000, 220),
            new Version(7, 1, 17000, 290),
            new Version(7, 1, 17000, 600),
            new Version(7, 1, 17000, 648),
            new Version(7, 1, 17000, 825),
            new Version(7, 1, 17000, 1127),
            new Version(7, 1, 17000, 1412),
            new Version(7, 1, 17000, 3868),
            new Version(7, 1, 17000, 4563),
            new Version(7, 1, 17000, 4819),
            new Version(7, 1, 17000, 5033),
            new Version(7, 1, 17000, 5120),
            new Version(7, 1, 17000, 5193),
            new Version(7, 1, 17000, 5459),
            new Version(7, 1, 17000, 7015),
            new Version(7, 1, 17000, 7044)
        };

        //

        private static readonly string URL_GitHub_Base = @"https://github.com/chibayuki/2048"; // 此项目在 GitHub 的 URL。
        private static readonly string URL_GitHub_Release = URL_GitHub_Base + @"/releases/latest"; // 此项目的最新发布版本在 GitHub 的 URL。

        #endregion

        #region 配置设置变量

        private Int32 ElementSize = 160; // 元素边长。

        //

        private static readonly Size Range_MAX = new Size(CAPACITY, CAPACITY); // 最大界面布局。
        private static readonly Size Range_MIN = new Size(2, 2); // 最小界面布局。
        private Size Range = new Size(4, 4); // 当前界面布局（以元素数为单位）。

        //

        private const Int32 Probability_MAX = 100; // 最大概率衰减。
        private const Int32 Probability_MIN = 0; // 最小概率衰减。
        private Int32 Probability = 10; // 当前概率衰减（%）。随机产生一个元素值的概率随其数值的增大按此百分比几何衰减，即随机产生的元素值为 2，4，8，16，… 的相对概率依次为 1，P，P ^ 2，P ^ 3，…（P = Probability%）。

        //

        private enum OperationModes { NULL = -1, Keyboard, MouseClick, TouchSlide, COUNT } // 操作方式枚举。
        private OperationModes OperationMode = OperationModes.Keyboard; // 当前操作方式。

        private bool AlwaysEnableKeyboard = true; // 是否即使选择其他操作方式，键盘仍然可用。

        //

        private bool EnableUndo = false; // 是否允许撤销操作步骤。
        private bool SaveEveryStep = true; // 退出游戏时是否自动保存所有步骤而不是仅最后一步。

        //

        private const Com.WinForm.Theme Theme_DEFAULT = Com.WinForm.Theme.Colorful; // 主题的默认值。

        private bool UseRandomThemeColor = true; // 是否使用随机的主题颜色。

        private static readonly Color ThemeColor_DEFAULT = Color.Gray; // 主题颜色的默认值。

        private const bool ShowFormTitleColor_DEFAULT = true; // 是否显示窗体标题栏的颜色的默认值。

        private const double Opacity_MIN = 0.05; // 总体不透明度的最小值。
        private const double Opacity_MAX = 1.0; // 总体不透明度的最大值。

        //

        private bool AntiAlias = true; // 是否使用抗锯齿模式绘图。

        #endregion

        #region 元素矩阵变量

        private const Int32 CAPACITY = 16; // 元素矩阵容量的平方根。

        private Int32[,] ElementMatrix = new Int32[CAPACITY, CAPACITY]; // 元素矩阵。

        private List<Point> ElementIndexList = new List<Point>(CAPACITY * CAPACITY); // 元素索引列表。

        #endregion

        #region 游戏变量

        private static readonly Size FormClientInitialSize = new Size(585, 420); // 窗体工作区初始大小。

        //

        private Color GameUIBackColor_DEC => Me.RecommendColors.Background_DEC.ToColor(); // 游戏 UI 背景颜色（浅色）。
        private Color GameUIBackColor_INC => Me.RecommendColors.Background_INC.ToColor(); // 游戏 UI 背景颜色（深色）。

        //

        private bool GameIsOver = false; // 游戏是否已经失败。

        //

        private DateTime GameStartingTime = DateTime.Now; // 本次游戏开始时刻。

        private TimeSpan ThisGameTime = TimeSpan.Zero; // 本次游戏时长。
        private TimeSpan TotalGameTime = TimeSpan.Zero; // 累计游戏时长。

        //

        private struct Record // 记录。
        {
            public Size Range; // 布局。
            public Int32 Probability; // 概率衰减（%）。
            public bool EnableUndo; // 是否允许撤销操作步骤。

            public double Score; // 得分。

            public double Max; // 最大值。
            public double Sum; // 求和。
        }

        private Record[,] BestRecordArray = new Record[Range_MAX.Width - Range_MIN.Width + 1, Range_MAX.Height - Range_MIN.Height + 1]; // 最高分记录矩阵。
        private Record BestRecord // 获取或设置当前当前界面布局下的最高分记录。
        {
            get
            {
                return BestRecordArray[Range.Width - Range_MIN.Width, Range.Height - Range_MIN.Height];
            }

            set
            {
                BestRecordArray[Range.Width - Range_MIN.Width, Range.Height - Range_MIN.Height] = value;
            }
        }

        private Record ThisRecord = new Record(); // 本次记录。

        //

        private Record Record_Last = new Record(); // 上次游戏的记录。

        private Int32[,] ElementMatrix_Last = new Int32[CAPACITY, CAPACITY]; // 上次游戏的元素矩阵。

        private List<Point> ElementIndexList_Last = new List<Point>(CAPACITY * CAPACITY); // 上次游戏的元素索引列表。

        private string StepListString = string.Empty; // 上次游戏中表示操作步骤列表的字符串。

        #endregion

        #region 窗体构造

        private Com.WinForm.FormManager Me;

        public Com.WinForm.FormManager FormManager
        {
            get
            {
                return Me;
            }
        }

        private void _Ctor(Com.WinForm.FormManager owner)
        {
            InitializeComponent();

            //

            if (owner != null)
            {
                Me = new Com.WinForm.FormManager(this, owner);
            }
            else
            {
                Me = new Com.WinForm.FormManager(this);
            }

            //

            FormDefine();
        }

        public Form_Main()
        {
            _Ctor(null);
        }

        public Form_Main(Com.WinForm.FormManager owner)
        {
            _Ctor(owner);
        }

        private void FormDefine()
        {
            Me.Caption = ApplicationName;
            Me.FormStyle = Com.WinForm.FormStyle.Sizable;
            Me.EnableFullScreen = true;
            Me.ClientSize = FormClientInitialSize;
            Me.Theme = Theme_DEFAULT;
            Me.ThemeColor = new Com.ColorX(ThemeColor_DEFAULT);
            Me.ShowCaptionBarColor = ShowFormTitleColor_DEFAULT;

            Me.Loading += LoadingEvents;
            Me.Loaded += LoadedEvents;
            Me.Closed += ClosedEvents;
            Me.Resize += ResizeEvents;
            Me.SizeChanged += SizeChangedEvents;
            Me.ThemeChanged += ThemeColorChangedEvents;
            Me.ThemeColorChanged += ThemeColorChangedEvents;

            Me.CloseVerification = CanClose;
        }

        #endregion

        #region 窗体事件

        private void LoadingEvents(object sender, EventArgs e)
        {
            //
            // 在窗体加载时发生。
            //

            TransConfig();

            DelOldConfig();

            LoadConfig();

            LoadUserData();

            LoadLastGame();

            //

            if (UseRandomThemeColor)
            {
                Me.ThemeColor = Com.ColorManipulation.GetRandomColorX();
            }
        }

        private void LoadedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体加载后发生。
            //

            Me.OnSizeChanged();
            Me.OnThemeChanged();

            //

            ComboBox_Range_Width.SelectedIndexChanged -= ComboBox_Range_Width_SelectedIndexChanged;
            ComboBox_Range_Height.SelectedIndexChanged -= ComboBox_Range_Height_SelectedIndexChanged;

            for (int i = Range_MIN.Width; i <= Range_MAX.Width; i++)
            {
                ComboBox_Range_Width.Items.Add(i.ToString());
            }

            for (int i = Range_MIN.Height; i <= Range_MAX.Height; i++)
            {
                ComboBox_Range_Height.Items.Add(i.ToString());
            }

            ComboBox_Range_Width.SelectedIndex = ComboBox_Range_Width.Items.IndexOf(Range.Width.ToString());
            ComboBox_Range_Height.SelectedIndex = ComboBox_Range_Height.Items.IndexOf(Range.Height.ToString());

            ComboBox_Range_Width.SelectedIndexChanged += ComboBox_Range_Width_SelectedIndexChanged;
            ComboBox_Range_Height.SelectedIndexChanged += ComboBox_Range_Height_SelectedIndexChanged;

            //

            ResetInterruptControls();

            //

            RadioButton_Keyboard.CheckedChanged -= RadioButton_Keyboard_CheckedChanged;
            RadioButton_MouseClick.CheckedChanged -= RadioButton_MouseClick_CheckedChanged;
            RadioButton_TouchSlide.CheckedChanged -= RadioButton_TouchSlide_CheckedChanged;

            switch (OperationMode)
            {
                case OperationModes.Keyboard: RadioButton_Keyboard.Checked = true; break;
                case OperationModes.MouseClick: RadioButton_MouseClick.Checked = true; break;
                case OperationModes.TouchSlide: RadioButton_TouchSlide.Checked = true; break;
            }

            RadioButton_Keyboard.CheckedChanged += RadioButton_Keyboard_CheckedChanged;
            RadioButton_MouseClick.CheckedChanged += RadioButton_MouseClick_CheckedChanged;
            RadioButton_TouchSlide.CheckedChanged += RadioButton_TouchSlide_CheckedChanged;

            CheckBox_AlwaysEnableKeyboard.CheckedChanged -= CheckBox_AlwaysEnableKeyboard_CheckedChanged;

            CheckBox_AlwaysEnableKeyboard.Checked = AlwaysEnableKeyboard;

            CheckBox_AlwaysEnableKeyboard.CheckedChanged += CheckBox_AlwaysEnableKeyboard_CheckedChanged;

            //

            CheckBox_EnableUndo.CheckedChanged += CheckBox_EnableUndo_CheckedChanged;

            CheckBox_EnableUndo.Checked = EnableUndo;

            CheckBox_EnableUndo.CheckedChanged += CheckBox_EnableUndo_CheckedChanged;

            ResetSaveOptionsControl();

            ResetSaveStepRadioButtons();

            //

            RadioButton_UseRandomThemeColor.CheckedChanged -= RadioButton_UseRandomThemeColor_CheckedChanged;
            RadioButton_UseCustomColor.CheckedChanged -= RadioButton_UseCustomColor_CheckedChanged;

            if (UseRandomThemeColor)
            {
                RadioButton_UseRandomThemeColor.Checked = true;
            }
            else
            {
                RadioButton_UseCustomColor.Checked = true;
            }

            RadioButton_UseRandomThemeColor.CheckedChanged += RadioButton_UseRandomThemeColor_CheckedChanged;
            RadioButton_UseCustomColor.CheckedChanged += RadioButton_UseCustomColor_CheckedChanged;

            Label_ThemeColorName.Enabled = !UseRandomThemeColor;

            //

            CheckBox_AntiAlias.CheckedChanged -= CheckBox_AntiAlias_CheckedChanged;

            CheckBox_AntiAlias.Checked = AntiAlias;

            CheckBox_AntiAlias.CheckedChanged += CheckBox_AntiAlias_CheckedChanged;

            //

            Label_ApplicationName.Text = ApplicationName;
            Label_ApplicationEdition.Text = ApplicationEdition;
            Label_Version.Text = "版本: " + MajorVersion + "." + MinorVersion + "." + BuildNumber + "." + BuildRevision;

            //

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_StartNewGame, Label_StartNewGame_Click);
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_ContinueLastGame, Label_ContinueLastGame_Click);

            //

            FunctionAreaTab = FunctionAreaTabs.Start;
        }

        private void ClosedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体关闭后发生。
            //

            SaveConfig();

            if (GameUINow)
            {
                Interrupt(InterruptActions.CloseApp);
            }
        }

        private void ResizeEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的大小调整时发生。
            //

            Panel_FunctionArea.Size = Panel_GameUI.Size = Panel_Client.Size = Panel_Main.Size;

            Panel_FunctionAreaOptionsBar.Size = new Size(Panel_FunctionArea.Width / 3, Panel_FunctionArea.Height);
            Label_Tab_Start.Size = Label_Tab_Record.Size = Label_Tab_Options.Size = Label_Tab_About.Size = new Size(Panel_FunctionAreaOptionsBar.Width, Panel_FunctionAreaOptionsBar.Height / 4);
            Label_Tab_Record.Top = Label_Tab_Start.Bottom;
            Label_Tab_Options.Top = Label_Tab_Record.Bottom;
            Label_Tab_About.Top = Label_Tab_Options.Bottom;

            Panel_FunctionAreaTab.Left = Panel_FunctionAreaOptionsBar.Right;
            Panel_FunctionAreaTab.Size = new Size(Panel_FunctionArea.Width - Panel_FunctionAreaOptionsBar.Width, Panel_FunctionArea.Height);

            Func<Control, Control, Size> GetTabSize = (Tab, Container) => new Size(Container.Width - (Container.Height < Tab.MinimumSize.Height ? 25 : 0), Container.Height - (Container.Width < Tab.MinimumSize.Width ? 25 : 0));

            Panel_Tab_Start.Size = GetTabSize(Panel_Tab_Start, Panel_FunctionAreaTab);
            Panel_Tab_Record.Size = GetTabSize(Panel_Tab_Record, Panel_FunctionAreaTab);
            Panel_Tab_Options.Size = GetTabSize(Panel_Tab_Options, Panel_FunctionAreaTab);
            Panel_Tab_About.Size = GetTabSize(Panel_Tab_About, Panel_FunctionAreaTab);

            //

            Panel_EnterGameSelection.Location = new Point((Panel_Tab_Start.Width - Panel_EnterGameSelection.Width) / 2, (Panel_Tab_Start.Height - Panel_EnterGameSelection.Height) / 2);

            Panel_Score.Width = Panel_Tab_Record.Width - Panel_Score.Left * 2;
            Panel_Score.Height = Panel_Tab_Record.Height - Panel_Score.Top * 2 - Panel_GameTime.Height;
            Panel_GameTime.Width = Panel_Tab_Record.Width - Panel_GameTime.Left * 2;
            Panel_GameTime.Top = Panel_Score.Bottom;
            Label_ThisRecord.Location = new Point(Math.Max(0, Math.Min(Panel_Score.Width - Label_ThisRecord.Width, (Panel_Score.Width / 2 - Label_ThisRecord.Width) / 2)), Panel_Score.Height - 25 - Label_ThisRecord.Height);
            Label_BestRecord.Location = new Point(Math.Max(0, Math.Min(Panel_Score.Width - Label_BestRecord.Width, Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Label_BestRecord.Width) / 2)), Panel_Score.Height - 25 - Label_BestRecord.Height);

            Panel_Range.Width = Panel_Tab_Options.Width - Panel_Range.Left * 2;
            Panel_Probability.Width = Panel_Tab_Options.Width - Panel_Probability.Left * 2;
            Panel_OperationMode.Width = Panel_Tab_Options.Width - Panel_OperationMode.Left * 2;
            Panel_Save.Width = Panel_Tab_Options.Width - Panel_Save.Left * 2;
            Panel_ThemeColor.Width = Panel_Tab_Options.Width - Panel_ThemeColor.Left * 2;
            Panel_AntiAlias.Width = Panel_Tab_Options.Width - Panel_AntiAlias.Left * 2;

            //

            Panel_Current.Width = Panel_GameUI.Width;

            Panel_Interrupt.Left = Panel_Current.Width - Panel_Interrupt.Width;

            Panel_Environment.Size = new Size(Panel_GameUI.Width, Panel_GameUI.Height - Panel_Environment.Top);
        }

        private void SizeChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的大小更改时发生。
            //

            if (Panel_GameUI.Visible)
            {
                ElementSize = Math.Max(1, Math.Min(Panel_Environment.Width / Range.Width, Panel_Environment.Height / Range.Height));

                EAryBmpRect.Size = new Size(Math.Max(1, ElementSize * Range.Width), Math.Max(1, ElementSize * Range.Height));
                EAryBmpRect.Location = new Point((Panel_Environment.Width - EAryBmpRect.Width) / 2, (Panel_Environment.Height - EAryBmpRect.Height) / 2);

                RepaintCurBmp();

                ElementMatrix_RepresentAll();
            }

            if (Panel_FunctionArea.Visible && FunctionAreaTab == FunctionAreaTabs.Record)
            {
                Panel_Tab_Record.Refresh();
            }
        }

        private void ThemeColorChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的主题色更改时发生。
            //

            // 功能区选项卡

            Panel_FunctionArea.BackColor = Me.RecommendColors.Background_DEC.ToColor();
            Panel_FunctionAreaOptionsBar.BackColor = Me.RecommendColors.Main.ToColor();

            FunctionAreaTab = _FunctionAreaTab;

            // "记录"区域

            Label_ThisRecord.ForeColor = Label_BestRecord.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_ThisRecordVal_Score.ForeColor = Label_BestRecordVal_Score.ForeColor = Me.RecommendColors.Text_INC.ToColor();
            Label_ThisRecordVal_MaxAndSum.ForeColor = Label_BestRecordVal_MaxAndSum.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_ThisTime.ForeColor = Label_TotalTime.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_ThisTimeVal.ForeColor = Label_TotalTimeVal.ForeColor = Me.RecommendColors.Text_INC.ToColor();

            // "选项"区域

            Label_Range.ForeColor = Label_Probability.ForeColor = Label_OperationMode.ForeColor = Label_Save.ForeColor = Label_ThemeColor.ForeColor = Label_AntiAlias.ForeColor = Me.RecommendColors.Text_INC.ToColor();

            Label_Range_Width.ForeColor = Label_Range_Height.ForeColor = Me.RecommendColors.Text.ToColor();

            ComboBox_Range_Width.BackColor = ComboBox_Range_Height.BackColor = Me.RecommendColors.MenuItemBackground.ToColor();
            ComboBox_Range_Width.ForeColor = ComboBox_Range_Height.ForeColor = Me.RecommendColors.MenuItemText.ToColor();

            Label_Probability_Note_Part1.ForeColor = Label_Probability_Note_Part3.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_Probability_Note_Part2.ForeColor = Me.RecommendColors.Text_INC.ToColor();
            Label_Probability_Value.ForeColor = Me.RecommendColors.Text.ToColor();

            Panel_ProbabilityAdjustment.BackColor = Panel_FunctionArea.BackColor;

            RadioButton_Keyboard.ForeColor = RadioButton_MouseClick.ForeColor = RadioButton_TouchSlide.ForeColor = Me.RecommendColors.Text.ToColor();

            CheckBox_AlwaysEnableKeyboard.ForeColor = Me.RecommendColors.Text.ToColor();

            CheckBox_EnableUndo.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_EnableUndo_Info.ForeColor = Me.RecommendColors.Text.ToColor();

            RadioButton_SaveEveryStep.ForeColor = RadioButton_SaveLastStep.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_TooSlow.ForeColor = Label_CleanGameStep.ForeColor = Label_CleanGameStepDone.ForeColor = Me.RecommendColors.Text.ToColor();

            RadioButton_UseRandomThemeColor.ForeColor = RadioButton_UseCustomColor.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_ThemeColorName.Text = Com.ColorManipulation.GetColorName(Me.ThemeColor.ToColor());
            Label_ThemeColorName.ForeColor = Me.RecommendColors.Text.ToColor();

            CheckBox_AntiAlias.ForeColor = Me.RecommendColors.Text.ToColor();

            // "关于"区域

            Label_ApplicationName.ForeColor = Me.RecommendColors.Text_INC.ToColor();
            Label_ApplicationEdition.ForeColor = Label_Version.ForeColor = Label_Copyright.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_GitHub_Part1.ForeColor = Label_GitHub_Base.ForeColor = Label_GitHub_Part2.ForeColor = Label_GitHub_Release.ForeColor = Me.RecommendColors.Text.ToColor();

            // 控件替代

            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Undo, PictureBox_Undo_Click, null, PictureBox_Undo_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Redo, PictureBox_Redo_Click, null, PictureBox_Redo_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Restart, PictureBox_Restart_Click, null, PictureBox_Restart_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_ExitGame, PictureBox_ExitGame_Click, null, PictureBox_ExitGame_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_CleanGameStep, Label_CleanGameStep_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_ThemeColorName, Label_ThemeColorName_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GitHub_Base, Label_GitHub_Base_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GitHub_Release, Label_GitHub_Release_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));

            // 中断按钮图像

            InterruptImages.Update(Me.RecommendColors.Text.ToColor());

            PictureBox_Undo.Image = InterruptImages.Undo;
            PictureBox_Redo.Image = InterruptImages.Redo;
            PictureBox_Restart.Image = InterruptImages.Restart;
            PictureBox_ExitGame.Image = InterruptImages.ExitGame;
        }

        //

        private bool CanClose(EventArgs e)
        {
            //
            // 用于验证是否允许窗口关闭的方法。
            //

            return (!BackgroundWorker_LoadGameStep.IsBusy && !BackgroundWorker_SaveGameStep.IsBusy);
        }

        #endregion

        #region 背景绘图

        private void Panel_FunctionAreaOptionsBar_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_FunctionAreaOptionsBar 绘图。
            //

            Graphics Grap = e.Graphics;
            Grap.SmoothingMode = SmoothingMode.AntiAlias;

            //

            Control[] TabCtrl = new Control[(Int32)FunctionAreaTabs.COUNT] { Label_Tab_Start, Label_Tab_Record, Label_Tab_Options, Label_Tab_About };

            List<bool> TabBtnPointed = new List<bool>(TabCtrl.Length);
            List<bool> TabBtnSeld = new List<bool>(TabCtrl.Length);

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                TabBtnPointed.Add(Com.Geometry.CursorIsInControl(TabCtrl[i]));
                TabBtnSeld.Add(FunctionAreaTab == (FunctionAreaTabs)i);
            }

            Color TabBtnCr_Bk_Pointed = Color.FromArgb(128, Color.White), TabBtnCr_Bk_Seld = Color.FromArgb(192, Color.White), TabBtnCr_Bk_Uns = Color.FromArgb(64, Color.White);

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                Color TabBtnCr_Bk = (TabBtnSeld[i] ? TabBtnCr_Bk_Seld : (TabBtnPointed[i] ? TabBtnCr_Bk_Pointed : TabBtnCr_Bk_Uns));

                GraphicsPath Path_TabBtn = new GraphicsPath();
                Path_TabBtn.AddRectangle(TabCtrl[i].Bounds);
                PathGradientBrush PGB_TabBtn = new PathGradientBrush(Path_TabBtn)
                {
                    CenterColor = Color.FromArgb(TabBtnCr_Bk.A / 2, TabBtnCr_Bk),
                    SurroundColors = new Color[] { TabBtnCr_Bk },
                    FocusScales = new PointF(1F, 0F)
                };
                Grap.FillPath(PGB_TabBtn, Path_TabBtn);
                Path_TabBtn.Dispose();
                PGB_TabBtn.Dispose();

                if (TabBtnSeld[i])
                {
                    PointF[] Polygon = new PointF[] { new PointF(TabCtrl[i].Right, TabCtrl[i].Top + TabCtrl[i].Height / 4), new PointF(TabCtrl[i].Right - TabCtrl[i].Height / 4, TabCtrl[i].Top + TabCtrl[i].Height / 2), new PointF(TabCtrl[i].Right, TabCtrl[i].Bottom - TabCtrl[i].Height / 4) };

                    Grap.FillPolygon(new SolidBrush(Panel_FunctionArea.BackColor), Polygon);
                }
            }
        }

        private void Panel_Score_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Score 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = PictureBox_Score;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }

            //

            PaintScore(e);
        }

        private void Panel_GameTime_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_GameTime 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = PictureBox_GameTime;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_Range_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Range 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Range;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_Probability_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Probability 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Probability;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_OperationMode_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_OperationMode 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_OperationMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_Save_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Save 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Save;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_ThemeColor_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_ThemeColor 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_ThemeColor;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_AntiAlias_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_AntiAlias 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_AntiAlias;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width - Ctrl.Left, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 配置设置

        private void TransConfig()
        {
            //
            // 从当前内部版本号下最近的旧版本迁移配置文件。
            //

            try
            {
                if (!Directory.Exists(RootDir_CurrentVersion))
                {
                    if (OldVersionList.Count > 0)
                    {
                        List<Version> OldVersionList_Copy = new List<Version>(OldVersionList);
                        List<Version> OldVersionList_Sorted = new List<Version>(OldVersionList_Copy.Count);

                        while (OldVersionList_Copy.Count > 0)
                        {
                            Version LatestVersion = OldVersionList_Copy[0];

                            foreach (Version Ver in OldVersionList_Copy)
                            {
                                if (LatestVersion <= Ver)
                                {
                                    LatestVersion = Ver;
                                }
                            }

                            OldVersionList_Sorted.Add(LatestVersion);
                            OldVersionList_Copy.Remove(LatestVersion);
                        }

                        for (int i = 0; i < OldVersionList_Sorted.Count; i++)
                        {
                            string Dir = RootDir_Product + "\\" + OldVersionList_Sorted[i].Build + "." + OldVersionList_Sorted[i].Revision;

                            if (Directory.Exists(Dir))
                            {
                                try
                                {
                                    Com.IO.CopyFolder(Dir, RootDir_CurrentVersion, true, true, true);

                                    break;
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void DelOldConfig()
        {
            //
            // 删除当前内部版本号下所有旧版本的配置文件。
            //

            try
            {
                if (OldVersionList.Count > 0)
                {
                    foreach (Version Ver in OldVersionList)
                    {
                        string Dir = RootDir_Product + "\\" + Ver.Build + "." + Ver.Revision;

                        if (Directory.Exists(Dir))
                        {
                            try
                            {
                                Directory.Delete(Dir, true);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void LoadConfig()
        {
            //
            // 加载配置文件。
            //

            if (File.Exists(ConfigFilePath))
            {
                if (new FileInfo(ConfigFilePath).Length > 0)
                {
                    StreamReader Read = new StreamReader(ConfigFilePath, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    Regex RegexUint = new Regex(@"[^0-9]");
                    Regex RegexFloat = new Regex(@"[^0-9\-\.]");

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<ElementSize>", "</ElementSize>", false, false), string.Empty);

                        ElementSize = Convert.ToInt32(SubStr);
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<Range>", "</Range>", false, false);

                        string[] Fields = SubStr.Split(',');

                        if (Fields.Length == 2)
                        {
                            int i = 0;

                            string StrW = RegexUint.Replace(Fields[i++], string.Empty);
                            string StrH = RegexUint.Replace(Fields[i++], string.Empty);

                            Size R = new Size(Convert.ToInt32(StrW), Convert.ToInt32(StrH));

                            if (R.Width >= Range_MIN.Width && R.Width <= Range_MAX.Width && R.Height >= Range_MIN.Height && R.Height <= Range_MAX.Height)
                            {
                                Range = R;
                            }
                        }
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Probability>", "</Probability>", false, false), string.Empty);

                        Int32 P = Convert.ToInt32(SubStr);

                        if (P >= Probability_MIN && P <= Probability_MAX)
                        {
                            Probability = P;
                        }
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<OperationMode>", "</OperationMode>", false, false);

                        foreach (object Obj in Enum.GetValues(typeof(OperationModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                OperationMode = (OperationModes)Obj;

                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<AlwaysEnableKeyboard>", "</AlwaysEnableKeyboard>", false, false).Contains((!AlwaysEnableKeyboard).ToString()))
                    {
                        AlwaysEnableKeyboard = !AlwaysEnableKeyboard;
                    }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<EnableUndo>", "</EnableUndo>", false, false).Contains((!EnableUndo).ToString()))
                    {
                        EnableUndo = !EnableUndo;
                    }

                    if (Com.Text.GetIntervalString(Cfg, "<SaveEveryStep>", "</SaveEveryStep>", false, false).Contains((!SaveEveryStep).ToString()))
                    {
                        SaveEveryStep = !SaveEveryStep;
                    }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<Theme>", "</Theme>", false, false);

                        foreach (object Obj in Enum.GetValues(typeof(Com.WinForm.Theme)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Me.Theme = (Com.WinForm.Theme)Obj;

                                break;
                            }
                        }
                    }
                    catch { }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<UseRandomThemeColor>", "</UseRandomThemeColor>", false, false).Contains((!UseRandomThemeColor).ToString()))
                    {
                        UseRandomThemeColor = !UseRandomThemeColor;
                    }

                    if (!UseRandomThemeColor)
                    {
                        try
                        {
                            string SubStr = Com.Text.GetIntervalString(Cfg, "<ThemeColor>", "</ThemeColor>", false, false);

                            string[] Fields = SubStr.Split(',');

                            if (Fields.Length == 3)
                            {
                                int i = 0;

                                string StrR = RegexUint.Replace(Fields[i++], string.Empty);
                                Int32 TC_R = Convert.ToInt32(StrR);

                                string StrG = RegexUint.Replace(Fields[i++], string.Empty);
                                Int32 TC_G = Convert.ToInt32(StrG);

                                string StrB = RegexUint.Replace(Fields[i++], string.Empty);
                                Int32 TC_B = Convert.ToInt32(StrB);

                                Me.ThemeColor = Com.ColorX.FromRGB(TC_R, TC_G, TC_B);
                            }
                        }
                        catch { }
                    }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<ShowFormTitleColor>", "</ShowFormTitleColor>", false, false).Contains((!Me.ShowCaptionBarColor).ToString()))
                    {
                        Me.ShowCaptionBarColor = !Me.ShowCaptionBarColor;
                    }

                    //

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Opacity>", "</Opacity>", false, false), string.Empty);

                        double Op = Convert.ToDouble(SubStr);

                        if (Op >= Opacity_MIN && Op <= Opacity_MAX)
                        {
                            Me.Opacity = Op;
                        }
                    }
                    catch { }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<AntiAlias>", "</AntiAlias>", false, false).Contains((!AntiAlias).ToString()))
                    {
                        AntiAlias = !AntiAlias;
                    }
                }
            }
        }

        private void SaveConfig()
        {
            //
            // 保存配置文件。
            //

            string Cfg = string.Empty;

            Cfg += "<Config>";

            Cfg += "<ElementSize>" + ElementSize + "</ElementSize>";
            Cfg += "<Range>(" + Range.Width + "," + Range.Height + ")</Range>";
            Cfg += "<Probability>" + Probability + "%</Probability>";
            Cfg += "<OperationMode>" + OperationMode + "</OperationMode>";
            Cfg += "<AlwaysEnableKeyboard>" + AlwaysEnableKeyboard + "</AlwaysEnableKeyboard>";
            Cfg += "<EnableUndo>" + EnableUndo + "</EnableUndo>";
            Cfg += "<SaveEveryStep>" + SaveEveryStep + "</SaveEveryStep>";

            Cfg += "<Theme>" + Me.Theme.ToString() + "</Theme>";
            Cfg += "<UseRandomThemeColor>" + UseRandomThemeColor + "</UseRandomThemeColor>";
            Cfg += "<ThemeColor>(" + Me.ThemeColor.ToColor().R + ", " + Me.ThemeColor.ToColor().G + ", " + Me.ThemeColor.ToColor().B + ")</ThemeColor>";
            Cfg += "<ShowFormTitleColor>" + Me.ShowCaptionBarColor + "</ShowFormTitleColor>";
            Cfg += "<Opacity>" + Me.Opacity + "</Opacity>";

            Cfg += "<AntiAlias>" + AntiAlias + "</AntiAlias>";

            Cfg += "</Config>";

            //

            try
            {
                if (!Directory.Exists(ConfigFileDir))
                {
                    Directory.CreateDirectory(ConfigFileDir);
                }

                StreamWriter Write = new StreamWriter(ConfigFilePath, false);
                Write.WriteLine(Cfg);
                Write.Close();
            }
            catch { }
        }

        #endregion

        #region 存档管理

        // 用户数据。

        private void LoadUserData()
        {
            //
            // 加载用户数据。
            //

            if (File.Exists(DataFilePath))
            {
                FileInfo FInfo = new FileInfo(DataFilePath);

                if (FInfo.Length > 0)
                {
                    StreamReader SR = new StreamReader(DataFilePath, false);
                    string Str = SR.ReadLine();
                    SR.Close();

                    Regex RegexUint = new Regex(@"[^0-9]");
                    Regex RegexFloatExp = new Regex(@"[^0-9E\+\-\.]");

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Str, "<TotalGameTime>", "</TotalGameTime>", false, false), string.Empty);

                        TotalGameTime = TimeSpan.FromMilliseconds(Convert.ToInt64(SubStr));
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Str, "<BestRecord>", "</BestRecord>", false, false);

                        while (SubStr.Contains("(") && SubStr.Contains(")"))
                        {
                            try
                            {
                                string StrRec = Com.Text.GetIntervalString(SubStr, "(", ")", false, false);

                                string[] Fields = StrRec.Split(',');

                                if (Fields.Length == 5)
                                {
                                    int i = 0;

                                    Record Rec = new Record();

                                    string StrRW = RegexUint.Replace(Fields[i++], string.Empty);
                                    Rec.Range.Width = Convert.ToInt32(StrRW);

                                    string StrRH = RegexUint.Replace(Fields[i++], string.Empty);
                                    Rec.Range.Height = Convert.ToInt32(StrRH);

                                    string StrScore = RegexFloatExp.Replace(Fields[i++], string.Empty);
                                    Rec.Score = Convert.ToDouble(StrScore);

                                    string StrMax = RegexFloatExp.Replace(Fields[i++], string.Empty);
                                    Rec.Max = Convert.ToDouble(StrMax);

                                    string StrSum = RegexFloatExp.Replace(Fields[i++], string.Empty);
                                    Rec.Sum = Convert.ToDouble(StrSum);

                                    if ((Rec.Range.Width >= Range_MIN.Width && Rec.Range.Width <= Range_MAX.Width) && (Rec.Range.Height >= Range_MIN.Height && Rec.Range.Height <= Range_MAX.Height) && Rec.Score >= 0 && Rec.Max >= 0 && Rec.Sum >= 0)
                                    {
                                        BestRecordArray[Rec.Range.Width - Range_MIN.Width, Rec.Range.Height - Range_MIN.Height] = Rec;
                                    }
                                }
                            }
                            catch { }

                            SubStr = SubStr.Substring(SubStr.IndexOf(")") + (")").Length);
                        }
                    }
                    catch { }
                }
            }
        }

        private void SaveUserData()
        {
            //
            // 保存用户数据。
            //

            if (!EnableUndo && (BestRecord.Score < ThisRecord.Score || (BestRecord.Score == ThisRecord.Score && BestRecord.Max < ThisRecord.Max)))
            {
                BestRecord = ThisRecord;
            }

            //

            string Str = string.Empty;

            Str += "<Log>";

            Str += "<TotalGameTime>" + (Int64)TotalGameTime.TotalMilliseconds + "</TotalGameTime>";

            Str += "<BestRecord>[";
            for (int w = Range_MIN.Width; w <= Range_MAX.Width; w++)
            {
                for (int h = Range_MIN.Height; h <= Range_MAX.Height; h++)
                {
                    Record Rec = BestRecordArray[w - Range_MIN.Width, h - Range_MIN.Height];

                    if (Rec.Score > 0)
                    {
                        Str += "(" + Rec.Range.Width + "," + Rec.Range.Height + "," + Rec.Score + "," + Rec.Max + "," + Rec.Sum + ")";
                    }
                }
            }
            Str += "]</BestRecord>";

            Str += "</Log>";

            //

            try
            {
                if (!Directory.Exists(LogFileDir))
                {
                    Directory.CreateDirectory(LogFileDir);
                }

                StreamWriter SW = new StreamWriter(DataFilePath, false);
                SW.WriteLine(Str);
                SW.Close();
            }
            catch { }
        }

        // 上次游戏。

        private void LoadLastGame()
        {
            //
            // 加载上次游戏。
            //

            if (File.Exists(RecordFilePath))
            {
                FileInfo FInfo = new FileInfo(RecordFilePath);

                if (FInfo.Length > 0)
                {
                    StreamReader SR = new StreamReader(RecordFilePath, false);
                    string Str = SR.ReadLine();
                    SR.Close();

                    Regex RegexUint = new Regex(@"[^0-9]");
                    Regex RegexFloatExp = new Regex(@"[^0-9E\+\-\.]");

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Str, "<Range>", "</Range>", false, false);

                        string[] Fields = SubStr.Split(',');

                        if (Fields.Length == 2)
                        {
                            int i = 0;

                            string StrW = RegexUint.Replace(Fields[i++], string.Empty);
                            string StrH = RegexUint.Replace(Fields[i++], string.Empty);

                            Size R = new Size(Convert.ToInt32(StrW), Convert.ToInt32(StrH));

                            if ((R.Width >= Range_MIN.Width && R.Width <= Range_MAX.Width) && (R.Height >= Range_MIN.Height && R.Height <= Range_MAX.Height))
                            {
                                Record_Last.Range = R;
                            }
                        }
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Str, "<Probability>", "</Probability>", false, false), string.Empty);

                        Int32 P = Convert.ToInt32(SubStr);

                        if (P >= Probability_MIN && P <= Probability_MAX)
                        {
                            Record_Last.Probability = P;
                        }
                    }
                    catch { }

                    //

                    if (Com.Text.GetIntervalString(Str, "<EnableUndo>", "</EnableUndo>", false, false).Contains((!Record_Last.EnableUndo).ToString()))
                    {
                        Record_Last.EnableUndo = !Record_Last.EnableUndo;
                    }

                    //

                    try
                    {
                        Int32 LastMaxElementValue = (Int32)Math.Sqrt(Record_Last.Range.Width * Record_Last.Range.Height) + Record_Last.Range.Width * Record_Last.Range.Height - 1;

                        string SubStr = Com.Text.GetIntervalString(Str, "<Element>", "</Element>", false, false);

                        while (SubStr.Contains("(") && SubStr.Contains(")"))
                        {
                            try
                            {
                                string StrE = Com.Text.GetIntervalString(SubStr, "(", ")", false, false);

                                string[] Fields = StrE.Split(',');

                                if (Fields.Length == 3)
                                {
                                    int i = 0;

                                    Point Index = new Point();
                                    Int32 E = 0;

                                    string StrIDX = RegexUint.Replace(Fields[i++], string.Empty);
                                    Index.X = Convert.ToInt32(StrIDX);

                                    string StrIDY = RegexUint.Replace(Fields[i++], string.Empty);
                                    Index.Y = Convert.ToInt32(StrIDY);

                                    string StrVal = RegexUint.Replace(Fields[i++], string.Empty);
                                    E = Convert.ToInt32(StrVal);

                                    if ((Index.X >= 0 && Index.X < Record_Last.Range.Width && Index.Y >= 0 && Index.Y < Record_Last.Range.Height) && (E > 0 && E <= LastMaxElementValue))
                                    {
                                        ElementMatrix_Last[Index.X, Index.Y] = E;
                                        ElementIndexList_Last.Add(Index);
                                    }
                                }
                            }
                            catch { }

                            SubStr = SubStr.Substring(SubStr.IndexOf(")") + (")").Length);
                        }
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = RegexFloatExp.Replace(Com.Text.GetIntervalString(Str, "<Score>", "</Score>", false, false), string.Empty);

                        double Sc = Convert.ToDouble(SubStr);

                        if (Sc >= 0)
                        {
                            Record_Last.Score = Sc;
                        }
                    }
                    catch { }

                    //

                    StepListString = string.Empty;

                    if (Record_Last.EnableUndo)
                    {
                        StepListString += Com.Text.GetIntervalString(Str, "<Previous>", "</Previous>", true, true);
                        StepListString += Com.Text.GetIntervalString(Str, "<Next>", "</Next>", true, true);
                    }
                }
            }
        }

        private void EraseLastGame()
        {
            //
            // 擦除上次游戏。
            //

            foreach (Point A in ElementIndexList_Last)
            {
                ElementMatrix_Last[A.X, A.Y] = 0;
            }

            ElementIndexList_Last.Clear();

            Record_Last = new Record();

            StepListString = string.Empty;

            //

            try
            {
                if (!Directory.Exists(LogFileDir))
                {
                    Directory.CreateDirectory(LogFileDir);
                }

                StreamWriter SW = new StreamWriter(RecordFilePath, false);
                SW.WriteLine(string.Empty);
                SW.Close();
            }
            catch { }
        }

        // 上次游戏：游戏步骤。

        private void CleanGameStep()
        {
            //
            // 清理游戏步骤。
            //

            if (StepListString.Length > 0)
            {
                StepListString = string.Empty;

                //

                string Str = string.Empty;

                Str += "<Log>";

                if (File.Exists(RecordFilePath))
                {
                    FileInfo FInfo = new FileInfo(RecordFilePath);

                    if (FInfo.Length > 0)
                    {
                        StreamReader SR = new StreamReader(RecordFilePath, false);
                        string S = SR.ReadLine();
                        SR.Close();

                        //

                        Str += Com.Text.GetIntervalString(S, "<Range>", "</Range>", true, true);
                        Str += Com.Text.GetIntervalString(S, "<Element>", "</Element>", true, true);
                        Str += Com.Text.GetIntervalString(S, "<Score>", "</Score>", true, true);
                    }
                }

                Str += "</Log>";

                //

                try
                {
                    if (!Directory.Exists(LogFileDir))
                    {
                        Directory.CreateDirectory(LogFileDir);
                    }

                    StreamWriter SW = new StreamWriter(RecordFilePath, false);
                    SW.WriteLine(Str);
                    SW.Close();
                }
                catch { }
            }
        }

        private DateTime BkgWkrStartingTime = DateTime.Now; // 处理游戏步骤的后台工作的开始时刻。
        private double BkgWkrComPct = 0; // 处理游戏步骤的后台工作的已完成百分比。

        private static string GetRemainingTimeString(Int64 Seconds)
        {
            //
            // 获取剩余时间字符串。Seconds：剩余时间的总秒数。
            //

            try
            {
                TimeSpan TS = TimeSpan.FromSeconds(Seconds);

                return ((Int32)TS.TotalDays >= 1 ? string.Concat(TS.Days, " 天 ", TS.Hours, " 小时") : ((Int32)TS.TotalHours >= 1 ? string.Concat(TS.Hours, " 小时 ", TS.Minutes / 5 * 5, " 分") : ((Int32)TS.TotalMinutes >= 1 ? string.Concat(TS.Minutes, " 分 ", TS.Seconds / 15 * 15, " 秒") : ((Int32)TS.TotalSeconds >= 0 ? string.Concat(Math.Max(1, TS.Seconds / 5) * 5, " 秒") : string.Empty))));
            }
            catch
            {
                return string.Empty;
            }
        }

        private void _LoadGameStep()
        {
            //
            // 为后台或前台操作提供加载游戏步骤功能。
            //

            if (EnableUndo && SaveEveryStep)
            {
                Regex RegexUint = new Regex(@"[^0-9]");
                Regex RegexFloatExp = new Regex(@"[^0-9E\+\-\.]");

                //

                Int32 PreviousStrLen = 0, NextStrLen = 0;
                double PreviousPct = 0, NextPct = 0;

                //

                Int32 LastMaxElementValue = (Int32)Math.Sqrt(Record_Last.Range.Width * Record_Last.Range.Height) + Record_Last.Range.Width * Record_Last.Range.Height - 1;

                //

                try
                {
                    string SubStr = Com.Text.GetIntervalString(StepListString, "<Previous>", "</Previous>", false, false);

                    PreviousStrLen = SubStr.Length;
                    PreviousPct = (double)PreviousStrLen / StepListString.Length;

                    while (SubStr.Contains("{") && SubStr.Contains("}"))
                    {
                        try
                        {
                            if (BackgroundWorker_LoadGameStep.IsBusy)
                            {
                                BkgWkrComPct = (1 - Math.Pow((double)SubStr.Length / PreviousStrLen, 2)) * PreviousPct * 100;

                                BackgroundWorker_LoadGameStep.ReportProgress((Int32)BkgWkrComPct);
                            }

                            string StepString = Com.Text.GetIntervalString(SubStr, "{", "}", false, false);

                            Step S = new Step();

                            //

                            string StepString_Array = Com.Text.GetIntervalString(StepString, "[", "]", false, false);

                            while (StepString_Array.Contains("(") && StepString_Array.Contains(")"))
                            {
                                try
                                {
                                    string StrE = Com.Text.GetIntervalString(StepString_Array, "(", ")", false, false);

                                    string[] Fields_Array = StrE.Split(',');

                                    if (Fields_Array.Length == 3)
                                    {
                                        Point Index = new Point();
                                        Int32 E = 0;

                                        string StrIDX = RegexUint.Replace(Fields_Array[0], string.Empty);
                                        Index.X = Convert.ToInt32(StrIDX);

                                        string StrIDY = RegexUint.Replace(Fields_Array[1], string.Empty);
                                        Index.Y = Convert.ToInt32(StrIDY);

                                        string StrVal = RegexUint.Replace(Fields_Array[2], string.Empty);
                                        E = Convert.ToInt32(StrVal);

                                        if ((Index.X >= 0 && Index.X < Record_Last.Range.Width && Index.Y >= 0 && Index.Y < Record_Last.Range.Height) && (E > 0 && E <= LastMaxElementValue))
                                        {
                                            S.Array[Index.X, Index.Y] = E;
                                        }
                                    }
                                }
                                catch { }

                                StepString_Array = StepString_Array.Substring(StepString_Array.IndexOf(")") + (")").Length);
                            }

                            //

                            StepString = StepString.Substring(StepString.IndexOf("]") + ("]").Length);

                            string StepString_Other = Com.Text.GetIntervalString(StepString, "(", ")", false, false);

                            string[] Fields_Other = StepString_Other.Split(',');

                            if (Fields_Other.Length == 5)
                            {
                                double Sc = 0;
                                Int32 D = 0;
                                Point Index = new Point();
                                Int32 E = 0;

                                string StrScore = RegexFloatExp.Replace(Fields_Other[0], string.Empty);
                                Sc = Convert.ToDouble(StrScore);

                                string StrD = RegexUint.Replace(Fields_Other[1], string.Empty);
                                D = Convert.ToInt32(StrD);

                                string StrIDX = RegexUint.Replace(Fields_Other[2], string.Empty);
                                Index.X = Convert.ToInt32(StrIDX);

                                string StrIDY = RegexUint.Replace(Fields_Other[3], string.Empty);
                                Index.Y = Convert.ToInt32(StrIDY);

                                string StrE = RegexUint.Replace(Fields_Other[4], string.Empty);
                                E = Convert.ToInt32(StrE);

                                if (Sc >= 0 && (D >= 0 && D <= 3) && (Index.X >= 0 && Index.X < Record_Last.Range.Width && Index.Y >= 0 && Index.Y < Record_Last.Range.Height) && (E > 0 && E <= LastMaxElementValue))
                                {
                                    S.Score = Sc;
                                    S.Direction = (Directions)D;
                                    S.Index = Index;
                                    S.Value = E;
                                }

                                //

                                StepList_Previous.Add(S);
                            }
                        }
                        catch { }

                        SubStr = SubStr.Substring(SubStr.IndexOf("}") + ("}").Length);
                    }
                }
                catch { }

                //

                try
                {
                    string SubStr = Com.Text.GetIntervalString(StepListString, "<Next>", "</Next>", false, false);

                    NextStrLen = SubStr.Length;
                    NextPct = (double)PreviousStrLen / StepListString.Length;

                    while (SubStr.Contains("(") && SubStr.Contains(")"))
                    {
                        try
                        {
                            if (BackgroundWorker_LoadGameStep.IsBusy)
                            {
                                BkgWkrComPct = (PreviousPct + (1 - Math.Pow((double)SubStr.Length / NextStrLen, 2)) * NextPct) * 100;

                                BackgroundWorker_LoadGameStep.ReportProgress((Int32)BkgWkrComPct);
                            }

                            string StepString = Com.Text.GetIntervalString(SubStr, "(", ")", false, false);

                            string[] Fields = StepString.Split(',');

                            if (Fields.Length == 4)
                            {
                                int i = 0;

                                Step S = new Step();

                                Int32 D = 0;
                                Point Index = new Point();
                                Int32 E = 0;

                                string StrD = RegexUint.Replace(Fields[i++], string.Empty);
                                D = Convert.ToInt32(StrD);

                                string StrIDX = RegexUint.Replace(Fields[i++], string.Empty);
                                Index.X = Convert.ToInt32(StrIDX);

                                string StrIDY = RegexUint.Replace(Fields[i++], string.Empty);
                                Index.Y = Convert.ToInt32(StrIDY);

                                string StrE = RegexUint.Replace(Fields[i++], string.Empty);
                                E = Convert.ToInt32(StrE);

                                if ((D >= 0 && D <= 3) && (Index.X >= 0 && Index.X < Record_Last.Range.Width && Index.Y >= 0 && Index.Y < Record_Last.Range.Height) && (E > 0 && E <= LastMaxElementValue))
                                {
                                    S.Direction = (Directions)D;
                                    S.Index = Index;
                                    S.Value = E;
                                }

                                StepList_Next.Add(S);
                            }
                        }
                        catch { }

                        SubStr = SubStr.Substring(SubStr.IndexOf(")") + (")").Length);
                    }
                }
                catch { }
            }

            //

            ElementMatrix_Initialize();

            foreach (Point A in ElementIndexList_Last)
            {
                ElementMatrix_Add(A, ElementMatrix_Last[A.X, A.Y]);
            }

            ThisRecord.Score = Record_Last.Score;
        }

        private void BackgroundWorker_LoadGameStep_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            // 加载游戏步骤后台工作。
            //

            _LoadGameStep();
        }

        private void BackgroundWorker_LoadGameStep_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //
            // 加载游戏步骤后台工作报告进度。
            //

            Int64 RemainingSeconds = ((e.ProgressPercentage > 0 && e.ProgressPercentage < 100) ? (Int64)((DateTime.Now - BkgWkrStartingTime).TotalSeconds / BkgWkrComPct * (100 - BkgWkrComPct)) : -1);

            Me.Caption = (RemainingSeconds >= 0 ? "已完成 " + e.ProgressPercentage + "% - 剩余 " + GetRemainingTimeString(RemainingSeconds) : ApplicationName) + " [正在打开]";
        }

        private void BackgroundWorker_LoadGameStep_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 加载游戏步骤后台工作完成。
            //

            ElementMatrix_AnimatePresentAt(ElementIndexList, true);

            Judgement();

            //

            if (StepList_Previous.Count > 0)
            {
                PictureBox_Undo.Enabled = true;
            }

            if (StepList_Next.Count > 0)
            {
                PictureBox_Redo.Enabled = true;
            }

            //

            Me.Caption = ApplicationName;

            //

            Panel_Interrupt.Enabled = true;
        }

        private void LoadGameStepInBackground()
        {
            //
            // 后台加载游戏步骤。
            //

            Me.Caption = ApplicationName + " [正在打开]";

            //

            Panel_Interrupt.Enabled = false;

            //

            BkgWkrStartingTime = DateTime.Now;

            BackgroundWorker_LoadGameStep.RunWorkerAsync();
        }

        private void LoadGameStepInForeground()
        {
            //
            // 前台加载游戏步骤。
            //

            Me.Caption = ApplicationName + " [正在打开]";

            //

            Panel_Interrupt.Enabled = false;

            //

            _LoadGameStep();

            //

            ElementMatrix_AnimatePresentAt(ElementIndexList, true);

            Judgement();

            //

            if (StepList_Previous.Count > 0)
            {
                PictureBox_Undo.Enabled = true;
            }

            if (StepList_Next.Count > 0)
            {
                PictureBox_Redo.Enabled = true;
            }

            //

            Me.Caption = ApplicationName;

            //

            Panel_Interrupt.Enabled = true;
        }

        private void _SaveGameStep()
        {
            //
            // 为后台或前台操作提供保存游戏步骤功能。
            //

            Record_Last = ThisRecord;

            foreach (Point A in ElementIndexList_Last)
            {
                ElementMatrix_Last[A.X, A.Y] = 0;
            }

            ElementIndexList_Last.Clear();

            foreach (Point A in ElementIndexList)
            {
                ElementMatrix_Last[A.X, A.Y] = ElementMatrix_GetValue(A);

                ElementIndexList_Last.Add(A);
            }

            StepListString = string.Empty;

            //

            StringBuilder StrBdr = new StringBuilder();

            StrBdr.Append("<Log>");

            StrBdr.Append("<Range>(" + Range.Width + "," + Range.Height + ")</Range>");
            StrBdr.Append("<Probability>" + Probability + "%</Probability>");
            StrBdr.Append("<EnableUndo>" + EnableUndo + "</EnableUndo>");

            StrBdr.Append("<Element>[");
            for (int i = 0; i < ElementIndexList.Count; i++)
            {
                Point A = ElementIndexList[i];

                StrBdr.Append("(" + A.X + "," + A.Y + "," + ElementMatrix_GetValue(A) + ")");
            }
            StrBdr.Append("]</Element>");

            StrBdr.Append("<Score>" + ThisRecord.Score + "</Score>");

            if (EnableUndo && SaveEveryStep)
            {
                double PreviousPct = 0, NextPct = 0;

                StringBuilder StepListStrBdr = new StringBuilder();

                if (StepList_Previous.Count > 0)
                {
                    PreviousPct = (double)StepList_Previous.Count / (StepList_Previous.Count + StepList_Next.Count);

                    StepListStrBdr.Append("<Previous>");
                    for (int i = 0; i < StepList_Previous.Count; i++)
                    {
                        if (BackgroundWorker_SaveGameStep.IsBusy)
                        {
                            BkgWkrComPct = Math.Pow((double)(i + 1) / StepList_Previous.Count, 2) * PreviousPct * 100;

                            BackgroundWorker_SaveGameStep.ReportProgress((Int32)BkgWkrComPct);
                        }

                        Step S = StepList_Previous[i];

                        StepListStrBdr.Append("{");

                        StepListStrBdr.Append("[");
                        for (int X = 0; X < Range.Width; X++)
                        {
                            for (int Y = 0; Y < Range.Height; Y++)
                            {
                                if (S.Array[X, Y] != 0)
                                {
                                    StepListStrBdr.Append("(" + X + "," + Y + "," + S.Array[X, Y] + ")");
                                }
                            }
                        }
                        StepListStrBdr.Append("]");

                        StepListStrBdr.Append("(" + S.Score + "," + (Int32)S.Direction + "," + S.Index.X + "," + S.Index.Y + "," + S.Value + ")");

                        StepListStrBdr.Append("}");
                    }
                    StepListStrBdr.Append("</Previous>");
                }

                if (StepList_Next.Count > 0)
                {
                    NextPct = (double)StepList_Next.Count / (StepList_Previous.Count + StepList_Next.Count);

                    StepListStrBdr.Append("<Next>");
                    for (int i = 0; i < StepList_Next.Count; i++)
                    {
                        if (BackgroundWorker_SaveGameStep.IsBusy)
                        {
                            BkgWkrComPct = (PreviousPct + Math.Pow((double)(i + 1) / StepList_Previous.Count, 2) * NextPct) * 100;

                            BackgroundWorker_SaveGameStep.ReportProgress((Int32)BkgWkrComPct);
                        }

                        Step S = StepList_Next[i];

                        StepListStrBdr.Append("(" + (Int32)S.Direction + "," + S.Index.X + "," + S.Index.Y + "," + S.Value + ")");
                    }
                    StepListStrBdr.Append("</Next>");
                }

                StepListString = StepListStrBdr.ToString();
            }

            StrBdr.Append("<Step>" + StepListString + "</Step>");

            StrBdr.Append("</Log>");

            //

            try
            {
                if (!Directory.Exists(LogFileDir))
                {
                    Directory.CreateDirectory(LogFileDir);
                }

                StreamWriter SW = new StreamWriter(RecordFilePath, false);
                SW.WriteLine(StrBdr.ToString());
                SW.Close();
            }
            catch { }
        }

        private void BackgroundWorker_SaveGameStep_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            // 保存游戏步骤后台工作。
            //

            _SaveGameStep();
        }

        private void BackgroundWorker_SaveGameStep_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //
            // 保存游戏步骤后台工作报告进度。
            //

            Int64 RemainingSeconds = ((e.ProgressPercentage > 0 && e.ProgressPercentage < 100) ? (Int64)((DateTime.Now - BkgWkrStartingTime).TotalSeconds / BkgWkrComPct * (100 - BkgWkrComPct)) : -1);

            Me.Caption = (RemainingSeconds >= 0 ? "已完成 " + e.ProgressPercentage + "% - 剩余 " + GetRemainingTimeString(RemainingSeconds) : ApplicationName) + " [正在保存]";
        }

        private void BackgroundWorker_SaveGameStep_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 保存游戏步骤后台工作完成。
            //

            ExitGameUI();

            //

            Me.Caption = ApplicationName;

            //

            Panel_Interrupt.Enabled = true;
        }

        private void SaveGameStepInBackground()
        {
            //
            // 后台保存游戏步骤。
            //

            Me.Caption = ApplicationName + " [正在保存]";

            //

            Panel_Interrupt.Enabled = false;

            //

            BkgWkrStartingTime = DateTime.Now;

            BackgroundWorker_SaveGameStep.RunWorkerAsync();
        }

        private void SaveGameStepInForeground()
        {
            //
            // 前台保存游戏步骤。
            //

            Me.Caption = ApplicationName + " [正在保存]";

            //

            _SaveGameStep();
        }

        #endregion

        #region 数组功能

        private static Int32[,] GetCopyOfArray(Int32[,] Array)
        {
            //
            // 返回二维矩阵的浅表副本。Array：矩阵。
            //

            try
            {
                if (Array != null)
                {
                    return (Int32[,])Array.Clone();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static Int32 GetZeroCountOfArray(Int32[,] Array, Size Cap)
        {
            //
            // 计算二维矩阵值为 0 的元素的数量。Array：矩阵，索引为 [x, y]；Cap：矩阵的大小，分量 (Width, Height) 分别表示沿 x 方向和沿 y 方向的元素数量。
            //

            try
            {
                if (Array != null)
                {
                    Int32 ZeroCount = 0;

                    for (int X = 0; X < Cap.Width; X++)
                    {
                        for (int Y = 0; Y < Cap.Height; Y++)
                        {
                            if (Array[X, Y] == 0)
                            {
                                ZeroCount++;
                            }
                        }
                    }

                    return ZeroCount;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private static List<Point> GetCertainIndexListOfArray(Int32[,] Array, Size Cap, Int32 Value)
        {
            //
            // 返回二维矩阵中所有值为指定值的元素的索引的列表。Array：矩阵，索引为 [x, y]；Cap：矩阵的大小，分量 (Width, Height) 分别表示沿 x 方向和沿 y 方向的元素数量；Value：指定值。
            //

            try
            {
                if (Array != null)
                {
                    List<Point> L = new List<Point>(Cap.Width * Cap.Height);

                    for (int X = 0; X < Cap.Width; X++)
                    {
                        for (int Y = 0; Y < Cap.Height; Y++)
                        {
                            if (Array[X, Y] == Value)
                            {
                                L.Add(new Point(X, Y));
                            }
                        }
                    }

                    return L;
                }

                return new List<Point>(0);
            }
            catch
            {
                return new List<Point>(0);
            }
        }

        private static Point GetRandomZeroIndexOfArray(Int32[,] Array, Size Cap)
        {
            //
            // 在二维矩阵中随机确定一个值为 0 的元素，返回其索引。Array：矩阵，索引为 [x, y]；Cap：矩阵的大小，分量 (Width, Height) 分别表示沿 x 方向和沿 y 方向的元素数量。
            //

            try
            {
                if (Array != null)
                {
                    if (GetZeroCountOfArray(Array, Cap) > 0)
                    {
                        List<Point> L = GetCertainIndexListOfArray(Array, Cap, 0);

                        return L[Com.Statistics.RandomInteger(L.Count)];
                    }
                }

                return new Point(-1, -1);
            }
            catch
            {
                return new Point(-1, -1);
            }
        }

        #endregion

        #region 元素矩阵基本功能

        // 初始化。

        private void ElementMatrix_Initialize()
        {
            //
            // 初始化元素矩阵。
            //

            foreach (Point A in ElementIndexList)
            {
                ElementMatrix[A.X, A.Y] = 0;
            }

            ElementIndexList.Clear();
        }

        // 索引。

        private bool ElementMatrix_IndexValid(Point A)
        {
            //
            // 检查指定的索引是否有效。A：索引。
            //

            try
            {
                return (A.X >= 0 && A.X < Range.Width && A.Y >= 0 && A.Y < Range.Height);
            }
            catch
            {
                return false;
            }
        }

        private Int32 ElementMatrix_GetValue(Point A)
        {
            //
            // 获取元素矩阵指定的索引的元素的值。A：索引。
            //

            try
            {
                if (ElementMatrix_IndexValid(A))
                {
                    return ElementMatrix[A.X, A.Y];
                }

                return Int32.MinValue;
            }
            catch
            {
                return Int32.MinValue;
            }
        }

        private Int32 ElementMatrix_GetValue(Int32 X, Int32 Y)
        {
            //
            // 获取元素矩阵指定的索引的元素的值。X，Y：索引。
            //

            try
            {
                if (ElementMatrix_IndexValid(new Point(X, Y)))
                {
                    return ElementMatrix[X, Y];
                }

                return Int32.MinValue;
            }
            catch
            {
                return Int32.MinValue;
            }
        }

        private Point ElementMatrix_GetIndex(Point P)
        {
            //
            // 获取绘图容器中的指定坐标所在元素的索引。P：坐标。
            //

            try
            {
                Point dP = new Point(P.X - EAryBmpRect.X, P.Y - EAryBmpRect.Y);
                Point A = new Point((Int32)Math.Floor((double)dP.X / ElementSize), (Int32)Math.Floor((double)dP.Y / ElementSize));

                if (ElementMatrix_IndexValid(A))
                {
                    return A;
                }

                return new Point(-1, -1);
            }
            catch
            {
                return new Point(-1, -1);
            }
        }

        // 添加与移除。

        private void ElementMatrix_Add(Point A, Int32 E)
        {
            //
            // 向元素矩阵添加一个元素。A：索引；E：元素的值。
            //

            if (E != 0 && ElementMatrix_IndexValid(A))
            {
                if (!ElementIndexList.Contains(A))
                {
                    ElementMatrix[A.X, A.Y] = E;

                    ElementIndexList.Add(A);
                }
            }
        }

        private void ElementMatrix_RemoveAt(Point A)
        {
            //
            // 从元素矩阵移除一个元素。A：索引。
            //

            if (ElementMatrix_IndexValid(A))
            {
                ElementMatrix[A.X, A.Y] = 0;

                if (ElementIndexList.Contains(A))
                {
                    ElementIndexList.Remove(A);
                }
            }
        }

        // 颜色。

        private static readonly Color[] ElementColor = new Color[] { Color.SandyBrown, Color.Crimson, Color.BlueViolet, Color.RoyalBlue, Color.SeaGreen, Color.DarkSlateGray }; // 元素颜色系列。

        private Color ElementMatrix_GetColor(Int32 E)
        {
            //
            // 获取元素颜色。E：元素的值。
            //

            try
            {
                if (E == 0)
                {
                    return Me.RecommendColors.Background.ToColor();
                }
                else if (E > 0)
                {
                    Int32 Pow = E - 1;

                    Int32 Index = (Pow / 4) % ElementColor.Length;

                    double Cd = 0;

                    switch (Pow % 4)
                    {
                        case 0: Cd = 0.22; break;
                        case 1: Cd = 0.42; break;
                        case 2: Cd = 0.68; break;
                        case 3: Cd = 1; break;
                    }

                    return Com.ColorManipulation.ShiftLightnessByHSL(ElementColor[Index], 1 - Cd);
                }

                return Color.Empty;
            }
            catch
            {
                return Color.Empty;
            }
        }

        // 绘图与呈现。

        private Rectangle EAryBmpRect = new Rectangle(); // 元素矩阵位图区域（相对于绘图容器）。

        private Bitmap EAryBmp; // 元素矩阵位图。

        private Graphics EAryBmpGrap; // 元素矩阵位图绘图。

        private void ElementMatrix_DrawInRectangle(Int32 E, Rectangle Rect, bool PresentNow)
        {
            //
            // 在元素矩阵位图的指矩形区域内绘制一个元素。E：元素的值；Rect：矩形区域；PresentNow：是否立即呈现此元素，如果为 true，那么将在位图中绘制此元素，并在不重绘整个位图的情况下在容器中绘制此元素，如果为 false，那么将仅在位图中绘制此元素。
            //

            Rectangle BmpRect = new Rectangle(new Point(Rect.X - (ElementSize - Rect.Width) / 2, Rect.Y - (ElementSize - Rect.Height) / 2), new Size(ElementSize, ElementSize));

            Bitmap Bmp = new Bitmap(BmpRect.Width, BmpRect.Height);

            Graphics BmpGrap = Graphics.FromImage(Bmp);

            if (AntiAlias)
            {
                BmpGrap.SmoothingMode = SmoothingMode.AntiAlias;
                BmpGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
            }

            //

            const double ElementClientDistPct = 1.0 / 12.0; // 相邻两元素有效区域的间距与元素边长之比。

            if (Rect.Width < BmpRect.Width || Rect.Height < BmpRect.Height)
            {
                Rectangle Rect_Bkg = new Rectangle(new Point((Int32)(ElementSize * ElementClientDistPct / 2), (Int32)(ElementSize * ElementClientDistPct / 2)), new Size((Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct))), (Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct)))));

                GraphicsPath RndRect_Bkg = Com.Geometry.CreateRoundedRectanglePath(Rect_Bkg, (Int32)(ElementSize * ElementClientDistPct / 2));

                Color Cr_Bkg = (E > 0 ? ElementMatrix_GetColor(0) : Color.FromArgb((Int32)(Math.Max(0, Math.Min(1, (double)(Rect.Width * Rect.Height) / (BmpRect.Width * BmpRect.Height))) * 255), ElementMatrix_GetColor(0)));

                if (GameIsOver)
                {
                    Cr_Bkg = Com.ColorManipulation.GetGrayscaleColor(Cr_Bkg);
                }

                BmpGrap.FillPath(new LinearGradientBrush(new Point(Rect_Bkg.X - 1, Rect_Bkg.Y - 1), new Point(Rect_Bkg.Right, Rect_Bkg.Bottom), Com.ColorManipulation.ShiftLightnessByHSL(Cr_Bkg, 0.3), Cr_Bkg), RndRect_Bkg);
            }

            Rectangle Rect_Cen = new Rectangle(new Point((Int32)((ElementSize - Rect.Width) / 2 + Rect.Width * ElementClientDistPct / 2), (Int32)((ElementSize - Rect.Height) / 2 + Rect.Height * ElementClientDistPct / 2)), new Size((Int32)(Math.Max(1, Rect.Width * (1 - ElementClientDistPct))), (Int32)(Math.Max(1, Rect.Height * (1 - ElementClientDistPct)))));

            GraphicsPath RndRect_Cen = Com.Geometry.CreateRoundedRectanglePath(Rect_Cen, (Int32)(ElementSize * ElementClientDistPct / 2));

            Color Cr_Cen = Color.FromArgb((Int32)(Math.Max(0, Math.Min(1, (double)(Rect.Width * Rect.Height) / (BmpRect.Width * BmpRect.Height))) * 255), ElementMatrix_GetColor(E));

            if (GameIsOver)
            {
                Cr_Cen = Com.ColorManipulation.GetGrayscaleColor(Cr_Cen);
            }

            BmpGrap.FillPath(new LinearGradientBrush(new Point(Rect_Cen.X - 1, Rect_Cen.Y - 1), new Point(Rect_Cen.Right, Rect_Cen.Bottom), Com.ColorManipulation.ShiftLightnessByHSL(Cr_Cen, 0.3), Cr_Cen), RndRect_Cen);

            //

            string StringText = (E > 0 ? Math.Pow(2, E).ToString() : string.Empty);

            if (StringText.Length > 0)
            {
                Color StringColor = Com.ColorManipulation.ShiftLightnessByHSL(Cr_Cen, -0.5);
                Font StringFont = Com.Text.GetSuitableFont(StringText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(Rect_Cen.Width * 0.8F, Rect_Cen.Height * 0.8F));
                RectangleF StringRect = new RectangleF();
                StringRect.Size = BmpGrap.MeasureString(StringText, StringFont);
                StringRect.Location = new PointF(Rect_Cen.X + (Rect_Cen.Width - StringRect.Width) / 2, Rect_Cen.Y + (Rect_Cen.Height - StringRect.Height) / 2);

                Com.Painting2D.PaintTextWithShadow(Bmp, StringText, StringFont, StringColor, StringColor, StringRect.Location, 0.02F, AntiAlias);
            }

            //

            if (Bmp != null)
            {
                EAryBmpGrap.DrawImage(Bmp, BmpRect.Location);

                if (PresentNow)
                {
                    Panel_Environment.CreateGraphics().DrawImage(Bmp, new Point(EAryBmpRect.X + BmpRect.X, EAryBmpRect.Y + BmpRect.Y));
                }
            }
        }

        private void ElementMatrix_RepresentAll()
        {
            //
            // 更新并呈现元素矩阵包含的所有元素。
            //

            if (Panel_Environment.Visible && (Panel_Environment.Width > 0 && Panel_Environment.Height > 0))
            {
                if (EAryBmp != null)
                {
                    EAryBmp.Dispose();
                }

                EAryBmp = new Bitmap(Math.Max(1, EAryBmpRect.Width), Math.Max(1, EAryBmpRect.Height));

                EAryBmpGrap = Graphics.FromImage(EAryBmp);

                if (AntiAlias)
                {
                    EAryBmpGrap.SmoothingMode = SmoothingMode.AntiAlias;
                    EAryBmpGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
                }

                EAryBmpGrap.Clear(GameUIBackColor_INC);

                //

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        Int32 E = ElementMatrix_GetValue(X, Y);

                        Rectangle Rect = new Rectangle(new Point(X * ElementSize, Y * ElementSize), new Size(ElementSize, ElementSize));

                        ElementMatrix_DrawInRectangle(E, Rect, false);
                    }
                }

                //

                if (GameIsOver)
                {
                    EAryBmpGrap.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.White)), new Rectangle(new Point(0, 0), EAryBmp.Size));

                    //

                    string StringText = "失败";
                    Color StringColor = Me.RecommendColors.Text.ToColor();

                    Font StringFont = Com.Text.GetSuitableFont(StringText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(EAryBmp.Width * 0.8F, EAryBmp.Height * 0.2F));
                    RectangleF StringRect = new RectangleF();
                    StringRect.Size = EAryBmpGrap.MeasureString(StringText, StringFont);
                    StringRect.Location = new PointF((EAryBmp.Width - StringRect.Width) / 2, (EAryBmp.Height - StringRect.Height) / 2);

                    Color StringBkColor = Com.ColorManipulation.ShiftLightnessByHSL(StringColor, 0.5);
                    Rectangle StringBkRect = new Rectangle(new Point(0, (Int32)StringRect.Y), new Size(EAryBmp.Width, Math.Max(1, (Int32)StringRect.Height)));

                    GraphicsPath Path_StringBk = new GraphicsPath();
                    Path_StringBk.AddRectangle(StringBkRect);
                    PathGradientBrush PGB_StringBk = new PathGradientBrush(Path_StringBk)
                    {
                        CenterColor = Color.FromArgb(192, StringBkColor),
                        SurroundColors = new Color[] { Color.Transparent },
                        FocusScales = new PointF(0F, 1F)
                    };
                    EAryBmpGrap.FillPath(PGB_StringBk, Path_StringBk);
                    Path_StringBk.Dispose();
                    PGB_StringBk.Dispose();

                    Com.Painting2D.PaintTextWithShadow(EAryBmp, StringText, StringFont, StringColor, StringColor, StringRect.Location, 0.02F, AntiAlias);
                }

                //

                RepaintEAryBmp();
            }
        }

        private void ElementMatrix_AnimatePresentAt(Point A)
        {
            //
            // 以动画效果呈现元素矩阵中指定的索引处的一个元素。A：索引。
            //

            if (Panel_Environment.Visible && (Panel_Environment.Width > 0 && Panel_Environment.Height > 0))
            {
                if (ElementMatrix_IndexValid(A))
                {
                    Int32 E = ElementMatrix_GetValue(A);

                    Com.Animation.Frame Frame = (frameId, frameCount, msPerFrame) =>
                    {
                        double Pct_F = (frameId == frameCount ? 1 : 1 - Math.Pow(1 - (double)frameId / frameCount, 2));

                        Int32 RectSize = (Int32)Math.Max(1, ElementSize * Pct_F);

                        Rectangle Rect = new Rectangle(new Point(A.X * ElementSize + (ElementSize - RectSize) / 2, A.Y * ElementSize + (ElementSize - RectSize) / 2), new Size(RectSize, RectSize));

                        ElementMatrix_DrawInRectangle(E, Rect, false);

                        RepaintEAryBmp();
                    };

                    Com.Animation.Show(Frame, 12, 15);
                }
            }
        }

        private void ElementMatrix_AnimatePresentAt(List<Point> A, bool ClearBmp)
        {
            //
            // 以动画效果同时呈现元素矩阵中由索引数组指定的所有元素。A：索引列表；ClearBmp：在呈现之前是否首先清除绘图。
            //

            if (Panel_Environment.Visible && (Panel_Environment.Width > 0 && Panel_Environment.Height > 0))
            {
                if (ClearBmp)
                {
                    EAryBmpGrap.Clear(GameUIBackColor_INC);

                    for (int X = 0; X < Range.Width; X++)
                    {
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            Rectangle Rect = new Rectangle(new Point(X * ElementSize, Y * ElementSize), new Size(ElementSize, ElementSize));

                            ElementMatrix_DrawInRectangle(0, Rect, false);
                        }
                    }
                }

                Com.Animation.Frame Frame = (frameId, frameCount, msPerFrame) =>
                {
                    double Pct_F = (frameId == frameCount ? 1 : 1 - Math.Pow(1 - (double)frameId / frameCount, 2));

                    Int32 RectSize = (Int32)Math.Max(1, ElementSize * Pct_F);

                    foreach (Point _A in A)
                    {
                        if (ElementMatrix_IndexValid(_A))
                        {
                            Int32 E = ElementMatrix_GetValue(_A);

                            Rectangle Rect = new Rectangle(new Point(_A.X * ElementSize + (ElementSize - RectSize) / 2, _A.Y * ElementSize + (ElementSize - RectSize) / 2), new Size(RectSize, RectSize));

                            ElementMatrix_DrawInRectangle(E, Rect, false);
                        }
                    }

                    RepaintEAryBmp();
                };

                Com.Animation.Show(Frame, 12, 15);
            }
        }

        private void ElementMatrix_AnimateMove(Point[,] OldIndex, Record OldRecord, Record NewRecord)
        {
            //
            // 以动画效果呈现元素矩阵中若干元素的平移。OldIndex：元素矩阵中所有元素在平移之前的索引；OldRecord，NewRecord：元素矩阵中所有元素在平移之前与之后的记录。
            //

            if (Panel_Environment.Visible && (Panel_Environment.Width > 0 && Panel_Environment.Height > 0))
            {
                Com.Animation.Frame FrameA = (frameId, frameCount, msPerFrame) =>
                {
                    double N = (frameId == frameCount ? 0 : 1 - Math.Pow((double)frameId / frameCount, 2));

                    EAryBmpGrap.Clear(GameUIBackColor_INC);

                    for (int X = 0; X < Range.Width; X++)
                    {
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            Int32 E = ElementMatrix_GetValue(X, Y);

                            Rectangle Rect = new Rectangle(new Point(X * ElementSize, Y * ElementSize), new Size(ElementSize, ElementSize));

                            if (E != 0 && OldIndex[X, Y] != new Point(X, Y))
                            {
                                ElementMatrix_DrawInRectangle(0, Rect, false);
                            }
                            else
                            {
                                ElementMatrix_DrawInRectangle(E, Rect, false);
                            }
                        }
                    }

                    for (int X = 0; X < Range.Width; X++)
                    {
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            Int32 E = ElementMatrix_GetValue(X, Y);

                            if (E != 0 && OldIndex[X, Y] != new Point(X, Y))
                            {
                                Rectangle Rect = new Rectangle(new Point((Int32)((X * (1 - N) + OldIndex[X, Y].X * N) * ElementSize), (Int32)((Y * (1 - N) + OldIndex[X, Y].Y * N) * ElementSize)), new Size(ElementSize, ElementSize));

                                ElementMatrix_DrawInRectangle(E, Rect, false);
                            }
                        }
                    }

                    RepaintEAryBmp();

                    if (OldRecord.Score != NewRecord.Score || OldRecord.Max != NewRecord.Max || OldRecord.Sum != NewRecord.Sum)
                    {
                        double Pct_Rec = (frameId == frameCount ? 1 : (double)frameId / frameCount);

                        ThisRecord.Score = Math.Round(OldRecord.Score * (1 - Pct_Rec) + NewRecord.Score * Pct_Rec);
                        ThisRecord.Max = Math.Round(OldRecord.Max * (1 - Pct_Rec) + NewRecord.Max * Pct_Rec);

                        RepaintCurBmp();
                    }
                };

                Com.Animation.Show(FrameA, 9, 15);

                Com.Animation.Frame FrameB = (frameId, frameCount, msPerFrame) =>
                {
                    EAryBmpGrap.Clear(GameUIBackColor_INC);

                    for (int X = 0; X < Range.Width; X++)
                    {
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            Int32 E = ElementMatrix_GetValue(X, Y);

                            Rectangle Rect = new Rectangle(new Point(X * ElementSize, Y * ElementSize), new Size(ElementSize, ElementSize));

                            if (E != 0 && OldIndex[X, Y] != new Point(X, Y))
                            {
                                ElementMatrix_DrawInRectangle(0, Rect, false);
                            }
                            else
                            {
                                ElementMatrix_DrawInRectangle(E, Rect, false);
                            }
                        }
                    }

                    double N = (frameId == frameCount ? 0 : 0.1 * (1 - Math.Pow((frameId - frameCount * 0.4) / (frameCount * 0.6), 2)));

                    for (int X = 0; X < Range.Width; X++)
                    {
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            Int32 E = ElementMatrix_GetValue(X, Y);

                            if (E != 0 && OldIndex[X, Y] != new Point(X, Y))
                            {
                                Rectangle Rect = new Rectangle(new Point((Int32)((X + Math.Sign(OldIndex[X, Y].X - X) * N) * ElementSize), (Int32)((Y + Math.Sign(OldIndex[X, Y].Y - Y) * N) * ElementSize)), new Size(ElementSize, ElementSize));

                                ElementMatrix_DrawInRectangle(E, Rect, false);
                            }
                        }
                    }

                    RepaintEAryBmp();
                };

                Com.Animation.Show(FrameB, 5, 15);

                ElementMatrix_RepresentAll();
            }
        }

        private void RepaintEAryBmp()
        {
            //
            // 重绘元素矩阵位图。
            //

            if (EAryBmp != null)
            {
                if (Panel_Environment.Width > EAryBmp.Width)
                {
                    Panel_Environment.CreateGraphics().FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)), new Rectangle(new Point(Panel_Environment.Width - (Panel_Environment.Width - EAryBmp.Width) / 2, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)) });
                }

                if (Panel_Environment.Height > EAryBmp.Height)
                {
                    Panel_Environment.CreateGraphics().FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)), new Rectangle(new Point(0, Panel_Environment.Height - (Panel_Environment.Height - EAryBmp.Height) / 2), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)) });
                }

                Panel_Environment.CreateGraphics().DrawImage(EAryBmp, EAryBmpRect.Location);
            }
        }

        private void Panel_Environment_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Environment 绘图。
            //

            if (EAryBmp != null)
            {
                if (Panel_Environment.Width > EAryBmp.Width)
                {
                    e.Graphics.FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)), new Rectangle(new Point(Panel_Environment.Width - (Panel_Environment.Width - EAryBmp.Width) / 2, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)) });
                }

                if (Panel_Environment.Height > EAryBmp.Height)
                {
                    e.Graphics.FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)), new Rectangle(new Point(0, Panel_Environment.Height - (Panel_Environment.Height - EAryBmp.Height) / 2), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)) });
                }

                e.Graphics.DrawImage(EAryBmp, EAryBmpRect.Location);
            }
        }

        #endregion

        #region 元素矩阵扩展功能

        // 可逻辑平移性。

        private bool GetLogicalMovableOfElementMatrix()
        {
            //
            // 计算元素矩阵的可逻辑平移性，若此矩阵的可逻辑平移性为真，则返回 true。
            //

            try
            {
                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        if (ElementMatrix_GetValue(X, Y) < 0)
                        {
                            return false;
                        }
                    }
                }

                if (GetZeroCountOfArray(ElementMatrix, Range) > 0)
                {
                    return true;
                }
                else
                {
                    for (int X = 0; X < Range.Width; X++)
                    {
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            if ((X >= 1 && ElementMatrix_GetValue(X, Y) == ElementMatrix_GetValue(X - 1, Y)) || (Y >= 1 && ElementMatrix_GetValue(X, Y) == ElementMatrix_GetValue(X, Y - 1)))
                            {
                                return true;
                            }
                        }
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // 逻辑平移与添加。

        private enum Directions { NULL = -1, X_DEC, X_INC, Y_DEC, Y_INC, COUNT }; // 逻辑平移方向枚举。

        private bool ElementMatrix_LogicalMove(Directions D)
        {
            //
            // 将元素矩阵向指定方向进行逻辑平移（与合并相邻相同值），并返回此操作之后的元素矩阵是否与之前的元素矩阵相同，如果不相同，以动画效果呈现此过程。D：逻辑平移方向。
            //

            try
            {
                Int32[,] OriginalElementMatrix = GetCopyOfArray(ElementMatrix);

                bool Flag = true;

                Record OldRecord = ThisRecord, NewRecord = ThisRecord;

                //

                double MergedValue = 0;

                switch (D)
                {
                    case Directions.X_DEC:
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            List<Int32> Row = new List<Int32>(Range.Width);

                            for (int X = 0; X < Range.Width; X++)
                            {
                                if (ElementMatrix_GetValue(X, Y) != 0)
                                {
                                    Row.Add(ElementMatrix_GetValue(X, Y));

                                    ElementMatrix_RemoveAt(new Point(X, Y));
                                }
                            }

                            for (int X = 0; X <= Row.Count - 2; X++)
                            {
                                if (Row[X] == Row[X + 1])
                                {
                                    Row[X] += 1;
                                    MergedValue += Math.Pow(2, Row[X]);

                                    for (int i = X + 1; i <= Row.Count - 2; i++)
                                    {
                                        Row[i] = Row[i + 1];
                                    }

                                    Row.RemoveAt(Row.Count - 1);
                                }
                            }

                            for (int X = 0; X <= Row.Count - 1; X++)
                            {
                                ElementMatrix_Add(new Point(X, Y), Row[X]);
                            }
                        }
                        break;

                    case Directions.X_INC:
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            List<Int32> Row = new List<Int32>(Range.Width);

                            for (int X = Range.Width - 1; X >= 0; X--)
                            {
                                if (ElementMatrix_GetValue(X, Y) != 0)
                                {
                                    Row.Add(ElementMatrix_GetValue(X, Y));

                                    ElementMatrix_RemoveAt(new Point(X, Y));
                                }
                            }

                            for (int X = 0; X <= Row.Count - 2; X++)
                            {
                                if (Row[X] == Row[X + 1])
                                {
                                    Row[X] += 1;
                                    MergedValue += Math.Pow(2, Row[X]);

                                    for (int i = X + 1; i <= Row.Count - 2; i++)
                                    {
                                        Row[i] = Row[i + 1];
                                    }

                                    Row.RemoveAt(Row.Count - 1);
                                }
                            }

                            for (int X = 0; X <= Row.Count - 1; X++)
                            {
                                ElementMatrix_Add(new Point(Range.Width - 1 - X, Y), Row[X]);
                            }
                        }
                        break;

                    case Directions.Y_DEC:
                        for (int X = 0; X < Range.Width; X++)
                        {
                            List<Int32> Column = new List<Int32>(Range.Height);

                            for (int Y = 0; Y < Range.Height; Y++)
                            {
                                if (ElementMatrix_GetValue(X, Y) != 0)
                                {
                                    Column.Add(ElementMatrix_GetValue(X, Y));

                                    ElementMatrix_RemoveAt(new Point(X, Y));
                                }
                            }

                            for (int Y = 0; Y <= Column.Count - 2; Y++)
                            {
                                if (Column[Y] == Column[Y + 1])
                                {
                                    Column[Y] += 1;
                                    MergedValue += Math.Pow(2, Column[Y]);

                                    for (int i = Y + 1; i <= Column.Count - 2; i++)
                                    {
                                        Column[i] = Column[i + 1];
                                    }

                                    Column.RemoveAt(Column.Count - 1);
                                }
                            }

                            for (int Y = 0; Y <= Column.Count - 1; Y++)
                            {
                                ElementMatrix_Add(new Point(X, Y), Column[Y]);
                            }
                        }
                        break;

                    case Directions.Y_INC:
                        for (int X = 0; X < Range.Width; X++)
                        {
                            List<Int32> Column = new List<Int32>(Range.Height);

                            for (int Y = Range.Height - 1; Y >= 0; Y--)
                            {
                                if (ElementMatrix_GetValue(X, Y) != 0)
                                {
                                    Column.Add(ElementMatrix_GetValue(X, Y));

                                    ElementMatrix_RemoveAt(new Point(X, Y));
                                }
                            }

                            for (int Y = 0; Y <= Column.Count - 2; Y++)
                            {
                                if (Column[Y] == Column[Y + 1])
                                {
                                    Column[Y] += 1;
                                    MergedValue += Math.Pow(2, Column[Y]);

                                    for (int i = Y + 1; i <= Column.Count - 2; i++)
                                    {
                                        Column[i] = Column[i + 1];
                                    }

                                    Column.RemoveAt(Column.Count - 1);
                                }
                            }

                            for (int Y = 0; Y <= Column.Count - 1; Y++)
                            {
                                ElementMatrix_Add(new Point(X, Range.Height - 1 - Y), Column[Y]);
                            }
                        }
                        break;
                }

                if (MergedValue > 0)
                {
                    ThisRecord.Score = Math.Round(ThisRecord.Score + MergedValue);

                    NewRecord.Score = ThisRecord.Score;
                    NewRecord.Max = GetMaxValueOfElementMatrix();
                }

                //

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        if (ElementMatrix_GetValue(X, Y) != OriginalElementMatrix[X, Y])
                        {
                            Flag = false;

                            break;
                        }
                    }
                }

                //

                if (!Flag)
                {
                    Point[,] OldIndex = new Point[Range.Width, Range.Height];

                    switch (D)
                    {
                        case Directions.X_DEC:
                            for (int Y = 0; Y < Range.Height; Y++)
                            {
                                List<Int32> IndexList = new List<Int32>(Range.Width);

                                for (int X = 0; X < Range.Width; X++)
                                {
                                    if (OriginalElementMatrix[X, Y] != 0)
                                    {
                                        bool F = false;

                                        for (int i = X + 1; i < Range.Width; i++)
                                        {
                                            if (OriginalElementMatrix[i, Y] != 0)
                                            {
                                                if (OriginalElementMatrix[i, Y] == OriginalElementMatrix[X, Y])
                                                {
                                                    F = true;

                                                    IndexList.Add(i);

                                                    X = i;
                                                }

                                                break;
                                            }
                                        }

                                        if (!F)
                                        {
                                            IndexList.Add(X);
                                        }
                                    }
                                }

                                for (int X = 0; X < IndexList.Count; X++)
                                {
                                    OldIndex[X, Y] = new Point(IndexList[X], Y);
                                }
                            }
                            break;

                        case Directions.X_INC:
                            for (int Y = 0; Y < Range.Height; Y++)
                            {
                                List<Int32> IndexList = new List<Int32>(Range.Width);

                                for (int X = Range.Width - 1; X >= 0; X--)
                                {
                                    if (OriginalElementMatrix[X, Y] != 0)
                                    {
                                        bool F = false;

                                        for (int i = X - 1; i >= 0; i--)
                                        {
                                            if (OriginalElementMatrix[i, Y] != 0)
                                            {
                                                if (OriginalElementMatrix[i, Y] == OriginalElementMatrix[X, Y])
                                                {
                                                    F = true;

                                                    IndexList.Add(i);

                                                    X = i;
                                                }

                                                break;
                                            }
                                        }

                                        if (!F)
                                        {
                                            IndexList.Add(X);
                                        }
                                    }
                                }

                                for (int X = 0; X < IndexList.Count; X++)
                                {
                                    OldIndex[Range.Width - 1 - X, Y] = new Point(IndexList[X], Y);
                                }
                            }
                            break;

                        case Directions.Y_DEC:
                            for (int X = 0; X < Range.Width; X++)
                            {
                                List<Int32> IndexList = new List<Int32>(Range.Height);

                                for (int Y = 0; Y < Range.Height; Y++)
                                {
                                    if (OriginalElementMatrix[X, Y] != 0)
                                    {
                                        bool F = false;

                                        for (int i = Y + 1; i < Range.Height; i++)
                                        {
                                            if (OriginalElementMatrix[X, i] != 0)
                                            {
                                                if (OriginalElementMatrix[X, i] == OriginalElementMatrix[X, Y])
                                                {
                                                    F = true;

                                                    IndexList.Add(i);

                                                    Y = i;
                                                }

                                                break;
                                            }
                                        }

                                        if (!F)
                                        {
                                            IndexList.Add(Y);
                                        }
                                    }
                                }

                                for (int Y = 0; Y < IndexList.Count; Y++)
                                {
                                    OldIndex[X, Y] = new Point(X, IndexList[Y]);
                                }
                            }
                            break;

                        case Directions.Y_INC:
                            for (int X = 0; X < Range.Width; X++)
                            {
                                List<Int32> IndexList = new List<Int32>(Range.Height);

                                for (int Y = Range.Height - 1; Y >= 0; Y--)
                                {
                                    if (OriginalElementMatrix[X, Y] != 0)
                                    {
                                        bool F = false;

                                        for (int i = Y - 1; i >= 0; i--)
                                        {
                                            if (OriginalElementMatrix[X, i] != 0)
                                            {
                                                if (OriginalElementMatrix[X, i] == OriginalElementMatrix[X, Y])
                                                {
                                                    F = true;

                                                    IndexList.Add(i);

                                                    Y = i;
                                                }

                                                break;
                                            }
                                        }

                                        if (!F)
                                        {
                                            IndexList.Add(Y);
                                        }
                                    }
                                }

                                for (int Y = 0; Y < IndexList.Count; Y++)
                                {
                                    OldIndex[X, Range.Height - 1 - Y] = new Point(X, IndexList[Y]);
                                }
                            }
                            break;
                    }

                    //

                    ElementMatrix_AnimateMove(OldIndex, OldRecord, NewRecord);
                }

                //

                return Flag;
            }
            catch
            {
                return true;
            }
        }

        private void ElementMatrix_LogicalAppend(Point A, Int32 E)
        {
            //
            // 向元素矩阵逻辑添加一个元素，以动画效果呈现此元素，并进行判定。A：索引；E：元素的值。
            //

            if (E != 0 && ElementMatrix_IndexValid(A))
            {
                if (!ElementIndexList.Contains(A))
                {
                    ElementMatrix_Add(A, E);

                    ElementMatrix_AnimatePresentAt(A);

                    Judgement();
                }
            }
        }

        // 概率与随机值。

        private Int32 MaxRandomElementValue => (Int32)Math.Sqrt(Range.Width * Range.Height); // 当前布局下，随机产生一个元素值的最大可能值。

        private List<double> ElementValueProbabilityList // 按照概率衰减的规则，随机产生一个元素值为此列表索引的概率列表
        {
            get
            {
                List<double> PL = new List<double>(MaxRandomElementValue + 1);

                for (int i = 0; i <= MaxRandomElementValue; i++)
                {
                    PL.Add(0);
                }

                if (Probability > 0)
                {
                    double P_Sum = 0;

                    for (int i = 1; i < PL.Count; i++)
                    {
                        PL[i] = Math.Pow(Probability * 0.01, i - 1);

                        P_Sum += PL[i];
                    }

                    for (int i = 1; i < PL.Count; i++)
                    {
                        PL[i] /= P_Sum;
                    }
                }
                else
                {
                    PL[1] = 1;
                }

                return PL;
            }
        }

        private Int32 GetRandomElementValue()
        {
            //
            // 按照概率衰减的规则，随机产生一个元素值。
            //

            try
            {
                if (Probability > 0)
                {
                    List<double> PL = ElementValueProbabilityList;

                    for (int i = 1; i < PL.Count; i++)
                    {
                        if (i >= 2)
                        {
                            PL[i] += PL[i - 1];
                        }
                    }

                    double N = Com.Statistics.RandomDouble();

                    for (int i = 1; i < PL.Count; i++)
                    {
                        if (N <= PL[i])
                        {
                            return i;
                        }
                    }
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        // 最大值与求和。

        private Int32 MaxElementValue => (MaxRandomElementValue + Range.Width * Range.Height - 1); // 当前布局下元素值的最大可能值。

        private double TheoreticalMaxValueOfMax => Math.Pow(2, MaxElementValue); // 当前布局下元素矩阵所有元素的示值最大值的理论最大值。

        private double GetMaxValueOfElementMatrix()
        {
            //
            // 计算元素矩阵所有元素的示值最大值。
            //

            try
            {
                Int64 MaxValue = ElementMatrix_GetValue(0, 0);

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        if (MaxValue < ElementMatrix_GetValue(X, Y))
                        {
                            MaxValue = ElementMatrix_GetValue(X, Y);
                        }
                    }
                }

                if (MaxValue > 0)
                {
                    return Math.Pow(2, MaxValue);
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private double TheoreticalMaxValueOfSum => (Math.Pow(2, MaxRandomElementValue) * (Math.Pow(2, Range.Width * Range.Height) - 1)); // 当前布局下元素矩阵所有元素的示值求和的理论最大值。

        private double GetSumOfElementMatrix()
        {
            //
            // 计算元素矩阵所有元素的示值求和。
            //

            try
            {
                double Sum = 0;

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        if (ElementMatrix_GetValue(X, Y) > 0)
                        {
                            Sum += Math.Pow(2, ElementMatrix_GetValue(X, Y));
                        }
                    }
                }

                return Sum;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region 步骤管理

        private class Step // 操作步骤。
        {
            public Step()
            {
                Array = new Int32[CAPACITY, CAPACITY];
            }

            public Int32[,] Array; // 二维矩阵。
            public double Score; // 得分。

            public Directions Direction; // 逻辑平移方向。

            public Point Index; // 索引。
            public Int32 Value; // 元素的值。
        }

        private List<Step> StepList_Previous = new List<Step>(0); // 操作步骤列表（之前的）。
        private List<Step> StepList_Next = new List<Step>(0); // 操作步骤列表（接下来的）。

        private void StepList_Clear()
        {
            //
            // 清空操作步骤列表。
            //

            StepList_Previous.Clear();
            StepList_Next.Clear();

            PictureBox_Undo.Enabled = false;
            PictureBox_Redo.Enabled = false;
        }

        private void StepList_Append(Step S)
        {
            //
            // 追加一步操作。S：操作步骤。
            //

            if (EnableUndo)
            {
                StepList_Previous.Add(S);

                PictureBox_Undo.Enabled = true;

                //

                StepList_Next.Clear();

                PictureBox_Redo.Enabled = false;
            }
        }

        private void StepList_Undo()
        {
            //
            // 撤销一步操作。
            //

            if (EnableUndo)
            {
                if (StepList_Previous.Count > 0)
                {
                    Step Previous = StepList_Previous[StepList_Previous.Count - 1];
                    Step S = new Step();

                    S.Direction = Previous.Direction;
                    S.Index = Previous.Index;
                    S.Value = Previous.Value;

                    StepList_Next.Add(S);

                    PictureBox_Redo.Enabled = true;

                    //

                    List<Point> ElementIndexList_Copy = new List<Point>(ElementIndexList);

                    ElementMatrix_Initialize();

                    ElementMatrix_AnimatePresentAt(ElementIndexList_Copy, false);

                    for (int X = 0; X < Range.Width; X++)
                    {
                        for (int Y = 0; Y < Range.Height; Y++)
                        {
                            Int32 E = Previous.Array[X, Y];

                            if (E != 0)
                            {
                                ElementMatrix_Add(new Point(X, Y), E);
                            }
                        }
                    }

                    ThisRecord.Score = Previous.Score;

                    //

                    StepList_Previous.RemoveAt(StepList_Previous.Count - 1);

                    //

                    ElementMatrix_RepresentAll();

                    Judgement();
                }

                if (StepList_Previous.Count == 0)
                {
                    PictureBox_Undo.Enabled = false;

                    Panel_Environment.Focus();
                }
            }
        }

        private void StepList_Redo()
        {
            //
            // 重做一步操作。
            //

            if (EnableUndo)
            {
                if (StepList_Next.Count > 0)
                {
                    Step Next = StepList_Next[StepList_Next.Count - 1];
                    Step S = new Step();

                    S.Array = GetCopyOfArray(ElementMatrix);

                    S.Score = ThisRecord.Score;

                    S.Direction = Next.Direction;
                    S.Index = Next.Index;
                    S.Value = Next.Value;

                    StepList_Previous.Add(S);

                    PictureBox_Undo.Enabled = true;

                    //

                    ElementMatrix_LogicalMove(S.Direction);

                    ElementMatrix_LogicalAppend(S.Index, S.Value);

                    //

                    StepList_Next.RemoveAt(StepList_Next.Count - 1);

                    //

                    ElementMatrix_RepresentAll();

                    Judgement();
                }

                if (StepList_Next.Count == 0)
                {
                    PictureBox_Redo.Enabled = false;

                    Panel_Environment.Focus();
                }
            }
        }

        #endregion

        #region 中断管理

        // 判定。

        private void Judgement()
        {
            //
            // 失败判定。
            //

            ThisRecord.Max = GetMaxValueOfElementMatrix();
            ThisRecord.Sum = GetSumOfElementMatrix();

            //

            if (!GameIsOver)
            {
                if (!GetLogicalMovableOfElementMatrix())
                {
                    GameIsOver = true;

                    ElementMatrix_RepresentAll();

                    ThisGameTime = (DateTime.Now - GameStartingTime);
                    TotalGameTime += ThisGameTime;

                    ThisRecord.Range = Range;
                    ThisRecord.Probability = Probability;
                    ThisRecord.EnableUndo = EnableUndo;

                    SaveUserData();

                    EraseLastGame();

                    StepList_Clear();
                }
            }

            //

            RepaintCurBmp();
        }

        // 中断。

        private enum InterruptActions { NULL = -1, StartNew, Continue, Undo, Redo, Restart, ExitGame, CloseApp, COUNT } // 中断动作枚举。

        private void Interrupt(InterruptActions IA)
        {
            //
            // 中断。
            //

            switch (IA)
            {
                case InterruptActions.StartNew: // 开始新游戏。
                    {
                        EraseLastGame();

                        //

                        EnterGameUI();

                        //

                        ElementMatrix_Add(GetRandomZeroIndexOfArray(ElementMatrix, Range), GetRandomElementValue());
                        ElementMatrix_Add(GetRandomZeroIndexOfArray(ElementMatrix, Range), GetRandomElementValue());

                        ElementMatrix_AnimatePresentAt(ElementIndexList, true);

                        Judgement();
                    }
                    break;

                case InterruptActions.Continue: // 继续上次的游戏。
                    {
                        Range = Record_Last.Range;
                        Probability = Record_Last.Probability;
                        EnableUndo = Record_Last.EnableUndo;

                        //

                        EnterGameUI();

                        //

                        LoadGameStepInBackground();

                        //

                        CheckBox_EnableUndo.CheckedChanged += CheckBox_EnableUndo_CheckedChanged;

                        CheckBox_EnableUndo.Checked = EnableUndo;

                        CheckBox_EnableUndo.CheckedChanged += CheckBox_EnableUndo_CheckedChanged;

                        ResetSaveOptionsControl();

                        ResetSaveStepRadioButtons();

                        //

                        ComboBox_Range_Width.SelectedIndexChanged -= ComboBox_Range_Width_SelectedIndexChanged;
                        ComboBox_Range_Height.SelectedIndexChanged -= ComboBox_Range_Height_SelectedIndexChanged;

                        ComboBox_Range_Width.SelectedIndex = ComboBox_Range_Width.Items.IndexOf(Range.Width.ToString());
                        ComboBox_Range_Height.SelectedIndex = ComboBox_Range_Height.Items.IndexOf(Range.Height.ToString());

                        ComboBox_Range_Width.SelectedIndexChanged += ComboBox_Range_Width_SelectedIndexChanged;
                        ComboBox_Range_Height.SelectedIndexChanged += ComboBox_Range_Height_SelectedIndexChanged;
                    }
                    break;

                case InterruptActions.Undo: // 撤销一步。
                    {
                        StepList_Undo();
                    }
                    break;

                case InterruptActions.Redo: // 重做一步。
                    {
                        StepList_Redo();
                    }
                    break;

                case InterruptActions.Restart: // 重新开始。
                    {
                        EraseLastGame();

                        //

                        if (!GameIsOver)
                        {
                            TotalGameTime += (DateTime.Now - GameStartingTime);
                        }

                        ThisRecord.Range = Range;
                        ThisRecord.Probability = Probability;
                        ThisRecord.EnableUndo = EnableUndo;

                        SaveUserData();

                        GameStartingTime = DateTime.Now;

                        //

                        GameIsOver = false;

                        ThisRecord = new Record();

                        StepList_Clear();

                        ElementMatrix_Initialize();

                        RepaintCurBmp();

                        ElementMatrix_Add(GetRandomZeroIndexOfArray(ElementMatrix, Range), GetRandomElementValue());
                        ElementMatrix_Add(GetRandomZeroIndexOfArray(ElementMatrix, Range), GetRandomElementValue());

                        ElementMatrix_AnimatePresentAt(ElementIndexList, true);

                        Judgement();

                        //

                        Panel_Environment.Focus();
                    }
                    break;

                case InterruptActions.ExitGame: // 退出游戏。
                    {
                        if (!GameIsOver)
                        {
                            ThisGameTime = (DateTime.Now - GameStartingTime);
                            TotalGameTime += ThisGameTime;
                        }

                        ThisRecord.Range = Range;
                        ThisRecord.Probability = Probability;
                        ThisRecord.EnableUndo = EnableUndo;

                        SaveUserData();

                        //

                        Panel_Environment.Focus();

                        //

                        if (!GameIsOver && (ThisRecord.Score > 0 || StepList_Previous.Count + StepList_Next.Count > 0))
                        {
                            SaveGameStepInBackground();
                        }
                        else
                        {
                            ExitGameUI();
                        }
                    }
                    break;

                case InterruptActions.CloseApp: // 关闭程序。
                    {
                        if (!GameIsOver)
                        {
                            ThisGameTime = (DateTime.Now - GameStartingTime);
                            TotalGameTime += ThisGameTime;
                        }

                        ThisRecord.Range = Range;
                        ThisRecord.Probability = Probability;
                        ThisRecord.EnableUndo = EnableUndo;

                        SaveUserData();

                        //

                        if ((!BackgroundWorker_LoadGameStep.IsBusy && !BackgroundWorker_SaveGameStep.IsBusy) && !GameIsOver && (ThisRecord.Score > 0 || StepList_Previous.Count + StepList_Next.Count > 0))
                        {
                            SaveGameStepInForeground();
                        }
                    }
                    break;
            }
        }

        // 中断按钮。

        private static class InterruptImages // 包含表示中断的图像的静态类。
        {
            private static readonly Size _Size = new Size(25, 25);

            private static Bitmap _Undo = null;
            private static Bitmap _Redo = null;
            private static Bitmap _Restart = null;
            private static Bitmap _ExitGame = null;

            //

            public static Bitmap Undo => _Undo; // 撤销一步。
            public static Bitmap Redo => _Redo; // 重做一步。
            public static Bitmap Restart => _Restart; // 重新开始。
            public static Bitmap ExitGame => _ExitGame; // 退出游戏。

            //

            public static void Update(Color color) // 使用指定的颜色更新所有图像。
            {
                _Undo = new Bitmap(_Size.Width, _Size.Height);

                using (Graphics Grap = Graphics.FromImage(_Undo))
                {
                    Grap.SmoothingMode = SmoothingMode.AntiAlias;

                    Grap.DrawLine(new Pen(color, 2F), new Point(5, 12), new Point(19, 12));
                    Grap.DrawLines(new Pen(color, 2F), new Point[] { new Point(12, 5), new Point(5, 12), new Point(12, 19) });
                }

                //

                _Redo = new Bitmap(_Size.Width, _Size.Height);

                using (Graphics Grap = Graphics.FromImage(_Redo))
                {
                    Grap.SmoothingMode = SmoothingMode.AntiAlias;

                    Grap.DrawLine(new Pen(color, 2F), new Point(5, 12), new Point(19, 12));
                    Grap.DrawLines(new Pen(color, 2F), new Point[] { new Point(12, 5), new Point(19, 12), new Point(12, 19) });
                }

                //

                _Restart = new Bitmap(_Size.Width, _Size.Height);

                using (Graphics Grap = Graphics.FromImage(_Restart))
                {
                    Grap.SmoothingMode = SmoothingMode.AntiAlias;

                    Grap.DrawArc(new Pen(color, 2F), new Rectangle(new Point(5, 5), new Size(15, 15)), -150F, 300F);
                    Grap.DrawLines(new Pen(color, 2F), new Point[] { new Point(5, 5), new Point(5, 10), new Point(10, 10) });
                }

                //

                _ExitGame = new Bitmap(_Size.Width, _Size.Height);

                using (Graphics Grap = Graphics.FromImage(_ExitGame))
                {
                    Grap.SmoothingMode = SmoothingMode.AntiAlias;

                    Grap.DrawLine(new Pen(color, 2F), new Point(5, 5), new Point(19, 19));
                    Grap.DrawLine(new Pen(color, 2F), new Point(19, 5), new Point(5, 19));
                }
            }
        }

        private void ResetInterruptControls()
        {
            //
            // 重置中断控件。
            //

            PictureBox_Undo.Visible = PictureBox_Redo.Visible = EnableUndo;

            PictureBox_Restart.Left = (EnableUndo ? PictureBox_Redo.Right : 0);
            PictureBox_ExitGame.Left = PictureBox_Restart.Right;

            Panel_Interrupt.Width = PictureBox_ExitGame.Right;
            Panel_Interrupt.Left = Panel_Current.Width - Panel_Interrupt.Width;
        }

        private void Label_StartNewGame_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_StartNewGame。
            //

            Interrupt(InterruptActions.StartNew);
        }

        private void Label_ContinueLastGame_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_ContinueLastGame。
            //

            Interrupt(InterruptActions.Continue);
        }

        private void PictureBox_Undo_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Undo。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            if (StepList_Previous.Count > 0)
            {
                ToolTip_InterruptPrompt.SetToolTip(PictureBox_Undo, "撤销一步");
            }
        }

        private void PictureBox_Undo_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_Undo。
            //

            Interrupt(InterruptActions.Undo);
        }

        private void PictureBox_Redo_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Redo。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            if (StepList_Next.Count > 0)
            {
                ToolTip_InterruptPrompt.SetToolTip(PictureBox_Redo, "重做一步");
            }
        }

        private void PictureBox_Redo_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_Redo。
            //

            Interrupt(InterruptActions.Redo);
        }

        private void PictureBox_Restart_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Restart。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            ToolTip_InterruptPrompt.SetToolTip(PictureBox_Restart, "重新开始");
        }

        private void PictureBox_Restart_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_Restart。
            //

            Interrupt(InterruptActions.Restart);
        }

        private void PictureBox_ExitGame_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_ExitGame。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            ToolTip_InterruptPrompt.SetToolTip(PictureBox_ExitGame, ((!GameIsOver && (ThisRecord.Score > 0 || StepList_Previous.Count + StepList_Next.Count > 0)) ? "保存并退出" : "退出"));
        }

        private void PictureBox_ExitGame_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_ExitGame。
            //

            Interrupt(InterruptActions.ExitGame);
        }

        #endregion

        #region UI 切换

        private bool GameUINow = false; // 当前 UI 是否为游戏 UI。

        private void EnterGameUI()
        {
            //
            // 进入游戏 UI。
            //

            GameUINow = true;

            //

            ElementMatrix_Initialize();

            //

            GameIsOver = false;

            GameStartingTime = DateTime.Now;

            ThisRecord = new Record();

            StepList_Clear();

            //

            ResetInterruptControls();

            //

            Panel_FunctionArea.Visible = false;
            Panel_GameUI.Visible = true;

            //

            Panel_Environment.Focus();

            //

            while (ElementSize * Range.Width > Screen.PrimaryScreen.WorkingArea.Width || Me.CaptionBarHeight + Panel_Current.Height + ElementSize * Range.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                ElementSize = ElementSize * 9 / 10;
            }

            Rectangle NewBounds = new Rectangle();
            NewBounds.Size = new Size(ElementSize * Range.Width, Me.CaptionBarHeight + Panel_Current.Height + ElementSize * Range.Height);
            NewBounds.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - NewBounds.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - NewBounds.Height) / 2);
            Me.Bounds = NewBounds;

            ElementSize = Math.Max(1, Math.Min(Panel_Environment.Width / Range.Width, Panel_Environment.Height / Range.Height));

            EAryBmpRect.Size = new Size(Math.Max(1, ElementSize * Range.Width), Math.Max(1, ElementSize * Range.Height));
            EAryBmpRect.Location = new Point((Panel_Environment.Width - EAryBmpRect.Width) / 2, (Panel_Environment.Height - EAryBmpRect.Height) / 2);

            //

            RepaintCurBmp();

            ElementMatrix_RepresentAll();
        }

        private void ExitGameUI()
        {
            //
            // 退出游戏 UI。
            //

            GameUINow = false;

            //

            Panel_FunctionArea.Visible = true;
            Panel_GameUI.Visible = false;

            //

            ElementMatrix_Initialize();

            //

            Rectangle NewBounds = new Rectangle();
            NewBounds.Size = new Size(FormClientInitialSize.Width, Me.CaptionBarHeight + FormClientInitialSize.Height);
            NewBounds.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - NewBounds.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - NewBounds.Height) / 2);
            Me.Bounds = NewBounds;

            //

            FunctionAreaTab = FunctionAreaTabs.Start;
        }

        #endregion

        #region 游戏 UI 交互

        private bool TryLogicalMove(Directions D)
        {
            //
            // 通过某种交互方式尝试将元素矩阵向指定方向进行逻辑平移，并返回此操作之后的元素矩阵是否与之前的元素矩阵相同。D：逻辑平移方向。
            //

            Step S = new Step();

            S.Array = GetCopyOfArray(ElementMatrix);

            S.Score = ThisRecord.Score;

            S.Direction = D;

            bool Flag = ElementMatrix_LogicalMove(S.Direction);

            if (!Flag)
            {
                S.Index = GetRandomZeroIndexOfArray(ElementMatrix, Range);

                S.Value = GetRandomElementValue();

                StepList_Append(S);

                ElementMatrix_LogicalAppend(S.Index, S.Value);
            }

            return Flag;
        }

        private const double TouchSlideEffMinDist = 60; // 使触屏滑动有效的“或”判据：滑动最小距离（像素）。
        private const double TouchSlideEffMinVelo = 240; // 使触屏滑动有效的“与或”判据：滑动最小速度（像素每秒）。
        private const double TouchSlideEffMinLT = 2; // 使触屏滑动有效的“与或”判据：滑动最小距离时间（像素秒）。

        private bool TouchDown = false; // 是否在触屏按下。

        private Point TouchDownPosition = new Point(); // 在触屏按下的位置。
        private DateTime TouchDownTime = new DateTime(); // 在触屏按下的时刻。

        private void Panel_Environment_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Panel_Environment。
            //

            if (!BackgroundWorker_LoadGameStep.IsBusy && !BackgroundWorker_SaveGameStep.IsBusy)
            {
                if (OperationMode == OperationModes.MouseClick)
                {
                    if (!GameIsOver && e.Button == MouseButtons.Left)
                    {
                        double CursorAngle = Com.Geometry.GetAngleOfTwoPoints(new Com.PointD(Panel_Environment.Width / 2, Panel_Environment.Height / 2), new Com.PointD(Com.Geometry.GetCursorPositionOfControl(Panel_Environment)));

                        if (CursorAngle >= Math.PI / 4 && CursorAngle < Math.PI * 3 / 4)
                        {
                            TryLogicalMove(Directions.Y_INC);
                        }
                        else if (CursorAngle >= Math.PI * 3 / 4 && CursorAngle < Math.PI * 5 / 4)
                        {
                            TryLogicalMove(Directions.X_DEC);
                        }
                        else if (CursorAngle >= Math.PI * 5 / 4 && CursorAngle < Math.PI * 7 / 4)
                        {
                            TryLogicalMove(Directions.Y_DEC);
                        }
                        else
                        {
                            TryLogicalMove(Directions.X_INC);
                        }
                    }
                }
                else if (OperationMode == OperationModes.TouchSlide)
                {
                    if (!GameIsOver && e.Button == MouseButtons.Left)
                    {
                        TouchDown = true;

                        TouchDownPosition = Com.Geometry.GetCursorPositionOfControl(Panel_Environment);
                        TouchDownTime = DateTime.Now;
                    }
                }
            }
        }

        private void Panel_Environment_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // 鼠标释放 Panel_Environment。
            //

            if (!BackgroundWorker_LoadGameStep.IsBusy && !BackgroundWorker_SaveGameStep.IsBusy)
            {
                if (OperationMode == OperationModes.TouchSlide)
                {
                    if (!GameIsOver && e.Button == MouseButtons.Left)
                    {
                        TouchDown = false;
                    }
                }
            }
        }

        private void Panel_Environment_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Panel_Environment。
            //

            if (!BackgroundWorker_LoadGameStep.IsBusy && !BackgroundWorker_SaveGameStep.IsBusy)
            {
                if (Me.IsActive)
                {
                    Panel_Environment.Focus();
                }

                //

                if (OperationMode == OperationModes.TouchSlide && TouchDown)
                {
                    Point TouchPosition = Com.Geometry.GetCursorPositionOfControl(Panel_Environment);
                    DateTime TouchTime = DateTime.Now;

                    double TouchDist = Com.PointD.DistanceBetween(new Com.PointD(TouchPosition), new Com.PointD(TouchDownPosition));
                    double TouchSec = (TouchTime - TouchDownTime).TotalSeconds;

                    if (TouchDist >= TouchSlideEffMinDist || (TouchDist / TouchSec >= TouchSlideEffMinVelo && TouchDist * TouchSec >= TouchSlideEffMinLT))
                    {
                        TouchDown = false;

                        double CursorAngle = Com.Geometry.GetAngleOfTwoPoints(new Com.PointD(TouchDownPosition), new Com.PointD(TouchPosition));

                        if (CursorAngle >= Math.PI / 4 && CursorAngle < Math.PI * 3 / 4)
                        {
                            TryLogicalMove(Directions.Y_INC);
                        }
                        else if (CursorAngle >= -Math.PI * 3 / 4 && CursorAngle < -Math.PI / 4)
                        {
                            TryLogicalMove(Directions.Y_DEC);
                        }
                        else if (CursorAngle >= -Math.PI / 4 && CursorAngle < Math.PI / 4)
                        {
                            TryLogicalMove(Directions.X_INC);
                        }
                        else
                        {
                            TryLogicalMove(Directions.X_DEC);
                        }
                    }
                }
            }
        }

        private void Panel_Environment_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // 在 Panel_Environment 按下键。
            //

            if (!BackgroundWorker_LoadGameStep.IsBusy && !BackgroundWorker_SaveGameStep.IsBusy)
            {
                if (AlwaysEnableKeyboard || OperationMode == OperationModes.Keyboard)
                {
                    if (!GameIsOver)
                    {
                        if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.Left: TryLogicalMove(Directions.X_DEC); break;
                                case Keys.Right: TryLogicalMove(Directions.X_INC); break;
                                case Keys.Up: TryLogicalMove(Directions.Y_DEC); break;
                                case Keys.Down: TryLogicalMove(Directions.Y_INC); break;
                            }
                        }
                        else
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.PageUp:
                                case Keys.Back: Interrupt(InterruptActions.Undo); break;
                                case Keys.PageDown:
                                case Keys.Space: Interrupt(InterruptActions.Redo); break;
                            }
                        }
                    }

                    switch (e.KeyCode)
                    {
                        case Keys.Home: Interrupt(InterruptActions.Restart); break;
                        case Keys.End:
                        case Keys.Escape: Interrupt(InterruptActions.ExitGame); break;
                    }
                }
            }
        }

        #endregion

        #region 鼠标滚轮功能

        private void Panel_FunctionAreaOptionsBar_MouseWheel(object sender, MouseEventArgs e)
        {
            //
            // 鼠标滚轮在 Panel_FunctionAreaOptionsBar 滚动。
            //

            if (e.Delta < 0 && (Int32)FunctionAreaTab < (Int32)FunctionAreaTabs.COUNT - 1)
            {
                FunctionAreaTab++;
            }
            else if (e.Delta > 0 && (Int32)FunctionAreaTab > 0)
            {
                FunctionAreaTab--;
            }
        }

        private void Panel_Environment_MouseWheel(object sender, MouseEventArgs e)
        {
            //
            // 鼠标滚轮在 Panel_Environment 滚动。
            //

            Rectangle NewBounds = Me.Bounds;

            if (Range.Width <= Range.Height)
            {
                if (e.Delta > 0)
                {
                    NewBounds.Location = new Point(NewBounds.X - NewBounds.Width / 20, NewBounds.Y - NewBounds.Width / 20 * Range.Height / Range.Width);
                    NewBounds.Size = new Size(NewBounds.Width + NewBounds.Width / 20 * 2, NewBounds.Height + NewBounds.Width / 20 * Range.Height / Range.Width * 2);
                }
                else if (e.Delta < 0)
                {
                    NewBounds.Location = new Point(NewBounds.X + NewBounds.Width / 20, NewBounds.Y + NewBounds.Width / 20 * Range.Height / Range.Width);
                    NewBounds.Size = new Size(NewBounds.Width - NewBounds.Width / 20 * 2, NewBounds.Height - NewBounds.Width / 20 * Range.Height / Range.Width * 2);
                }
            }
            else
            {
                if (e.Delta > 0)
                {
                    NewBounds.Location = new Point(NewBounds.X - NewBounds.Height / 20 * Range.Width / Range.Height, NewBounds.Y - NewBounds.Height / 20);
                    NewBounds.Size = new Size(NewBounds.Width + NewBounds.Height / 20 * Range.Width / Range.Height * 2, NewBounds.Height + NewBounds.Height / 20 * 2);
                }
                else if (e.Delta < 0)
                {
                    NewBounds.Location = new Point(NewBounds.X + NewBounds.Height / 20 * Range.Width / Range.Height, NewBounds.Y + NewBounds.Height / 20);
                    NewBounds.Size = new Size(NewBounds.Width - NewBounds.Height / 20 * Range.Width / Range.Height * 2, NewBounds.Height - NewBounds.Height / 20 * 2);
                }
            }

            NewBounds.Location = new Point(Math.Max(0, Math.Min(Screen.PrimaryScreen.WorkingArea.Width - NewBounds.Width, NewBounds.X)), Math.Max(0, Math.Min(Screen.PrimaryScreen.WorkingArea.Height - NewBounds.Height, NewBounds.Y)));

            Me.Bounds = NewBounds;
        }

        #endregion

        #region 计分栏

        private Bitmap CurBmp; // 计分栏位图。

        private void UpdateCurBmp()
        {
            //
            // 更新计分栏位图。
            //

            if (CurBmp != null)
            {
                CurBmp.Dispose();
            }

            CurBmp = new Bitmap(Math.Max(1, Panel_Current.Width), Math.Max(1, Panel_Current.Height));

            using (Graphics CurBmpGrap = Graphics.FromImage(CurBmp))
            {
                if (AntiAlias)
                {
                    CurBmpGrap.SmoothingMode = SmoothingMode.AntiAlias;
                    CurBmpGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
                }

                CurBmpGrap.Clear(GameUIBackColor_DEC);

                //

                Rectangle Rect_Total = new Rectangle(new Point(0, 0), new Size(Math.Max(1, Panel_Current.Width), Math.Max(1, Panel_Current.Height)));
                Rectangle Rect_Current = new Rectangle(Rect_Total.Location, new Size((Int32)Math.Max(2, Math.Min(1, Math.Sqrt(ThisRecord.Sum / TheoreticalMaxValueOfSum)) * Rect_Total.Width), Rect_Total.Height));

                Color RectCr_Total = Me.RecommendColors.Background.ToColor(), RectCr_Current = Me.RecommendColors.Border.ToColor();

                GraphicsPath Path_Total = new GraphicsPath();
                Path_Total.AddRectangle(Rect_Total);
                PathGradientBrush PGB_Total = new PathGradientBrush(Path_Total)
                {
                    CenterColor = RectCr_Total,
                    SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr_Total, 0.3) },
                    FocusScales = new PointF(1F, 0F)
                };
                CurBmpGrap.FillPath(PGB_Total, Path_Total);
                Path_Total.Dispose();
                PGB_Total.Dispose();

                GraphicsPath Path_Current = new GraphicsPath();
                Path_Current.AddRectangle(Rect_Current);
                PathGradientBrush PGB_Current = new PathGradientBrush(Path_Current)
                {
                    CenterColor = RectCr_Current,
                    SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr_Current, 0.3) },
                    FocusScales = new PointF(1F, 0F)
                };
                CurBmpGrap.FillPath(PGB_Current, Path_Current);
                Path_Current.Dispose();
                PGB_Current.Dispose();

                //

                SizeF RegionSize_L = new SizeF(), RegionSize_R = new SizeF();
                RectangleF RegionRect = new RectangleF();

                string StringText_Score = Math.Max(0, ThisRecord.Score).ToString();
                Color StringColor_Score = Me.RecommendColors.Text_INC.ToColor();
                Font StringFont_Score = new Font("微软雅黑", 24F, FontStyle.Regular, GraphicsUnit.Point, 134);
                RectangleF StringRect_Score = new RectangleF();
                StringRect_Score.Size = CurBmpGrap.MeasureString(StringText_Score, StringFont_Score);

                string StringText_Max = "最大值: ", StringText_Max_Val = Math.Max(0, ThisRecord.Max).ToString();
                Color StringColor_Max = Me.RecommendColors.Text.ToColor(), StringColor_Max_Val = Me.RecommendColors.Text_INC.ToColor();
                Font StringFont_Max = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134), StringFont_Max_Val = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
                RectangleF StringRect_Max = new RectangleF(), StringRect_Max_Val = new RectangleF();
                StringRect_Max.Size = CurBmpGrap.MeasureString(StringText_Max, StringFont_Max);
                StringRect_Max_Val.Size = CurBmpGrap.MeasureString(StringText_Max_Val, StringFont_Max_Val);

                string StringText_Sum = "求和: ", StringText_Sum_Val = Math.Max(0, ThisRecord.Sum).ToString();
                Color StringColor_Sum = Me.RecommendColors.Text.ToColor(), StringColor_Sum_Val = Me.RecommendColors.Text_INC.ToColor();
                Font StringFont_Sum = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134), StringFont_Sum_Val = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
                RectangleF StringRect_Sum = new RectangleF(), StringRect_Sum_Val = new RectangleF();
                StringRect_Sum.Size = CurBmpGrap.MeasureString(StringText_Sum, StringFont_Sum);
                StringRect_Sum_Val.Size = CurBmpGrap.MeasureString(StringText_Sum_Val, StringFont_Sum_Val);

                RegionSize_L = StringRect_Score.Size;
                RegionSize_R = new SizeF(Math.Max(StringRect_Max.Width + StringRect_Max_Val.Width, StringRect_Sum.Width + StringRect_Sum_Val.Width), 0);

                RegionRect.Size = new SizeF(Math.Max(RegionSize_L.Width + RegionSize_R.Width, Math.Min(EAryBmpRect.Width, Panel_Interrupt.Left - EAryBmpRect.X)), Panel_Current.Height);
                RegionRect.Location = new PointF(Math.Max(0, Math.Min(EAryBmpRect.X + (EAryBmpRect.Width - RegionRect.Width) / 2, Panel_Interrupt.Left - RegionRect.Width)), 0);

                StringRect_Score.Location = new PointF(RegionRect.X, (RegionRect.Height - StringRect_Score.Height) / 2);

                Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Score, StringFont_Score, StringColor_Score, StringColor_Score, StringRect_Score.Location, 0.05F, AntiAlias);

                StringRect_Max_Val.Location = new PointF(RegionRect.Right - StringRect_Max_Val.Width, (RegionRect.Height / 2 - StringRect_Max_Val.Height) / 2);
                StringRect_Max.Location = new PointF(StringRect_Max_Val.X - StringRect_Max.Width, (RegionRect.Height / 2 - StringRect_Max.Height) / 2);

                Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Max, StringFont_Max, StringColor_Max, StringColor_Max, StringRect_Max.Location, 0.1F, AntiAlias);
                Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Max_Val, StringFont_Max_Val, StringColor_Max_Val, StringColor_Max_Val, StringRect_Max_Val.Location, 0.1F, AntiAlias);

                StringRect_Sum_Val.Location = new PointF(RegionRect.Right - StringRect_Sum_Val.Width, RegionRect.Height / 2 + (RegionRect.Height / 2 - StringRect_Sum_Val.Height) / 2);
                StringRect_Sum.Location = new PointF(StringRect_Sum_Val.X - StringRect_Sum.Width, RegionRect.Height / 2 + (RegionRect.Height / 2 - StringRect_Sum.Height) / 2);

                Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Sum, StringFont_Sum, StringColor_Sum, StringColor_Sum, StringRect_Sum.Location, 0.1F, AntiAlias);
                Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Sum_Val, StringFont_Sum_Val, StringColor_Sum_Val, StringColor_Sum_Val, StringRect_Sum_Val.Location, 0.1F, AntiAlias);
            }
        }

        private void RepaintCurBmp()
        {
            //
            // 更新并重绘计分栏位图。
            //

            UpdateCurBmp();

            if (CurBmp != null)
            {
                Panel_Current.CreateGraphics().DrawImage(CurBmp, new Point(0, 0));

                foreach (object Obj in Panel_Current.Controls)
                {
                    ((Control)Obj).Refresh();
                }
            }
        }

        private void Panel_Current_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Current 绘图。
            //

            UpdateCurBmp();

            if (CurBmp != null)
            {
                e.Graphics.DrawImage(CurBmp, new Point(0, 0));
            }
        }

        #endregion

        #region 功能区

        private enum FunctionAreaTabs { NULL = -1, Start, Record, Options, About, COUNT } // 功能区选项卡枚举。

        private FunctionAreaTabs _FunctionAreaTab = FunctionAreaTabs.NULL; // 当前打开的功能区选项卡。
        private FunctionAreaTabs FunctionAreaTab
        {
            get
            {
                return _FunctionAreaTab;
            }

            set
            {
                _FunctionAreaTab = value;

                Color TabBtnCr_Fr_Seld = Me.RecommendColors.Main_INC.ToColor(), TabBtnCr_Fr_Uns = Color.White;
                Color TabBtnCr_Bk_Seld = Color.Transparent, TabBtnCr_Bk_Uns = Color.Transparent;
                Font TabBtnFt_Seld = new Font("微软雅黑", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 134), TabBtnFt_Uns = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);

                Label_Tab_Start.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Start ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Start.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Start ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Start.Font = (_FunctionAreaTab == FunctionAreaTabs.Start ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_Record.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Record ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Record.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Record ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Record.Font = (_FunctionAreaTab == FunctionAreaTabs.Record ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_Options.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Options ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Options.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Options ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Options.Font = (_FunctionAreaTab == FunctionAreaTabs.Options ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_About.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_About.BackColor = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_About.Font = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnFt_Seld : TabBtnFt_Uns);

                switch (_FunctionAreaTab)
                {
                    case FunctionAreaTabs.Start:
                        {
                            if (ElementIndexList_Last.Count > 0)
                            {
                                Label_ContinueLastGame.Visible = true;

                                Label_ContinueLastGame.Focus();
                            }
                            else
                            {
                                Label_ContinueLastGame.Visible = false;

                                Label_StartNewGame.Focus();
                            }
                        }
                        break;

                    case FunctionAreaTabs.Record:
                        {
                            if (BestRecord.Score == 0)
                            {
                                Label_ThisRecordVal_Score.Text = "无记录";
                                Label_ThisRecordVal_MaxAndSum.Text = "最大值: 无 / 求和: 无";
                                Label_BestRecordVal_Score.Text = "无记录";
                                Label_BestRecordVal_MaxAndSum.Text = "最大值: 无 / 求和: 无";
                            }
                            else
                            {
                                Record ThRec = new Record();

                                if (ThisRecord.Range == Range)
                                {
                                    ThRec = ThisRecord;
                                }

                                Label_ThisRecordVal_Score.Text = ThRec.Score.ToString();
                                Label_ThisRecordVal_MaxAndSum.Text = "最大值: " + ThRec.Max + " / 求和: " + ThRec.Sum;
                                Label_BestRecordVal_Score.Text = BestRecord.Score.ToString();
                                Label_BestRecordVal_MaxAndSum.Text = "最大值: " + BestRecord.Max + " / 求和: " + BestRecord.Sum;
                            }

                            Label_ThisTimeVal.Text = Com.Text.GetTimeStringFromTimeSpan(ThisGameTime);
                            Label_TotalTimeVal.Text = Com.Text.GetTimeStringFromTimeSpan(TotalGameTime);
                        }
                        break;

                    case FunctionAreaTabs.Options:
                        {
                            ResetSaveOptionsControl();
                        }
                        break;

                    case FunctionAreaTabs.About:
                        {

                        }
                        break;
                }

                Timer_EnterPrompt.Enabled = (_FunctionAreaTab == FunctionAreaTabs.Start);

                if (Panel_FunctionAreaTab.AutoScroll)
                {
                    // Panel 的 AutoScroll 功能似乎存在 bug，下面的代码可以规避某些显示问题

                    Panel_FunctionAreaTab.AutoScroll = false;

                    foreach (object Obj in Panel_FunctionAreaTab.Controls)
                    {
                        if (Obj is Panel)
                        {
                            Panel Pnl = Obj as Panel;

                            Pnl.Location = new Point(0, 0);
                        }
                    }

                    Panel_FunctionAreaTab.AutoScroll = true;
                }

                Panel_Tab_Start.Visible = (_FunctionAreaTab == FunctionAreaTabs.Start);
                Panel_Tab_Record.Visible = (_FunctionAreaTab == FunctionAreaTabs.Record);
                Panel_Tab_Options.Visible = (_FunctionAreaTab == FunctionAreaTabs.Options);
                Panel_Tab_About.Visible = (_FunctionAreaTab == FunctionAreaTabs.About);
            }
        }

        private void Label_Tab_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 Label_Tab。
            //

            Panel_FunctionAreaOptionsBar.Refresh();
        }

        private void Label_Tab_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Label_Tab。
            //

            Panel_FunctionAreaOptionsBar.Refresh();
        }

        private void Label_Tab_Start_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_Start。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Start)
                {
                    FunctionAreaTab = FunctionAreaTabs.Start;
                }
            }
        }

        private void Label_Tab_Record_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_Record。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Record)
                {
                    FunctionAreaTab = FunctionAreaTabs.Record;
                }
            }
        }

        private void Label_Tab_Options_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_Options。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Options)
                {
                    FunctionAreaTab = FunctionAreaTabs.Options;
                }
            }
        }

        private void Label_Tab_About_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_About。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.About)
                {
                    FunctionAreaTab = FunctionAreaTabs.About;
                }
            }
        }

        #endregion

        #region "开始"区域

        private const Int32 EnterGameButtonHeight_Min = 30, EnterGameButtonHeight_Max = 50; // 进入游戏按钮高度的取值范围。

        private Color EnterGameBackColor_INC = Color.Empty; // Panel_EnterGameSelection 绘图使用的颜色（深色）。
        private Color EnterGameBackColor_DEC => Panel_FunctionArea.BackColor; // Panel_EnterGameSelection 绘图使用的颜色（浅色）。

        private void Panel_EnterGameSelection_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_EnterGameSelection 绘图。
            //

            Rectangle Rect_StartNew = new Rectangle(Label_StartNewGame.Location, Label_StartNewGame.Size);

            Color Cr_StartNew = Com.ColorManipulation.BlendByRGB(EnterGameBackColor_INC, EnterGameBackColor_DEC, Math.Sqrt((double)(Label_StartNewGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min)));

            GraphicsPath Path_StartNew = new GraphicsPath();
            Path_StartNew.AddRectangle(Rect_StartNew);
            PathGradientBrush PGB_StartNew = new PathGradientBrush(Path_StartNew)
            {
                CenterColor = Cr_StartNew,
                SurroundColors = new Color[] { Com.ColorManipulation.BlendByRGB(Cr_StartNew, EnterGameBackColor_DEC, 0.7) },
                FocusScales = new PointF(1F, 0F)
            };
            e.Graphics.FillPath(PGB_StartNew, Path_StartNew);
            Path_StartNew.Dispose();
            PGB_StartNew.Dispose();

            //

            if (Label_ContinueLastGame.Visible)
            {
                Rectangle Rect_Continue = new Rectangle(Label_ContinueLastGame.Location, Label_ContinueLastGame.Size);

                Color Cr_Continue = Com.ColorManipulation.BlendByRGB(EnterGameBackColor_INC, EnterGameBackColor_DEC, Math.Sqrt((double)(Label_ContinueLastGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min)));

                GraphicsPath Path_Continue = new GraphicsPath();
                Path_Continue.AddRectangle(Rect_Continue);
                PathGradientBrush PGB_Continue = new PathGradientBrush(Path_Continue)
                {
                    CenterColor = Cr_Continue,
                    SurroundColors = new Color[] { Com.ColorManipulation.BlendByRGB(Cr_Continue, EnterGameBackColor_DEC, 0.7) },
                    FocusScales = new PointF(1F, 0F)
                };
                e.Graphics.FillPath(PGB_Continue, Path_Continue);
                Path_Continue.Dispose();
                PGB_Continue.Dispose();
            }
        }

        private double EnterPrompt_Val = 0; // 闪烁相位。
        private double EnterPrompt_Step = 0.025; // 闪烁步长。

        private void Timer_EnterPrompt_Tick(object sender, EventArgs e)
        {
            //
            // Timer_EnterPrompt。
            //

            if (EnterPrompt_Val >= 0 && EnterPrompt_Val <= 1)
            {
                EnterPrompt_Val += EnterPrompt_Step;
            }

            if (EnterPrompt_Val < 0 || EnterPrompt_Val > 1)
            {
                EnterPrompt_Val = Math.Max(0, Math.Min(EnterPrompt_Val, 1));

                EnterPrompt_Step = -EnterPrompt_Step;
            }

            EnterGameBackColor_INC = Com.ColorManipulation.BlendByRGB(Me.RecommendColors.Border_INC, Me.RecommendColors.Border, EnterPrompt_Val).ToColor();

            //

            if (Label_ContinueLastGame.Visible)
            {
                Label_StartNewGame.Top = 0;

                if (Com.Geometry.CursorIsInControl(Label_StartNewGame))
                {
                    Label_StartNewGame.Height = Math.Max(EnterGameButtonHeight_Min, Math.Min(EnterGameButtonHeight_Max, Label_StartNewGame.Height + Math.Max(1, (EnterGameButtonHeight_Max - Label_StartNewGame.Height) / 4)));
                }
                else
                {
                    Label_StartNewGame.Height = Math.Max(EnterGameButtonHeight_Min, Math.Min(EnterGameButtonHeight_Max, Label_StartNewGame.Height - Math.Max(1, (Label_StartNewGame.Height - EnterGameButtonHeight_Min) / 4)));
                }

                Label_ContinueLastGame.Top = Label_StartNewGame.Bottom;
                Label_ContinueLastGame.Height = Panel_EnterGameSelection.Height - Label_ContinueLastGame.Top;
            }
            else
            {
                Label_StartNewGame.Height = EnterGameButtonHeight_Max;

                Label_StartNewGame.Top = (Panel_EnterGameSelection.Height - Label_StartNewGame.Height) / 2;
            }

            Label_StartNewGame.Width = (Int32)(Math.Sqrt((double)Label_StartNewGame.Height / EnterGameButtonHeight_Max) * Panel_EnterGameSelection.Width);
            Label_StartNewGame.Left = (Panel_EnterGameSelection.Width - Label_StartNewGame.Width) / 2;

            Label_ContinueLastGame.Width = (Int32)(Math.Sqrt((double)Label_ContinueLastGame.Height / EnterGameButtonHeight_Max) * Panel_EnterGameSelection.Width);
            Label_ContinueLastGame.Left = (Panel_EnterGameSelection.Width - Label_ContinueLastGame.Width) / 2;

            Label_StartNewGame.Font = new Font("微软雅黑", Math.Max(1F, (Label_StartNewGame.Height - 4) / 3F), FontStyle.Regular, GraphicsUnit.Point, 134);
            Label_ContinueLastGame.Font = new Font("微软雅黑", Math.Max(1F, (Label_ContinueLastGame.Height - 4) / 3F), FontStyle.Regular, GraphicsUnit.Point, 134);

            Label_StartNewGame.ForeColor = Com.ColorManipulation.BlendByRGB(Me.RecommendColors.Text_INC, Me.RecommendColors.Text, Math.Sqrt((double)(Label_StartNewGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min))).ToColor();
            Label_ContinueLastGame.ForeColor = Com.ColorManipulation.BlendByRGB(Me.RecommendColors.Text_INC, Me.RecommendColors.Text, Math.Sqrt((double)(Label_ContinueLastGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min))).ToColor();

            //

            Panel_EnterGameSelection.Refresh();
        }

        #endregion

        #region "记录"区域

        private void PaintScore(PaintEventArgs e)
        {
            //
            // 绘制成绩。
            //

            Graphics Grap = e.Graphics;
            Grap.SmoothingMode = SmoothingMode.AntiAlias;

            //

            Int32 RectBottom = Panel_Score.Height - 50;

            Size RectSize_Max = new Size(Math.Max(2, Panel_Score.Width / 8), Math.Max(2, Panel_Score.Height - 120));
            Size RectSize_Min = new Size(Math.Max(2, Panel_Score.Width / 8), 2);

            Rectangle Rect_This = new Rectangle();
            Rectangle Rect_Best = new Rectangle();

            if (BestRecord.Score == 0)
            {
                Rect_Best.Size = new Size(RectSize_Max.Width, RectSize_Min.Height);
                Rect_This.Size = new Size(Rect_Best.Width, RectSize_Min.Height);
            }
            else
            {
                Record ThRec = new Record();

                if (ThisRecord.Range == Range)
                {
                    ThRec = ThisRecord;
                }

                if (BestRecord.Score >= ThRec.Score)
                {
                    Rect_Best.Size = RectSize_Max;
                    Rect_This.Size = new Size(Rect_Best.Width, (Int32)Math.Max(RectSize_Min.Height, Math.Sqrt(ThRec.Score / BestRecord.Score) * Rect_Best.Height));
                }
                else
                {
                    Rect_This.Size = RectSize_Max;
                    Rect_Best.Size = new Size(Rect_This.Width, (Int32)Math.Max(RectSize_Min.Height, Math.Sqrt(BestRecord.Score / ThRec.Score) * Rect_This.Height));
                }
            }

            Rect_This.Location = new Point((Panel_Score.Width / 2 - Rect_This.Width) / 2, RectBottom - Rect_This.Height);
            Rect_Best.Location = new Point(Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Rect_Best.Width) / 2, RectBottom - Rect_Best.Height);

            Color RectCr = Me.RecommendColors.Border.ToColor();

            GraphicsPath Path_This = new GraphicsPath();
            Path_This.AddRectangle(Rect_This);
            PathGradientBrush PGB_This = new PathGradientBrush(Path_This)
            {
                CenterColor = RectCr,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr, 0.3) },
                FocusScales = new PointF(0F, 1F)
            };
            Grap.FillPath(PGB_This, Path_This);
            Path_This.Dispose();
            PGB_This.Dispose();

            GraphicsPath Path_Best = new GraphicsPath();
            Path_Best.AddRectangle(Rect_Best);
            PathGradientBrush PGB_Best = new PathGradientBrush(Path_Best)
            {
                CenterColor = RectCr,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr, 0.3) },
                FocusScales = new PointF(0F, 1F)
            };
            Grap.FillPath(PGB_Best, Path_Best);
            Path_Best.Dispose();
            PGB_Best.Dispose();

            //

            Label_ThisRecordVal_MaxAndSum.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_ThisRecordVal_MaxAndSum.Width, (Panel_Score.Width / 2 - Label_ThisRecordVal_MaxAndSum.Width) / 2));
            Label_ThisRecordVal_MaxAndSum.Top = Rect_This.Y - 5 - Label_ThisRecordVal_MaxAndSum.Height;
            Label_ThisRecordVal_Score.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_ThisRecordVal_Score.Width, (Panel_Score.Width / 2 - Label_ThisRecordVal_Score.Width) / 2));
            Label_ThisRecordVal_Score.Top = Label_ThisRecordVal_MaxAndSum.Top - Label_ThisRecordVal_Score.Height;

            Label_BestRecordVal_MaxAndSum.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_BestRecordVal_MaxAndSum.Width, Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Label_BestRecordVal_MaxAndSum.Width) / 2));
            Label_BestRecordVal_MaxAndSum.Top = Rect_Best.Y - 5 - Label_BestRecordVal_MaxAndSum.Height;
            Label_BestRecordVal_Score.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_BestRecordVal_Score.Width, Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Label_BestRecordVal_Score.Width) / 2));
            Label_BestRecordVal_Score.Top = Label_BestRecordVal_MaxAndSum.Top - Label_BestRecordVal_Score.Height;
        }

        #endregion

        #region "选项"区域

        // 布局。

        private void ComboBox_Range_Width_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            // ComboBox_Range_Width 选中项索引改变。
            //

            Range.Width = Convert.ToInt32(ComboBox_Range_Width.Text);

            //

            UpdateProbabilityNote();
        }

        private void ComboBox_Range_Height_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            // ComboBox_Range_Height 选中项索引改变。
            //

            Range.Height = Convert.ToInt32(ComboBox_Range_Height.Text);

            //

            UpdateProbabilityNote();
        }

        // 概率。

        private const Int32 ProbabilityMouseWheelStep = 5; // 概率衰减的鼠标滚轮调节步长。

        private bool ProbabilityIsAdjusting = false; // 是否正在调整概率衰减。

        private Bitmap ProbabilityTrbBmp; // 概率衰减调节器位图。

        private Size ProbabilityTrbSliderSize => new Size(2, Panel_ProbabilityAdjustment.Height); // 概率衰减调节器滑块大小。

        private void UpdateProbabilityTrbBmp()
        {
            //
            // 更新概率衰减调节器位图。
            //

            if (ProbabilityTrbBmp != null)
            {
                ProbabilityTrbBmp.Dispose();
            }

            ProbabilityTrbBmp = new Bitmap(Math.Max(1, Panel_ProbabilityAdjustment.Width), Math.Max(1, Panel_ProbabilityAdjustment.Height));

            using (Graphics ProbabilityTrbBmpGrap = Graphics.FromImage(ProbabilityTrbBmp))
            {
                ProbabilityTrbBmpGrap.Clear(Panel_ProbabilityAdjustment.BackColor);

                //

                Color Color_Slider, Color_ScrollBar_Current, Color_ScrollBar_Unavailable;

                if (Com.Geometry.CursorIsInControl(Panel_ProbabilityAdjustment) || ProbabilityIsAdjusting)
                {
                    Color_Slider = Com.ColorManipulation.ShiftLightnessByHSL(Me.RecommendColors.Border_INC, 0.3).ToColor();
                    Color_ScrollBar_Current = Com.ColorManipulation.ShiftLightnessByHSL(Me.RecommendColors.Border_INC, 0.3).ToColor();
                    Color_ScrollBar_Unavailable = Com.ColorManipulation.ShiftLightnessByHSL(Me.RecommendColors.Border_DEC, 0.3).ToColor();
                }
                else
                {
                    Color_Slider = Me.RecommendColors.Border_INC.ToColor();
                    Color_ScrollBar_Current = Me.RecommendColors.Border_INC.ToColor();
                    Color_ScrollBar_Unavailable = Me.RecommendColors.Border_DEC.ToColor();
                }

                Rectangle Rect_Slider = new Rectangle(new Point((Panel_ProbabilityAdjustment.Width - ProbabilityTrbSliderSize.Width) * (Probability - Probability_MIN) / (Probability_MAX - Probability_MIN), 0), ProbabilityTrbSliderSize);
                Rectangle Rect_ScrollBar_Current = new Rectangle(new Point(0, 0), new Size(Rect_Slider.X, Panel_ProbabilityAdjustment.Height));
                Rectangle Rect_ScrollBar_Unavailable = new Rectangle(new Point(Rect_Slider.Right, 0), new Size(Panel_ProbabilityAdjustment.Width - Rect_Slider.Right, Panel_ProbabilityAdjustment.Height));

                Rect_Slider.Width = Math.Max(1, Rect_Slider.Width);
                Rect_ScrollBar_Current.Width = Math.Max(1, Rect_ScrollBar_Current.Width);
                Rect_ScrollBar_Unavailable.Width = Math.Max(1, Rect_ScrollBar_Unavailable.Width);

                GraphicsPath Path_ScrollBar_Unavailable = new GraphicsPath();
                Path_ScrollBar_Unavailable.AddRectangle(Rect_ScrollBar_Unavailable);
                PathGradientBrush PGB_ScrollBar_Unavailable = new PathGradientBrush(Path_ScrollBar_Unavailable)
                {
                    CenterColor = Color_ScrollBar_Unavailable,
                    SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(Color_ScrollBar_Unavailable, 0.3) },
                    FocusScales = new PointF(1F, 0F)
                };
                ProbabilityTrbBmpGrap.FillPath(PGB_ScrollBar_Unavailable, Path_ScrollBar_Unavailable);
                Path_ScrollBar_Unavailable.Dispose();
                PGB_ScrollBar_Unavailable.Dispose();

                GraphicsPath Path_ScrollBar_Current = new GraphicsPath();
                Path_ScrollBar_Current.AddRectangle(Rect_ScrollBar_Current);
                PathGradientBrush PGB_ScrollBar_Current = new PathGradientBrush(Path_ScrollBar_Current)
                {
                    CenterColor = Color_ScrollBar_Current,
                    SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(Color_ScrollBar_Current, 0.3) },
                    FocusScales = new PointF(1F, 0F)
                };
                ProbabilityTrbBmpGrap.FillPath(PGB_ScrollBar_Current, Path_ScrollBar_Current);
                Path_ScrollBar_Current.Dispose();
                PGB_ScrollBar_Current.Dispose();

                GraphicsPath Path_Slider = new GraphicsPath();
                Path_Slider.AddRectangle(Rect_Slider);
                PathGradientBrush PGB_Slider = new PathGradientBrush(Path_Slider)
                {
                    CenterColor = Color_Slider,
                    SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(Color_Slider, 0.3) },
                    FocusScales = new PointF(1F, 0F)
                };
                ProbabilityTrbBmpGrap.FillPath(PGB_Slider, Path_Slider);
                Path_Slider.Dispose();
                PGB_Slider.Dispose();
            }

            //

            Label_Probability_Value.Text = Probability + "%";

            //

            UpdateProbabilityNote();
        }

        private void RepaintProbabilityTrbBmp()
        {
            //
            // 更新并重绘概率衰减调节器位图。
            //

            UpdateProbabilityTrbBmp();

            if (ProbabilityTrbBmp != null)
            {
                Panel_ProbabilityAdjustment.CreateGraphics().DrawImage(ProbabilityTrbBmp, new Point(0, 0));
            }
        }

        private void ProbabilityAdjustment()
        {
            //
            // 调整概率衰减。
            //

            Int32 CurPosXOfCtrl = Math.Max(-ProbabilityTrbSliderSize.Width, Math.Min(Com.Geometry.GetCursorPositionOfControl(Panel_ProbabilityAdjustment).X, Panel_ProbabilityAdjustment.Width + ProbabilityTrbSliderSize.Width));

            double DivisionWidth = (double)(Panel_ProbabilityAdjustment.Width - ProbabilityTrbSliderSize.Width) / (Probability_MAX - Probability_MIN);

            Probability = (Int32)Math.Max(Probability_MIN, Math.Min(Probability_MIN + (CurPosXOfCtrl - (ProbabilityTrbSliderSize.Width - DivisionWidth) / 2) / DivisionWidth, Probability_MAX));

            RepaintProbabilityTrbBmp();
        }

        private void Panel_ProbabilityAdjustment_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_ProbabilityAdjustment 绘图。
            //

            UpdateProbabilityTrbBmp();

            if (ProbabilityTrbBmp != null)
            {
                e.Graphics.DrawImage(ProbabilityTrbBmp, new Point(0, 0));
            }
        }

        private void Panel_ProbabilityAdjustment_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 Panel_ProbabilityAdjustment。
            //

            RepaintProbabilityTrbBmp();
        }

        private void Panel_ProbabilityAdjustment_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Panel_ProbabilityAdjustment。
            //

            RepaintProbabilityTrbBmp();
        }

        private void Panel_ProbabilityAdjustment_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Panel_ProbabilityAdjustment。
            //

            if (e.Button == MouseButtons.Left)
            {
                ProbabilityIsAdjusting = true;

                ProbabilityAdjustment();
            }
        }

        private void Panel_ProbabilityAdjustment_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // 鼠标释放 Panel_ProbabilityAdjustment。
            //

            ProbabilityIsAdjusting = false;
        }

        private void Panel_ProbabilityAdjustment_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Panel_ProbabilityAdjustment。
            //

            if (ProbabilityIsAdjusting)
            {
                ProbabilityAdjustment();
            }
        }

        private void Panel_ProbabilityAdjustment_MouseWheel(object sender, MouseEventArgs e)
        {
            //
            // 鼠标滚轮在 Panel_ProbabilityAdjustment 滚动。
            //

            if (e.Delta > 0)
            {
                if (Probability % ProbabilityMouseWheelStep == 0)
                {
                    Probability = Math.Min(Probability_MAX, Probability + ProbabilityMouseWheelStep);
                }
                else
                {
                    Probability = Math.Min(Probability_MAX, Probability - Probability % ProbabilityMouseWheelStep + ProbabilityMouseWheelStep);
                }
            }
            else if (e.Delta < 0)
            {
                if (Probability % ProbabilityMouseWheelStep == 0)
                {
                    Probability = Math.Max(Probability_MIN, Probability - ProbabilityMouseWheelStep);
                }
                else
                {
                    Probability = Math.Max(Probability_MIN, Probability - Probability % ProbabilityMouseWheelStep);
                }
            }

            RepaintProbabilityTrbBmp();
        }

        private void UpdateProbabilityNote()
        {
            //
            // 更新概率提示信息。
            //

            List<double> PL = ElementValueProbabilityList;

            string[] PS_A = new string[6];
            string PS = string.Empty;

            if (PL.Count - 1 <= 6)
            {
                for (int i = 1; i <= PL.Count - 1; i++)
                {
                    PS_A[i - 1] = Math.Pow(2, i) + ": " + (PL[i] * 100).ToString("N3") + "%";
                }

                switch (PL.Count - 1)
                {
                    case 2: PS = PS_A[0] + ",\n" + PS_A[1]; break;
                    case 3: PS = PS_A[0] + ",    " + PS_A[1] + ",\n" + PS_A[2]; break;
                    case 4: PS = PS_A[0] + ",    " + PS_A[1] + ",\n" + PS_A[2] + ",    " + PS_A[3]; break;
                    case 5: PS = PS_A[0] + ",    " + PS_A[1] + ",    " + PS_A[2] + ",\n" + PS_A[3] + ",    " + PS_A[4]; break;
                    case 6: PS = PS_A[0] + ",    " + PS_A[1] + ",    " + PS_A[2] + ",\n" + PS_A[3] + ",    " + PS_A[4] + ",    " + PS_A[5]; break;
                }
            }
            else
            {
                for (int i = 1; i <= 4; i++)
                {
                    PS_A[i - 1] = Math.Pow(2, i) + ": " + (PL[i] * 100).ToString("N3") + "%";
                }

                PS_A[4] = "…… ";
                PS_A[5] = Math.Pow(2, PL.Count - 1) + ": " + (PL[PL.Count - 1] * 100).ToString("N3") + "%";

                PS = PS_A[0] + ",    " + PS_A[1] + ",    " + PS_A[2] + ",\n" + PS_A[3] + ",    " + PS_A[4] + ",    " + PS_A[5];
            }

            Label_Probability_Note_Part2.Text = PS;
        }

        // 操作方式。

        private void RadioButton_Keyboard_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_Keyboard 选中状态改变。
            //

            if (RadioButton_Keyboard.Checked)
            {
                OperationMode = OperationModes.Keyboard;
            }
        }

        private void CheckBox_AlwaysEnableKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            //
            // CheckBox_AlwaysEnableKeyboard 选中状态改变。
            //

            AlwaysEnableKeyboard = CheckBox_AlwaysEnableKeyboard.Checked;
        }

        private void RadioButton_MouseClick_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_MouseClick 选中状态改变。
            //

            if (RadioButton_MouseClick.Checked)
            {
                OperationMode = OperationModes.MouseClick;
            }
        }

        private void RadioButton_TouchSlide_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_TouchSlide 选中状态改变。
            //

            if (RadioButton_TouchSlide.Checked)
            {
                OperationMode = OperationModes.TouchSlide;
            }
        }

        // 自动保存。

        private void ResetSaveOptionsControl()
        {
            //
            // 重置存档选项控件。
            //

            Label_EnableUndo_Info.Enabled = EnableUndo;

            if (EnableUndo)
            {
                RadioButton_SaveEveryStep.Enabled = RadioButton_SaveLastStep.Enabled = true;

                if (SaveEveryStep && ElementIndexList_Last.Count > 0 && StepListString.Length > 0)
                {
                    Label_TooSlow.Enabled = Label_CleanGameStep.Enabled = true;
                }
                else
                {
                    Label_TooSlow.Enabled = Label_CleanGameStep.Enabled = false;
                }
            }
            else
            {
                RadioButton_SaveEveryStep.Enabled = RadioButton_SaveLastStep.Enabled = false;

                Label_TooSlow.Enabled = Label_CleanGameStep.Enabled = false;
            }

            Label_CleanGameStepDone.Visible = false;
        }

        private void ResetSaveStepRadioButtons()
        {
            //
            // 重置保存步骤单选按钮。
            //

            if (EnableUndo)
            {
                RadioButton_SaveEveryStep.CheckedChanged -= RadioButton_SaveEveryStep_CheckedChanged;
                RadioButton_SaveLastStep.CheckedChanged -= RadioButton_SaveLastStep_CheckedChanged;

                if (SaveEveryStep)
                {
                    RadioButton_SaveEveryStep.Checked = true;
                }
                else
                {
                    RadioButton_SaveLastStep.Checked = true;
                }

                RadioButton_SaveEveryStep.CheckedChanged += RadioButton_SaveEveryStep_CheckedChanged;
                RadioButton_SaveLastStep.CheckedChanged += RadioButton_SaveLastStep_CheckedChanged;
            }
            else
            {
                RadioButton_SaveEveryStep.CheckedChanged -= RadioButton_SaveEveryStep_CheckedChanged;
                RadioButton_SaveLastStep.CheckedChanged -= RadioButton_SaveLastStep_CheckedChanged;

                RadioButton_SaveEveryStep.Checked = RadioButton_SaveLastStep.Checked = false;

                RadioButton_SaveEveryStep.CheckedChanged += RadioButton_SaveEveryStep_CheckedChanged;
                RadioButton_SaveLastStep.CheckedChanged += RadioButton_SaveLastStep_CheckedChanged;
            }
        }

        private void CheckBox_EnableUndo_CheckedChanged(object sender, EventArgs e)
        {
            //
            // CheckBox_EnableUndo 选中状态改变。
            //

            EnableUndo = CheckBox_EnableUndo.Checked;

            ResetSaveOptionsControl();

            ResetSaveStepRadioButtons();
        }

        private void RadioButton_SaveEveryStep_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_SaveEveryStep 选中状态改变。
            //

            if (RadioButton_SaveEveryStep.Checked)
            {
                SaveEveryStep = true;
            }

            ResetSaveOptionsControl();
        }

        private void Label_CleanGameStep_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_CleanGameStep。
            //

            CleanGameStep();

            //

            Label_CleanGameStepDone.Visible = true;
        }

        private void RadioButton_SaveLastStep_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_SaveLastStep 选中状态改变。
            //

            if (RadioButton_SaveLastStep.Checked)
            {
                SaveEveryStep = false;
            }

            ResetSaveOptionsControl();
        }

        // 主题颜色。

        private void RadioButton_UseRandomThemeColor_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_UseRandomThemeColor 选中状态改变。
            //

            if (RadioButton_UseRandomThemeColor.Checked)
            {
                UseRandomThemeColor = true;
            }

            Label_ThemeColorName.Enabled = !UseRandomThemeColor;
        }

        private void RadioButton_UseCustomColor_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_UseCustomColor 选中状态改变。
            //

            if (RadioButton_UseCustomColor.Checked)
            {
                UseRandomThemeColor = false;
            }

            Label_ThemeColorName.Enabled = !UseRandomThemeColor;
        }

        private void Label_ThemeColorName_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_ThemeColorName。
            //

            ColorDialog_ThemeColor.Color = Me.ThemeColor.ToColor();

            Me.Enabled = false;

            DialogResult DR = ColorDialog_ThemeColor.ShowDialog();

            if (DR == DialogResult.OK)
            {
                Me.ThemeColor = new Com.ColorX(ColorDialog_ThemeColor.Color);
            }

            Me.Enabled = true;
        }

        // 抗锯齿。

        private void CheckBox_AntiAlias_CheckedChanged(object sender, EventArgs e)
        {
            //
            // CheckBox_AntiAlias 选中状态改变。
            //

            AntiAlias = CheckBox_AntiAlias.Checked;
        }

        #endregion

        #region "关于"区域

        private void Label_GitHub_Base_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_GitHub_Base。
            //

            Process.Start(URL_GitHub_Base);
        }

        private void Label_GitHub_Release_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_GitHub_Release。
            //

            Process.Start(URL_GitHub_Release);
        }

        #endregion

    }
}