namespace WinFormApp
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.Panel_Client = new System.Windows.Forms.Panel();
            this.Panel_FunctionArea = new System.Windows.Forms.Panel();
            this.Panel_FunctionAreaOptionsBar = new System.Windows.Forms.Panel();
            this.Label_Tab_Start = new System.Windows.Forms.Label();
            this.Label_Tab_Record = new System.Windows.Forms.Label();
            this.Label_Tab_Options = new System.Windows.Forms.Label();
            this.Label_Tab_About = new System.Windows.Forms.Label();
            this.Panel_FunctionAreaTab = new System.Windows.Forms.Panel();
            this.Panel_Tab_Start = new System.Windows.Forms.Panel();
            this.Panel_EnterGameSelection = new System.Windows.Forms.Panel();
            this.Label_StartNewGame = new System.Windows.Forms.Label();
            this.Label_ContinueLastGame = new System.Windows.Forms.Label();
            this.Panel_Tab_Record = new System.Windows.Forms.Panel();
            this.Panel_Score = new System.Windows.Forms.Panel();
            this.PictureBox_Score = new System.Windows.Forms.PictureBox();
            this.Label_ThisRecord = new System.Windows.Forms.Label();
            this.Label_ThisRecordVal_Score = new System.Windows.Forms.Label();
            this.Label_ThisRecordVal_MaxAndSum = new System.Windows.Forms.Label();
            this.Label_BestRecord = new System.Windows.Forms.Label();
            this.Label_BestRecordVal_Score = new System.Windows.Forms.Label();
            this.Label_BestRecordVal_MaxAndSum = new System.Windows.Forms.Label();
            this.Panel_GameTime = new System.Windows.Forms.Panel();
            this.PictureBox_GameTime = new System.Windows.Forms.PictureBox();
            this.Label_ThisTime = new System.Windows.Forms.Label();
            this.Label_ThisTimeVal = new System.Windows.Forms.Label();
            this.Label_TotalTime = new System.Windows.Forms.Label();
            this.Label_TotalTimeVal = new System.Windows.Forms.Label();
            this.Panel_Tab_Options = new System.Windows.Forms.Panel();
            this.Panel_Range = new System.Windows.Forms.Panel();
            this.Label_Range = new System.Windows.Forms.Label();
            this.Label_Range_Width = new System.Windows.Forms.Label();
            this.ComboBox_Range_Width = new System.Windows.Forms.ComboBox();
            this.Label_Range_Height = new System.Windows.Forms.Label();
            this.ComboBox_Range_Height = new System.Windows.Forms.ComboBox();
            this.Panel_Probability = new System.Windows.Forms.Panel();
            this.Label_Probability = new System.Windows.Forms.Label();
            this.Label_Probability_Note_Part1 = new System.Windows.Forms.Label();
            this.Label_Probability_Note_Part2 = new System.Windows.Forms.Label();
            this.Label_Probability_Note_Part3 = new System.Windows.Forms.Label();
            this.Panel_ProbabilityAdjustment = new System.Windows.Forms.Panel();
            this.Label_Probability_Value = new System.Windows.Forms.Label();
            this.Panel_OperationMode = new System.Windows.Forms.Panel();
            this.Label_OperationMode = new System.Windows.Forms.Label();
            this.RadioButton_Keyboard = new System.Windows.Forms.RadioButton();
            this.CheckBox_AlwaysEnableKeyboard = new System.Windows.Forms.CheckBox();
            this.RadioButton_MouseClick = new System.Windows.Forms.RadioButton();
            this.RadioButton_TouchSlide = new System.Windows.Forms.RadioButton();
            this.Panel_Save = new System.Windows.Forms.Panel();
            this.Label_Save = new System.Windows.Forms.Label();
            this.CheckBox_EnableUndo = new System.Windows.Forms.CheckBox();
            this.Label_EnableUndo_Info = new System.Windows.Forms.Label();
            this.RadioButton_SaveEveryStep = new System.Windows.Forms.RadioButton();
            this.Label_TooSlow = new System.Windows.Forms.Label();
            this.Label_CleanGameStep = new System.Windows.Forms.Label();
            this.Label_CleanGameStepDone = new System.Windows.Forms.Label();
            this.RadioButton_SaveLastStep = new System.Windows.Forms.RadioButton();
            this.Panel_ThemeColor = new System.Windows.Forms.Panel();
            this.Label_ThemeColor = new System.Windows.Forms.Label();
            this.RadioButton_UseRandomThemeColor = new System.Windows.Forms.RadioButton();
            this.RadioButton_UseCustomColor = new System.Windows.Forms.RadioButton();
            this.Label_ThemeColorName = new System.Windows.Forms.Label();
            this.Panel_AntiAlias = new System.Windows.Forms.Panel();
            this.CheckBox_AntiAlias = new System.Windows.Forms.CheckBox();
            this.Label_AntiAlias = new System.Windows.Forms.Label();
            this.Panel_Tab_About = new System.Windows.Forms.Panel();
            this.PictureBox_ApplicationLogo = new System.Windows.Forms.PictureBox();
            this.Label_ApplicationName = new System.Windows.Forms.Label();
            this.Label_ApplicationEdition = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.Label_Copyright = new System.Windows.Forms.Label();
            this.Panel_GitHub = new System.Windows.Forms.Panel();
            this.Label_GitHub_Part1 = new System.Windows.Forms.Label();
            this.Label_GitHub_Base = new System.Windows.Forms.Label();
            this.Label_GitHub_Part2 = new System.Windows.Forms.Label();
            this.Label_GitHub_Release = new System.Windows.Forms.Label();
            this.Panel_GameUI = new System.Windows.Forms.Panel();
            this.Panel_Current = new System.Windows.Forms.Panel();
            this.Panel_Interrupt = new System.Windows.Forms.Panel();
            this.PictureBox_Undo = new System.Windows.Forms.PictureBox();
            this.PictureBox_Redo = new System.Windows.Forms.PictureBox();
            this.PictureBox_Restart = new System.Windows.Forms.PictureBox();
            this.PictureBox_ExitGame = new System.Windows.Forms.PictureBox();
            this.Panel_Environment = new System.Windows.Forms.Panel();
            this.Timer_EnterPrompt = new System.Windows.Forms.Timer(this.components);
            this.BackgroundWorker_LoadGameStep = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_SaveGameStep = new System.ComponentModel.BackgroundWorker();
            this.ToolTip_InterruptPrompt = new System.Windows.Forms.ToolTip(this.components);
            this.ColorDialog_ThemeColor = new System.Windows.Forms.ColorDialog();
            this.Panel_Main.SuspendLayout();
            this.Panel_Client.SuspendLayout();
            this.Panel_FunctionArea.SuspendLayout();
            this.Panel_FunctionAreaOptionsBar.SuspendLayout();
            this.Panel_FunctionAreaTab.SuspendLayout();
            this.Panel_Tab_Start.SuspendLayout();
            this.Panel_EnterGameSelection.SuspendLayout();
            this.Panel_Tab_Record.SuspendLayout();
            this.Panel_Score.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Score)).BeginInit();
            this.Panel_GameTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameTime)).BeginInit();
            this.Panel_Tab_Options.SuspendLayout();
            this.Panel_Range.SuspendLayout();
            this.Panel_Probability.SuspendLayout();
            this.Panel_OperationMode.SuspendLayout();
            this.Panel_Save.SuspendLayout();
            this.Panel_ThemeColor.SuspendLayout();
            this.Panel_AntiAlias.SuspendLayout();
            this.Panel_Tab_About.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ApplicationLogo)).BeginInit();
            this.Panel_GitHub.SuspendLayout();
            this.Panel_GameUI.SuspendLayout();
            this.Panel_Current.SuspendLayout();
            this.Panel_Interrupt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Undo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Redo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Restart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ExitGame)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Main
            // 
            this.Panel_Main.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Main.Controls.Add(this.Panel_Client);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Main.Location = new System.Drawing.Point(0, 0);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Size = new System.Drawing.Size(585, 420);
            this.Panel_Main.TabIndex = 0;
            // 
            // Panel_Client
            // 
            this.Panel_Client.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Client.Controls.Add(this.Panel_FunctionArea);
            this.Panel_Client.Controls.Add(this.Panel_GameUI);
            this.Panel_Client.Location = new System.Drawing.Point(0, 0);
            this.Panel_Client.Name = "Panel_Client";
            this.Panel_Client.Size = new System.Drawing.Size(585, 420);
            this.Panel_Client.TabIndex = 0;
            // 
            // Panel_FunctionArea
            // 
            this.Panel_FunctionArea.BackColor = System.Drawing.Color.Transparent;
            this.Panel_FunctionArea.Controls.Add(this.Panel_FunctionAreaOptionsBar);
            this.Panel_FunctionArea.Controls.Add(this.Panel_FunctionAreaTab);
            this.Panel_FunctionArea.Location = new System.Drawing.Point(0, 0);
            this.Panel_FunctionArea.Name = "Panel_FunctionArea";
            this.Panel_FunctionArea.Size = new System.Drawing.Size(585, 420);
            this.Panel_FunctionArea.TabIndex = 0;
            // 
            // Panel_FunctionAreaOptionsBar
            // 
            this.Panel_FunctionAreaOptionsBar.BackColor = System.Drawing.Color.Transparent;
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_Start);
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_Record);
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_Options);
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_About);
            this.Panel_FunctionAreaOptionsBar.Location = new System.Drawing.Point(0, 0);
            this.Panel_FunctionAreaOptionsBar.MaximumSize = new System.Drawing.Size(150, 65535);
            this.Panel_FunctionAreaOptionsBar.MinimumSize = new System.Drawing.Size(40, 40);
            this.Panel_FunctionAreaOptionsBar.Name = "Panel_FunctionAreaOptionsBar";
            this.Panel_FunctionAreaOptionsBar.Size = new System.Drawing.Size(150, 420);
            this.Panel_FunctionAreaOptionsBar.TabIndex = 0;
            this.Panel_FunctionAreaOptionsBar.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_FunctionAreaOptionsBar_Paint);
            this.Panel_FunctionAreaOptionsBar.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel_FunctionAreaOptionsBar_MouseWheel);
            // 
            // Label_Tab_Start
            // 
            this.Label_Tab_Start.AutoEllipsis = true;
            this.Label_Tab_Start.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_Start.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_Start.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_Start.Location = new System.Drawing.Point(0, 0);
            this.Label_Tab_Start.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_Start.MinimumSize = new System.Drawing.Size(40, 10);
            this.Label_Tab_Start.Name = "Label_Tab_Start";
            this.Label_Tab_Start.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_Start.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_Start.TabIndex = 0;
            this.Label_Tab_Start.Text = "开始";
            this.Label_Tab_Start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_Start.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_Start_MouseDown);
            this.Label_Tab_Start.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_Start.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Label_Tab_Record
            // 
            this.Label_Tab_Record.AutoEllipsis = true;
            this.Label_Tab_Record.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_Record.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_Record.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_Record.Location = new System.Drawing.Point(0, 50);
            this.Label_Tab_Record.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_Record.MinimumSize = new System.Drawing.Size(40, 10);
            this.Label_Tab_Record.Name = "Label_Tab_Record";
            this.Label_Tab_Record.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_Record.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_Record.TabIndex = 0;
            this.Label_Tab_Record.Text = "记录";
            this.Label_Tab_Record.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_Record.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_Record_MouseDown);
            this.Label_Tab_Record.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_Record.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Label_Tab_Options
            // 
            this.Label_Tab_Options.AutoEllipsis = true;
            this.Label_Tab_Options.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_Options.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_Options.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_Options.Location = new System.Drawing.Point(0, 100);
            this.Label_Tab_Options.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_Options.MinimumSize = new System.Drawing.Size(40, 10);
            this.Label_Tab_Options.Name = "Label_Tab_Options";
            this.Label_Tab_Options.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_Options.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_Options.TabIndex = 0;
            this.Label_Tab_Options.Text = "选项";
            this.Label_Tab_Options.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_Options.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_Options_MouseDown);
            this.Label_Tab_Options.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_Options.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Label_Tab_About
            // 
            this.Label_Tab_About.AutoEllipsis = true;
            this.Label_Tab_About.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_About.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_About.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_About.Location = new System.Drawing.Point(0, 150);
            this.Label_Tab_About.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_About.MinimumSize = new System.Drawing.Size(40, 10);
            this.Label_Tab_About.Name = "Label_Tab_About";
            this.Label_Tab_About.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_About.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_About.TabIndex = 0;
            this.Label_Tab_About.Text = "关于";
            this.Label_Tab_About.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_About.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_About_MouseDown);
            this.Label_Tab_About.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_About.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Panel_FunctionAreaTab
            // 
            this.Panel_FunctionAreaTab.AutoScroll = true;
            this.Panel_FunctionAreaTab.BackColor = System.Drawing.Color.Transparent;
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_Start);
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_Record);
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_Options);
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_About);
            this.Panel_FunctionAreaTab.Location = new System.Drawing.Point(150, 0);
            this.Panel_FunctionAreaTab.Name = "Panel_FunctionAreaTab";
            this.Panel_FunctionAreaTab.Size = new System.Drawing.Size(435, 420);
            this.Panel_FunctionAreaTab.TabIndex = 0;
            // 
            // Panel_Tab_Start
            // 
            this.Panel_Tab_Start.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_Start.Controls.Add(this.Panel_EnterGameSelection);
            this.Panel_Tab_Start.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_Start.MinimumSize = new System.Drawing.Size(260, 80);
            this.Panel_Tab_Start.Name = "Panel_Tab_Start";
            this.Panel_Tab_Start.Size = new System.Drawing.Size(435, 420);
            this.Panel_Tab_Start.TabIndex = 0;
            // 
            // Panel_EnterGameSelection
            // 
            this.Panel_EnterGameSelection.BackColor = System.Drawing.Color.Transparent;
            this.Panel_EnterGameSelection.Controls.Add(this.Label_StartNewGame);
            this.Panel_EnterGameSelection.Controls.Add(this.Label_ContinueLastGame);
            this.Panel_EnterGameSelection.Location = new System.Drawing.Point(75, 170);
            this.Panel_EnterGameSelection.Name = "Panel_EnterGameSelection";
            this.Panel_EnterGameSelection.Size = new System.Drawing.Size(260, 80);
            this.Panel_EnterGameSelection.TabIndex = 0;
            this.Panel_EnterGameSelection.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_EnterGameSelection_Paint);
            // 
            // Label_StartNewGame
            // 
            this.Label_StartNewGame.AutoEllipsis = true;
            this.Label_StartNewGame.BackColor = System.Drawing.Color.Transparent;
            this.Label_StartNewGame.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Label_StartNewGame.ForeColor = System.Drawing.Color.White;
            this.Label_StartNewGame.Location = new System.Drawing.Point(0, 0);
            this.Label_StartNewGame.Name = "Label_StartNewGame";
            this.Label_StartNewGame.Size = new System.Drawing.Size(260, 40);
            this.Label_StartNewGame.TabIndex = 0;
            this.Label_StartNewGame.Text = "开始新游戏";
            this.Label_StartNewGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_ContinueLastGame
            // 
            this.Label_ContinueLastGame.AutoEllipsis = true;
            this.Label_ContinueLastGame.BackColor = System.Drawing.Color.Transparent;
            this.Label_ContinueLastGame.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Label_ContinueLastGame.ForeColor = System.Drawing.Color.White;
            this.Label_ContinueLastGame.Location = new System.Drawing.Point(0, 40);
            this.Label_ContinueLastGame.Name = "Label_ContinueLastGame";
            this.Label_ContinueLastGame.Size = new System.Drawing.Size(260, 40);
            this.Label_ContinueLastGame.TabIndex = 0;
            this.Label_ContinueLastGame.Text = "继续上次的游戏";
            this.Label_ContinueLastGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_Tab_Record
            // 
            this.Panel_Tab_Record.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_Record.Controls.Add(this.Panel_Score);
            this.Panel_Tab_Record.Controls.Add(this.Panel_GameTime);
            this.Panel_Tab_Record.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_Record.MinimumSize = new System.Drawing.Size(350, 300);
            this.Panel_Tab_Record.Name = "Panel_Tab_Record";
            this.Panel_Tab_Record.Size = new System.Drawing.Size(435, 420);
            this.Panel_Tab_Record.TabIndex = 0;
            this.Panel_Tab_Record.Visible = false;
            // 
            // Panel_Score
            // 
            this.Panel_Score.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Score.Controls.Add(this.PictureBox_Score);
            this.Panel_Score.Controls.Add(this.Label_ThisRecord);
            this.Panel_Score.Controls.Add(this.Label_ThisRecordVal_Score);
            this.Panel_Score.Controls.Add(this.Label_ThisRecordVal_MaxAndSum);
            this.Panel_Score.Controls.Add(this.Label_BestRecord);
            this.Panel_Score.Controls.Add(this.Label_BestRecordVal_Score);
            this.Panel_Score.Controls.Add(this.Label_BestRecordVal_MaxAndSum);
            this.Panel_Score.Location = new System.Drawing.Point(30, 30);
            this.Panel_Score.Name = "Panel_Score";
            this.Panel_Score.Size = new System.Drawing.Size(360, 120);
            this.Panel_Score.TabIndex = 0;
            this.Panel_Score.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Score_Paint);
            // 
            // PictureBox_Score
            // 
            this.PictureBox_Score.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Score.ErrorImage = null;
            this.PictureBox_Score.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_Score.Image")));
            this.PictureBox_Score.InitialImage = null;
            this.PictureBox_Score.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Score.Name = "PictureBox_Score";
            this.PictureBox_Score.Size = new System.Drawing.Size(20, 20);
            this.PictureBox_Score.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Score.TabIndex = 0;
            this.PictureBox_Score.TabStop = false;
            // 
            // Label_ThisRecord
            // 
            this.Label_ThisRecord.AutoSize = true;
            this.Label_ThisRecord.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisRecord.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThisRecord.ForeColor = System.Drawing.Color.White;
            this.Label_ThisRecord.Location = new System.Drawing.Point(55, 70);
            this.Label_ThisRecord.Name = "Label_ThisRecord";
            this.Label_ThisRecord.Size = new System.Drawing.Size(69, 20);
            this.Label_ThisRecord.TabIndex = 0;
            this.Label_ThisRecord.Text = "本次得分";
            // 
            // Label_ThisRecordVal_Score
            // 
            this.Label_ThisRecordVal_Score.AutoSize = true;
            this.Label_ThisRecordVal_Score.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisRecordVal_Score.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label_ThisRecordVal_Score.ForeColor = System.Drawing.Color.White;
            this.Label_ThisRecordVal_Score.Location = new System.Drawing.Point(20, 30);
            this.Label_ThisRecordVal_Score.Name = "Label_ThisRecordVal_Score";
            this.Label_ThisRecordVal_Score.Size = new System.Drawing.Size(141, 19);
            this.Label_ThisRecordVal_Score.TabIndex = 0;
            this.Label_ThisRecordVal_Score.Text = "ThisRecord_Score";
            // 
            // Label_ThisRecordVal_MaxAndSum
            // 
            this.Label_ThisRecordVal_MaxAndSum.AutoSize = true;
            this.Label_ThisRecordVal_MaxAndSum.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisRecordVal_MaxAndSum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThisRecordVal_MaxAndSum.ForeColor = System.Drawing.Color.White;
            this.Label_ThisRecordVal_MaxAndSum.Location = new System.Drawing.Point(15, 50);
            this.Label_ThisRecordVal_MaxAndSum.Name = "Label_ThisRecordVal_MaxAndSum";
            this.Label_ThisRecordVal_MaxAndSum.Size = new System.Drawing.Size(151, 17);
            this.Label_ThisRecordVal_MaxAndSum.TabIndex = 0;
            this.Label_ThisRecordVal_MaxAndSum.Text = "ThisRecord_MaxAndSum";
            // 
            // Label_BestRecord
            // 
            this.Label_BestRecord.AutoSize = true;
            this.Label_BestRecord.BackColor = System.Drawing.Color.Transparent;
            this.Label_BestRecord.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_BestRecord.ForeColor = System.Drawing.Color.White;
            this.Label_BestRecord.Location = new System.Drawing.Point(235, 70);
            this.Label_BestRecord.Name = "Label_BestRecord";
            this.Label_BestRecord.Size = new System.Drawing.Size(69, 20);
            this.Label_BestRecord.TabIndex = 0;
            this.Label_BestRecord.Text = "最高得分";
            // 
            // Label_BestRecordVal_Score
            // 
            this.Label_BestRecordVal_Score.AutoSize = true;
            this.Label_BestRecordVal_Score.BackColor = System.Drawing.Color.Transparent;
            this.Label_BestRecordVal_Score.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label_BestRecordVal_Score.ForeColor = System.Drawing.Color.White;
            this.Label_BestRecordVal_Score.Location = new System.Drawing.Point(200, 30);
            this.Label_BestRecordVal_Score.Name = "Label_BestRecordVal_Score";
            this.Label_BestRecordVal_Score.Size = new System.Drawing.Size(143, 19);
            this.Label_BestRecordVal_Score.TabIndex = 0;
            this.Label_BestRecordVal_Score.Text = "BestRecord_Score";
            // 
            // Label_BestRecordVal_MaxAndSum
            // 
            this.Label_BestRecordVal_MaxAndSum.AutoSize = true;
            this.Label_BestRecordVal_MaxAndSum.BackColor = System.Drawing.Color.Transparent;
            this.Label_BestRecordVal_MaxAndSum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_BestRecordVal_MaxAndSum.ForeColor = System.Drawing.Color.White;
            this.Label_BestRecordVal_MaxAndSum.Location = new System.Drawing.Point(195, 50);
            this.Label_BestRecordVal_MaxAndSum.Name = "Label_BestRecordVal_MaxAndSum";
            this.Label_BestRecordVal_MaxAndSum.Size = new System.Drawing.Size(153, 17);
            this.Label_BestRecordVal_MaxAndSum.TabIndex = 0;
            this.Label_BestRecordVal_MaxAndSum.Text = "BestRecord_MaxAndSum";
            // 
            // Panel_GameTime
            // 
            this.Panel_GameTime.BackColor = System.Drawing.Color.Transparent;
            this.Panel_GameTime.Controls.Add(this.PictureBox_GameTime);
            this.Panel_GameTime.Controls.Add(this.Label_ThisTime);
            this.Panel_GameTime.Controls.Add(this.Label_ThisTimeVal);
            this.Panel_GameTime.Controls.Add(this.Label_TotalTime);
            this.Panel_GameTime.Controls.Add(this.Label_TotalTimeVal);
            this.Panel_GameTime.Location = new System.Drawing.Point(30, 150);
            this.Panel_GameTime.Name = "Panel_GameTime";
            this.Panel_GameTime.Size = new System.Drawing.Size(360, 120);
            this.Panel_GameTime.TabIndex = 0;
            this.Panel_GameTime.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_GameTime_Paint);
            // 
            // PictureBox_GameTime
            // 
            this.PictureBox_GameTime.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_GameTime.ErrorImage = null;
            this.PictureBox_GameTime.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_GameTime.Image")));
            this.PictureBox_GameTime.InitialImage = null;
            this.PictureBox_GameTime.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_GameTime.Name = "PictureBox_GameTime";
            this.PictureBox_GameTime.Size = new System.Drawing.Size(20, 20);
            this.PictureBox_GameTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_GameTime.TabIndex = 0;
            this.PictureBox_GameTime.TabStop = false;
            // 
            // Label_ThisTime
            // 
            this.Label_ThisTime.AutoSize = true;
            this.Label_ThisTime.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisTime.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThisTime.ForeColor = System.Drawing.Color.White;
            this.Label_ThisTime.Location = new System.Drawing.Point(25, 30);
            this.Label_ThisTime.Name = "Label_ThisTime";
            this.Label_ThisTime.Size = new System.Drawing.Size(103, 20);
            this.Label_ThisTime.TabIndex = 0;
            this.Label_ThisTime.Text = "本次游戏时长:";
            // 
            // Label_ThisTimeVal
            // 
            this.Label_ThisTimeVal.AutoSize = true;
            this.Label_ThisTimeVal.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisTimeVal.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThisTimeVal.ForeColor = System.Drawing.Color.White;
            this.Label_ThisTimeVal.Location = new System.Drawing.Point(135, 30);
            this.Label_ThisTimeVal.Name = "Label_ThisTimeVal";
            this.Label_ThisTimeVal.Size = new System.Drawing.Size(120, 19);
            this.Label_ThisTimeVal.TabIndex = 0;
            this.Label_ThisTimeVal.Text = "ThisGameTime";
            // 
            // Label_TotalTime
            // 
            this.Label_TotalTime.AutoSize = true;
            this.Label_TotalTime.BackColor = System.Drawing.Color.Transparent;
            this.Label_TotalTime.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_TotalTime.ForeColor = System.Drawing.Color.White;
            this.Label_TotalTime.Location = new System.Drawing.Point(25, 70);
            this.Label_TotalTime.Name = "Label_TotalTime";
            this.Label_TotalTime.Size = new System.Drawing.Size(103, 20);
            this.Label_TotalTime.TabIndex = 0;
            this.Label_TotalTime.Text = "累计游戏时长:";
            // 
            // Label_TotalTimeVal
            // 
            this.Label_TotalTimeVal.AutoSize = true;
            this.Label_TotalTimeVal.BackColor = System.Drawing.Color.Transparent;
            this.Label_TotalTimeVal.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label_TotalTimeVal.ForeColor = System.Drawing.Color.White;
            this.Label_TotalTimeVal.Location = new System.Drawing.Point(135, 70);
            this.Label_TotalTimeVal.Name = "Label_TotalTimeVal";
            this.Label_TotalTimeVal.Size = new System.Drawing.Size(128, 19);
            this.Label_TotalTimeVal.TabIndex = 0;
            this.Label_TotalTimeVal.Text = "TotalGameTime";
            // 
            // Panel_Tab_Options
            // 
            this.Panel_Tab_Options.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_Options.Controls.Add(this.Panel_Range);
            this.Panel_Tab_Options.Controls.Add(this.Panel_Probability);
            this.Panel_Tab_Options.Controls.Add(this.Panel_OperationMode);
            this.Panel_Tab_Options.Controls.Add(this.Panel_Save);
            this.Panel_Tab_Options.Controls.Add(this.Panel_ThemeColor);
            this.Panel_Tab_Options.Controls.Add(this.Panel_AntiAlias);
            this.Panel_Tab_Options.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_Options.MinimumSize = new System.Drawing.Size(410, 800);
            this.Panel_Tab_Options.Name = "Panel_Tab_Options";
            this.Panel_Tab_Options.Size = new System.Drawing.Size(435, 800);
            this.Panel_Tab_Options.TabIndex = 0;
            this.Panel_Tab_Options.Visible = false;
            // 
            // Panel_Range
            // 
            this.Panel_Range.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Range.Controls.Add(this.Label_Range);
            this.Panel_Range.Controls.Add(this.Label_Range_Width);
            this.Panel_Range.Controls.Add(this.ComboBox_Range_Width);
            this.Panel_Range.Controls.Add(this.Label_Range_Height);
            this.Panel_Range.Controls.Add(this.ComboBox_Range_Height);
            this.Panel_Range.Location = new System.Drawing.Point(30, 30);
            this.Panel_Range.Name = "Panel_Range";
            this.Panel_Range.Size = new System.Drawing.Size(350, 70);
            this.Panel_Range.TabIndex = 0;
            this.Panel_Range.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Range_Paint);
            // 
            // Label_Range
            // 
            this.Label_Range.AutoSize = true;
            this.Label_Range.BackColor = System.Drawing.Color.Transparent;
            this.Label_Range.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_Range.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Range.ForeColor = System.Drawing.Color.White;
            this.Label_Range.Location = new System.Drawing.Point(0, 0);
            this.Label_Range.Name = "Label_Range";
            this.Label_Range.Size = new System.Drawing.Size(39, 20);
            this.Label_Range.TabIndex = 0;
            this.Label_Range.Text = "布局";
            // 
            // Label_Range_Width
            // 
            this.Label_Range_Width.AutoSize = true;
            this.Label_Range_Width.BackColor = System.Drawing.Color.Transparent;
            this.Label_Range_Width.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_Range_Width.ForeColor = System.Drawing.Color.White;
            this.Label_Range_Width.Location = new System.Drawing.Point(25, 34);
            this.Label_Range_Width.Name = "Label_Range_Width";
            this.Label_Range_Width.Size = new System.Drawing.Size(38, 19);
            this.Label_Range_Width.TabIndex = 0;
            this.Label_Range_Width.Text = "宽度:";
            // 
            // ComboBox_Range_Width
            // 
            this.ComboBox_Range_Width.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Range_Width.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.ComboBox_Range_Width.FormattingEnabled = true;
            this.ComboBox_Range_Width.Location = new System.Drawing.Point(70, 30);
            this.ComboBox_Range_Width.Name = "ComboBox_Range_Width";
            this.ComboBox_Range_Width.Size = new System.Drawing.Size(85, 27);
            this.ComboBox_Range_Width.TabIndex = 0;
            this.ComboBox_Range_Width.TabStop = false;
            this.ComboBox_Range_Width.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Range_Width_SelectedIndexChanged);
            // 
            // Label_Range_Height
            // 
            this.Label_Range_Height.AutoSize = true;
            this.Label_Range_Height.BackColor = System.Drawing.Color.Transparent;
            this.Label_Range_Height.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_Range_Height.ForeColor = System.Drawing.Color.White;
            this.Label_Range_Height.Location = new System.Drawing.Point(195, 34);
            this.Label_Range_Height.Name = "Label_Range_Height";
            this.Label_Range_Height.Size = new System.Drawing.Size(38, 19);
            this.Label_Range_Height.TabIndex = 0;
            this.Label_Range_Height.Text = "高度:";
            // 
            // ComboBox_Range_Height
            // 
            this.ComboBox_Range_Height.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Range_Height.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.ComboBox_Range_Height.FormattingEnabled = true;
            this.ComboBox_Range_Height.Location = new System.Drawing.Point(240, 30);
            this.ComboBox_Range_Height.Name = "ComboBox_Range_Height";
            this.ComboBox_Range_Height.Size = new System.Drawing.Size(85, 27);
            this.ComboBox_Range_Height.TabIndex = 0;
            this.ComboBox_Range_Height.TabStop = false;
            this.ComboBox_Range_Height.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Range_Height_SelectedIndexChanged);
            // 
            // Panel_Probability
            // 
            this.Panel_Probability.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Probability.Controls.Add(this.Label_Probability);
            this.Panel_Probability.Controls.Add(this.Label_Probability_Note_Part1);
            this.Panel_Probability.Controls.Add(this.Label_Probability_Note_Part2);
            this.Panel_Probability.Controls.Add(this.Label_Probability_Note_Part3);
            this.Panel_Probability.Controls.Add(this.Panel_ProbabilityAdjustment);
            this.Panel_Probability.Controls.Add(this.Label_Probability_Value);
            this.Panel_Probability.Location = new System.Drawing.Point(30, 100);
            this.Panel_Probability.Name = "Panel_Probability";
            this.Panel_Probability.Size = new System.Drawing.Size(350, 160);
            this.Panel_Probability.TabIndex = 0;
            this.Panel_Probability.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Probability_Paint);
            // 
            // Label_Probability
            // 
            this.Label_Probability.AutoSize = true;
            this.Label_Probability.BackColor = System.Drawing.Color.Transparent;
            this.Label_Probability.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_Probability.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Probability.ForeColor = System.Drawing.Color.White;
            this.Label_Probability.Location = new System.Drawing.Point(0, 0);
            this.Label_Probability.Name = "Label_Probability";
            this.Label_Probability.Size = new System.Drawing.Size(39, 20);
            this.Label_Probability.TabIndex = 0;
            this.Label_Probability.Text = "概率";
            // 
            // Label_Probability_Note_Part1
            // 
            this.Label_Probability_Note_Part1.AutoSize = true;
            this.Label_Probability_Note_Part1.BackColor = System.Drawing.Color.Transparent;
            this.Label_Probability_Note_Part1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Probability_Note_Part1.ForeColor = System.Drawing.Color.White;
            this.Label_Probability_Note_Part1.Location = new System.Drawing.Point(25, 30);
            this.Label_Probability_Note_Part1.Name = "Label_Probability_Note_Part1";
            this.Label_Probability_Note_Part1.Size = new System.Drawing.Size(272, 19);
            this.Label_Probability_Note_Part1.TabIndex = 0;
            this.Label_Probability_Note_Part1.Text = "当前布局下，所有可能出现的数值及其概率为:";
            // 
            // Label_Probability_Note_Part2
            // 
            this.Label_Probability_Note_Part2.AutoSize = true;
            this.Label_Probability_Note_Part2.BackColor = System.Drawing.Color.Transparent;
            this.Label_Probability_Note_Part2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Probability_Note_Part2.ForeColor = System.Drawing.Color.White;
            this.Label_Probability_Note_Part2.Location = new System.Drawing.Point(41, 55);
            this.Label_Probability_Note_Part2.Name = "Label_Probability_Note_Part2";
            this.Label_Probability_Note_Part2.Size = new System.Drawing.Size(152, 38);
            this.Label_Probability_Note_Part2.TabIndex = 0;
            this.Label_Probability_Note_Part2.Text = "2: x%,    4: x%,    8: x%,\r\n16: x%,    …… ,    n: x%";
            // 
            // Label_Probability_Note_Part3
            // 
            this.Label_Probability_Note_Part3.AutoSize = true;
            this.Label_Probability_Note_Part3.BackColor = System.Drawing.Color.Transparent;
            this.Label_Probability_Note_Part3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Probability_Note_Part3.ForeColor = System.Drawing.Color.White;
            this.Label_Probability_Note_Part3.Location = new System.Drawing.Point(25, 100);
            this.Label_Probability_Note_Part3.Name = "Label_Probability_Note_Part3";
            this.Label_Probability_Note_Part3.Size = new System.Drawing.Size(207, 19);
            this.Label_Probability_Note_Part3.TabIndex = 0;
            this.Label_Probability_Note_Part3.Text = "相邻数值的概率按如下百分比递减:";
            // 
            // Panel_ProbabilityAdjustment
            // 
            this.Panel_ProbabilityAdjustment.BackColor = System.Drawing.Color.Black;
            this.Panel_ProbabilityAdjustment.Location = new System.Drawing.Point(25, 125);
            this.Panel_ProbabilityAdjustment.Name = "Panel_ProbabilityAdjustment";
            this.Panel_ProbabilityAdjustment.Size = new System.Drawing.Size(250, 24);
            this.Panel_ProbabilityAdjustment.TabIndex = 0;
            this.Panel_ProbabilityAdjustment.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_ProbabilityAdjustment_Paint);
            this.Panel_ProbabilityAdjustment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_ProbabilityAdjustment_MouseDown);
            this.Panel_ProbabilityAdjustment.MouseEnter += new System.EventHandler(this.Panel_ProbabilityAdjustment_MouseEnter);
            this.Panel_ProbabilityAdjustment.MouseLeave += new System.EventHandler(this.Panel_ProbabilityAdjustment_MouseLeave);
            this.Panel_ProbabilityAdjustment.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_ProbabilityAdjustment_MouseMove);
            this.Panel_ProbabilityAdjustment.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_ProbabilityAdjustment_MouseUp);
            this.Panel_ProbabilityAdjustment.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel_ProbabilityAdjustment_MouseWheel);
            // 
            // Label_Probability_Value
            // 
            this.Label_Probability_Value.AutoSize = true;
            this.Label_Probability_Value.BackColor = System.Drawing.Color.Transparent;
            this.Label_Probability_Value.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Probability_Value.ForeColor = System.Drawing.Color.White;
            this.Label_Probability_Value.Location = new System.Drawing.Point(280, 127);
            this.Label_Probability_Value.Name = "Label_Probability_Value";
            this.Label_Probability_Value.Size = new System.Drawing.Size(45, 19);
            this.Label_Probability_Value.TabIndex = 0;
            this.Label_Probability_Value.Text = "100%";
            // 
            // Panel_OperationMode
            // 
            this.Panel_OperationMode.BackColor = System.Drawing.Color.Transparent;
            this.Panel_OperationMode.Controls.Add(this.Label_OperationMode);
            this.Panel_OperationMode.Controls.Add(this.RadioButton_Keyboard);
            this.Panel_OperationMode.Controls.Add(this.CheckBox_AlwaysEnableKeyboard);
            this.Panel_OperationMode.Controls.Add(this.RadioButton_MouseClick);
            this.Panel_OperationMode.Controls.Add(this.RadioButton_TouchSlide);
            this.Panel_OperationMode.Location = new System.Drawing.Point(30, 260);
            this.Panel_OperationMode.Name = "Panel_OperationMode";
            this.Panel_OperationMode.Size = new System.Drawing.Size(350, 140);
            this.Panel_OperationMode.TabIndex = 0;
            this.Panel_OperationMode.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_OperationMode_Paint);
            // 
            // Label_OperationMode
            // 
            this.Label_OperationMode.AutoSize = true;
            this.Label_OperationMode.BackColor = System.Drawing.Color.Transparent;
            this.Label_OperationMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_OperationMode.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_OperationMode.ForeColor = System.Drawing.Color.White;
            this.Label_OperationMode.Location = new System.Drawing.Point(0, 0);
            this.Label_OperationMode.Name = "Label_OperationMode";
            this.Label_OperationMode.Size = new System.Drawing.Size(69, 20);
            this.Label_OperationMode.TabIndex = 0;
            this.Label_OperationMode.Text = "操作方式";
            // 
            // RadioButton_Keyboard
            // 
            this.RadioButton_Keyboard.AutoSize = true;
            this.RadioButton_Keyboard.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_Keyboard.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RadioButton_Keyboard.ForeColor = System.Drawing.Color.White;
            this.RadioButton_Keyboard.Location = new System.Drawing.Point(25, 30);
            this.RadioButton_Keyboard.Name = "RadioButton_Keyboard";
            this.RadioButton_Keyboard.Size = new System.Drawing.Size(104, 23);
            this.RadioButton_Keyboard.TabIndex = 0;
            this.RadioButton_Keyboard.Text = "键盘 (方向键)";
            this.RadioButton_Keyboard.UseVisualStyleBackColor = false;
            this.RadioButton_Keyboard.CheckedChanged += new System.EventHandler(this.RadioButton_Keyboard_CheckedChanged);
            // 
            // CheckBox_AlwaysEnableKeyboard
            // 
            this.CheckBox_AlwaysEnableKeyboard.AutoSize = true;
            this.CheckBox_AlwaysEnableKeyboard.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_AlwaysEnableKeyboard.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.CheckBox_AlwaysEnableKeyboard.ForeColor = System.Drawing.Color.White;
            this.CheckBox_AlwaysEnableKeyboard.Location = new System.Drawing.Point(45, 55);
            this.CheckBox_AlwaysEnableKeyboard.Name = "CheckBox_AlwaysEnableKeyboard";
            this.CheckBox_AlwaysEnableKeyboard.Size = new System.Drawing.Size(249, 23);
            this.CheckBox_AlwaysEnableKeyboard.TabIndex = 0;
            this.CheckBox_AlwaysEnableKeyboard.TabStop = false;
            this.CheckBox_AlwaysEnableKeyboard.Text = "即使选择其他操作方式，键盘仍然可用";
            this.CheckBox_AlwaysEnableKeyboard.UseVisualStyleBackColor = false;
            this.CheckBox_AlwaysEnableKeyboard.CheckedChanged += new System.EventHandler(this.CheckBox_AlwaysEnableKeyboard_CheckedChanged);
            // 
            // RadioButton_MouseClick
            // 
            this.RadioButton_MouseClick.AutoSize = true;
            this.RadioButton_MouseClick.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_MouseClick.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_MouseClick.ForeColor = System.Drawing.Color.White;
            this.RadioButton_MouseClick.Location = new System.Drawing.Point(25, 80);
            this.RadioButton_MouseClick.Name = "RadioButton_MouseClick";
            this.RadioButton_MouseClick.Size = new System.Drawing.Size(130, 23);
            this.RadioButton_MouseClick.TabIndex = 0;
            this.RadioButton_MouseClick.Text = "鼠标 (点击或单击)";
            this.RadioButton_MouseClick.UseVisualStyleBackColor = false;
            this.RadioButton_MouseClick.CheckedChanged += new System.EventHandler(this.RadioButton_MouseClick_CheckedChanged);
            // 
            // RadioButton_TouchSlide
            // 
            this.RadioButton_TouchSlide.AutoSize = true;
            this.RadioButton_TouchSlide.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_TouchSlide.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_TouchSlide.ForeColor = System.Drawing.Color.White;
            this.RadioButton_TouchSlide.Location = new System.Drawing.Point(25, 105);
            this.RadioButton_TouchSlide.Name = "RadioButton_TouchSlide";
            this.RadioButton_TouchSlide.Size = new System.Drawing.Size(117, 23);
            this.RadioButton_TouchSlide.TabIndex = 0;
            this.RadioButton_TouchSlide.Text = "触屏 (滑动手势)";
            this.RadioButton_TouchSlide.UseVisualStyleBackColor = false;
            this.RadioButton_TouchSlide.CheckedChanged += new System.EventHandler(this.RadioButton_TouchSlide_CheckedChanged);
            // 
            // Panel_Save
            // 
            this.Panel_Save.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Save.Controls.Add(this.Label_Save);
            this.Panel_Save.Controls.Add(this.CheckBox_EnableUndo);
            this.Panel_Save.Controls.Add(this.Label_EnableUndo_Info);
            this.Panel_Save.Controls.Add(this.RadioButton_SaveEveryStep);
            this.Panel_Save.Controls.Add(this.Label_TooSlow);
            this.Panel_Save.Controls.Add(this.Label_CleanGameStep);
            this.Panel_Save.Controls.Add(this.Label_CleanGameStepDone);
            this.Panel_Save.Controls.Add(this.RadioButton_SaveLastStep);
            this.Panel_Save.Location = new System.Drawing.Point(30, 400);
            this.Panel_Save.Name = "Panel_Save";
            this.Panel_Save.Size = new System.Drawing.Size(350, 190);
            this.Panel_Save.TabIndex = 0;
            this.Panel_Save.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Save_Paint);
            // 
            // Label_Save
            // 
            this.Label_Save.AutoSize = true;
            this.Label_Save.BackColor = System.Drawing.Color.Transparent;
            this.Label_Save.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_Save.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Save.ForeColor = System.Drawing.Color.White;
            this.Label_Save.Location = new System.Drawing.Point(0, 0);
            this.Label_Save.Name = "Label_Save";
            this.Label_Save.Size = new System.Drawing.Size(39, 20);
            this.Label_Save.TabIndex = 0;
            this.Label_Save.Text = "存档";
            // 
            // CheckBox_EnableUndo
            // 
            this.CheckBox_EnableUndo.AutoSize = true;
            this.CheckBox_EnableUndo.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_EnableUndo.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.CheckBox_EnableUndo.ForeColor = System.Drawing.Color.White;
            this.CheckBox_EnableUndo.Location = new System.Drawing.Point(25, 30);
            this.CheckBox_EnableUndo.Name = "CheckBox_EnableUndo";
            this.CheckBox_EnableUndo.Size = new System.Drawing.Size(132, 23);
            this.CheckBox_EnableUndo.TabIndex = 0;
            this.CheckBox_EnableUndo.TabStop = false;
            this.CheckBox_EnableUndo.Text = "允许撤销操作步骤";
            this.CheckBox_EnableUndo.UseVisualStyleBackColor = false;
            this.CheckBox_EnableUndo.CheckedChanged += new System.EventHandler(this.CheckBox_EnableUndo_CheckedChanged);
            // 
            // Label_EnableUndo_Info
            // 
            this.Label_EnableUndo_Info.AutoSize = true;
            this.Label_EnableUndo_Info.BackColor = System.Drawing.Color.Transparent;
            this.Label_EnableUndo_Info.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_EnableUndo_Info.ForeColor = System.Drawing.Color.White;
            this.Label_EnableUndo_Info.Location = new System.Drawing.Point(41, 55);
            this.Label_EnableUndo_Info.Name = "Label_EnableUndo_Info";
            this.Label_EnableUndo_Info.Size = new System.Drawing.Size(178, 19);
            this.Label_EnableUndo_Info.TabIndex = 0;
            this.Label_EnableUndo_Info.Text = "选择此项将不会保存你的记录";
            // 
            // RadioButton_SaveEveryStep
            // 
            this.RadioButton_SaveEveryStep.AutoSize = true;
            this.RadioButton_SaveEveryStep.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_SaveEveryStep.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RadioButton_SaveEveryStep.ForeColor = System.Drawing.Color.White;
            this.RadioButton_SaveEveryStep.Location = new System.Drawing.Point(45, 80);
            this.RadioButton_SaveEveryStep.Name = "RadioButton_SaveEveryStep";
            this.RadioButton_SaveEveryStep.Size = new System.Drawing.Size(105, 23);
            this.RadioButton_SaveEveryStep.TabIndex = 0;
            this.RadioButton_SaveEveryStep.Text = "保存所有步骤";
            this.RadioButton_SaveEveryStep.UseVisualStyleBackColor = false;
            this.RadioButton_SaveEveryStep.CheckedChanged += new System.EventHandler(this.RadioButton_SaveEveryStep_CheckedChanged);
            // 
            // Label_TooSlow
            // 
            this.Label_TooSlow.AutoSize = true;
            this.Label_TooSlow.BackColor = System.Drawing.Color.Transparent;
            this.Label_TooSlow.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_TooSlow.ForeColor = System.Drawing.Color.White;
            this.Label_TooSlow.Location = new System.Drawing.Point(61, 105);
            this.Label_TooSlow.Name = "Label_TooSlow";
            this.Label_TooSlow.Size = new System.Drawing.Size(181, 19);
            this.Label_TooSlow.TabIndex = 0;
            this.Label_TooSlow.Text = "如果打开或保存较慢，你可以:";
            // 
            // Label_CleanGameStep
            // 
            this.Label_CleanGameStep.AutoSize = true;
            this.Label_CleanGameStep.BackColor = System.Drawing.Color.Transparent;
            this.Label_CleanGameStep.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_CleanGameStep.ForeColor = System.Drawing.Color.White;
            this.Label_CleanGameStep.Location = new System.Drawing.Point(61, 130);
            this.Label_CleanGameStep.Name = "Label_CleanGameStep";
            this.Label_CleanGameStep.Size = new System.Drawing.Size(87, 19);
            this.Label_CleanGameStep.TabIndex = 0;
            this.Label_CleanGameStep.Text = "清理所有步骤";
            // 
            // Label_CleanGameStepDone
            // 
            this.Label_CleanGameStepDone.AutoSize = true;
            this.Label_CleanGameStepDone.BackColor = System.Drawing.Color.Transparent;
            this.Label_CleanGameStepDone.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_CleanGameStepDone.ForeColor = System.Drawing.Color.White;
            this.Label_CleanGameStepDone.Location = new System.Drawing.Point(155, 130);
            this.Label_CleanGameStepDone.Name = "Label_CleanGameStepDone";
            this.Label_CleanGameStepDone.Size = new System.Drawing.Size(35, 19);
            this.Label_CleanGameStepDone.TabIndex = 0;
            this.Label_CleanGameStepDone.Text = "完成";
            // 
            // RadioButton_SaveLastStep
            // 
            this.RadioButton_SaveLastStep.AutoSize = true;
            this.RadioButton_SaveLastStep.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_SaveLastStep.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_SaveLastStep.ForeColor = System.Drawing.Color.White;
            this.RadioButton_SaveLastStep.Location = new System.Drawing.Point(45, 155);
            this.RadioButton_SaveLastStep.Name = "RadioButton_SaveLastStep";
            this.RadioButton_SaveLastStep.Size = new System.Drawing.Size(118, 23);
            this.RadioButton_SaveLastStep.TabIndex = 0;
            this.RadioButton_SaveLastStep.Text = "仅保存最后一步";
            this.RadioButton_SaveLastStep.UseVisualStyleBackColor = false;
            this.RadioButton_SaveLastStep.CheckedChanged += new System.EventHandler(this.RadioButton_SaveLastStep_CheckedChanged);
            // 
            // Panel_ThemeColor
            // 
            this.Panel_ThemeColor.BackColor = System.Drawing.Color.Transparent;
            this.Panel_ThemeColor.Controls.Add(this.Label_ThemeColor);
            this.Panel_ThemeColor.Controls.Add(this.RadioButton_UseRandomThemeColor);
            this.Panel_ThemeColor.Controls.Add(this.RadioButton_UseCustomColor);
            this.Panel_ThemeColor.Controls.Add(this.Label_ThemeColorName);
            this.Panel_ThemeColor.Location = new System.Drawing.Point(30, 590);
            this.Panel_ThemeColor.Name = "Panel_ThemeColor";
            this.Panel_ThemeColor.Size = new System.Drawing.Size(350, 115);
            this.Panel_ThemeColor.TabIndex = 0;
            this.Panel_ThemeColor.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_ThemeColor_Paint);
            // 
            // Label_ThemeColor
            // 
            this.Label_ThemeColor.AutoSize = true;
            this.Label_ThemeColor.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThemeColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_ThemeColor.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_ThemeColor.ForeColor = System.Drawing.Color.White;
            this.Label_ThemeColor.Location = new System.Drawing.Point(0, 0);
            this.Label_ThemeColor.Name = "Label_ThemeColor";
            this.Label_ThemeColor.Size = new System.Drawing.Size(54, 20);
            this.Label_ThemeColor.TabIndex = 0;
            this.Label_ThemeColor.Text = "主题色";
            // 
            // RadioButton_UseRandomThemeColor
            // 
            this.RadioButton_UseRandomThemeColor.AutoSize = true;
            this.RadioButton_UseRandomThemeColor.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_UseRandomThemeColor.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RadioButton_UseRandomThemeColor.ForeColor = System.Drawing.Color.White;
            this.RadioButton_UseRandomThemeColor.Location = new System.Drawing.Point(25, 30);
            this.RadioButton_UseRandomThemeColor.Name = "RadioButton_UseRandomThemeColor";
            this.RadioButton_UseRandomThemeColor.Size = new System.Drawing.Size(53, 23);
            this.RadioButton_UseRandomThemeColor.TabIndex = 0;
            this.RadioButton_UseRandomThemeColor.Text = "随机";
            this.RadioButton_UseRandomThemeColor.UseVisualStyleBackColor = false;
            this.RadioButton_UseRandomThemeColor.CheckedChanged += new System.EventHandler(this.RadioButton_UseRandomThemeColor_CheckedChanged);
            // 
            // RadioButton_UseCustomColor
            // 
            this.RadioButton_UseCustomColor.AutoSize = true;
            this.RadioButton_UseCustomColor.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_UseCustomColor.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_UseCustomColor.ForeColor = System.Drawing.Color.White;
            this.RadioButton_UseCustomColor.Location = new System.Drawing.Point(25, 55);
            this.RadioButton_UseCustomColor.Name = "RadioButton_UseCustomColor";
            this.RadioButton_UseCustomColor.Size = new System.Drawing.Size(69, 23);
            this.RadioButton_UseCustomColor.TabIndex = 0;
            this.RadioButton_UseCustomColor.Text = "自定义:";
            this.RadioButton_UseCustomColor.UseVisualStyleBackColor = false;
            this.RadioButton_UseCustomColor.CheckedChanged += new System.EventHandler(this.RadioButton_UseCustomColor_CheckedChanged);
            // 
            // Label_ThemeColorName
            // 
            this.Label_ThemeColorName.AutoSize = true;
            this.Label_ThemeColorName.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThemeColorName.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThemeColorName.ForeColor = System.Drawing.Color.White;
            this.Label_ThemeColorName.Location = new System.Drawing.Point(41, 80);
            this.Label_ThemeColorName.Name = "Label_ThemeColorName";
            this.Label_ThemeColorName.Size = new System.Drawing.Size(83, 19);
            this.Label_ThemeColorName.TabIndex = 0;
            this.Label_ThemeColorName.Text = "ThemeColor";
            // 
            // Panel_AntiAlias
            // 
            this.Panel_AntiAlias.BackColor = System.Drawing.Color.Transparent;
            this.Panel_AntiAlias.Controls.Add(this.CheckBox_AntiAlias);
            this.Panel_AntiAlias.Controls.Add(this.Label_AntiAlias);
            this.Panel_AntiAlias.Location = new System.Drawing.Point(30, 705);
            this.Panel_AntiAlias.Name = "Panel_AntiAlias";
            this.Panel_AntiAlias.Size = new System.Drawing.Size(350, 65);
            this.Panel_AntiAlias.TabIndex = 0;
            this.Panel_AntiAlias.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_AntiAlias_Paint);
            // 
            // CheckBox_AntiAlias
            // 
            this.CheckBox_AntiAlias.AutoSize = true;
            this.CheckBox_AntiAlias.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_AntiAlias.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.CheckBox_AntiAlias.ForeColor = System.Drawing.Color.White;
            this.CheckBox_AntiAlias.Location = new System.Drawing.Point(25, 30);
            this.CheckBox_AntiAlias.Name = "CheckBox_AntiAlias";
            this.CheckBox_AntiAlias.Size = new System.Drawing.Size(145, 23);
            this.CheckBox_AntiAlias.TabIndex = 0;
            this.CheckBox_AntiAlias.TabStop = false;
            this.CheckBox_AntiAlias.Text = "使用抗锯齿模式绘图";
            this.CheckBox_AntiAlias.UseVisualStyleBackColor = false;
            this.CheckBox_AntiAlias.CheckedChanged += new System.EventHandler(this.CheckBox_AntiAlias_CheckedChanged);
            // 
            // Label_AntiAlias
            // 
            this.Label_AntiAlias.AutoSize = true;
            this.Label_AntiAlias.BackColor = System.Drawing.Color.Transparent;
            this.Label_AntiAlias.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_AntiAlias.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_AntiAlias.ForeColor = System.Drawing.Color.White;
            this.Label_AntiAlias.Location = new System.Drawing.Point(0, 0);
            this.Label_AntiAlias.Name = "Label_AntiAlias";
            this.Label_AntiAlias.Size = new System.Drawing.Size(54, 20);
            this.Label_AntiAlias.TabIndex = 0;
            this.Label_AntiAlias.Text = "抗锯齿";
            // 
            // Panel_Tab_About
            // 
            this.Panel_Tab_About.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_About.Controls.Add(this.PictureBox_ApplicationLogo);
            this.Panel_Tab_About.Controls.Add(this.Label_ApplicationName);
            this.Panel_Tab_About.Controls.Add(this.Label_ApplicationEdition);
            this.Panel_Tab_About.Controls.Add(this.Label_Version);
            this.Panel_Tab_About.Controls.Add(this.Label_Copyright);
            this.Panel_Tab_About.Controls.Add(this.Panel_GitHub);
            this.Panel_Tab_About.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_About.MinimumSize = new System.Drawing.Size(395, 315);
            this.Panel_Tab_About.Name = "Panel_Tab_About";
            this.Panel_Tab_About.Size = new System.Drawing.Size(430, 420);
            this.Panel_Tab_About.TabIndex = 0;
            this.Panel_Tab_About.Visible = false;
            // 
            // PictureBox_ApplicationLogo
            // 
            this.PictureBox_ApplicationLogo.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_ApplicationLogo.ErrorImage = null;
            this.PictureBox_ApplicationLogo.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_ApplicationLogo.Image")));
            this.PictureBox_ApplicationLogo.InitialImage = null;
            this.PictureBox_ApplicationLogo.Location = new System.Drawing.Point(60, 60);
            this.PictureBox_ApplicationLogo.Name = "PictureBox_ApplicationLogo";
            this.PictureBox_ApplicationLogo.Size = new System.Drawing.Size(64, 64);
            this.PictureBox_ApplicationLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox_ApplicationLogo.TabIndex = 0;
            this.PictureBox_ApplicationLogo.TabStop = false;
            // 
            // Label_ApplicationName
            // 
            this.Label_ApplicationName.AutoSize = true;
            this.Label_ApplicationName.BackColor = System.Drawing.Color.Transparent;
            this.Label_ApplicationName.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ApplicationName.ForeColor = System.Drawing.Color.White;
            this.Label_ApplicationName.Location = new System.Drawing.Point(155, 65);
            this.Label_ApplicationName.Name = "Label_ApplicationName";
            this.Label_ApplicationName.Size = new System.Drawing.Size(212, 31);
            this.Label_ApplicationName.TabIndex = 0;
            this.Label_ApplicationName.Text = "ApplicationName";
            // 
            // Label_ApplicationEdition
            // 
            this.Label_ApplicationEdition.AutoSize = true;
            this.Label_ApplicationEdition.BackColor = System.Drawing.Color.Transparent;
            this.Label_ApplicationEdition.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ApplicationEdition.ForeColor = System.Drawing.Color.White;
            this.Label_ApplicationEdition.Location = new System.Drawing.Point(157, 100);
            this.Label_ApplicationEdition.Name = "Label_ApplicationEdition";
            this.Label_ApplicationEdition.Size = new System.Drawing.Size(149, 21);
            this.Label_ApplicationEdition.TabIndex = 0;
            this.Label_ApplicationEdition.Text = "ApplicationEdition";
            // 
            // Label_Version
            // 
            this.Label_Version.AutoSize = true;
            this.Label_Version.BackColor = System.Drawing.Color.Transparent;
            this.Label_Version.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Version.ForeColor = System.Drawing.Color.White;
            this.Label_Version.Location = new System.Drawing.Point(60, 185);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(88, 19);
            this.Label_Version.TabIndex = 0;
            this.Label_Version.Text = "版本: Version";
            // 
            // Label_Copyright
            // 
            this.Label_Copyright.AutoSize = true;
            this.Label_Copyright.BackColor = System.Drawing.Color.Transparent;
            this.Label_Copyright.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_Copyright.ForeColor = System.Drawing.Color.White;
            this.Label_Copyright.Location = new System.Drawing.Point(60, 210);
            this.Label_Copyright.Name = "Label_Copyright";
            this.Label_Copyright.Size = new System.Drawing.Size(272, 19);
            this.Label_Copyright.TabIndex = 0;
            this.Label_Copyright.Text = "Copyright © 2019 chibayuki@foxmail.com";
            // 
            // Panel_GitHub
            // 
            this.Panel_GitHub.BackColor = System.Drawing.Color.Transparent;
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Part1);
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Base);
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Part2);
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Release);
            this.Panel_GitHub.Location = new System.Drawing.Point(60, 235);
            this.Panel_GitHub.Name = "Panel_GitHub";
            this.Panel_GitHub.Size = new System.Drawing.Size(270, 19);
            this.Panel_GitHub.TabIndex = 0;
            // 
            // Label_GitHub_Part1
            // 
            this.Label_GitHub_Part1.AutoSize = true;
            this.Label_GitHub_Part1.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Part1.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_GitHub_Part1.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Part1.Location = new System.Drawing.Point(0, 0);
            this.Label_GitHub_Part1.Name = "Label_GitHub_Part1";
            this.Label_GitHub_Part1.Size = new System.Drawing.Size(113, 19);
            this.Label_GitHub_Part1.TabIndex = 0;
            this.Label_GitHub_Part1.Text = "访问 GitHub 查看";
            // 
            // Label_GitHub_Base
            // 
            this.Label_GitHub_Base.AutoSize = true;
            this.Label_GitHub_Base.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Base.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline);
            this.Label_GitHub_Base.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Base.Location = new System.Drawing.Point(113, 0);
            this.Label_GitHub_Base.Name = "Label_GitHub_Base";
            this.Label_GitHub_Base.Size = new System.Drawing.Size(48, 19);
            this.Label_GitHub_Base.TabIndex = 0;
            this.Label_GitHub_Base.Text = "源代码";
            // 
            // Label_GitHub_Part2
            // 
            this.Label_GitHub_Part2.AutoSize = true;
            this.Label_GitHub_Part2.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Part2.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_GitHub_Part2.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Part2.Location = new System.Drawing.Point(161, 0);
            this.Label_GitHub_Part2.Name = "Label_GitHub_Part2";
            this.Label_GitHub_Part2.Size = new System.Drawing.Size(22, 19);
            this.Label_GitHub_Part2.TabIndex = 0;
            this.Label_GitHub_Part2.Text = "或";
            // 
            // Label_GitHub_Release
            // 
            this.Label_GitHub_Release.AutoSize = true;
            this.Label_GitHub_Release.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Release.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline);
            this.Label_GitHub_Release.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Release.Location = new System.Drawing.Point(183, 0);
            this.Label_GitHub_Release.Name = "Label_GitHub_Release";
            this.Label_GitHub_Release.Size = new System.Drawing.Size(87, 19);
            this.Label_GitHub_Release.TabIndex = 0;
            this.Label_GitHub_Release.Text = "最新发布版本";
            // 
            // Panel_GameUI
            // 
            this.Panel_GameUI.BackColor = System.Drawing.Color.Transparent;
            this.Panel_GameUI.Controls.Add(this.Panel_Current);
            this.Panel_GameUI.Controls.Add(this.Panel_Environment);
            this.Panel_GameUI.Location = new System.Drawing.Point(0, 0);
            this.Panel_GameUI.Name = "Panel_GameUI";
            this.Panel_GameUI.Size = new System.Drawing.Size(585, 420);
            this.Panel_GameUI.TabIndex = 0;
            this.Panel_GameUI.Visible = false;
            // 
            // Panel_Current
            // 
            this.Panel_Current.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Current.Controls.Add(this.Panel_Interrupt);
            this.Panel_Current.Location = new System.Drawing.Point(0, 0);
            this.Panel_Current.Name = "Panel_Current";
            this.Panel_Current.Size = new System.Drawing.Size(585, 50);
            this.Panel_Current.TabIndex = 0;
            this.Panel_Current.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Current_Paint);
            // 
            // Panel_Interrupt
            // 
            this.Panel_Interrupt.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Undo);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Redo);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Restart);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_ExitGame);
            this.Panel_Interrupt.Location = new System.Drawing.Point(385, 0);
            this.Panel_Interrupt.Name = "Panel_Interrupt";
            this.Panel_Interrupt.Size = new System.Drawing.Size(200, 50);
            this.Panel_Interrupt.TabIndex = 0;
            // 
            // PictureBox_Undo
            // 
            this.PictureBox_Undo.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Undo.ErrorImage = null;
            this.PictureBox_Undo.InitialImage = null;
            this.PictureBox_Undo.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Undo.Name = "PictureBox_Undo";
            this.PictureBox_Undo.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Undo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Undo.TabIndex = 0;
            this.PictureBox_Undo.TabStop = false;
            // 
            // PictureBox_Redo
            // 
            this.PictureBox_Redo.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Redo.ErrorImage = null;
            this.PictureBox_Redo.InitialImage = null;
            this.PictureBox_Redo.Location = new System.Drawing.Point(50, 0);
            this.PictureBox_Redo.Name = "PictureBox_Redo";
            this.PictureBox_Redo.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Redo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Redo.TabIndex = 0;
            this.PictureBox_Redo.TabStop = false;
            // 
            // PictureBox_Restart
            // 
            this.PictureBox_Restart.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Restart.ErrorImage = null;
            this.PictureBox_Restart.InitialImage = null;
            this.PictureBox_Restart.Location = new System.Drawing.Point(100, 0);
            this.PictureBox_Restart.Name = "PictureBox_Restart";
            this.PictureBox_Restart.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Restart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Restart.TabIndex = 0;
            this.PictureBox_Restart.TabStop = false;
            // 
            // PictureBox_ExitGame
            // 
            this.PictureBox_ExitGame.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_ExitGame.ErrorImage = null;
            this.PictureBox_ExitGame.InitialImage = null;
            this.PictureBox_ExitGame.Location = new System.Drawing.Point(150, 0);
            this.PictureBox_ExitGame.Name = "PictureBox_ExitGame";
            this.PictureBox_ExitGame.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_ExitGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_ExitGame.TabIndex = 0;
            this.PictureBox_ExitGame.TabStop = false;
            // 
            // Panel_Environment
            // 
            this.Panel_Environment.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Environment.Location = new System.Drawing.Point(0, 50);
            this.Panel_Environment.Name = "Panel_Environment";
            this.Panel_Environment.Size = new System.Drawing.Size(585, 370);
            this.Panel_Environment.TabIndex = 0;
            this.Panel_Environment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Panel_Environment_KeyDown);
            this.Panel_Environment.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Environment_Paint);
            this.Panel_Environment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseDown);
            this.Panel_Environment.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseMove);
            this.Panel_Environment.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseUp);
            this.Panel_Environment.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseWheel);
            // 
            // Timer_EnterPrompt
            // 
            this.Timer_EnterPrompt.Interval = 10;
            this.Timer_EnterPrompt.Tick += new System.EventHandler(this.Timer_EnterPrompt_Tick);
            // 
            // BackgroundWorker_LoadGameStep
            // 
            this.BackgroundWorker_LoadGameStep.WorkerReportsProgress = true;
            this.BackgroundWorker_LoadGameStep.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_LoadGameStep_DoWork);
            this.BackgroundWorker_LoadGameStep.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_LoadGameStep_ProgressChanged);
            this.BackgroundWorker_LoadGameStep.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_LoadGameStep_RunWorkerCompleted);
            // 
            // BackgroundWorker_SaveGameStep
            // 
            this.BackgroundWorker_SaveGameStep.WorkerReportsProgress = true;
            this.BackgroundWorker_SaveGameStep.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_SaveGameStep_DoWork);
            this.BackgroundWorker_SaveGameStep.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_SaveGameStep_ProgressChanged);
            this.BackgroundWorker_SaveGameStep.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_SaveGameStep_RunWorkerCompleted);
            // 
            // ToolTip_InterruptPrompt
            // 
            this.ToolTip_InterruptPrompt.ShowAlways = true;
            // 
            // ColorDialog_ThemeColor
            // 
            this.ColorDialog_ThemeColor.Color = System.Drawing.Color.White;
            this.ColorDialog_ThemeColor.FullOpen = true;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(585, 420);
            this.Controls.Add(this.Panel_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Panel_Main.ResumeLayout(false);
            this.Panel_Client.ResumeLayout(false);
            this.Panel_FunctionArea.ResumeLayout(false);
            this.Panel_FunctionAreaOptionsBar.ResumeLayout(false);
            this.Panel_FunctionAreaTab.ResumeLayout(false);
            this.Panel_Tab_Start.ResumeLayout(false);
            this.Panel_EnterGameSelection.ResumeLayout(false);
            this.Panel_Tab_Record.ResumeLayout(false);
            this.Panel_Score.ResumeLayout(false);
            this.Panel_Score.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Score)).EndInit();
            this.Panel_GameTime.ResumeLayout(false);
            this.Panel_GameTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameTime)).EndInit();
            this.Panel_Tab_Options.ResumeLayout(false);
            this.Panel_Range.ResumeLayout(false);
            this.Panel_Range.PerformLayout();
            this.Panel_Probability.ResumeLayout(false);
            this.Panel_Probability.PerformLayout();
            this.Panel_OperationMode.ResumeLayout(false);
            this.Panel_OperationMode.PerformLayout();
            this.Panel_Save.ResumeLayout(false);
            this.Panel_Save.PerformLayout();
            this.Panel_ThemeColor.ResumeLayout(false);
            this.Panel_ThemeColor.PerformLayout();
            this.Panel_AntiAlias.ResumeLayout(false);
            this.Panel_AntiAlias.PerformLayout();
            this.Panel_Tab_About.ResumeLayout(false);
            this.Panel_Tab_About.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ApplicationLogo)).EndInit();
            this.Panel_GitHub.ResumeLayout(false);
            this.Panel_GitHub.PerformLayout();
            this.Panel_GameUI.ResumeLayout(false);
            this.Panel_Current.ResumeLayout(false);
            this.Panel_Interrupt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Undo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Redo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Restart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ExitGame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Panel Panel_Environment;
        private System.Windows.Forms.Panel Panel_Client;
        private System.Windows.Forms.Panel Panel_Current;
        private System.Windows.Forms.Panel Panel_GameUI;
        private System.Windows.Forms.Panel Panel_FunctionArea;
        private System.Windows.Forms.Panel Panel_Tab_Start;
        private System.Windows.Forms.Panel Panel_Tab_About;
        private System.Windows.Forms.Label Label_ApplicationEdition;
        private System.Windows.Forms.Label Label_Copyright;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.Label Label_ApplicationName;
        private System.Windows.Forms.PictureBox PictureBox_ApplicationLogo;
        private System.Windows.Forms.Panel Panel_FunctionAreaOptionsBar;
        private System.Windows.Forms.Panel Panel_EnterGameSelection;
        private System.Windows.Forms.Panel Panel_Tab_Options;
        private System.Windows.Forms.Panel Panel_Range;
        private System.Windows.Forms.Label Label_Range;
        private System.Windows.Forms.Timer Timer_EnterPrompt;
        private System.Windows.Forms.Panel Panel_Tab_Record;
        private System.Windows.Forms.Panel Panel_GameTime;
        private System.Windows.Forms.Label Label_TotalTimeVal;
        private System.Windows.Forms.Label Label_ThisTimeVal;
        private System.Windows.Forms.Label Label_TotalTime;
        private System.Windows.Forms.Label Label_ThisTime;
        private System.Windows.Forms.Panel Panel_Score;
        private System.Windows.Forms.Label Label_BestRecordVal_Score;
        private System.Windows.Forms.Label Label_ThisRecordVal_Score;
        private System.Windows.Forms.Label Label_BestRecord;
        private System.Windows.Forms.Label Label_ThisRecord;
        private System.Windows.Forms.ComboBox ComboBox_Range_Height;
        private System.Windows.Forms.ComboBox ComboBox_Range_Width;
        private System.Windows.Forms.Panel Panel_OperationMode;
        private System.Windows.Forms.Label Label_OperationMode;
        private System.Windows.Forms.RadioButton RadioButton_TouchSlide;
        private System.Windows.Forms.RadioButton RadioButton_MouseClick;
        private System.Windows.Forms.RadioButton RadioButton_Keyboard;
        private System.Windows.Forms.Panel Panel_Save;
        private System.Windows.Forms.Label Label_Save;
        private System.Windows.Forms.RadioButton RadioButton_SaveEveryStep;
        private System.Windows.Forms.RadioButton RadioButton_SaveLastStep;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_LoadGameStep;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_SaveGameStep;
        private System.Windows.Forms.Label Label_CleanGameStep;
        private System.Windows.Forms.Label Label_TooSlow;
        private System.Windows.Forms.Label Label_CleanGameStepDone;
        private System.Windows.Forms.ToolTip ToolTip_InterruptPrompt;
        private System.Windows.Forms.Panel Panel_Interrupt;
        private System.Windows.Forms.Panel Panel_Probability;
        private System.Windows.Forms.Label Label_Probability;
        private System.Windows.Forms.Panel Panel_ProbabilityAdjustment;
        private System.Windows.Forms.Label Label_Probability_Value;
        private System.Windows.Forms.Label Label_ThisRecordVal_MaxAndSum;
        private System.Windows.Forms.Label Label_BestRecordVal_MaxAndSum;
        private System.Windows.Forms.Label Label_Range_Height;
        private System.Windows.Forms.Label Label_Range_Width;
        private System.Windows.Forms.Panel Panel_FunctionAreaTab;
        private System.Windows.Forms.Label Label_Probability_Note_Part1;
        private System.Windows.Forms.PictureBox PictureBox_Score;
        private System.Windows.Forms.PictureBox PictureBox_GameTime;
        private System.Windows.Forms.Panel Panel_AntiAlias;
        private System.Windows.Forms.CheckBox CheckBox_AntiAlias;
        private System.Windows.Forms.Label Label_AntiAlias;
        private System.Windows.Forms.CheckBox CheckBox_AlwaysEnableKeyboard;
        private System.Windows.Forms.Label Label_Probability_Note_Part3;
        private System.Windows.Forms.Label Label_Probability_Note_Part2;
        private System.Windows.Forms.ColorDialog ColorDialog_ThemeColor;
        private System.Windows.Forms.Panel Panel_ThemeColor;
        private System.Windows.Forms.Label Label_ThemeColor;
        private System.Windows.Forms.RadioButton RadioButton_UseRandomThemeColor;
        private System.Windows.Forms.Label Label_ThemeColorName;
        private System.Windows.Forms.RadioButton RadioButton_UseCustomColor;
        private System.Windows.Forms.CheckBox CheckBox_EnableUndo;
        private System.Windows.Forms.Label Label_EnableUndo_Info;
        private System.Windows.Forms.Label Label_Tab_Start;
        private System.Windows.Forms.Label Label_Tab_About;
        private System.Windows.Forms.Label Label_Tab_Options;
        private System.Windows.Forms.Label Label_Tab_Record;
        private System.Windows.Forms.PictureBox PictureBox_Undo;
        private System.Windows.Forms.PictureBox PictureBox_Redo;
        private System.Windows.Forms.PictureBox PictureBox_Restart;
        private System.Windows.Forms.PictureBox PictureBox_ExitGame;
        private System.Windows.Forms.Label Label_StartNewGame;
        private System.Windows.Forms.Label Label_ContinueLastGame;
        private System.Windows.Forms.Label Label_GitHub_Part1;
        private System.Windows.Forms.Label Label_GitHub_Release;
        private System.Windows.Forms.Label Label_GitHub_Part2;
        private System.Windows.Forms.Label Label_GitHub_Base;
        private System.Windows.Forms.Panel Panel_GitHub;
    }
}