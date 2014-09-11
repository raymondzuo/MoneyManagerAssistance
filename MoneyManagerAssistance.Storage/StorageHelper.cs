using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MoneyManagerAssistance.Storage
{
    public class StorageHelper
    {
        private static StorageFolder localFolder = ApplicationData.Current.LocalFolder;


        #region 文件夹相关
        /// <summary>
        /// 在本地新建一个文件夹
        /// </summary>
        /// <param name="folderName"></param>
        public static async Task<StorageFolder> CreateLocalFolder(string folderName)
        {
            var targetFolder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            return targetFolder;
        }

        #endregion

        #region 文件相关
        /// <summary>
        /// 在本地新建一个文件
        /// </summary>
        /// <param name="fileName"></param>
        public static async Task<StorageFile> CreateLocalFile(string fileName)
        {
            var targetFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            return targetFile;
        }

        #endregion
    }
}
