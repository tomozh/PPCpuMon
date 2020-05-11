using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PPCpuMon
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// CPUの表示モード
        /// </summary>
        private enum DISPLAY_MODE
        { 
            /// <summary>
            /// トータル
            /// </summary>
            TOTAL,

            /// <summary>
            /// 物理コアを表示
            /// </summary>
            PHYSICAL_CORE,

            /// <summary>
            /// 論理コアを表示
            /// </summary>
            LOGICAL_CORE,
        }

        /// <summary>
        /// CPUデータのリスト
        /// </summary>
        private readonly List<CpuItem> _cpuItems = new List<CpuItem>();

        /// <summary>
        /// アニメ停止メニューアイテム
        /// </summary>
        private readonly List<ToolStripMenuItem> _stopAnimationToolStripMenuItems = new List<ToolStripMenuItem>();


        /// <summary>
        /// CPUデータ
        /// </summary>
        public class CpuItem
        {
            /// <summary>
            /// アニメのシーケンス番号 (0～)
            /// </summary>
            private int _anmSeq = 0;

            /// <summary>
            /// タスクトレイアイコン
            /// </summary>
            private NotifyIcon _notifyIcon;

            /// <summary>
            /// パフォーマンスカウンタ
            /// </summary>
            private List<PerformanceCounter> _perfCount;

            /// <summary>
            /// CPU時間 (0～100)
            /// </summary>
            private float _prosessorTime;

            /// <summary>
            /// アイコン
            /// </summary>
            private Icon[] _icons;

            /// <summary>
            /// 表示名
            /// </summary>
            private string _displayName;


            /// <summary>
            /// コンストラクタ
            /// </summary>
            private CpuItem()
            {
            }

            /// <summary>
            /// CpuItem インスタンスを作成
            /// </summary>
            /// <param name="displayName">表示名</param>
            /// <param name="icons">アニメーションアイコン</param>
            /// <param name="instanceList">インスタンス名のリスト</param>
            /// <param name="popupMenu">ポップアップメニュー</param>
            /// <returns>CpuItem インスタンス</returns>
            public static CpuItem Create(string displayName, List<string> instanceList, List<Icon> icons, ContextMenuStrip popupMenu)
            {
                var ci = new CpuItem
                {
                    _icons = icons.ToArray(),
                    _anmSeq = 0,
                    _displayName = displayName,
                    _perfCount = new List<PerformanceCounter>(),
                    _notifyIcon = new NotifyIcon
                    {
                        Icon = icons[0],
                        Visible = true,
                        Text = "",
                        ContextMenuStrip = popupMenu,
                    },
                };

                foreach (var instance in instanceList)
                {
                    ci._perfCount.Add(new PerformanceCounter("Processor Information", "% Processor Time", instance));
                }

                return ci;
            }

            /// <summary>
            /// タスクトレイアイコンを消去
            /// </summary>
            public void ClearTaskTrayIcon()
            {
                if (_notifyIcon != null)
                {
                    _notifyIcon.Visible = false;
                    _notifyIcon.Dispose();
                }
            }

            /// <summary>
            /// CPU時間を更新する (タイマから呼び出すこと)
            /// </summary>
            public void UpdateProsessorTime()
            {
                float prosessorTime = 0;

                // 平均値を求める
                foreach(var perf in _perfCount)
                {
                    prosessorTime += perf.NextValue();
                }

                _prosessorTime = prosessorTime / _perfCount.Count;

                _notifyIcon.Text = string.Format("{0} : {1:F0}%", _displayName, (int)_prosessorTime);
            }

            /// <summary>
            /// タスクトレイアイコンを更新する (タイマから呼び出すこと)
            /// </summary>
            public void UpdateIcon()
            {
                if (_notifyIcon != null)
                {
                    if ((Properties.Settings.Default.StopAnimationCpuLoad <= 0) || (_prosessorTime >= Properties.Settings.Default.StopAnimationCpuLoad))
                    {
                        _notifyIcon.Icon = _icons[_anmSeq];
                        int step = ((int)_prosessorTime / 10) + 1;
                        _anmSeq = (_anmSeq + step) % _icons.Length;
                    }
                }
            }
        }


        public MainForm()
        {
            InitializeComponent();

            // メニュー：表示モード
            totalToolStripMenuItem.Tag = DISPLAY_MODE.TOTAL;
            physicalCoreToolStripMenuItem.Tag = DISPLAY_MODE.PHYSICAL_CORE;
            logicalCoreToolStripMenuItem.Tag = DISPLAY_MODE.LOGICAL_CORE;

            // メニュー：速度
            fastToolStripMenuItem.Tag = 50;
            normalToolStripMenuItem.Tag = 100;
            slowToolStripMenuItem.Tag = 200;

            // メニュー：アニメ停止閾値
            var loads = new int[] { 0, 1, 5, 10, 15, 20 };

            foreach (var load in loads)
            {
                string text = (load <= 0) ? "None" : "CPU <= " + load.ToString() + "%";
                ToolStripItem item = stopAnimationToolStripMenuItem.DropDownItems.Add(text);
                item.Tag = load;
                item.Click += StopAnimationToolStripMenuItemClick;
            }

            // メニュー：CPU使用率サンプリング間隔
            var samples = new int[] { 250, 500, 1000, 2000, 5000 };

            foreach (var sample in samples)
            {
                string text = sample.ToString() + "ms";
                ToolStripItem item = samplingIntervalToolStripMenuItem.DropDownItems.Add(text);
                item.Tag = sample;
                item.Click += SamplingIntervalToolStripMenuItemClick;
            }


            ChangeDisplayMode((DISPLAY_MODE)Properties.Settings.Default.DisplayMode);
            ChangeAnimationSpeed((int)Properties.Settings.Default.AnimationSpeed);
            ChangeStopAnimationCpuLoad(Properties.Settings.Default.StopAnimationCpuLoad);
            ChangeCpuSamplingInterval(Properties.Settings.Default.CPUSamplingInterval);

            InitAllIcons();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns>true：成功</returns>
        private bool InitAllIcons()
        {
            bool success = false;

            timerMonitor.Enabled = false;
            timerAnimation.Enabled = false;

            try
            {
                var icons = new List<Icon>
                {
                    Properties.Resources._01,
                    Properties.Resources._02,
                    Properties.Resources._03,
                    Properties.Resources._04,
                    Properties.Resources._05,
                    Properties.Resources._06,
                    Properties.Resources._07,
                    Properties.Resources._08,
                    Properties.Resources._09,
                    Properties.Resources._10
                };

                // 既に表示しているアイコンを消去
                foreach (var cpuItem in _cpuItems)
                {
                    cpuItem.ClearTaskTrayIcon();
                }

                _cpuItems.Clear();


                DISPLAY_MODE mode = (DISPLAY_MODE)Properties.Settings.Default.DisplayMode;

                if (mode == DISPLAY_MODE.TOTAL)
                {
                    // トータル表示
                    _cpuItems.Add(CpuItem.Create("CPU", new List<string> { "_Total" }, icons, contextMenuStrip));
                }
                else
                {
                    var category = new PerformanceCounterCategory
                    {
                        CategoryName = "Processor Information"
                    };

                    var instanceNames = category.GetInstanceNames();

                    var regex = new Regex(@"^\d+,\d+$");

                    // Socket,CPU の列のみフィルタ後、数値順にソート
                    var query = instanceNames
                        .Where(x => regex.IsMatch(x))
                        .Select(x => x.Split(new char[] { ',' }))
                        .OrderBy(x => int.Parse(x[0]))      // Socket
                        .OrderBy(x => int.Parse(x[1]))      // CPU
                        .ToList();

                    // 物理コアあたりの論理コア数
                    int logicalCorePerPhisicalCore = 0;

                    if (mode == DISPLAY_MODE.PHYSICAL_CORE)
                    {
                        // 物理コアを表示
                        uint numberOfCores = 0; // 物理プロセッサ数
                        uint numberOfLogicalProcessors = 0; // 論理プロセッサ数

                        var managementClass = new ManagementClass("Win32_Processor");
                        var managementObj = managementClass.GetInstances();

                        foreach (var mo in managementObj)
                        {
                            numberOfCores = (uint)mo["NumberOfCores"];
                            numberOfLogicalProcessors = (uint)mo["NumberOfLogicalProcessors"];
                            break;
                        }

                        if (numberOfCores > 0)
                        {
                            logicalCorePerPhisicalCore = (int)(numberOfLogicalProcessors / numberOfCores);
                        }
                    }
                    else if (mode == DISPLAY_MODE.LOGICAL_CORE)
                    {
                        // 論理コアを表示
                        logicalCorePerPhisicalCore = 1;
                    }

                    if (logicalCorePerPhisicalCore > 0)
                    {
                        for (int i = 0; i < query.Count; i += logicalCorePerPhisicalCore)
                        {
                            var instanceList = new List<string>();
                            var cpuNames = new List<string>();

                            for (int j = 0; j < logicalCorePerPhisicalCore; j++)
                            {
                                instanceList.Add(query[i + j][0] + ',' + query[i + j][1]);
                                cpuNames.Add((i + j + 1).ToString());
                            }

                            _cpuItems.Add(CpuItem.Create(
                                "CPU#" + string.Join(", ", cpuNames.ToArray()),
                                instanceList,
                                icons,
                                contextMenuStrip
                                ));
                        }
                    }
                }

                timerMonitor.Enabled = true;
                timerAnimation.Enabled = true;

                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                Application.Exit();
            }

            return success;
        }

        /// <summary>
        /// フォームが閉じられる (終了処理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerAnimation.Enabled = false;
            timerMonitor.Enabled = false;

            foreach (var ci in _cpuItems)
            {
                ci.ClearTaskTrayIcon();
            }

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// フォームの非表示化
        /// </summary>
        protected override CreateParams CreateParams
        {
            [System.Security.Permissions.SecurityPermission(
                System.Security.Permissions.SecurityAction.LinkDemand,
                Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                const long WS_POPUP = 0x80000000L;
                const int WS_VISIBLE = 0x10000000;
                const int WS_SYSMENU = 0x80000;
                const int WS_MAXIMIZEBOX = 0x10000;

                var cp = base.CreateParams;

                cp.ExStyle = WS_EX_TOOLWINDOW;
                cp.Style = unchecked((int)WS_POPUP) | WS_VISIBLE | WS_SYSMENU | WS_MAXIMIZEBOX;
                cp.Width = 0;
                cp.Height = 0;

                return cp;
            }
        }

        /// <summary>
        /// アニメーションのタイマ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            timerAnimation.Enabled = false;

            foreach (var ci in _cpuItems)
            {
                ci.UpdateIcon();
            }

            timerAnimation.Interval = Properties.Settings.Default.AnimationSpeed;
            timerAnimation.Enabled = true;
        }

        /// <summary>
        /// CPU時間更新のタイマ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerMonitor_Tick(object sender, EventArgs e)
        {
            timerMonitor.Enabled = false;

            foreach (var ci in _cpuItems)
            {
                ci.UpdateProsessorTime();
            }

            timerMonitor.Interval = Properties.Settings.Default.CPUSamplingInterval;
            timerMonitor.Enabled = true;
        }

        /// <summary>
        /// ポップアップメニューから終了が選択された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 表示モードを変更する
        /// </summary>
        /// <param name="mode">表示モード</param>
        private void ChangeDisplayMode(DISPLAY_MODE mode)
        {
            foreach (var item in cpuDisplayModeToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.CheckState = ((DISPLAY_MODE)menuItem.Tag == mode) ? CheckState.Checked : CheckState.Unchecked;
                }
            }

            Properties.Settings.Default.DisplayMode = (int)mode;
        }

        /// <summary>
        /// アニメーション速度を変更する
        /// </summary>
        /// <param name="speed">アニメーション速度(ms)</param>
        private void ChangeAnimationSpeed(int speed)
        {
            foreach (var item in speedToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.CheckState = ((int)menuItem.Tag == speed) ? CheckState.Checked : CheckState.Unchecked;
                }
            }

            if (speed < 50)
            {
                speed = 50;
            }
            
            Properties.Settings.Default.AnimationSpeed = (int)speed;
        }

        /// <summary>
        /// アニメーションを停止するCPU使用率を設定する
        /// </summary>
        /// <param name="cpuLoad">CPU使用率(0:停止しない)</param>
        private void ChangeStopAnimationCpuLoad(int cpuLoad)
        {
            foreach (var item in stopAnimationToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Checked = ((int)menuItem.Tag == cpuLoad);
                }
            }

            Properties.Settings.Default.StopAnimationCpuLoad = (int)cpuLoad;
        }

        /// <summary>
        /// CPU使用率サンプリング間隔を設定する
        /// </summary>
        /// <param name="interval">CPU使用率(0:停止しない)</param>
        private void ChangeCpuSamplingInterval(int interval)
        {
            foreach (var item in samplingIntervalToolStripMenuItem.DropDownItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Checked = ((int)menuItem.Tag == interval);
                }
            }

            if (interval < 100)
            {
                interval = 100;
            }

            Properties.Settings.Default.CPUSamplingInterval = (int)interval;
        }

        /// <summary>
        /// メニューの「Mode」が変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem)sender;
            ChangeDisplayMode((DISPLAY_MODE)menu.Tag);
            InitAllIcons();
        }

        /// <summary>
        /// メニューの「Speed」が変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem)sender;
            ChangeAnimationSpeed((int)menu.Tag);
        }

        /// <summary>
        /// メニューの「Animation speed」が変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopAnimationToolStripMenuItemClick(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem)sender;
            ChangeStopAnimationCpuLoad((int)menu.Tag);
        }

        /// <summary>
        /// メニューの「CPU sampling interval」が変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SamplingIntervalToolStripMenuItemClick(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem)sender;
            ChangeCpuSamplingInterval((int)menu.Tag);
        }
    }
}
