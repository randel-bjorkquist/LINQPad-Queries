<Query Kind="Statements" />

string input = "Header: Value to extract Footer".Dump("input");
string header = "Header: ".Dump("Header");
string footer = " Footer".Dump("Footer");

int startIndex = input.IndexOf(header) + header.Length;
int endIndex = input.IndexOf(footer, startIndex);

string extractedValue = input.Substring(startIndex, endIndex - startIndex);
extractedValue.Dump("extracted value");