﻿/*!
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Pso2LogViewer
{
    /// <summary>
    /// ログファイルの内容を構造体にパースするクラス
    /// </summary>
    class LogParser {
        // ファイル名
        private string FileName;
        // ISO 8601 マッチパターン
        private const string ISO8601Pattern = "yyyy-MM-ddTHH:mm:ss";
        // ログデータの一覧
        private List<LogEntry> Entries = new List<LogEntry>();

        /// <summary>
        /// ログのエントリ構造体
        /// </summary>
        public struct LogEntry
        {
            // 入力日時
            public DateTime DateTime;
            // イベント番号
            public int Number;
            // イベントの種類
            public string Type;
            // アカウントID
            public int AccountId;
            // キャラクター名
            public string CharacterName;
            // メッセージ
            public string Message;
        }
 
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Type">読み込むログのタイプ</param>
        /// <param name="Date">読み込むログの日付</param>
        public LogParser(string Type, string Date)
        {
            FileName = Type + "Log" + Date + "_00"; // ChatLog20161206_00.txt　という形式。末尾の_00は固定っぽい
        }

        private void Process()
        {
            // 1行ずつ読み込む
            using (StreamReader sr = new StreamReader(FileName))
            {
                // ログのデーターの形式はタブ区切りで以下のような感じ
                // 　ISO8061形式の日時	番号	種類	アカウントID	キャラ名	メッセージ
                // ただし、メッセージに改行が含まれていた場合、改行を含んで、"でくくってある。

                string Line = sr.ReadLine();
                // タブ区切りなので分割
                string[] Fields = Line.Split('\t');

                string Message = "";
                // ログ内容構造体
                LogEntry Entry = new LogEntry();
                // 入力日時
                Entry.DateTime = DateTime.ParseExact(Fields[0], ISO8601Pattern, null);
                // イベント番号
                Entry.Number = Int32.Parse(Fields[1]);
                // イベントの種類（[と]は削除する）
                Entry.Type = Regex.Replace(Fields[2], "/^[|]$/", "");
                // アカウントID
                Entry.AccountId = Int32.Parse(Fields[3]);
                // キャラクター名
                Entry.CharacterName = Fields[4];
                // メッセージ
                Message = Fields[5];
                if (Message.IndexOf("\"") == 0)
                {
                    // ※第5変数が"から始まってた場合、"で終わる行まで追記する（なぜ改行コードがLF？）
                    while (Line.LastIndexOf("\"") == 0){
                        Message += sr.ReadLine() + "\n";
                    }
                }
                // 先頭と末尾の"を削除してエントリに書き込む
                Entry.Message = Regex.Replace(Fields[5], "/^\"|\"$/", "");
                // 配列に書き込む
                Entries.Add(Entry);
            }
        }
    }
}
