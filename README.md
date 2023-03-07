# VLookupTool 

A console application written in C# that provides similar functionality to the VLOOKUP function in Microsoft Excel. 

The tool is ideal for data analysts, researchers, and developers who need to match and extract data from large data sets quickly and easily.

# Usage

To use the VLookupTool application, follow these steps:

1. Start the application by running the executable file.
2. Use the arrow keys to navigate to the folder where the files are stored. Alternatively, you can select the "current folder" option to use the folder where the application is running.
3. Select the first file (fileA) and the second file (fileB) to be compared.
4. Choose a column from fileA to perform the match on and select the matching column from fileB.
5. After selecting the match columns, you will be prompted to select the columns to extract from fileB appended with vlookup_.
6. Press enter to perform the match and extract the selected columns from fileB.
7. You will be prompted to select a location to save the result file. The filename will include today's date, and if there are already existing files with the same name, a numeric suffix (e.g. (1), (2), etc.) will be added.

Note: VLookupTool currently supports CSV and Excel files.

The repository contains sample files under the _sampleFiles folder.
