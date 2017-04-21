using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pso2LogViewer
{
    /// <summary>
    /// ユーザのデータのディレクトリ
    /// </summary>
    class UserDataDirectory
    {
        // PSO2のユーザデータのある場所
        const string UserDataDir = "SEGA\\PHANTASYSTARONLINE2\\";
        // ログディレクトリ
        private string LogDir;
        public DirectoryInfo LogDirInfo;

        // シンボルアードキャッシュディレクトリ
        private string SACacheDir;
        public DirectoryInfo SACacheDirInfo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        UserDataDirectory()
        {
            // ユーザのドキュメントディレクトリ
            string DocumentDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // PSO2のユーザデータのディレクトリ
            string UserDataDirectory = DocumentDir + "\\" + UserDataDir + "\\";

            if (!Directory.Exists(UserDataDirectory))
            {
                // ディレクトリが存在しない場合例外を出力
                // TODO: i18n化
                throw new DirectoryNotFoundException("PSO2 User Data directory is not found. If you never launch PSO2 client, please run client first.");
            }
            // ログのディレクトリ
            LogDir = UserDataDirectory + "log\\";
            LogDirInfo = new DirectoryInfo(LogDir);
            // シンボルアートのキャッシュディレクトリ
            SACacheDir = UserDataDirectory + "symbolarts\\cache";
            SACacheDirInfo = new DirectoryInfo(SACacheDir);
        }
    }
}
