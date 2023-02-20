VLookupTool is a console application written in C# that provides similar functionality to the VLOOKUP function in Microsoft Excel. 
It currently supports CSV files and allows you to browse your local drive to load two files (fileA and fileB) that contain data to be compared.

Using VLookupTool, you can select a column from fileA and a matching column from fileB. 
You can then choose which columns to extract from fileB after the matching is performed. 

VLookupTool loops through every row of fileA and compares the selected column value to each row in fileB for the matching column. 
Finally, the tool exports the result as a CSV file.

In the future, VLookupTool aims to support other data file formats such as JSON, XLSX, and more. 

The tool is ideal for data analysts, researchers, and developers who need to match and extract data from large data sets quickly and easily.
