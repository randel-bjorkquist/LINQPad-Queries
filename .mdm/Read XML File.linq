<Query Kind="Program">
  <Reference Relative="data_files\Employee.xml">&lt;MyDocuments&gt;\LINQPad Queries\data_files\Employee.xml</Reference>
</Query>

//---------------------------------------------------------------------------------------------
// resource url: https://www.dotnetcurry.com/linq/564/linq-to-xml-tutorials-examples
//
//This tutorial has been divided into 2 sections:
//  Section 1: Read XML and Traverse the Document using LINQ To XML
//  Section 2: Manipulate XML content and Persist the changes using LINQ To XML
//
//The following namespaces are needed while testing the samples: System; 
//System.Collections.Generic; System.Linq; System.Text; System.Xml; System.Xml.Linq;
//---------------------------------------------------------------------------------------------
void Main()
{
  var current_path    = Path.GetDirectoryName( Util.CurrentQueryPath );
  var data_files_path = Path.Combine(current_path, "data_files");
  var xml_file        = Path.Combine( data_files_path, "Employee.xml" );

  #region Section 1: Read XML and Traverse the XML Document using LINQ To XML

  #region 1. How Do I Read XML using LINQ to XML -----------------------------------------------------
  
  var xelement = XElement.Load(xml_file);
  
  //NOTE: commented out, only to save space in the results pane; uncomment to show results.
  //xelement.Dump("1. How Do I Read XML using LINQ to XML\n    - File \"Employee.xml\" content", 0);
  "The file dump has been commented out on line #23, uncomment to see it ...".Dump("1. How Do I Read XML using LINQ to XML", 0);
  
  #endregion
  
  #region 2. How Do I Access a Single Element using LINQ to XML --------------------------------------
  
  IEnumerable<XElement> employees = xelement.Elements();

  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  //Console.WriteLine("List of all Employee Names :");
  //
  //foreach(var employee in employees)
  //{
  //  Console.WriteLine(employee.Element("Name").Value);
  //}
  //
  #endregion
  #region Lambda Expression -------------------------------------------------
  
  employees.Select(e => e.Element("Name").Value)
           .Dump("2. How Do I Access a Single Element using LINQ to XML \n    - List of all Employee Names", 0);

  #endregion
  #endregion

  #region 3. How Do I Access Multiple Elements using LINQ to XML -------------------------------------
  
  //NOTE: defined above, in step 2.
  //IEnumerable<XElement> employees = xelement.Elements();

  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  //Console.WriteLine("List of all Employee Names along with their ID:");
  //
  //foreach(var employee in employees)
  //{
  //  Console.WriteLine( "{0} has Employee ID {1}"
  //                    ,employee.Element("Name").Value
  //                    ,employee.Element("EmpId").Value );
  //}
  //
  #endregion
  #region Lambda Expression -------------------------------------------------
  
  employees.Select(e => $"{e.Element("Name").Value} has Employee ID {e.Element("EmpId").Value}" )
           .Dump("3. How Do I Access Multiple Elements using LINQ to XML \n    - List of all Employee Names along with their ID", 0);

  #endregion
  #endregion

  #region 4. How Do I Access all Elements having a Specific Attribute using LINQ to XML --------------
  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  ////NOTE: defined above, in step 1
  ////XElement xelement = XElement.Load("..\\..\\Employees.xml");
  //
  //var name = from nm in xelement.Elements("Employee")
  //           where (string)nm.Element("Sex") == "Female"
  //           select nm;
  //
  //Console.WriteLine("Details of Female Employees:");
  //
  //foreach (XElement xEle in name)
  //{
  //  Console.WriteLine(xEle);
  //}
  //
  #endregion
  #region Lambda Expression -------------------------------------------------
  
  xelement.Elements("Employee")
          .Where(element => (string)element.Element("Sex") == "Female")
          .Dump("4. How Do I Access all Elements having a Specific Attribute using LINQ to XML \n    - Details of Female Employees", 0);
  
  #endregion
  #endregion

  #region 5. How Do I access Specific Element having a Specific Attribute using LINQ to XML ----------
  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  //var homePhone = from phoneno in xelement.Elements("Employee")
  //                where (string)phoneno.Element("Phone").Attribute("Type") == "Home"
  //                select phoneno;
  //
  //Console.WriteLine("List HomePhone Nos.");
  //
  //foreach (XElement xEle in homePhone)
  //{
  //  Console.WriteLine(xEle.Element("Phone").Value);
  //}
  //
  #endregion
  #region Lambda Expression -------------------------------------------------
  var home_phone = xelement.Elements("Employee")
                           //IMPORTANT: using the .ToString() method, DOES NOT WORK !!!
//                           .Where(e => (e.Element("Phone").Attribute("Type")).ToString() == "Home")
                           //NOTE: the use of (string) is extremely important; w/o it, nothing is returned !!!
                           .Where(e => (string)e.Element("Phone")
                                                .Attribute("Type") == "Home")
                           .Select(e => e)
                           .ToList()
                           ;
                           
  home_phone.Select(e => e.Element("Phone").Value)
            .Dump("5. How Do I access Specific Element having a Specific Attribute using LINQ to XML \n    - List HomePhone Numbers.", 0);

  #endregion
  #endregion

  #region 6. How Do I Find an Element within another Element using LINQ to XML ------------------------
  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  ////NOTE: defined above, in step 1.
  ////XElement xelement = XElement.Load("..\\..\\Employees.xml");
  //
  //var addresses = from address in xelement.Elements("Employee")
  //                where (string)address.Element("Address").Element("City") == "Alta"
  //                select address;
  //Console.WriteLine("Details of Employees living in Alta City");
  //
  //foreach (XElement xEle in addresses)
  //{
  //  Console.WriteLine(xEle);
  //}
  
  #endregion
  #region Lambda Expression -------------------------------------------------
  xelement.Elements("Employee")
          .Where(e => (string)e.Element("Address")
                               .Element("City") == "Alta")
          .Dump("6. How Do I Find an Element within another Element using LINQ to XML \n    - Details of Employees living in the city of Alta.", 0);
  #endregion
  #endregion

  #region 7. How Do I Find Nested Elements (using Descendants Axis) using LINQ to XML -----------------
  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  ////NOTE: defined above, in step 1.
  ////XElement xelement = XElement.Load("..\\..\\Employees.xml");
  //
  //Console.WriteLine("List of all Zip Codes");
  //
  //foreach (XElement xEle in xelement.Descendants("Zip"))
  //{
  //  Console.WriteLine((string)xEle);
  //}
  //
  #endregion
  #region Lambda Expression -------------------------------------------------
  
  xelement.Descendants("Zip")
          .Select(e => (string)e) //NOTE: converts the element to a string, just the like the 'Query Expression' example ...
          .Dump("7. How Do I Find Nested Elements (using Descendants Axis) using LINQ to XML \n    - List of all Zip Codes", 0);

  #endregion
  #endregion

  #region 8. How do I apply Sorting on Elements using LINQ to XML ------------------------------------
  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  ////NOTE: defined above, in step 1.
  ////XElement xelement = XElement.Load("..\\..\\Employees.xml");
  //
  //IEnumerable<string> codes = from code in xelement.Elements("Employee")
  //                            let zip = (string)code.Element("Address").Element("Zip")
  //                            orderby zip
  //                            select zip;
  //                            
  //Console.WriteLine("List and Sort all Zip Codes");
  //
  //foreach (string zp in codes)
  //{
  //  Console.WriteLine(zp);
  //}
  //
  #endregion
  #region Lambda Expression -------------------------------------------------
  #endregion
  
  xelement.Elements("Employee")
          .Select(e => (string)e.Element("Address").Element("Zip"))
          .OrderBy(e => e)
          .Dump("8. How do I apply Sorting on Elements using LINQ to XML \n    - Sorted list of all Zip Codes", 0);

  #endregion

  #endregion

  #region Section 2: Manipulate XML content and Persist the changes using LINQ To XML

  XNamespace empNM = "urn:lst-emp:emp";
  XDocument xDoc   = new XDocument( new XDeclaration("1.0", "UTF-16", null)
                                   ,new XElement( empNM + "Employees"
                                                 ,new XElement( "Employee"
                                                               ,new XComment( "Only 3 elements for demo purposes" )
                                                               ,new XElement( "EmpId", "5"    )
                                                               ,new XElement( "Name", "Kimmy" )
                                                               ,new XElement( "Sex", "Female" ) ) ) );
                   
  StringWriter sw  = new StringWriter();

  #region 9. Create an XML Document with Xml Declaration/Namespace/Comments using LINQ to XML --------
  
  //Description: 
  //  When you need to create an XML document containing XML declaration, XML Document Type (DTD) and 
  //  processing instructions, Comments, Namespaces, you should go in for the XDocument class.
  
  //NOTE: defined at the top of section 2.
  //XNamespace empNM = "urn:lst-emp:emp";
  //XDocument xDoc   = new XDocument( new XDeclaration("1.0", "UTF-16", null)
  //                                 ,new XElement( empNM + "Employees"
  //                                               ,new XElement( "Employee"
  //                                                             ,new XComment( "Only 3 elements for demo purposes" )
  //                                                             ,new XElement( "EmpId", "5"    )
  //                                                             ,new XElement( "Name", "Kimmy" )
  //                                                             ,new XElement( "Sex", "Female" ) ) ) );
  //
  //StringWriter sw = new StringWriter();
  xDoc.Save(sw);
  
  //Console.WriteLine(sw);  
  sw.Dump("9. Create an XML Document with Xml Declaration/Namespace/Comments using LINQ to XML", 0);

  #endregion

  #region 10. Save the XML Document to a XMLWriter or to the disk using LINQ to XML ------------------
  
  //Description: Use the following code to save the XML to a XMLWriter or to your physical disk
  
  //NOTE: defined at the top of section 2.
  //XNamespace empNM = "urn:lst-emp:emp";
  //XDocument xDoc   = new XDocument( new XDeclaration("1.0", "UTF-16", null)
  //                                 ,new XElement( empNM + "Employees"
  //                                               ,new XElement( "Employee"
  //                                                             ,new XComment( "Only 3 elements for demo purposes" )
  //                                                             ,new XElement( "EmpId", "5"    )
  //                                                             ,new XElement( "Name", "Kimmy" )
  //                                                             ,new XElement( "Sex", "Female" ) ) ) );
  //                 
  //StringWriter sw  = new StringWriter();
  XmlWriter xWrite = XmlWriter.Create(sw);
  
  xDoc.Save(xWrite);
  xWrite.Close();
  
  // Save to Disk
  xDoc.Save(data_files_path + "\\Something.xml");
  
  //Console.WriteLine("Saved");
  "Saved".Dump("10. Save the XML Document to a XMLWriter or to the disk using LINQ to XML", 0);

  #endregion

  #region 11. Load an XML Document using XML Reader using LINQ to XML --------------------------------

  //Description:
  //  Use the following code to load the XML Document into an XML Reader

  //XmlReader xRead = XmlReader.Create(@"..\\..\\Employees.xml");
  XmlReader xRead = XmlReader.Create(xml_file);
  XElement xEle   = XElement.Load(xRead);
  
  //Console.WriteLine(xEle);
  //xEle.Dump("11. Load an XML Document using XML Reader using LINQ to XML \n    - File \"Employee.xml\" content", 0);
  "The file dump has been commented out on line #288, uncomment to see it ...".Dump("11. Load an XML Document using XML Reader using LINQ to XML", 0);

  xRead.Close();

  #endregion

  #region 12. Find Element at a Specific Position using LINQ to XML ----------------------------------

  //DESCRIPTION: Find the 2nd Employee Element

  // Using XElement
  //Console.WriteLine("Using XElement");
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  var emp1 = xEle.Descendants("Employee").ElementAt(1);
  //emp1.Dump("12. Find Element at a Specific Position using LINQ to XML \n    - Using XElement");
  "The file dump has been commented out on line #304, uncomment to see it ...".Dump("12. Find Element at a Specific Position using LINQ to XML \n    - Using XElement", 0);
  
  //Console.WriteLine(emp);
  
  //Console.WriteLine("------------");

  // Using XDocument
  //Console.WriteLine("Using XDocument");
  //XDocument xDoc = XDocument.Load("..\\..\\Employees.xml");
  xDoc = XDocument.Load(xml_file);
  
  var emp2 = xDoc.Descendants("Employee").ElementAt(1);
  //Console.WriteLine(emp1);
  //emp2.Dump("12. Find Element at a Specific Position using LINQ to XML \n    - Using XDocument", 0);
  "The file dump has been commented out on line #318, uncomment to see it ...".Dump("12. Find Element at a Specific Position using LINQ to XML \n    - Using XDocument", 0);
  
  #endregion

  #region 13. List the First 2 Elements using LINQ to XML --------------------------------------------

  //DESCRIPTION: List the details of the first 2 Employees

  xEle.Descendants("Employee")
      .Take(2)
      .Dump("13. List the First 2 Elements using LINQ to XML \n    - Employees", 0);

  #endregion

  #region 14. List the 2nd and 3rd Element using LINQ to XML -----------------------------------------

  //DESCRIPTION: List the 2nd and 3rd Employees

  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  var employees_2_and_3 = xEle.Descendants("Employee").Skip(1).Take(2);
  
  //foreach(var _emp in employees_2_and_3)
  //{
  //  Console.WriteLine(_emp);
  //}
  
  employees_2_and_3.Dump("14. List the 2nd and 3rd Element using LINQ to XML \n    - Employees", 0);

  #endregion

  #region 15. List the Last 2 Elements using LINQ To XML ---------------------------------------------

  //DESCRIPTION: We have been posting the entire elements as output in our previous examples. Let us 
  //             say that you want to display only the Employee Name, use this query:

  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  //var last_2_employees = xEle.Descendants("Employee").Reverse().Take(2);
  //
  //foreach(var employee in last_2_employees)
  //{
  //  Console.WriteLine(employee.Element("EmpId") + "" + employee.Element("Name"));
  //}
  //
  
  xEle.Descendants("Employee")
      .Reverse()  //reverse to "take" the last 2
      .Take(2)
      .Reverse()  //reverse to put them back in the original order
      .Dump("15. List the Last 2 Elements using LINQ To XML \n    - Employees", 0);

  #endregion

  #region 16. Find the Element Count based on a condition using LINQ to XML --------------------------

  //DESCRIPTION: Count the number of Employees living in the state CA

  #region COMMENTED OUT: Query Expression -----------------------------------
  //
  ////NOTE: defined above, in step 1.
  ////XElement xelement = XElement.Load("..\\..\\Employees.xml");
  //
  //var stCnt = from address in xelement.Elements("Employee")
  //            where (string)address.Element("Address").Element("State") == "CA"
  //            select address;
  //
  //Console.WriteLine("Number of Employees living in CA State are {0}", stCnt.Count());
  //
  #endregion
  #region Lambda Expression -------------------------------------------------
  
  xelement.Elements("Employee")
          .Count(e => (string)e.Element("Address").Element("State") == "CA")
          .Dump("16. Find the Element Count based on a condition using LINQ to XML \n    - Number of Employees living in CA State", 0);

  #endregion
  #endregion

  #region 17. Add a new Element at runtime using LINQ to XML -----------------------------------------

  //DESCRIPTION: You can add a new Element to an XML document at runtime by using the Add() method of
  //             XElement. The new Element gets added as the last element of the XML document.
  
  //NOTE: defined above, in step 1.
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  xEle.Add(new XElement( "Employee"
                        ,new XElement( "EmpId", 5 )
                        ,new XElement( "Name", "George" ) ) );

  //Console.WriteLine(xEle);
  //xEle.Dump("17. Add a new Element at runtime using LINQ to XML \n    - Employees", 0);
  "The file dump has been commented out on line #408, uncomment to see it ...".Dump("17. Add a new Element at runtime using LINQ to XML \n    - Employees", 0);

  #endregion

  #region 18. Add a new Element as the First Child using LINQ to XML ---------------------------------

  //DESCRIPTION: In the previous example, by default the new Element gets added to the end of the XML
  //             document. If you want to add the Element as the First Child, use the ‘AddFirst()’ method

  //NOTE: defined above, in step 1.
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  xEle.AddFirst( new XElement( "Employee"
                              ,new XElement("EmpId", 6)
                              ,new XElement("Name", "Lisa")));

  //Console.Write(xEle);
  //xEle.Dump("18. Add a new Element as the First Child using LINQ to XML \n    - Employees", 0);
  "The file dump has been commented out on line #425, uncomment to see it ...".Dump("18. Add a new Element as the First Child using LINQ to XML \n    - Employees", 0);

  #endregion

  #region 19. Add an attribute to an Element using LINQ to XML ---------------------------------------

  //DESCRIPTION: To add an attribute to an Element, use the following code:

  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  
  xEle.Add( new XElement( "Employee"
                         ,new XElement("EmpId", 7)
                         ,new XElement("Phone", "423-555-4224", new XAttribute("Type", "Home") ) ) );

  //Console.Write(xEle);
  //xEle.Dump("19. Add an attribute to an Element using LINQ to XML \n    - Employees", 0);
  "The file dump has been commented out on line #441, uncomment to see it ...".Dump("19. Add an attribute to an Element using LINQ to XML \n    - Employees", 0);

  #endregion

  #region 20. Replace Contents of an Element/Elements using LINQ to XML ------------------------------

  //DESCRIPTION: Let us say that in the XML file, you want to change the Country from “USA” to “United
  //             States of America” for all the Elements.
  
  //NOTE: defined above, in step 1.
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  
  //xEle.Dump("20. Replace Contents of an Element/Elements using LINQ to XML \n    - Employees PRE-UPDATE", 0);
  "The file dump has been commented out on line #454, uncomment to see it ...".Dump("20. Replace Contents of an Element/Elements using LINQ to XML \n    - Employees PRE-UPDATE", 0);
  
  var countries = xEle.Elements("Employee")
                      .Elements("Address")
                      .Elements("Country")
                      .ToList();

  foreach(XElement cEle in countries)
  {
    cEle.ReplaceNodes("United States Of America");
  }

  //Console.Write(xEle);
  //xEle.Dump("20. Replace Contents of an Element/Elements using LINQ to XML \n    - Employees POST-UPDATE", 0);
  "The file dump has been commented out on line #468, uncomment to see it ...".Dump("20. Replace Contents of an Element/Elements using LINQ to XML \n    - Employees POST-UPDATE", 0);

  #endregion

  #region 21. Remove an attribute from all the Elements using LINQ to XML ----------------------------

  //DESCRIPTION: Let us say if you want to remove the Type attribute ( <Phone Type=”Home”> ) attribute
  //             for all the elements ...
  //
  //             To remove attribute of one Element based on a condition, traverse to that Element and
  //             SetAttributeValue("Type", null); You can also use SetAttributeValue(XName,object) to 
  //             update an attribute value.

  //NOTE: defined above, in step 1.
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");

  //xEle.Dump("21. Remove an attribute from all the Elements using LINQ to XML \n    - Employees PRE-UPDATE", 0);
  "The file dump has been commented out on line #484, uncomment to see it ...".Dump("21. Remove an attribute from all the Elements using LINQ to XML \n    - Employees PRE-UPDATE", 0);
  
  var phones = xEle.Elements("Employee")
                   .Elements("Phone")
                   .ToList();

  foreach(XElement phone in phones)
  {
    phone.RemoveAttributes(); 
  }

  //Console.Write(xEle);
  //xEle.Dump("21. Remove an attribute from all the Elements using LINQ to XML \n    - Employees POST-UPDATE", 0);
  "The file dump has been commented out on line #498, uncomment to see it ...".Dump("21. Remove an attribute from all the Elements using LINQ to XML \n    - Employees POST-UPDATE", 0);

  #endregion

  #region 22. Delete an Element based on a condition using LINQ to XML -------------------------------
  
  //DESCRIPTION: If you want to delete an entire element based on a condition, here’s how to do it. We
  //             are deleting the entire Address Element
  //
  //             FY - SetElementValue() can also be used to Update the content of an Element.
  
  //NOTE: defined about in step 1.
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  
  //xEle.Dump("22. Delete an Element based on a condition using LINQ to XML \n    - Employees PRE-UPDATE", 0);
  "The file dump has been commented out on line #513, uncomment to see it ...".Dump("22. Delete an Element based on a condition using LINQ to XML \n    - Employees PRE-UPDATE", 0);
  
  var addr = xEle.Elements("Employee").ToList();
  
  foreach(XElement addEle in addr)
  {
    addEle.SetElementValue("Address", null);
  }
  
  //Console.Write(xEle);
  //xEle.Dump("22. Delete an Element based on a condition using LINQ to XML \n    - Employees POST-UPDATE", 0);
  "The file dump has been commented out on line #524, uncomment to see it ...".Dump("22. Delete an Element based on a condition using LINQ to XML \n    - Employees POST-UPDATE", 0);

  #endregion

  #region 23. Remove ‘n’ number of Elements using LINQ to XML ----------------------------------------

  //DESCRIPTION: If you have a requirement where you have to remove ‘n’ number of Elements; For E.g. 
  //             To remove the last 2 Elements

  //NOTE: defined above, in step 1.
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");

  //xEle.Dump("23. Remove ‘n’ number of Elements using LINQ to XML \n    - Employees PRE-UPDATE", 0);
  "The file dump has been commented out on line #537, uncomment to see it ...".Dump("23. Remove ‘n’ number of Elements using LINQ to XML \n    - Employees PRE-UPDATE", 0);

  var emps = xEle.Descendants("Employee");
  emps.Reverse().Take(2).Remove();

  //Console.Write(xEle);
  //xEle.Dump("23. Remove ‘n’ number of Elements using LINQ to XML \n    - Employees POST-UPDATE", 0);
  "The file dump has been commented out on line #544, uncomment to see it ...".Dump("23. Remove ‘n’ number of Elements using LINQ to XML \n    - Employees POST-UPDATE", 0);
  
  #endregion

  #region 24. Save/Persists Changes to the XML using LINQ to XML -------------------------------------
  
  //DESCRIPTION: All the manipulations we have done so far were in the memory and were not persisted 
  //             in the XML file. If you have been wondering how to persist changes to the XML, once 
  //             it is modified, then here’s how to do so. It’s quite simple. You just need to call the 
  //             Save() method. It’s also worth observing that the structure of the code shown below is 
  //             similar to the structure of the end result (XML document). That’s one of the benefits 
  //             of LINQ to XML, that it makes life easier for developers by making it so easy to 
  //             create and structure XML documents.
  
  //NOTE: defined above, in step 1.
  //XElement xEle = XElement.Load("..\\..\\Employees.xml");
  //
  //xEle.Add( new XElement( "Employee"
  //                       ,new XElement("EmpId", 5)
  //                       ,new XElement("Name", "George")
  //                       ,new XElement("Sex", "Male")
  //                       ,new XElement("Phone", "423-555-4224", new XAttribute("Type", "Home"))
  //                       ,new XElement("Phone", "424-555-0545", new XAttribute("Type", "Work"))
  //                       ,new XElement( "Address"
  //                                     ,new XElement("Street", "Fred Park, East Bay")
  //                                     ,new XElement("City", "Acampo")
  //                                     ,new XElement("State", "CA")
  //                                     ,new XElement("Zip", "95220")
  //                                     ,new XElement("Country", "USA"))));
  //
  //xEle.Save("..\\..\\Employees.xml");
  
  #endregion  
  #endregion
}
