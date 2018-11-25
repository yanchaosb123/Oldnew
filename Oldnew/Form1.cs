using Oldnew.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oldnew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }                                  //C:\Program Files (x86)\Avenue SmartIDE\CodingPartner\tools\SvnCmdClient\Version_1.8
        private const string SVNEXE = @"C:\Program Files (x86)\Avenue SmartIDE\CodingPartner\tools\SvnCmdClient\Version_1.8\SVN.exe" ;
        private const string SVNPATH = @"D:\ConsoleAAAA\Code";
        private const string VERSION_COLUMN = "VersionNum";
        private const string OPERATOR_COLUMN = "Operator";
        private const string COMMITDATE_COLUMN = "CommitDate";
        private const string REMARKS_COLUMN = "Remarks";

        private static Font DGV_LOGS_FONT = new System.Drawing.Font("Microsoft YaHei UI", 12);

        private string strSvnlogs = "";

        private void SearchForVersion(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string searchParam = this.seachText.Text;
            DoSearchLogInSvn();
            ShowDgvLogs(searchParam);
        }

        private void DoSearchLogInSvn()
        {
            //" log " + SVNPATH
            strSvnlogs = CommandHelper.ExecuteCmd(SVNEXE, " log " + SVNPATH);
        }


        private void ShowDgvLogs(string filter)
        {
            InitSvnLogDgvDisplay();
            InitSvnLogColumns();
            InitSvnLogDataSource(filter);

        }

        private void InitSvnLogDataSource(string filter)
        {
            bool IsFilter = !String.IsNullOrWhiteSpace(filter);
            String[] s = this.strSvnlogs.Split('\n');
            Console.WriteLine(this.strSvnlogs);
            Match match = null;
            string  commitDate, remarks,version, operatr;
            int lines;
            
            StringBuilder stringBuilder = new StringBuilder();
            DataTable tb = new DataTable()  ;
            InitSVNLogTableColumns(tb);
            

            for (int i = 0; i < s.Length; i++)
            {
                string head = s[i];
                match = Regex.Match(head, @"r\d{5}");
                if ( !match.Success )
                {
                    Console.WriteLine("can not match version");
                    continue;
                }
                version = head.Substring(match.Index, match.Length);
                Console.WriteLine(version);

                match = Regex.Match(head,@"(\d*)\slines*");
                if(! match.Success || match.Groups.Count < 2)
                {
                    Console.WriteLine("can not match lines");
                    Application.Exit();
                }
                lines = Convert.ToInt32(match.Groups[1].Value) + 2 ;
               

                match = Regex.Match(head, @"[a-z]{1,3}[0-9]{6,8}");
                operatr = head.Substring(match.Index, match.Length);

                match = Regex.Match(head, @"\d{4}-\d{2}-\d{2}\s*\d{2}:\d{2}:\d{2}");
                commitDate = head.Substring(match.Index, match.Length);

                for (int j = 0; j < lines; j++)
                {
                    if(j ==0 || j == lines -1)
                    {
                        continue;
                    }
                    stringBuilder.Append(s[i + j]);
                }
                remarks = stringBuilder.ToString();
                stringBuilder.Clear();
                if(!IsFilter ||remarks.Contains(filter.Trim())||operatr.Contains(filter.Trim())|| version.Contains(filter.Trim()) )
                {
                    DataRow row = tb.NewRow();
                    row[VERSION_COLUMN] = version;
                    row[OPERATOR_COLUMN] = operatr;
                    row[COMMITDATE_COLUMN] = commitDate;
                    row[REMARKS_COLUMN] = remarks;
                    tb.Rows.Add(row);
                }
                i += lines;
            }
            dgvLogs.DataSource = tb;
        }

        private void InitSVNLogTableColumns(DataTable tb)
        {
            tb.Columns.Add(new DataColumn() {
                ColumnName = VERSION_COLUMN,
                DataType = typeof(String)
            });
            tb.Columns.Add(new DataColumn()
            {
                ColumnName = OPERATOR_COLUMN,
                DataType = typeof(String)
            });
            tb.Columns.Add(new DataColumn()
            {
                ColumnName = COMMITDATE_COLUMN,
                DataType = typeof(String)
            });
            tb.Columns.Add(new DataColumn()
            {
                ColumnName = REMARKS_COLUMN,
                DataType = typeof(String)
            });
        }
        

        private void InitSvnLogColumns()
        {
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = COMMITDATE_COLUMN,
                DataPropertyName = COMMITDATE_COLUMN,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 15,
                CellTemplate = new DataGridViewTextBoxCell(),
                ValueType = typeof(String)
            });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = REMARKS_COLUMN,
                DataPropertyName = REMARKS_COLUMN,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 70,
                CellTemplate = new DataGridViewTextBoxCell(),
                ValueType = typeof(String)
            });

       
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = OPERATOR_COLUMN,
                DataPropertyName = OPERATOR_COLUMN,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 5,
                ValueType = typeof(String),
                CellTemplate = new DataGridViewTextBoxCell(),
            });
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = VERSION_COLUMN,
                DataPropertyName = VERSION_COLUMN,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 10,
                ValueType = typeof(String),
                CellTemplate = new DataGridViewTextBoxCell()
            });

            foreach (DataGridViewColumn col in dgvLogs.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitSvnLogDgvDisplay()
        {
            dgvLogs.Font = DGV_LOGS_FONT; 

            // for 禁止用户列表调整行高，
            dgvLogs.AllowUserToResizeRows = false;
            dgvLogs.AllowUserToResizeColumns = false;

            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.MultiSelect = false;

          
        }

        private void GenerateOldNew(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectRows = dgvLogs.SelectedRows;
            if(selectRows.Count != 1)
            {
                MessageBox.Show("Can only select one row");
                return;
            }
            DataGridViewRow row = selectRows[0];
            string version = (string)row.Cells[VERSION_COLUMN].Value;
            GenerateOldNewByVersionNum(version);
        }

        private void GenerateOldNewByVersionNum(string version)
        {
            // get directory
            string[] files = GetChangedFilePathsByVersionNum(version);

            // get each file at this version as new

            // get each file before this as old
        }


        private string[] GetChangedFilePathsByVersionNum(string version)
        {
            throw new NotImplementedException();
        }
    }
}
