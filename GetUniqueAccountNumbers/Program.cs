using System;
using System.IO;
using System.Collections;

using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetUniqueAccountNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            //My directory for the Directories 
            String[] errorDirectory = Directory.GetDirectories(@"C:\glExportData\errors");
            String outputData = "";

            var output = "";
            int index = 0;
            var accountNumbersList = new ArrayList();

            //Loop through each file directory
            foreach (String errorFileDir in errorDirectory)
            {
                String[] errorFile = Directory.GetFiles(errorFileDir);
                //Loop through each file
                foreach (String fileDir in errorFile)
                {
                    String fileData = System.IO.File.ReadAllText(fileDir);
                    //If its an account number issue (some are pipeline related)
                    if (fileData.IndexOf("The Account Number (ACTNUMST) does not exist in the Account Index Master Table") > 0)
                    {
                        //Parse account number and company from file
                        String accountNumber = fileData.Substring(fileData.IndexOf("<ACTNUMST>") + 10, fileData.IndexOf("</ACTNUMST>") - fileData.IndexOf("<ACTNUMST>") - 10);
                        String company = fileData.Substring(fileData.IndexOf("databasename=") + 13, fileData.IndexOf(";host") - fileData.IndexOf("databasename=") - 13);

                        //Check if account number is unique
                        if (!accountNumbersList.Contains(accountNumber)){
                            output += company + "," + accountNumber + "\n";
                            accountNumbersList.Add(accountNumber);
                        }
                        
                    }
                }
            }
            //Write output
            System.IO.File.WriteAllText(@"C:\glExportData\output\uniqueAccountNumbers.csv", output);
        }
    }
}
