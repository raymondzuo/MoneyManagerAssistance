using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;

namespace Raysoft.Storage
{
    public class StorageHelper
    {
        #region 字段

        private static readonly object SyncObject = new object();
        private static string _container = "Container";
        private static ApplicationDataContainer localSettings;

        /// <summary>
        /// 本地文件夹根目录
        /// </summary>
        private static readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;

        /// <summary>
        /// sd卡的存储空间
        /// </summary>
        private static readonly StorageFolder sdCardFolder = KnownFolders.RemovableDevices;

        #endregion

        #region 文件夹相关

        /// <summary>
        /// 根据给定的路径，判断一个文件夹是否存在。
        /// </summary>
        /// <param name="folderName">文件夹的路径</param>
        /// <returns>是否存在指定的文件夹</returns>
        public static async Task<bool> IsFolderExist(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return false;
            }

            var targetFolder = await LocalFolder.GetFolderAsync(folderName);
            if (targetFolder != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 根据给定的路径，返回得到的文件夹，如果不存在返回空
        /// </summary>
        /// <param name="folderName">文件夹的路径</param>
        /// <returns>获取到的文件夹对象</returns>
        public static async Task<StorageFolder> GetFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return null;
            }

            var targetFolder = await LocalFolder.GetFolderAsync(folderName);
            if (targetFolder != null)
            {
                return targetFolder;
            }

            return targetFolder;
        }

        /// <summary>
        /// 获取某个文件夹下所有的文件
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static async Task<IReadOnlyList<StorageFile>> GetFilesOfOneFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return null;
            }

            var targetFolder = await LocalFolder.GetFolderAsync(folderName);
            if (targetFolder != null)
            {
                var allFiles = await targetFolder.GetFilesAsync();

                return allFiles;
            }

            return null;
        }

        /// <summary>
        /// 根据给定的路径，创建一个文件夹，并作为对象返回。
        /// </summary>
        /// <param name="folderName">文件夹的路径</param>
        /// <returns>新创建的文件夹对象</returns>
        public static async Task<StorageFolder> CreateAndGetTheFolder(string folderName, CreationCollisionOption option = CreationCollisionOption.OpenIfExists)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return null;
            }

            var targetFolder = await LocalFolder.CreateFolderAsync(folderName, option);
            if (targetFolder != null)
            {
                return targetFolder;
            }

            return null;
        }

        /// <summary>
        /// 删除一个指定的文件夹
        /// </summary>
        /// <param name="folderName">指定的文件夹路径</param>
        /// <returns>是否删除成功</returns>
        public static async Task<bool> DeleteFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return false;
            }

            var targetFolder = await LocalFolder.GetFolderAsync(folderName);
            if (targetFolder != null)
            {
                try
                {
                    await targetFolder.DeleteAsync();
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;
        }

        #endregion

        #region 文件相关

        /// <summary>
        /// 判断一个文件是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<bool> IsFileExist(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            var targetFile = await LocalFolder.GetFileAsync(fileName);
            if (targetFile != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 读取某个文件，并以字符串的形式返回。
        /// </summary>
        /// <param name="fileName">文件的路径</param>
        /// <returns>文件的内容</returns>
        public static async Task<string> ReadFileAsText(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            var file = await LocalFolder.GetFileAsync(fileName);
            if (file != null)
            {
                return await FileIO.ReadTextAsync(file);
            }

            return string.Empty;
        }

        /// <summary>
        /// 读取一个文件，并以随机访问流的形式返回
        /// </summary>
        /// <param name="fileName">文件的路径</param>
        /// <returns>文件的内容</returns>
        public static async Task<Stream> ReadFileAsStream(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var file = await LocalFolder.GetFileAsync(fileName);
            if (file != null)
            {
                var stream = await file.OpenReadAsync();
                if (stream != null)
                {
                    return stream.AsStream();
                }
            }

            return null;
        }

        /// <summary>
        /// 读取一个文件，并以byte数组的形式返回。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadFileAsByteArray(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var file = await LocalFolder.GetFileAsync(fileName);
            if (file != null)
            {
                var randomAccessStream = await file.OpenReadAsync();
                byte[] bufferBytes = new byte[16 * 1024];
                using (var memoryStream = new MemoryStream())
                {
                    int read;

                    while ((read = randomAccessStream.AsStreamForRead().Read(bufferBytes, 0, bufferBytes.Length)) > 0)
                    {
                        memoryStream.Write(bufferBytes, 0, read);
                    }

                    return memoryStream.ToArray();
                }
            }

            return null;
        }

        /// <summary>
        /// 将字符串保存到文件当中。
        /// </summary>
        /// <param name="fileContent">文件内容</param>
        /// <param name="fileName">文件名</param>
        /// <param name="isOverride">是否覆盖原来的内容</param>
        /// <returns>是否保存成功</returns>
        public static async Task<bool> WriteStringToFile(string fileContent, string fileName, bool isOverride)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!await IsFileExist(fileName) || isOverride)
            {
                await LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            var file = await LocalFolder.GetFileAsync(fileName);
            if (file != null)
            {
                try
                {
                    await FileIO.WriteTextAsync(file, fileContent);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// 将一个流保存至文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileStream">流</param>
        /// <param name="isOverride">是否覆盖原有的文件</param>
        /// <returns></returns>
        public static async Task<bool> WriteStreamToFile(string fileName, Stream fileStream, bool isOverride)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!await IsFileExist(fileName) || isOverride)
            {
                await LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            var file = await LocalFolder.GetFileAsync(fileName);
            if (file != null)
            {
                try
                {
                    var allFileBytes = new byte[fileStream.Length];
                    var buffer = new byte[16 * 1024];

                    using (var ms = new MemoryStream())
                    {
                        int read;
                        while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        allFileBytes = ms.ToArray();
                    }

                    await FileIO.WriteBytesAsync(file, allFileBytes);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// 将byte数组写入文件。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileDataBytes"></param>
        /// <param name="isOverride"></param>
        /// <returns></returns>
        public static async Task<bool> WriteBytesToFile(string fileName, byte[] fileDataBytes, bool isOverride = false)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!await IsFileExist(fileName) || isOverride)
            {
                await LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            var file = await LocalFolder.GetFileAsync(fileName);
            if (file != null)
            {
                try
                {
                    await FileIO.WriteBytesAsync(file, fileDataBytes);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// 保存位图至本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="imageSource"></param>
        /// <returns>是否保存成功</returns>
        public async static Task<bool> SaveImageCache(string fileName, BitmapImage imageSource)
        {
            try
            {
                bool flag = await IsFileExist(fileName);
                if (flag)//文件存在则不保存
                    return true;

                WriteableBitmap wb = new WriteableBitmap(imageSource.PixelWidth, imageSource.PixelHeight);
                var file = await LocalFolder.CreateFileAsync(fileName);
                await FileIO.WriteBufferAsync(file, wb.PixelBuffer);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取sln中的文件。参数不要传 "ms-appx:///"
        ///  1. "ms-appx:///GBK/gbk.bin"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<StorageFile> GetAppResourceFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            try
            {
                var uri = new Uri(string.Format("ms-appx:///{0}", path), UriKind.Absolute);
                var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                return file;
            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// 读取sln中的文件，并以流的形式返回。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<Stream> ReadAppResourceFileAsStream(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            try
            {
                var uri = new Uri(string.Format("ms-appx:///{0}", path), UriKind.Absolute);
                var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                var stream = await file.OpenReadAsync();
                if (stream != null)
                {
                    return stream.AsStream();
                }
            }
            catch
            {
            }

            return null;
        }

        /// <summary>
        /// 删除一个文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>是否删除成功</returns>
        public static async Task<bool> DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!await IsFileExist(fileName))
            {
                return false;
            }

            var toDeleteFile = await LocalFolder.GetFileAsync(fileName);

            try
            {
                await toDeleteFile.DeleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region SD卡相关
        /// <summary>
        /// 判断在sd卡中是否存在一个目录
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static async Task<bool> IsFolderExistInSdCard(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return false;
            }

            var targetFolder = await sdCardFolder.GetFolderAsync(folderName);
            if (targetFolder != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断在sd卡中是否存在一个文件。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<bool> IsFileExistInSdCard(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            var targetFile = await sdCardFolder.GetFileAsync(fileName);
            if (targetFile != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 从sd中获取一个文件对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<StorageFile> GetFileFromSdCard(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var targetFile = await sdCardFolder.GetFileAsync(fileName);

            return targetFile;
        }

        /// <summary>
        /// 从sd中读取文件，并以流的形式返回
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<Stream> GetFileFromSdCardAsStream(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var file = await sdCardFolder.GetFileAsync(fileName);
            if (file != null)
            {
                var stream = await file.OpenReadAsync();
                if (stream != null)
                {
                    return stream.AsStream();
                }
            }

            return null;
        }

        /// <summary>
        /// 将一个流写入sd中的文件。
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <param name="isOverride"></param>
        /// <returns></returns>
        public static async Task<bool> WriteStreamToSdCard(string fileName, Stream fileStream, bool isOverride)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!await IsFileExistInSdCard(fileName) || isOverride)
            {
                await sdCardFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }

            var file = await sdCardFolder.GetFileAsync(fileName);

            try
            {
                byte[] allFileBytes = new byte[fileStream.Length];
                byte[] buffer = new byte[16 * 1024];

                using (var memoryStream = new MemoryStream())
                {
                    int read;

                    while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, read);
                    }

                    allFileBytes = memoryStream.ToArray();
                }

                await FileIO.WriteBytesAsync(file, allFileBytes);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 是否是上次正确安装的那个sd卡。
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> IsSdCardRecognized()
        {
            bool isCardRecognized;
            StorageFolder sdCard = (await sdCardFolder.GetFoldersAsync()).FirstOrDefault();

            if (sdCard != null)
            {
                var allProperties = sdCard.Properties;
                IEnumerable<string> propertiesToRetrieve = new List<string> { "WindowsPhone.ExternalStorageId" };

                var storageIdProperties = await allProperties.RetrievePropertiesAsync(propertiesToRetrieve);

                var cardId = (string)storageIdProperties["WindowsPhone.ExternalStorageId"];

                return true;
            }

            return false;
        }

        #endregion

        #region LocalSetting 相关
        public static void SetIsolatedKeyValue(string key, object value)
        {
            lock (SyncObject)
            {
                if (localSettings == null)
                    localSettings = ApplicationData.Current.LocalSettings.CreateContainer(_container, ApplicationDataCreateDisposition.Always);

                if (!localSettings.Containers.ContainsKey(_container))
                {
                    var container = localSettings.CreateContainer(_container,
                       ApplicationDataCreateDisposition.Always);
                }
                var values = localSettings.Containers[_container].Values;
                if (values.ContainsKey(key))
                    values[key] = value;
                else
                {
                    try
                    {
                        values.Add(key, value);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public static object GetIsolatedValue(string key)
        {
            lock (SyncObject)
            {
                if (localSettings == null)
                    localSettings = Windows.Storage.ApplicationData.Current.LocalSettings.CreateContainer(_container,
                        ApplicationDataCreateDisposition.Always);
                var values = localSettings.Containers[_container].Values;
                if (values.ContainsKey(key))
                    return values[key];
                return null;
            }
        }

        #endregion
    }
}
