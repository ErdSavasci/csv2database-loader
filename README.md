# csv2database-loader

> Software Name: CSV2Database Loader</br>
> Software Version: 1.0.0

### Purpose
This software is firstly intended to use in ISE314 - Data Warehousing and Data Mining course in Atılım University by creating OLTP database system and loading data from CSV files into the database. But its user settings part make this software work in every case and its usage can be really effective  outside of this course. </br>

### Description
The program simply gets an file with .csv extension, parses the data with respect to user settings and load them into database.

### Usage Manual
<img src="/logos_and_screenshots/main.png" width=700/>

#### Delimeter
Enter the separation character between data values

#### Perform Step
Enter the beginning index only to start from this index to the end of rows or enter both the beginning value and step value. Step value is how many data is skipped. </br>(Ex. 1 => Start from 1 to the end) (Ex. 1,2 => Start from 1 and retrieve 3rd, 5th, 7th ... nth columns)

#### Columns of Input Data
Enter the indexes of columns which is needed to be taken.

#### Columns of Output Data
Enter the indexes of columns in database which the data is loaded to these indexes.

#### Skip Row
Enter the indexes of to be skipped rows

#### Add Unique Key
Check the checkbox if an unique key is needed. Enter the beginning value of unique key and its column index in the database.

#### Parse DateTime
Check the checkbox if datetime (ex. yyyy-mmm-dd hh:MM:ss) is parsed into separate values (year, month, day, hour, minute, second etc.)</br>
Note:</br>
Also "Is INT" checkbox makes datetime to be parsed into integer values if the checkbox is checked.
