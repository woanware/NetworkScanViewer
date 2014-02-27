using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlResultExport
    {
        #region Properties
        public List<Result> Results { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public XmlResultExport()
        {
            Results = new List<Result>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Serializes the class to the config file if any of the settings have changed.
        /// </summary>
        /// <returns></returns>
        public string Save(string fileName)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlResultExport));
                using (StreamWriter streamWriter = new StreamWriter(fileName, false))
                {
                    xmlSerializer.Serialize(streamWriter, this);
                    return String.Empty;
                }
            }
            catch (FileNotFoundException fex)
            {
                return fex.Message;
            }
            catch (UnauthorizedAccessException auex)
            {
                return auex.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Deserializes the class from the config file.
        /// </summary>
        /// <returns></returns>
        public string Load(string fileName)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlResultExport));

                if (File.Exists(fileName) == false)
                {
                    return "Cannot locate the XML file: " + fileName;
                }

                FileInfo fileInfo = new FileInfo(fileName);
                using (FileStream fileStream = fileInfo.OpenRead())
                {
                    XmlResultExport temp = (XmlResultExport)xmlSerializer.Deserialize(fileStream);
                    Results = temp.Results;
                    return String.Empty;
                }
            }
            catch (FileNotFoundException fex)
            {
                return fex.Message;
            }
            catch (UnauthorizedAccessException auex)
            {
                return auex.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
