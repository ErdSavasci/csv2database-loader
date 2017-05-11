using DatabaseLoader.App_Code;
using MaterialSkin;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseLoader
{
    public partial class Form1 : MetroForm
    {
        private Thread connectThread = null;
        private Thread readCsvThread = null;
        private Stream csvStream = null;
        private bool isFileOpened = false;

        public Form1()
        {
            InitializeComponent();

            SetTheme();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArrangeComponents();
        }

        private void SetTheme()
        {
            MetroStyleManager metroStyleManager = new MetroStyleManager();
            metroStyleManager.Style = MetroFramework.MetroColorStyle.Blue;
            metroStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            performStepCheckBox.SkinManager = materialSkinManager;
            performStepCheckBox.Invalidate();

            this.StyleManager = metroStyleManager;
            exitButton.StyleManager = metroStyleManager;
            openButton.StyleManager = metroStyleManager;
            connectButton.StyleManager = metroStyleManager;
            databaseNameTextBox.StyleManager = metroStyleManager;
            tableNameComboBox.StyleManager = metroStyleManager;
            databaseNameLabel.StyleManager = metroStyleManager;
            tableNameLabel.StyleManager = metroStyleManager;
            columnsOfCsvLabel.StyleManager = metroStyleManager;
            columnsOfCsvTextBox.StyleManager = metroStyleManager;
            selectedFileLink.StyleManager = metroStyleManager;
            skipRowLabel.StyleManager = metroStyleManager;
            skipRowsTextBox.StyleManager = metroStyleManager;
            inputDataLabel.StyleManager = metroStyleManager;
            outputDataLabel.StyleManager = metroStyleManager;
            columnsOfTableLabel.StyleManager = metroStyleManager;
            columnsOfTableTextBox.StyleManager = metroStyleManager;
            uniqueIndexPositionTextBox.StyleManager = metroStyleManager;
            delimiterLabel.StyleManager = metroStyleManager;
            delimiterTextBox.StyleManager = metroStyleManager;
        }
        private void ArrangeComponents()
        {
            connectionStatusLabel.ForeColor = Color.Red;
            connectionStatusLabel.Text = "No Connection is Present";
            commandExecutionStatusLabel.Visible = false;
            tableNameComboBox.Enabled = false;
            insertButton.Enabled = false;
            loadingCircle.Visible = false;
            loadingStatusLabel.ForeColor = Color.ForestGreen;
            loadingStatusLabel.Visible = false;
            openFileDialog.Filter = "CSV Files (*.csv) | *.csv";
            openFileDialog.Title = "Select CSV File";
            openFileDialog.FileName = "*.csv";
            columnsOfCsvTextBox.Enabled = false;
            skipRowsTextBox.Enabled = false;
            columnsOfTableTextBox.Enabled = false;
            int leftPaddingOfProgressBar = exitButton.Location.X - statusStrip.Location.X;
            toolStripProgressBar.Margin = new Padding(leftPaddingOfProgressBar, toolStripProgressBar.Margin.Top, toolStripProgressBar.Margin.Right, toolStripProgressBar.Margin.Bottom);
            toolStripProgressBar.Size = new Size(statusStrip.Width - leftPaddingOfProgressBar - 5, toolStripProgressBar.Height);
            uniqueIndexPositionTextBox.Enabled = false;
            addUniqueKeyCheckBox.Enabled = false;
            performStepCheckBox.Enabled = false;
            parseDateTimeCheckBox.Enabled = false;
            delimiterTextBox.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DatabaseConn.CheckConnection())
                DatabaseConn.CloseConnection();

            if (connectThread != null && connectThread.IsAlive)
            {
                Environment.Exit(0);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit(null);
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if ((connectThread == null || !connectThread.IsAlive) && Regex.IsMatch(databaseNameTextBox.Text, "^[a-zA-Z]+$"))
            {
                connectThread = new Thread(() =>
                {
                    try
                    {
                        Action invokeAction = () => commandExecutionStatusLabel.Visible = false;
                        statusStrip.Invoke(invokeAction);

                        string databaseName = databaseNameTextBox.Text;

                        bool checkConnection = DatabaseConn.OpenConnection(databaseName);

                        if (checkConnection)
                        {
                            invokeAction = () =>
                            {
                                if (!insertButton.Enabled && isFileOpened && tableNameComboBox.SelectedItem != null)
                                    insertButton.Enabled = true;
                            };
                            insertButton.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!tableNameComboBox.Enabled)
                                    tableNameComboBox.Enabled = true;
                                if (tableNameComboBox.Items.Count > 0)
                                    tableNameComboBox.Items.Clear();
                                tableNameComboBox.Invalidate();
                                tableNameComboBox.Items.AddRange(DatabaseConn.GetAllTables().ToArray());
                            };
                            tableNameComboBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                connectionStatusLabel.ForeColor = Color.ForestGreen;
                                connectionStatusLabel.Text = "Connection Successful";
                            };
                            connectionStatusLabel.Invoke(invokeAction);

                            invokeAction = () => loadingStatusLabel.Visible = false;
                            loadingStatusLabel.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!columnsOfCsvTextBox.Enabled && isFileOpened)
                                    columnsOfCsvTextBox.Enabled = true;
                            };
                            columnsOfCsvTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (skipRowsTextBox.Enabled && isFileOpened)
                                    skipRowsTextBox.Enabled = true;
                            };
                            skipRowsTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!columnsOfTableTextBox.Enabled && isFileOpened)
                                    columnsOfTableTextBox.Enabled = true;
                            };
                            columnsOfTableTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!performStepCheckBox.Enabled && isFileOpened)
                                    performStepCheckBox.Enabled = true;
                            };
                            performStepCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!parseDateTimeCheckBox.Enabled && isFileOpened)
                                    parseDateTimeCheckBox.Enabled = true;
                            };
                            parseDateTimeCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!addUniqueKeyCheckBox.Enabled && isFileOpened)
                                    addUniqueKeyCheckBox.Enabled = true;
                            };
                            addUniqueKeyCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!uniqueIndexPositionTextBox.Enabled && isFileOpened && addUniqueKeyCheckBox.Checked)
                                    uniqueIndexPositionTextBox.Enabled = true;
                            };
                            uniqueIndexPositionTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!dateTimeTypeCheckBox.Enabled && isFileOpened && addUniqueKeyCheckBox.Checked)
                                    dateTimeTypeCheckBox.Enabled = true;
                            };
                            dateTimeTypeCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!delimiterTextBox.Enabled && isFileOpened)
                                    delimiterTextBox.Enabled = true;
                            };
                            delimiterTextBox.Invoke(invokeAction);
                        }
                        else
                        {
                            invokeAction = () =>
                            {
                                if (insertButton.Enabled)
                                    insertButton.Enabled = false;
                            };
                            insertButton.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (tableNameComboBox.Enabled)
                                    tableNameComboBox.Enabled = false;
                                if (tableNameComboBox.Items.Count > 0)
                                    tableNameComboBox.Items.Clear();
                                tableNameComboBox.Invalidate();
                            };
                            tableNameComboBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                connectionStatusLabel.ForeColor = Color.Red;
                                connectionStatusLabel.Text = "Connection Failed";
                            };
                            connectionStatusLabel.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (columnsOfCsvTextBox.Enabled && isFileOpened)
                                    columnsOfCsvTextBox.Enabled = false;
                            };
                            columnsOfCsvTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (skipRowsTextBox.Enabled && isFileOpened)
                                    skipRowsTextBox.Enabled = false;
                            };
                            skipRowsTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (columnsOfTableTextBox.Enabled && isFileOpened)
                                    columnsOfTableTextBox.Enabled = false;
                            };
                            columnsOfTableTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (performStepCheckBox.Enabled && isFileOpened)
                                    performStepCheckBox.Enabled = false;
                            };
                            performStepCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (parseDateTimeCheckBox.Enabled && isFileOpened)
                                    parseDateTimeCheckBox.Enabled = false;
                            };
                            parseDateTimeCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (addUniqueKeyCheckBox.Enabled && isFileOpened)
                                    addUniqueKeyCheckBox.Enabled = false;
                            };
                            addUniqueKeyCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (uniqueIndexPositionTextBox.Enabled && isFileOpened)
                                    uniqueIndexPositionTextBox.Enabled = false;
                            };
                            uniqueIndexPositionTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (dateTimeTypeCheckBox.Enabled)
                                    dateTimeTypeCheckBox.Enabled = false;
                            };
                            dateTimeTypeCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (delimiterTextBox.Enabled && isFileOpened)
                                    delimiterTextBox.Enabled = false;
                            };
                            delimiterTextBox.Invoke(invokeAction);
                        }
                        Action invokeAction2 = () =>
                        {
                            loadingCircle.Visible = false;
                            loadingCircle.Active = false;
                        };
                        loadingCircle.Invoke(invokeAction2);

                        invokeAction2 = () => loadingStatusLabel.Visible = false;
                        loadingStatusLabel.Invoke(invokeAction2);
                    }
                    catch
                    {
                        //AN ERROR OCCURRED DURING OPENING CONNECTION
                        Action invokeAction = () =>
                        {
                            loadingCircle.Visible = false;
                            loadingCircle.Active = false;
                        };
                        loadingCircle.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (insertButton.Enabled)
                                insertButton.Enabled = false;
                        };
                        insertButton.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (tableNameComboBox.Enabled)
                                tableNameComboBox.Enabled = false;
                            if (tableNameComboBox.Items.Count > 0)
                                tableNameComboBox.Items.Clear();
                            tableNameComboBox.Invalidate();
                        };
                        tableNameComboBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            connectionStatusLabel.ForeColor = Color.Red;
                            connectionStatusLabel.Text = "Connection Failed";
                        };
                        connectionStatusLabel.Invoke(invokeAction);

                        invokeAction = () => loadingStatusLabel.Visible = false;
                        loadingStatusLabel.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (columnsOfCsvTextBox.Enabled && isFileOpened)
                                columnsOfCsvTextBox.Enabled = false;
                        };
                        columnsOfCsvTextBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (skipRowsTextBox.Enabled && isFileOpened)
                                skipRowsTextBox.Enabled = false;
                        };
                        skipRowsTextBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (columnsOfTableTextBox.Enabled && isFileOpened)
                                columnsOfTableTextBox.Enabled = false;
                        };
                        columnsOfTableTextBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (performStepCheckBox.Enabled && isFileOpened)
                                performStepCheckBox.Enabled = false;
                        };
                        performStepCheckBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (parseDateTimeCheckBox.Enabled && isFileOpened)
                                parseDateTimeCheckBox.Enabled = false;
                        };
                        parseDateTimeCheckBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (addUniqueKeyCheckBox.Enabled && isFileOpened)
                                addUniqueKeyCheckBox.Enabled = false;
                        };
                        addUniqueKeyCheckBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (uniqueIndexPositionTextBox.Enabled && isFileOpened)
                                uniqueIndexPositionTextBox.Enabled = false;
                        };
                        uniqueIndexPositionTextBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (dateTimeTypeCheckBox.Enabled)
                                dateTimeTypeCheckBox.Enabled = false;
                        };
                        dateTimeTypeCheckBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (delimiterTextBox.Enabled && isFileOpened)
                                delimiterTextBox.Enabled = false;
                        };
                        delimiterTextBox.Invoke(invokeAction);
                    }
                });
                connectThread.Start();
                loadingCircle.Visible = true;
                loadingCircle.Active = true;
                loadingStatusLabel.Visible = true;
                loadingStatusLabel.Text = "Connecting...";
            }
        }

        private void tableNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string databaseName = databaseNameTextBox.Text;

            if (!insertButton.Enabled && isFileOpened && DatabaseConn.CheckConnection() && tableNameComboBox.SelectedItem != null)
                insertButton.Enabled = true;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            int rowsAffected = -1;

            if (addUniqueKeyCheckBox.Checked)
            {
                UniqueIndex.SetBeginningValue(!string.IsNullOrEmpty(uniqueIndexPositionTextBox.Text) && !string.IsNullOrWhiteSpace(uniqueIndexPositionTextBox.Text) ? long.Parse(uniqueIndexPositionTextBox.Text) : 1);
                UniqueIndex.ResetUniqueIndex();
            }

            try
            {
                if (((!performStepCheckBox.Checked && Regex.IsMatch(columnsOfCsvTextBox.Text, @"^([1-9]+([\,\-][1-9]+)*)|([1-9]*)$")) || (performStepCheckBox.Checked && Regex.IsMatch(columnsOfCsvTextBox.Text, @"^([1-9]+[\,\-])|([1-9]*)$"))) && Regex.IsMatch(skipRowsTextBox.Text, @"^([1-9]+([\,\-][1-9]+)*)|([1-9]*)$") && Regex.IsMatch(uniqueIndexPositionTextBox.Text, @"^([1-9]*)$"))
                {
                    if (DatabaseConn.CheckConnection())
                    {
                        commandExecutionStatusLabel.Visible = false;

                        string databaseName = databaseNameTextBox.Text;
                        string tableName = (string)tableNameComboBox.SelectedItem;

                        string columnsOfCsvAsText = columnsOfCsvTextBox.Text; //ex. 0,1,2,3 or 0,1-3,5 or 0-5,7 or 1,2,3-9,4-5
                        string skipRowsOfCsvAsText = skipRowsTextBox.Text; //ex. 0,1,2,3 or 0,1-3,5 or 0-5,7 or 1,2,3-9,4-5
                        string columnsOfTableAsText = columnsOfTableTextBox.Text; //ex. 0,1,2,3 or 0,1-3,5 or 0-5,7 or 1,2,3-9,4-5
                        List<int> columnsOfCsv = null;
                        List<int> skipRowsOfCsv = null;
                        List<int> columnsOfTable = null;

                        columnsOfCsv = ParseColumnOrRowIndexes(columnsOfCsvTextBox.Text);
                        skipRowsOfCsv = ParseColumnOrRowIndexes(skipRowsTextBox.Text);
                        columnsOfTable = ParseColumnOrRowIndexes(columnsOfTableTextBox.Text);

                        //GATHERING DATA FROM CSV FILE
                        dynamic[] values = null;
                        bool executeOnce = true;
                        bool rowNotSkipped;

                        int columnIndex = 0;
                        int rowIndex = 1;
                        int valueIndex = 0;
                        int stepCount = -1;

                        if (performStepCheckBox.Checked && columnsOfCsv != null)
                        {
                            stepCount = columnsOfCsv[0];
                        }

                        if (readCsvThread == null || !readCsvThread.IsAlive)
                        {
                            readCsvThread = new Thread(() =>
                            {
                                string csvRow = "";

                                if (csvStream != null)
                                {
                                    using (StreamReader streamReader = new StreamReader(csvStream))
                                    {
                                        int lineCount = File.ReadAllLines(selectedFileLink.Text).Count();
                                        if (skipRowsOfCsv != null)
                                        {
                                            lineCount -= skipRowsOfCsv.Count;
                                        }

                                        Action invokeAction = () => toolStripProgressBar.Maximum = lineCount;
                                        statusStrip.Invoke(invokeAction);

                                        while ((csvRow = streamReader.ReadLine()) != null)
                                        {
                                            rowNotSkipped = true;

                                            if (skipRowsOfCsv != null)
                                            {
                                                for (int i = 0; i < skipRowsOfCsv.Count; i++)
                                                {
                                                    if (rowIndex == skipRowsOfCsv.ElementAt(i))
                                                        rowNotSkipped = false;
                                                }
                                            }

                                            if(performStepCheckBox.Checked && rowNotSkipped)
                                            {
                                                if(rowIndex < stepCount)
                                                    rowNotSkipped = false;
                                            }

                                            if (rowNotSkipped)
                                            {
                                                int totalColumns = 0;

                                                if (Regex.IsMatch(delimiterTextBox.Text, @"^[\,\;\'\|\.\&\%\+\-\^\*\_]$"))
                                                {
                                                    totalColumns += csvRow.Count(f => f.Equals(delimiterTextBox.Text.ElementAt(0)));
                                                    totalColumns++;
                                                }
                                                else
                                                {
                                                    csvRow.Count(f => f.Equals(','));
                                                    totalColumns += csvRow.Count(f => f.Equals(';'));
                                                    totalColumns++;
                                                }                                                

                                                if (executeOnce && totalColumns > 0)
                                                {
                                                    executeOnce = false;

                                                    if (columnsOfCsv != null)
                                                    {
                                                        values = new object[columnsOfCsv.Count];
                                                    }
                                                    else
                                                    {
                                                        columnsOfCsv = new List<int>();
                                                        for (int i = 1; i <= totalColumns; i++)
                                                        {
                                                            columnsOfCsv.Add(i);
                                                        }
                                                        values = new object[columnsOfCsv.Count];
                                                    }
                                                }

                                                string csvRowTemp = csvRow;
                                                for (int i = 1; i <= totalColumns; i++)
                                                {
                                                    if (columnIndex < columnsOfCsv.Count && i != columnsOfCsv.ElementAt(columnIndex))
                                                    {

                                                        csvRowTemp = csvRowTemp.Substring(csvRowTemp.IndexOf(",") + 1);
                                                        columnIndex++;
                                                    }
                                                    else if (valueIndex < values.Length)
                                                    {
                                                        if (!csvRowTemp.StartsWith(","))
                                                        {
                                                            values[valueIndex] = csvRowTemp.Substring(0, csvRowTemp.IndexOf(","));
                                                            valueIndex++;
                                                        }
                                                        else
                                                        {
                                                            values[valueIndex] = null;
                                                            valueIndex++;
                                                        }

                                                        csvRowTemp = csvRowTemp.Substring(csvRowTemp.IndexOf(",") + 1);
                                                        columnIndex++;
                                                    }
                                                }

                                                //INSERT THE VALUES FROM ROW INTO SELECTED TABLE
                                                if (DatabaseConn.CheckConnection() && values != null)
                                                {
                                                    rowsAffected = DatabaseConn.InsertIntoTable(databaseName, tableName, values, columnsOfTable, parseDateTimeCheckBox.Checked, !dateTimeTypeCheckBox.Checked, addUniqueKeyCheckBox.Checked, addUniqueKeyCheckBox.Checked ? (!string.IsNullOrEmpty(uniqueIndexPositionTextBox.Text) && !string.IsNullOrWhiteSpace(uniqueIndexPositionTextBox.Text) ? uniqueIndexPositionTextBox.Text : "1") : "-1");
                                                    invokeAction = () => toolStripProgressBar.PerformStep();
                                                    statusStrip.Invoke(invokeAction);
                                                }
                                            }

                                            valueIndex = 0;
                                            columnIndex = 0;                                          
                                            rowIndex++;

                                            if (stepCount > 0 && columnsOfCsv.Count > 1)
                                                stepCount += columnsOfCsv[1];
                                        }

                                        if (rowsAffected != -1)
                                        {
                                            invokeAction = () =>
                                            {
                                                commandExecutionStatusLabel.Visible = true;
                                                commandExecutionStatusLabel.ForeColor = Color.Green;
                                                commandExecutionStatusLabel.Text = "Insertion Successful";
                                            };
                                            statusStrip.Invoke(invokeAction);
                                        }
                                        else
                                        {
                                            invokeAction = () =>
                                            {
                                                commandExecutionStatusLabel.Visible = true;
                                                commandExecutionStatusLabel.ForeColor = Color.Red;
                                                commandExecutionStatusLabel.Text = "[Error2] An Error Occurred During Insertion Process";
                                            };
                                            statusStrip.Invoke(invokeAction);
                                        }

                                        if (DatabaseConn.CheckConnection())
                                            DatabaseConn.CloseConnection();

                                        invokeAction = () =>
                                        {
                                            connectionStatusLabel.ForeColor = Color.Red;
                                            connectionStatusLabel.Text = "Connection Closed";
                                        };
                                        connectionStatusLabel.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (insertButton.Enabled)
                                                insertButton.Enabled = false;
                                        };
                                        insertButton.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (tableNameComboBox.Enabled)
                                                tableNameComboBox.Enabled = false;
                                            tableNameComboBox.Items.Clear();
                                            tableNameComboBox.Invalidate();
                                        };
                                        tableNameComboBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (columnsOfCsvLabel.Enabled)
                                                columnsOfCsvLabel.Enabled = false;
                                            columnsOfCsvLabel.Enabled = false;
                                        };
                                        columnsOfCsvLabel.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (columnsOfCsvTextBox.Enabled)
                                                columnsOfCsvTextBox.Enabled = false;
                                        };
                                        columnsOfCsvTextBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (skipRowsTextBox.Enabled)
                                                skipRowsTextBox.Enabled = false;
                                        };
                                        skipRowsTextBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (columnsOfTableTextBox.Enabled)
                                                columnsOfTableTextBox.Enabled = false;
                                        };
                                        columnsOfTableTextBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (performStepCheckBox.Enabled)
                                                performStepCheckBox.Enabled = false;
                                        };
                                        performStepCheckBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (parseDateTimeCheckBox.Enabled)
                                                parseDateTimeCheckBox.Enabled = false;
                                        };
                                        parseDateTimeCheckBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (addUniqueKeyCheckBox.Enabled)
                                                addUniqueKeyCheckBox.Enabled = false;
                                        };
                                        addUniqueKeyCheckBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (uniqueIndexPositionTextBox.Enabled)
                                                uniqueIndexPositionTextBox.Enabled = false;
                                        };
                                        uniqueIndexPositionTextBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (dateTimeTypeCheckBox.Enabled)
                                                dateTimeTypeCheckBox.Enabled = false;
                                        };
                                        dateTimeTypeCheckBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (delimiterTextBox.Enabled)
                                                delimiterTextBox.Enabled = false;
                                        };
                                        delimiterTextBox.Invoke(invokeAction);
                                    }
                                }
                            });
                            readCsvThread.Start();
                        }
                        else
                        {
                            commandExecutionStatusLabel.Visible = true;
                            commandExecutionStatusLabel.ForeColor = Color.Red;
                            commandExecutionStatusLabel.Text = "[Error5] Insertion Process Continues";
                        }
                    }
                    else
                    {
                        connectionStatusLabel.Text = "No Connection is Present";

                        commandExecutionStatusLabel.Visible = true;
                        commandExecutionStatusLabel.ForeColor = Color.Red;
                        commandExecutionStatusLabel.Text = "[Error1] No Database Connection is Active";

                        if (insertButton.Enabled)
                            insertButton.Enabled = false;

                        if (tableNameComboBox.Enabled)
                            tableNameComboBox.Enabled = false;
                        tableNameComboBox.Items.Clear();
                        tableNameComboBox.Invalidate();

                        if (columnsOfCsvLabel.Enabled)
                            columnsOfCsvLabel.Enabled = false;

                        if (columnsOfCsvTextBox.Enabled)
                            columnsOfCsvTextBox.Enabled = false;

                        if (skipRowsTextBox.Enabled)
                            skipRowsTextBox.Enabled = false;

                        if (columnsOfTableTextBox.Enabled)
                            columnsOfTableTextBox.Enabled = false;

                        if (performStepCheckBox.Enabled)
                            performStepCheckBox.Enabled = false;

                        if (parseDateTimeCheckBox.Enabled)
                            parseDateTimeCheckBox.Enabled = false;

                        if (addUniqueKeyCheckBox.Enabled)
                            addUniqueKeyCheckBox.Enabled = false;

                        if (uniqueIndexPositionTextBox.Enabled)
                            uniqueIndexPositionTextBox.Enabled = false;

                        if (dateTimeTypeCheckBox.Enabled)
                            dateTimeTypeCheckBox.Enabled = false;

                        if (delimiterTextBox.Enabled)
                            delimiterTextBox.Enabled = false;
                    }
                }
                else
                {
                    commandExecutionStatusLabel.Visible = true;
                    commandExecutionStatusLabel.ForeColor = Color.Red;
                    commandExecutionStatusLabel.Text = "[Error3] No Match Found by Given Column or Skip Row Numbers";

                    if (DatabaseConn.CheckConnection())
                        DatabaseConn.CloseConnection();

                    connectionStatusLabel.ForeColor = Color.Red;
                    connectionStatusLabel.Text = "Connection Closed";

                    if (insertButton.Enabled)
                        insertButton.Enabled = false;

                    if (tableNameComboBox.Enabled)
                        tableNameComboBox.Enabled = false;
                    tableNameComboBox.Items.Clear();
                    tableNameComboBox.Invalidate();

                    if (columnsOfCsvLabel.Enabled)
                        columnsOfCsvLabel.Enabled = false;

                    if (columnsOfCsvTextBox.Enabled)
                        columnsOfCsvTextBox.Enabled = false;

                    if (columnsOfCsvTextBox.Enabled)
                        columnsOfCsvTextBox.Enabled = false;

                    if (skipRowsTextBox.Enabled)
                        skipRowsTextBox.Enabled = false;

                    if (columnsOfTableTextBox.Enabled)
                        columnsOfTableTextBox.Enabled = false;

                    if (performStepCheckBox.Enabled)
                        performStepCheckBox.Enabled = false;

                    if (parseDateTimeCheckBox.Enabled)
                        parseDateTimeCheckBox.Enabled = false;

                    if (addUniqueKeyCheckBox.Enabled)
                        addUniqueKeyCheckBox.Enabled = false;

                    if (uniqueIndexPositionTextBox.Enabled)
                        uniqueIndexPositionTextBox.Enabled = false;

                    if (dateTimeTypeCheckBox.Enabled)
                        dateTimeTypeCheckBox.Enabled = false;

                    if (delimiterTextBox.Enabled)
                        delimiterTextBox.Enabled = false;
                }
            }
            catch
            {
                commandExecutionStatusLabel.Visible = true;
                commandExecutionStatusLabel.ForeColor = Color.Red;
                commandExecutionStatusLabel.Text = "[Error2] An Error Occurred During Insertion Process";

                if (DatabaseConn.CheckConnection())
                    DatabaseConn.CloseConnection();

                connectionStatusLabel.ForeColor = Color.Red;
                connectionStatusLabel.Text = "Connection Closed";

                if (insertButton.Enabled)
                    insertButton.Enabled = false;

                if (tableNameComboBox.Enabled)
                    tableNameComboBox.Enabled = false;
                tableNameComboBox.Items.Clear();
                tableNameComboBox.Invalidate();

                if (columnsOfCsvLabel.Enabled)
                    columnsOfCsvLabel.Enabled = false;

                if (columnsOfCsvTextBox.Enabled)
                    columnsOfCsvTextBox.Enabled = false;

                if (columnsOfCsvTextBox.Enabled)
                    columnsOfCsvTextBox.Enabled = false;

                if (skipRowsTextBox.Enabled)
                    skipRowsTextBox.Enabled = false;

                if (columnsOfTableTextBox.Enabled)
                    columnsOfTableTextBox.Enabled = false;

                if (performStepCheckBox.Enabled)
                    performStepCheckBox.Enabled = false;

                if (parseDateTimeCheckBox.Enabled)
                    parseDateTimeCheckBox.Enabled = false;

                if (addUniqueKeyCheckBox.Enabled)
                    addUniqueKeyCheckBox.Enabled = false;

                if (uniqueIndexPositionTextBox.Enabled)
                    uniqueIndexPositionTextBox.Enabled = false;

                if (dateTimeTypeCheckBox.Enabled)
                    dateTimeTypeCheckBox.Enabled = false;

                if (delimiterTextBox.Enabled)
                    delimiterTextBox.Enabled = false;
            }
        }

        private List<int> ParseColumnOrRowIndexes(string columnsOrRowsFromTextBoxAsText)
        {
            if (!Regex.IsMatch(columnsOrRowsFromTextBoxAsText, "^$"))
            {
                //PARSING COLUMN VALUES INPUTTED BY USER
                string columnOrRowNumberAsText = "";
                int textIndex = 0;

                List<int> columnsOfCsv = new List<int>();

                //ADD INITIAL COLUMN NUMBER
                do
                {
                    columnOrRowNumberAsText += columnsOrRowsFromTextBoxAsText.ElementAt(textIndex);
                    textIndex++;
                }
                while (textIndex < columnsOrRowsFromTextBoxAsText.Length && columnsOrRowsFromTextBoxAsText.ElementAt(textIndex) != ',' && columnsOrRowsFromTextBoxAsText.ElementAt(textIndex) != '-');

                columnsOfCsv.Add(Int32.Parse(columnOrRowNumberAsText));

                //ADD REST
                bool startFromComma = false;
                bool startFromDash = false;
                columnOrRowNumberAsText = "";
                for (int i = textIndex; i < columnsOrRowsFromTextBoxAsText.Length; i++)
                {
                    if (startFromComma)
                    {
                        if (!columnsOrRowsFromTextBoxAsText.ElementAt(i).Equals(',') && !columnsOrRowsFromTextBoxAsText.ElementAt(i).Equals('-'))
                            columnOrRowNumberAsText += columnsOrRowsFromTextBoxAsText.ElementAt(i);
                    }
                    else if (startFromDash)
                    {
                        if (!columnsOrRowsFromTextBoxAsText.ElementAt(i).Equals(','))
                            columnOrRowNumberAsText += columnsOrRowsFromTextBoxAsText.ElementAt(i);
                        else
                        {
                            int lastRangeValue = Int32.Parse(columnOrRowNumberAsText);
                            int firstRangeValue = columnsOfCsv.ElementAt(columnsOfCsv.Count - 1);
                            for (int rangeValue = firstRangeValue + 1; rangeValue < lastRangeValue; rangeValue++)
                            {
                                if (!columnsOfCsv.Contains(rangeValue))
                                    columnsOfCsv.Add(rangeValue);
                            }
                        }
                    }

                    if (columnsOrRowsFromTextBoxAsText.ElementAt(i).Equals(',') || i == columnsOrRowsFromTextBoxAsText.Length - 1)
                    {
                        if (columnOrRowNumberAsText != "" && !columnsOfCsv.Contains(Int32.Parse(columnOrRowNumberAsText)))
                            columnsOfCsv.Add(Int32.Parse(columnOrRowNumberAsText));
                        startFromComma = true;
                        startFromDash = false;
                        columnOrRowNumberAsText = "";
                    }
                    else if (columnsOrRowsFromTextBoxAsText.ElementAt(i).Equals('-') || i == columnsOrRowsFromTextBoxAsText.Length - 1)
                    {
                        if (columnOrRowNumberAsText != "" && !columnsOfCsv.Contains(Int32.Parse(columnOrRowNumberAsText)))
                            columnsOfCsv.Add(Int32.Parse(columnOrRowNumberAsText));
                        startFromDash = true;
                        startFromComma = false;
                        columnOrRowNumberAsText = "";
                    }
                }
                columnsOfCsv.Sort();
                return columnsOfCsv;
            }
            else
            {
                return null;
            }
        }

        private void databaseNameTextBox_TextChanged(object sender, EventArgs e)
        {
            connectionStatusLabel.Text = "No Connection is Present";

            if (insertButton.Enabled)
                insertButton.Enabled = false;
            if (tableNameComboBox.Enabled)
                tableNameComboBox.Enabled = false;
            if (tableNameComboBox.Items.Count > 0)
                tableNameComboBox.Items.Clear();
            tableNameComboBox.Invalidate();

            commandExecutionStatusLabel.Visible = false;
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                isFileOpened = true;
                selectedFileLink.Text = openFileDialog.FileName;
                csvStream = openFileDialog.OpenFile();

                if (!columnsOfCsvTextBox.Enabled)
                    columnsOfCsvTextBox.Enabled = true;

                if (!skipRowsTextBox.Enabled)
                    skipRowsTextBox.Enabled = true;

                if (!columnsOfTableTextBox.Enabled)
                    columnsOfTableTextBox.Enabled = true;

                if (!performStepCheckBox.Enabled)
                    performStepCheckBox.Enabled = true;

                if (!parseDateTimeCheckBox.Enabled)
                    parseDateTimeCheckBox.Enabled = true;

                if (!addUniqueKeyCheckBox.Enabled)
                    addUniqueKeyCheckBox.Enabled = true;

                if (delimiterTextBox.Enabled)
                    delimiterTextBox.Enabled = false;

                if (!insertButton.Enabled && DatabaseConn.CheckConnection() && tableNameComboBox.SelectedItem != null)
                    insertButton.Enabled = true;             

                commandExecutionStatusLabel.Visible = false;
            }
            catch
            {
                isFileOpened = false;

                commandExecutionStatusLabel.Visible = true;
                commandExecutionStatusLabel.ForeColor = Color.Red;
                commandExecutionStatusLabel.Text = "[Error5] Cannot Open File";
            }
        }

        private void clearSelectedFilePictureBox_Click(object sender, EventArgs e)
        {
            isFileOpened = false;
            csvStream.Close();
            csvStream = null;
            selectedFileLink.Text = "No File Selected";
            commandExecutionStatusLabel.Visible = false;
        }

        private void selectedFileLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(selectedFileLink.Text))
                {
                    Process.Start("explorer.exe", " /select, " + selectedFileLink.Text);
                }
            }
            catch
            {
                commandExecutionStatusLabel.Visible = true;
                commandExecutionStatusLabel.ForeColor = Color.Red;
                commandExecutionStatusLabel.Text = "[Error4] Cannot Open File Location with Windows Explorer";
            }
        }

        private void performStepCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (performStepCheckBox.Checked)
            {
                columnsOfCsvTextBox.WaterMark = "ex. 1 or 1,2 (Start Index, Step Index)";
            }
            else
            {
                columnsOfCsvTextBox.WaterMark = "ex. 1,2,3 or 1,2-9";
            }
        }

        private void parseDateTimeCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (parseDateTimeCheckBox.Checked)
                dateTimeTypeCheckBox.Enabled = true;
            else
                dateTimeTypeCheckBox.Enabled = false;
        }

        private void addUniqueKeyCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (addUniqueKeyCheckBox.Checked)
                uniqueIndexPositionTextBox.Enabled = true;
            else
                uniqueIndexPositionTextBox.Enabled = false;
        }
    }
}
