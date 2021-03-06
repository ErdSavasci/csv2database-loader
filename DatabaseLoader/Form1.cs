﻿using DatabaseLoader.App_Code;
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
            uniqueIndexStartValueTextBox.StyleManager = metroStyleManager;
            uniqueIndexPositionLabel.StyleManager = metroStyleManager;
            uniqueIndexStartValueLabel.StyleManager = metroStyleManager;
        }
        private void ArrangeComponents()
        {
            connectionStatusLabel.ForeColor = Color.Red;
            connectionStatusLabel.Text = "No Connection is Present";
            commandExecutionStatusLabel.Visible = false;
            tableNameComboBox.Enabled = false;
            insertButton.Enabled = false;
            loadingCircle.Visible = false;
            loadingStatusLabel.ForeColor = Color.Green;
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
            dateTimeTypeCheckBox.Enabled = false;
            uniqueIndexStartValueTextBox.Enabled = false;
            selectRowIndexesCheckBox.Enabled = false;
            differentValuesCheckBox.Enabled = false;
            addWhenDifferentCheckBox.Enabled = false;
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
                                {
                                    tableNameComboBox.Enabled = true;                                    
                                }
                                if (tableNameComboBox.Items.Count > 0)
                                    tableNameComboBox.Items.Clear();
                                tableNameComboBox.Items.AddRange(DatabaseConn.GetAllTables().ToArray());
                                tableNameComboBox.Invalidate();
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
                                if (!skipRowsTextBox.Enabled && isFileOpened)
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
                                if (!dateTimeTypeCheckBox.Enabled && isFileOpened && parseDateTimeCheckBox.Checked)
                                    dateTimeTypeCheckBox.Enabled = true;
                            };
                            dateTimeTypeCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!addWhenDifferentCheckBox.Enabled && isFileOpened && addUniqueKeyCheckBox.Checked)
                                    addWhenDifferentCheckBox.Enabled = true;
                            };
                            addWhenDifferentCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!delimiterTextBox.Enabled && isFileOpened)
                                    delimiterTextBox.Enabled = true;
                            };
                            delimiterTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!uniqueIndexStartValueTextBox.Enabled && isFileOpened && addUniqueKeyCheckBox.Checked)
                                    uniqueIndexStartValueTextBox.Enabled = true;
                            };
                            uniqueIndexStartValueTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!selectRowIndexesCheckBox.Enabled && isFileOpened)
                                    selectRowIndexesCheckBox.Enabled = true;
                            };
                            selectRowIndexesCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (!differentValuesCheckBox.Enabled && isFileOpened)
                                    differentValuesCheckBox.Enabled = true;
                            };
                            differentValuesCheckBox.Invoke(invokeAction);
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
                                {
                                    tableNameComboBox.Enabled = false;                                    
                                }
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

                            invokeAction = () =>
                            {
                                if (uniqueIndexStartValueTextBox.Enabled)
                                    uniqueIndexStartValueTextBox.Enabled = false;
                            };
                            uniqueIndexStartValueTextBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (selectRowIndexesCheckBox.Enabled)
                                    selectRowIndexesCheckBox.Enabled = false;
                            };
                            selectRowIndexesCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (differentValuesCheckBox.Enabled)
                                    differentValuesCheckBox.Enabled = true;
                            };
                            differentValuesCheckBox.Invoke(invokeAction);

                            invokeAction = () =>
                            {
                                if (addWhenDifferentCheckBox.Enabled)
                                    addWhenDifferentCheckBox.Enabled = false;
                            };
                            addWhenDifferentCheckBox.Invoke(invokeAction);
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
                            if (delimiterTextBox.Enabled && isFileOpened)
                                delimiterTextBox.Enabled = false;
                        };
                        delimiterTextBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (uniqueIndexStartValueTextBox.Enabled)
                                uniqueIndexStartValueTextBox.Enabled = false;
                        };
                        uniqueIndexStartValueTextBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (selectRowIndexesCheckBox.Enabled)
                                selectRowIndexesCheckBox.Enabled = false;
                        };
                        selectRowIndexesCheckBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (differentValuesCheckBox.Enabled)
                                differentValuesCheckBox.Enabled = false;
                        };
                        differentValuesCheckBox.Invoke(invokeAction);

                        invokeAction = () =>
                        {
                            if (addWhenDifferentCheckBox.Enabled)
                                addWhenDifferentCheckBox.Enabled = false;
                        };
                        addWhenDifferentCheckBox.Invoke(invokeAction);
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
                UniqueIndex.SetBeginningValue(!string.IsNullOrEmpty(uniqueIndexStartValueTextBox.Text) && !string.IsNullOrWhiteSpace(uniqueIndexStartValueTextBox.Text) ? long.Parse(uniqueIndexStartValueTextBox.Text) : 1);
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

                        //GATHERING DATA FROM CSV FILE
                        dynamic[] values = null;
                        List<dynamic[]> valuesSame = null;
                        bool executeOnce = true;
                        bool rowNotSkipped;
                        bool isDelimeterEntered = false;

                        int columnIndex = 0;
                        int rowIndex = 1;
                        int valueIndex = 0;
                        int stepCount = -1;
                        int lastRange = -1;
                        int firstRange = -1;

                        if (readCsvThread == null || !readCsvThread.IsAlive)
                        {
                            readCsvThread = new Thread(() =>
                            {
                                Action invokeAction = () =>
                                {
                                    loadingCircle.Active = true;
                                    loadingCircle.Visible = true;
                                };
                                loadingCircle.Invoke(invokeAction);

                                invokeAction = () =>
                                {
                                    loadingStatusLabel.Visible = true;
                                    loadingStatusLabel.Text = "Inserting...";
                                };
                                loadingStatusLabel.Invoke(invokeAction);

                                invokeAction = () => insertButton.Enabled = false;
                                insertButton.Invoke(invokeAction);

                                invokeAction = () => databaseNameTextBox.Enabled = false;
                                databaseNameTextBox.Invoke(invokeAction);

                                invokeAction = () => tableNameComboBox.Enabled = false;
                                tableNameComboBox.Invoke(invokeAction);

                                invokeAction = () => connectButton.Enabled = false;
                                connectButton.Invoke(invokeAction);

                                if (csvStream != null)
                                {
                                    columnsOfCsv = ParseColumnOrRowIndexes(columnsOfCsvTextBox.Text, false);
                                    skipRowsOfCsv = ParseColumnOrRowIndexes(skipRowsTextBox.Text, true);
                                    columnsOfTable = ParseColumnOrRowIndexes(columnsOfTableTextBox.Text, true);

                                    if (performStepCheckBox.Checked && skipRowsOfCsv != null)
                                    {
                                        stepCount = skipRowsOfCsv[0];
                                    }

                                    string csvRow = "";
                                    valuesSame = new List<dynamic[]>();

                                    using (StreamReader streamReader = new StreamReader(csvStream))
                                    {
                                        int lineCount = File.ReadAllLines(selectedFileLink.Text).Count();
                                        if (skipRowsOfCsv != null)
                                        {
                                            if (!selectRowIndexesCheckBox.Checked)
                                                lineCount -= skipRowsOfCsv.Count;
                                        }

                                        invokeAction = () => toolStripProgressBar.Maximum = lineCount;
                                        statusStrip.Invoke(invokeAction);

                                        bool rowFound;
                                        bool containsSameValue;
                                        while ((csvRow = streamReader.ReadLine()) != null)
                                        {
                                            containsSameValue = false;
                                            rowNotSkipped = true;
                                            rowFound = false;

                                            if (skipRowsOfCsv != null)
                                            {
                                                for (int i = 0; i < skipRowsOfCsv.Count; i++)
                                                {
                                                    if (!rowFound)
                                                    {
                                                        if (performStepCheckBox.Checked)
                                                        {
                                                            if ((skipRowsOfCsv.Count > 1 && rowIndex == stepCount) || (skipRowsOfCsv.Count == 1 && rowIndex >= stepCount))
                                                            {
                                                                rowFound = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (rowIndex == skipRowsOfCsv.ElementAt(i))
                                                            {
                                                                rowFound = true;
                                                            }
                                                            else if ((rowIndex == skipRowsOfCsv.ElementAt(i) * -1) || (lastRange != -1 && rowIndex <= lastRange))
                                                            {
                                                                if (lastRange == -1)
                                                                    lastRange = skipRowsOfCsv.ElementAt(i + 1) * -1;
                                                                else if (rowIndex == lastRange)
                                                                    lastRange = -1;

                                                                rowFound = true;
                                                            }
                                                        }
                                                    }

                                                    if (rowFound)
                                                    {
                                                        if (selectRowIndexesCheckBox.Checked)
                                                            rowNotSkipped = true;
                                                        else
                                                            rowNotSkipped = false;
                                                    }
                                                    else
                                                    {
                                                        if (selectRowIndexesCheckBox.Checked)
                                                            rowNotSkipped = false;
                                                        else
                                                            rowNotSkipped = true;
                                                    }
                                                }
                                            }

                                            if (rowNotSkipped)
                                            {
                                                //Console.WriteLine("Row: " + rowIndex);

                                                int totalColumns = 0;

                                                if (Regex.IsMatch(delimiterTextBox.Text, @"^(\,|\;|\'|\.|\&|\%|\+|\-|\^|\*|\|)$"))
                                                {
                                                    isDelimeterEntered = true;
                                                    totalColumns = csvRow.Count(f => f.Equals(delimiterTextBox.Text.ElementAt(0)));
                                                    totalColumns++;
                                                }
                                                else
                                                {
                                                    if (csvRow.Contains(","))
                                                    {
                                                        totalColumns = csvRow.Count(f => f.Equals(','));
                                                        totalColumns++;
                                                    }
                                                    else if (csvRow.Contains(";"))
                                                    {
                                                        totalColumns = csvRow.Count(f => f.Equals(';'));
                                                        totalColumns++;
                                                    }
                                                }

                                                if (totalColumns > 0)
                                                {
                                                    executeOnce = false;

                                                    if (columnsOfCsv != null)
                                                    {
                                                        int columnsOfCsvCount = 0;
                                                        for (int i = 0; i < columnsOfCsv.Count; i++)
                                                        {
                                                            if (columnsOfCsv.ElementAt(i) > 0)
                                                                columnsOfCsvCount++;
                                                            else
                                                            {
                                                                if (firstRange == -1 && lastRange == -1)
                                                                {
                                                                    firstRange = columnsOfCsv.ElementAt(i) * -1;
                                                                    lastRange = columnsOfCsv.ElementAt(i + 1) * -1;
                                                                }
                                                                else
                                                                {
                                                                    firstRange = -1;
                                                                    lastRange = -1;
                                                                }

                                                                for (int j = firstRange; j <= lastRange; j++)
                                                                {
                                                                    columnsOfCsvCount++;
                                                                }
                                                            }
                                                        }

                                                        values = new object[columnsOfCsvCount];
                                                    }
                                                    else
                                                    {
                                                        values = new object[totalColumns];
                                                    }
                                                }

                                                lastRange = -1;
                                                string csvRowTemp = csvRow;
                                                for (int i = 0; i < columnsOfCsv.Count; i++)
                                                {
                                                    if (values != null && valueIndex < values.Length)
                                                    {
                                                        if (columnsOfCsv.ElementAt(i) > 0)
                                                        {
                                                            values[valueIndex] = GetElementFromCsvRow(columnsOfCsv.ElementAt(i), totalColumns, csvRow, isDelimeterEntered);
                                                            valueIndex++;
                                                        }
                                                        else
                                                        {
                                                            if (firstRange == -1 && lastRange == -1)
                                                            {
                                                                firstRange = columnsOfCsv.ElementAt(i) * -1;
                                                                lastRange = columnsOfCsv.ElementAt(i + 1) * -1;
                                                            }
                                                            else
                                                            {
                                                                firstRange = -1;
                                                                lastRange = -1;
                                                            }                                                          

                                                            for (int j = firstRange; j <= lastRange; j++)
                                                            {
                                                                if(valueIndex < values.Length)
                                                                {
                                                                    values[valueIndex] = GetElementFromCsvRow(firstRange, totalColumns, csvRow, isDelimeterEntered);
                                                                    valueIndex++;
                                                                }
                                                            }
                                                        }                                                
                                                    }
                                                }

                                                for (int i = 0; i < valuesSame.Count(); i++)
                                                {
                                                    for (int j = 0; j < values.Count(); j++)
                                                    {
                                                        if (valuesSame.ElementAt(i)[j] == values[j])
                                                            containsSameValue = true;
                                                    }
                                                }
                                                
                                                if((differentValuesCheckBox.Checked || addWhenDifferentCheckBox.Checked) && !containsSameValue)
                                                    valuesSame.Add(values);

                                                //INSERT THE VALUES FROM ROW INTO SELECTED TABLE
                                                if (DatabaseConn.CheckConnection() && values != null && values.Count() > 0)
                                                {
                                                    if (!differentValuesCheckBox.Checked || (differentValuesCheckBox.Checked && !containsSameValue) || (addWhenDifferentCheckBox.Checked && !differentValuesCheckBox.Checked))
                                                    {
                                                        rowsAffected = DatabaseConn.InsertIntoTable(databaseName, tableName, values, columnsOfTable, parseDateTimeCheckBox.Checked, !dateTimeTypeCheckBox.Checked, addUniqueKeyCheckBox.Checked, addUniqueKeyCheckBox.Checked ? (!string.IsNullOrEmpty(uniqueIndexPositionTextBox.Text) && !string.IsNullOrWhiteSpace(uniqueIndexPositionTextBox.Text) ? uniqueIndexPositionTextBox.Text : "1") : "-1", addWhenDifferentCheckBox.Checked, containsSameValue);
                                                        invokeAction = () => toolStripProgressBar.PerformStep();
                                                        statusStrip.Invoke(invokeAction);
                                                    }
                                                }
                                            }

                                            valueIndex = 0;
                                            columnIndex = 0;
                                            rowIndex++;

                                            if (stepCount > 0 && skipRowsOfCsv.Count > 1 && performStepCheckBox.Checked)
                                                stepCount += skipRowsOfCsv[1];
                                        }

                                        if (rowsAffected != -1)
                                        {
                                            invokeAction = () =>
                                            {
                                                commandExecutionStatusLabel.Visible = true;
                                                commandExecutionStatusLabel.ForeColor = Color.Chartreuse;
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

                                        invokeAction = () => databaseNameTextBox.Enabled = true;
                                        databaseNameTextBox.Invoke(invokeAction);

                                        invokeAction = () => connectButton.Enabled = true;
                                        connectButton.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (insertButton.Enabled)
                                                insertButton.Enabled = false;
                                        };
                                        insertButton.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (tableNameComboBox.Enabled)
                                            {
                                                tableNameComboBox.Enabled = false;                                               
                                            }
                                            if (tableNameComboBox.Items.Count > 0)
                                                tableNameComboBox.Items.Clear();
                                            tableNameComboBox.Invalidate();
                                        };
                                        tableNameComboBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (columnsOfCsvLabel.Enabled)
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

                                        invokeAction = () =>
                                        {
                                            if (uniqueIndexStartValueTextBox.Enabled)
                                                uniqueIndexStartValueTextBox.Enabled = false;
                                        };
                                        uniqueIndexStartValueTextBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (selectRowIndexesCheckBox.Enabled)
                                                selectRowIndexesCheckBox.Enabled = false;
                                        };
                                        selectRowIndexesCheckBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            loadingCircle.Active = false;
                                            loadingCircle.Visible = false;
                                        };
                                        loadingCircle.Invoke(invokeAction);

                                        invokeAction = () => loadingStatusLabel.Visible = false;
                                        loadingStatusLabel.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (differentValuesCheckBox.Enabled)
                                                differentValuesCheckBox.Enabled = false;
                                        };
                                        differentValuesCheckBox.Invoke(invokeAction);

                                        invokeAction = () =>
                                        {
                                            if (addWhenDifferentCheckBox.Enabled)
                                                addWhenDifferentCheckBox.Enabled = false;
                                        };
                                        addWhenDifferentCheckBox.Invoke(invokeAction);
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

                        databaseNameTextBox.Enabled = true;
                        connectButton.Enabled = true;

                        if (insertButton.Enabled)
                            insertButton.Enabled = false;

                        if (tableNameComboBox.Enabled)
                        {
                            tableNameComboBox.Enabled = false;
                            tableNameComboBox.Invalidate();
                        }
                        if (tableNameComboBox.Items.Count > 0)
                            tableNameComboBox.Items.Clear();                          

                        connectButton.Enabled = true;

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

                        if (uniqueIndexStartValueTextBox.Enabled)
                            uniqueIndexStartValueTextBox.Enabled = false;

                        if (selectRowIndexesCheckBox.Enabled)
                            selectRowIndexesCheckBox.Enabled = false;

                        loadingCircle.Active = false;
                        loadingCircle.Visible = false;

                        loadingStatusLabel.Visible = false;

                        if (differentValuesCheckBox.Enabled)
                            differentValuesCheckBox.Enabled = false;

                        if (addWhenDifferentCheckBox.Enabled)
                            addWhenDifferentCheckBox.Enabled = false;
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

                    databaseNameTextBox.Enabled = true;
                    connectButton.Enabled = true;

                    if (insertButton.Enabled)
                        insertButton.Enabled = false;

                    if (tableNameComboBox.Enabled)
                    {
                        tableNameComboBox.Enabled = false;                       
                    }
                    if (tableNameComboBox.Items.Count > 0)
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

                    if (uniqueIndexStartValueTextBox.Enabled)
                        uniqueIndexStartValueTextBox.Enabled = false;

                    if (selectRowIndexesCheckBox.Enabled)
                        selectRowIndexesCheckBox.Enabled = false;

                    loadingCircle.Active = false;
                    loadingCircle.Visible = false;

                    loadingStatusLabel.Visible = false;

                    if (differentValuesCheckBox.Enabled)
                        differentValuesCheckBox.Enabled = false;

                    if (addWhenDifferentCheckBox.Enabled)
                        addWhenDifferentCheckBox.Enabled = false;
                }
            }
            catch
            {
                //MessageBox.Show(ex.StackTrace);

                commandExecutionStatusLabel.Visible = true;
                commandExecutionStatusLabel.ForeColor = Color.Red;
                commandExecutionStatusLabel.Text = "[Error2] An Error Occurred During Insertion Process";

                if (DatabaseConn.CheckConnection())
                    DatabaseConn.CloseConnection();

                connectionStatusLabel.ForeColor = Color.Red;
                connectionStatusLabel.Text = "Connection Closed";

                databaseNameTextBox.Enabled = true;
                connectButton.Enabled = true;

                if (insertButton.Enabled)
                    insertButton.Enabled = false;

                if (tableNameComboBox.Enabled)
                {
                    tableNameComboBox.Enabled = false;                   
                }
                if (tableNameComboBox.Items.Count > 0)
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

                if (uniqueIndexStartValueTextBox.Enabled)
                    uniqueIndexStartValueTextBox.Enabled = false;

                if (selectRowIndexesCheckBox.Enabled)
                    selectRowIndexesCheckBox.Enabled = false;

                loadingCircle.Active = false;
                loadingCircle.Visible = false;

                loadingStatusLabel.Visible = false;

                if (differentValuesCheckBox.Enabled)
                    differentValuesCheckBox.Enabled = false;

                if (addWhenDifferentCheckBox.Enabled)
                    addWhenDifferentCheckBox.Enabled = false;
            }
        }

        private string GetElementFromCsvRow(int pos, int totalColumns, string csvRowTemp, bool isDelimeterEntered)
        {
            for (int i = 1; i < pos; i++)
            {
                //GO TO NEXT ELEMENT
                if (csvRowTemp.StartsWith("\""))
                {
                    csvRowTemp = csvRowTemp.Substring(1);
                    csvRowTemp = csvRowTemp.Substring(csvRowTemp.IndexOf("\"") + 2);
                }
                else
                {
                    if (isDelimeterEntered)
                        csvRowTemp = csvRowTemp.Substring(csvRowTemp.IndexOf(delimiterTextBox.Text.ElementAt(0)) + 1);
                    else
                    {
                        if (csvRowTemp.Contains(","))
                            csvRowTemp = csvRowTemp.Substring(csvRowTemp.IndexOf(",") + 1);
                        else if (csvRowTemp.Contains(";"))
                            csvRowTemp = csvRowTemp.Substring(csvRowTemp.IndexOf(";") + 1);
                    }
                }
            }

            if ((!isDelimeterEntered && !csvRowTemp.StartsWith(",") && !csvRowTemp.StartsWith(";")) || (isDelimeterEntered && !csvRowTemp.StartsWith(delimiterTextBox.Text.ElementAt(0).ToString())))
            {
                if (csvRowTemp.StartsWith("\""))
                {
                    csvRowTemp = csvRowTemp.Substring(1);
                    return csvRowTemp.Substring(0, csvRowTemp.IndexOf("\""));
                }
                else
                {
                    if (isDelimeterEntered)
                        return csvRowTemp.Substring(0, csvRowTemp.IndexOf(delimiterTextBox.Text.ElementAt(0))).Replace("\"", "");
                    else
                    {
                        if (csvRowTemp.Contains(","))
                            return csvRowTemp.Substring(0, csvRowTemp.IndexOf(",")).Replace("\"", "");
                        else
                            return csvRowTemp.Substring(0, csvRowTemp.IndexOf(";")).Replace("\"", "");
                    }
                }
            }
            else
                return null;
        }

        private List<int> ParseColumnOrRowIndexes(string columnsOrRowsFromTextBoxAsText, bool sort)
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

                        if (columnsOrRowsFromTextBoxAsText.ElementAt(i).Equals(',') || i == columnsOrRowsFromTextBoxAsText.Length - 1)
                        {
                            int lastRangeValue = Int32.Parse(columnOrRowNumberAsText);
                            int firstRangeValue = columnsOfCsv.ElementAt(columnsOfCsv.Count - 1);

                            columnsOfCsv.RemoveAt(columnsOfCsv.Count - 1);
                            columnsOfCsv.Add(firstRangeValue * -1);

                            /*for (int rangeValue = firstRangeValue + 1; rangeValue < lastRangeValue; rangeValue++)
                            {
                                if (!columnsOfCsv.Contains(rangeValue))
                                    columnsOfCsv.Add(rangeValue);
                            }*/
                        }
                    }

                    if (columnsOrRowsFromTextBoxAsText.ElementAt(i).Equals(',') || i == columnsOrRowsFromTextBoxAsText.Length - 1)
                    {
                        if (columnOrRowNumberAsText != "" && !columnsOfCsv.Contains(Int32.Parse(columnOrRowNumberAsText)))
                        {
                            if (startFromDash)
                                columnsOfCsv.Add(Int32.Parse(columnOrRowNumberAsText) * -1);
                            else
                                columnsOfCsv.Add(Int32.Parse(columnOrRowNumberAsText));
                        }

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

                if(sort)
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
            {
                tableNameComboBox.Enabled = false;                
            }
            if (tableNameComboBox.Items.Count > 0)
                tableNameComboBox.Items.Clear();
            tableNameComboBox.Invalidate();

            commandExecutionStatusLabel.Visible = false;

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

            if (uniqueIndexStartValueTextBox.Enabled)
                uniqueIndexStartValueTextBox.Enabled = false;

            if (selectRowIndexesCheckBox.Enabled)
                selectRowIndexesCheckBox.Enabled = false;

            if (differentValuesCheckBox.Enabled)
                differentValuesCheckBox.Enabled = false;

            if (addWhenDifferentCheckBox.Enabled)
                addWhenDifferentCheckBox.Enabled = false;
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

                if (!delimiterTextBox.Enabled)
                    delimiterTextBox.Enabled = true;

                if (!insertButton.Enabled && DatabaseConn.CheckConnection() && tableNameComboBox.SelectedItem != null)
                    insertButton.Enabled = true;

                if (!selectRowIndexesCheckBox.Enabled)
                    selectRowIndexesCheckBox.Enabled = true;

                if (!differentValuesCheckBox.Enabled)
                    differentValuesCheckBox.Enabled = true;

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
                skipRowsTextBox.WaterMark = "ex. 1 or 1,2 (Start Index, Step Index)";
            else
                skipRowsTextBox.WaterMark = "ex. 1,2,3 or 1,2-9";
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
            {
                uniqueIndexPositionTextBox.Enabled = true;
                uniqueIndexStartValueTextBox.Enabled = true;
                addWhenDifferentCheckBox.Enabled = true;
            }
            else
            {
                uniqueIndexPositionTextBox.Enabled = false;
                uniqueIndexStartValueTextBox.Enabled = false;
                addWhenDifferentCheckBox.Enabled = false;
            }
        }

        private void selectRowIndexesCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (selectRowIndexesCheckBox.Checked)
                skipRowLabel.Text = "Select Rows";
            else
                skipRowLabel.Text = "Skip Rows";
        }
    }
}
