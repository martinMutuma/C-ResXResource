using System;
using System.Collections.Generic;
using System.Collections;
using System.Resources;

/*
 * 
 * Very un optimized code to read resource (resx) files to compare and move resource entries from one file to another 
 * 
 * 
 */

namespace xmlManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            string resxSourceFileEng = @"{to appp}\App_GlobalResources\LocalizedText.resx";
            string resxSourceFileGerm = @"{to appp}\App_GlobalResources\LocalizedText.de-DE.resx";
           string resxDestFileEng = @"{to appp}\App_GlobalResources\LocalizedText.resx";
            string resxDestFileGerm = @"{to appp}\App_GlobalResources\LocalizedText.de-DE.resx";
            List<ResXDataNode> existing = new List<ResXDataNode>();
            List<DictionaryEntry> sourceRESXEng = new List<DictionaryEntry>();
            List<DictionaryEntry> sourceRESXGerm = new List<DictionaryEntry>();
            List<DictionaryEntry> destRESXEng = new List<DictionaryEntry>();
            List<DictionaryEntry> destRESXGerm = new List<DictionaryEntry>();
            ResXResourceReader sourceReaderEng = new ResXResourceReader(resxSourceFileEng);
            ResXResourceReader sourceReaderGerm = new ResXResourceReader(resxSourceFileGerm);
            ResXResourceReader destReaderEng = new ResXResourceReader(resxDestFileEng);
            ResXResourceReader destReaderGerm = new ResXResourceReader(resxDestFileGerm);
           ResXResourceWriter destWriterEng = new ResXResourceWriter(resxDestFileEng);
            ResXResourceWriter destWriterGerm = new ResXResourceWriter(resxDestFileGerm);
           
            foreach (DictionaryEntry entry in sourceReaderEng)
            {
                sourceRESXEng.Add(entry);
            } 
            foreach (DictionaryEntry entry in sourceReaderGerm)
            {
                sourceRESXGerm.Add(entry);
            }  
            foreach (DictionaryEntry entry in destReaderEng)
            {
                destRESXEng.Add(entry);
            }  
            foreach (DictionaryEntry entry in destReaderGerm)
            {
                destRESXGerm.Add(entry);
            }
            ResXResourceSet resSetDestEng = new ResXResourceSet(resxDestFileEng);
            List<DictionaryEntry> engDiff = new List<DictionaryEntry>();
            foreach (DictionaryEntry data in sourceRESXEng)
            {
                var dataExists = resSetDestEng.GetString(data.Key.ToString());
                if ( String.IsNullOrEmpty( dataExists))
                {
                    engDiff.Add(data);

                }

            }
            List<string> sameEngKeys = new List<string>();
            foreach (var dat in engDiff)
            {
                var datKey = dat.Key.ToString();
                var found = false;
                foreach (var des in destRESXEng)
                {
                    var desKey = des.Key.ToString();
                    if (datKey.ToLower().ToString() == desKey.ToLower().ToString())
                    {
                        found = true;
                        sameEngKeys.Add(datKey);
                        Console.WriteLine(datKey + " :: " + desKey);

                        break;

                    }
                }
                if (found == false)
                {
                    destRESXEng.Add(dat);
                }
            }



            ResXResourceSet resSetDestGerm = new ResXResourceSet(resxDestFileEng);
            List<DictionaryEntry> germDiff = new List<DictionaryEntry>();
            foreach (DictionaryEntry data in sourceRESXGerm)
            {
                var dataExists = resSetDestGerm.GetString(data.Key.ToString());
                if (String.IsNullOrEmpty(dataExists))
                {
                    germDiff.Add(data);
                }

            }
            List<string> sameGermKeys = new List<string>();
            foreach (var dat in germDiff)
            {
                var datKey = dat.Key.ToString();
                var found = false;
                foreach (var des in destRESXGerm)
                {
                    var desKey = des.Key.ToString();
                    if (datKey.ToLower().ToString() == desKey.ToLower().ToString())
                    {
                        found = true;
                        sameGermKeys.Add(datKey);
                        //Console.WriteLine(datKey+" :: "+ desKey);
                        break;

                    }
                }
                if (found == false)
                {

                    destRESXGerm.Add(dat);
                }
            }
            Console.WriteLine(sameGermKeys);
            Console.WriteLine("========================Eng file===========================");
            foreach (var df in destRESXEng)
            {
                Console.WriteLine(df.Key.ToString() + ":\t \t \t" + df.Value.ToString());
                ResXDataNode node = new ResXDataNode(df.Key.ToString(), df.Value.ToString());
                destWriterEng.AddResource(node);
            }

            destWriterEng.Generate();
            destWriterEng.Close();



            Console.WriteLine("========================German file===========================");
            foreach (var df in destRESXGerm)
            {
                Console.WriteLine(df.Key.ToString() + ":\t \t \t" + df.Value.ToString());
                ResXDataNode node = new ResXDataNode(df.Key.ToString(), df.Value.ToString());
                destWriterGerm.AddResource(node);
            }

            destWriterGerm.Generate();
            destWriterGerm.Close();

            Console.WriteLine("========================Done!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!===========================");
            Console.ReadKey();

        }
    }
}
