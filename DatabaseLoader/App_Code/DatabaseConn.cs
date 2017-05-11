using Simple.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DatabaseLoader.App_Code
{
    class DatabaseConn
    {
        private static string connectionString = "data source = localhost; initial catalog = " + "(*)" + "; integrated Security=true; Uid=;pwd=";
        private static DataContext dbDataContext = null;
        private static SqlConnection sqlConnection = null;

        public static string extractedLocationOfExe = "";
        public static string locationOfMap = "";
        public static string locationOfDbml = "";
        public static string locationOfDataContext = "";

        public static bool OpenConnection(String databaseName)
        {
            try
            {
                string namedConnectionString = connectionString.Replace("(*)", databaseName);
                sqlConnection = new SqlConnection(namedConnectionString);
                sqlConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void CloseConnection()
        {
            if (sqlConnection != null)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }

        public static bool CheckConnection()
        {
            if (sqlConnection != null)
                return sqlConnection.State == System.Data.ConnectionState.Open;
            else
                return false;
        }

        public static List<string> GetAllTables()
        {
            List<string> tableNames = new List<string>();
            foreach (DataRow row in sqlConnection.GetSchema("Tables").Rows)
            {
                tableNames.Add((string)row[2]);
            }
            return tableNames;
        }

        public static int InsertIntoTable(String databaseName, String tableName, dynamic[] values, List<int> insertColumnNumbers, bool parseDateTime, bool isDateTimeString, bool addUniqueKey, string uniqueIndexPos)
        {
            try
            {
                DataTable columnsOfSpecifiedTable = sqlConnection.GetSchema("Columns", new string[] { databaseName, null, tableName });

                String commandString = "INSERT INTO " + tableName + "(";
                int columnIndex = 0;

                for (int i = 0; i < columnsOfSpecifiedTable.Rows.Count; i++)
                {
                    if ((insertColumnNumbers != null && (i + 1) == insertColumnNumbers[columnIndex]) || insertColumnNumbers == null)
                    {
                        if (i != columnsOfSpecifiedTable.Rows.Count - 1)
                        {
                            commandString += columnsOfSpecifiedTable.Rows[i].ItemArray[3] + ", ";
                        }
                        else
                        {
                            commandString += columnsOfSpecifiedTable.Rows[i].ItemArray[3] + ")";
                        }
                        columnIndex++;
                    }
                }

                commandString += " VALUES(";

                for (int i = 0; i < columnIndex; i++)
                {
                    if (i != columnIndex - 1)
                    {
                        commandString += "@" + i + ", ";
                    }
                    else
                    {
                        commandString += "@" + i + ")";
                    }
                }

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                int commandParameterCount = 0;
                int valueIndex = 0;
                while (commandParameterCount < columnIndex)
                {
                    string columnDataTypeAsText = (string)columnsOfSpecifiedTable.Rows[commandParameterCount].ItemArray[7];
                    Type columnDataType = SqlColType.GetSqlColumnType(columnDataTypeAsText);

                    if (addUniqueKey && (commandParameterCount + 1).ToString().Equals(uniqueIndexPos))
                    {                      
                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), UniqueIndex.GetUniqueIndex()));
                        UniqueIndex.IncreaseUniqueIndex();
                        commandParameterCount++;
                    }

                    DateTime dateTimeValue;
                    int dateTimeCount = 0;
                    bool parsed = DateTime.TryParse(values[valueIndex].ToString(), out dateTimeValue);
                    if (parseDateTime && parsed)
                    {
                        DataRowCollection dataRowCollection = columnsOfSpecifiedTable.Rows;
                        foreach (DataRow dataRow in dataRowCollection)
                        {
                            string columnName = dataRow.ItemArray[3].ToString();
                            columnDataTypeAsText = (string)columnsOfSpecifiedTable.Rows[dateTimeCount].ItemArray[7];
                            columnDataType = SqlColType.GetSqlColumnType(columnDataTypeAsText);

                            if (columnName.ToLower().Contains("day"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Day < 10 ? "0" + dateTimeValue.Day : dateTimeValue.Day.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Day < 10 ? "0" + dateTimeValue.Day : dateTimeValue.Day.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Day)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Day));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("month"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Month < 10 ? "0" + dateTimeValue.Month : dateTimeValue.Month.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Month < 10 ? "0" + dateTimeValue.Month : dateTimeValue.Month.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Month)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Month));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("year"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Year < 10 ? "0" + dateTimeValue.Year : dateTimeValue.Year.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Year < 10 ? "0" + dateTimeValue.Year : dateTimeValue.Year.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Year)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Year));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("hour"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Hour < 10 ? "0" + dateTimeValue.Hour : dateTimeValue.Hour.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Hour < 10 ? "0" + dateTimeValue.Hour : dateTimeValue.Hour.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Hour)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Hour));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("minute"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Minute < 10 ? "0" + dateTimeValue.Minute : dateTimeValue.Minute.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Minute < 10 ? "0" + dateTimeValue.Minute : dateTimeValue.Minute.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Minute)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Minute));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("second"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Second < 10 ? "0" + dateTimeValue.Second : dateTimeValue.Second.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Second < 10 ? "0" + dateTimeValue.Second : dateTimeValue.Second.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Second)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Second));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("millisecond"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Millisecond < 10 ? "0" + dateTimeValue.Millisecond : dateTimeValue.Millisecond.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Millisecond < 10 ? "0" + dateTimeValue.Millisecond : dateTimeValue.Millisecond.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Millisecond)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.Millisecond));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("dayofyear"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.DayOfYear < 10 ? "0" + dateTimeValue.DayOfYear : dateTimeValue.DayOfYear.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.DayOfYear < 10 ? "0" + dateTimeValue.DayOfYear : dateTimeValue.DayOfYear.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.DayOfYear)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), dateTimeValue.DayOfYear));
                                }
                                commandParameterCount++;
                            }
                            else if (columnName.ToLower().Contains("dayofweek"))
                            {
                                if (isDateTimeString)
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), (int)dateTimeValue.DayOfWeek < 10 ? "0" + dateTimeValue.DayOfWeek : dateTimeValue.DayOfWeek.ToString())))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), (int)dateTimeValue.DayOfWeek < 10 ? "0" + dateTimeValue.DayOfWeek : dateTimeValue.DayOfWeek.ToString()));
                                }
                                else
                                {
                                    if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), (int)dateTimeValue.DayOfWeek)))
                                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), (int)dateTimeValue.DayOfWeek));
                                }
                                commandParameterCount++;
                            }

                            dateTimeCount++;
                        }
                        valueIndex++;
                    }
                    else if (!sqlCommand.Parameters.Contains(new SqlParameter(commandParameterCount.ToString(), values[commandParameterCount])))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(commandParameterCount.ToString(), Convert.ChangeType(values[commandParameterCount], columnDataType)));
                        commandParameterCount++;
                        valueIndex++;
                    }
                }

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                return rowsAffected;
            }
            catch
            {
                return 0;
            }
        }

        public static DataContext CreateDatabaseModel(String databaseName)
        {
            string namedConnectionString = connectionString.Replace("(*)", databaseName);
            string winDirDrive = "";
            dbDataContext = null;

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream sqlMetalResourceStream = assembly.GetManifestResourceStream("DatabaseLoader.SqlMetal.exe"))
                {
                    if (sqlMetalResourceStream != null)
                    {
                        byte[] byteArray = new byte[sqlMetalResourceStream.Length];
                        sqlMetalResourceStream.Read(byteArray, 0, byteArray.Length);

                        winDirDrive = Path.GetPathRoot(Environment.SystemDirectory);
                        extractedLocationOfExe = Path.GetTempPath() + "SqlMetal.exe";

                        if (!File.Exists(extractedLocationOfExe))
                        {
                            using (FileStream sqlMetalExeStream = new FileStream(extractedLocationOfExe, FileMode.Append))
                            {
                                sqlMetalExeStream.Write(byteArray, 0, byteArray.Length);
                            }
                        }

                        locationOfDbml = Path.GetTempPath() + "Dynamic" + databaseName + ".dbml";
                        string parameters = " /server:localhost" + " /database:" + databaseName + " /dbml:\"" + locationOfDbml + "\"" + " /pluralize";

                        using (Process sqlMetalProcess = Process.Start(extractedLocationOfExe, parameters))
                        {
                            sqlMetalProcess.WaitForExit();
                        }

                        if (File.Exists(locationOfDbml))
                        {
                            locationOfMap = Path.GetTempPath() + databaseName + "MappingSource.map";
                            locationOfDataContext = Path.GetTempPath() + "Dynamic" + databaseName + "DataContext.cs";

                            string parameters2 = " /map:\"" + locationOfMap + "\"" + " \"" + locationOfDbml + "\"" + " /code:\"" + locationOfDataContext + "\"";

                            using (Process sqlMetalProcess = Process.Start(extractedLocationOfExe, parameters2))
                            {
                                sqlMetalProcess.WaitForExit();
                            }

                            if (locationOfMap != "")
                            {
                                MappingSource mappingSource = XmlMappingSource.FromReader(XmlReader.Create(locationOfMap));
                                if (mappingSource != null)
                                {
                                    dbDataContext = new DataContext(namedConnectionString, mappingSource);
                                }
                                else
                                    dbDataContext = new DataContext(namedConnectionString);
                            }
                            else
                                dbDataContext = new DataContext(namedConnectionString);
                        }
                    }
                }
            }
            catch
            {
                //MessageBox.Show("An Error Occurred");
            }

            return dbDataContext;
        }

        public static DataContext GetDbInstance(String databaseName)
        {
            if (dbDataContext == null)
                dbDataContext = CreateDatabaseModel(databaseName);

            return dbDataContext;
        }
    }
}
