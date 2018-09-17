using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WCF
{
    public class readFile
    {
        public void recursivelySearchFile(List<string> args, ArrayList pathArrayList)
        {
            foreach (string path in args)
            {
                if (File.Exists(path))
                {
                    ProcessFile(path, pathArrayList);
                }
                else if (Directory.Exists(path))
                {
                    ProcessDirectory(path, pathArrayList);
                }
                else
                {
                    pathArrayList.Add("No found file!");
                }
            }
        }

        public void ProcessDirectory(string targetDirectory, ArrayList pathArrayList)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, pathArrayList);
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, pathArrayList);
        }

        public void ProcessFile(string path, ArrayList pathArrayList)
        {
            pathArrayList.Add(path);
        }
    }
}