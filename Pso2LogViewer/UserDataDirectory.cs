/*!
 * Phantasy Star Online 2 Log Viewer
 * 
 * The MIT License(MIT)
 * 
 * Copyright(c) 2017 Logue <logue@hotmail.co.jp>.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pso2LogViewer
{
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
